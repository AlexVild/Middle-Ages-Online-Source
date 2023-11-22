using Intersect.GameObjects;
using Intersect.Server.Database.PlayerData.Players;
using Intersect.Server.Localization;
using Intersect.Server.Networking;
using System;

namespace Intersect.Server.Entities.PlayerData
{
    public class ChallengeProgress
    {
        public Guid ChallengeId { get; set; }
        public ChallengeDescriptor Descriptor { get; set; }
        public ChallengeInstance Instance { get; set; }
        public ChallengeType Type => Descriptor.Type;

        private int _reps { get; set; }
        public int Reps
        {
            get => _reps;
            set
            {
                var prevVal = _reps;
                _reps = value;
                if (RepsChanged != null && _reps != prevVal) RepsChanged(value, Descriptor.Reps);
            }
        }

        public int Streak { get; set; }

        private int _sets { get; set; }
        public int Sets
        {
            get => _sets;
            set
            {
                var prevVal = _sets;
                _sets = value;
                if (SetsChanged != null && prevVal != _sets) SetsChanged(value, Descriptor.Sets);
            }
        }

        int NumParam { get; set; }
        Guid IdParam { get; set; }
        Player Player { get; set; }

        public delegate void ChallengeProgressUpdate(int updateVal, int requiredVal);
        public event ChallengeProgressUpdate SetsChanged;
        public event ChallengeProgressUpdate RepsChanged;

        public ChallengeProgress(ChallengeInstance instance, Player player)
        {
            Instance = instance;
            ChallengeId = instance.ChallengeId;
            Descriptor = ChallengeDescriptor.Get(ChallengeId);
            Player = player;

            if (Descriptor == null)
            {
#if DEBUG
                throw new ArgumentNullException(nameof(Descriptor));
#else
                Logging.Log.Error($"Null challenge descriptor for {player.Name} with ID {ChallengeId}");
                return;
#endif
            }

            Reps = 0;
            NumParam = Descriptor.Param;
            IdParam = Descriptor.ChallengeParamId;

            Sets = Instance.Progress;

            if (Sets == Descriptor.Sets)
            {
                Instance.Complete = true;
            }

            RepsChanged += ChallengeProgress_RepsChanged;
            SetsChanged += ChallengeProgress_SetsChanged;
        }

        private void ChallengeProgress_RepsChanged(int reps, int required)
        {
            if (Instance.Complete || reps < Descriptor.Reps)
            {
                return;
            }

            Sets++;
        }

        private void ChallengeProgress_SetsChanged(int sets, int required)
        {
            if (Instance.Complete)
            {
                return;
            }

            Instance.Progress++;

            RepsChanged -= ChallengeProgress_RepsChanged;
            Reps = 0;
            RepsChanged += ChallengeProgress_RepsChanged;

            // If we're not done yet, inform the player of their new progress
            if (Sets < Descriptor.Sets)
            {
                PacketSender.SendChatMsg(Player,
                    Strings.Player.ChallengeProgress.ToString(Descriptor?.Name ?? "NOT FOUND", sets, required),
                    Enums.ChatMessageType.Experience,
                    sendToast: true);
                return;
            }

            // Otherwise, mark this challenge as complete, which will allow the weapon mastery track to progress on the next
            // ProgressMastery() call
            Instance.Progress = Descriptor.Sets;
            Instance.Complete = true;
            if (Descriptor.RequiresContract && Player.ChallengeContractId == Descriptor.Id)
            {
                Player.ChallengeContractId = Guid.Empty;
            }
        }
    }
}
