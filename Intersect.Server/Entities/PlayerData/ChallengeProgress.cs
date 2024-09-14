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
                if (RepsChanged != null && _reps != prevVal) RepsChanged(value, _reps - prevVal, Descriptor.Reps);
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
                if (SetsChanged != null && prevVal != _sets) SetsChanged(value, _sets - prevVal, Descriptor.Sets);
            }
        }

        int NumParam { get; set; }
        Guid IdParam { get; set; }
        Player Player { get; set; }

        public delegate void ChallengeProgressUpdate(int updateVal, int change, int requiredVal);
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

        private void ChallengeProgress_RepsChanged(int reps, int change, int required)
        {
            if (Instance.Complete || reps < Descriptor.Reps)
            {
                return;
            }

            Sets++;
        }

        private void ChallengeProgress_SetsChanged(int sets, int change, int required)
        {
            if (Instance.Complete)
            {
                return;
            }

            Instance.Progress += change;

            RepsChanged -= ChallengeProgress_RepsChanged;
            Reps = 0;
            RepsChanged += ChallengeProgress_RepsChanged;

            // If we're not done yet, inform the player of their new progress
            if (Sets < Descriptor.Sets)
            {
                InformProgress(sets - change, sets, required);
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

        public void InformProgress(int prevSets, int currSets, int required)
        {
            // Some of these challenges are better done as percent informed.
            if (Instance.Challenge == null)
            {
                return;
            }

            var challengeName = ChallengeDescriptor.GetName(ChallengeId);
            switch (Instance.Challenge.Type)
            {
                case ChallengeType.BackstabDamage:
                {
                    // Calculate percentage progress
                    var totalSets = Instance.Challenge.Sets;
                    var percent = (int)((currSets / (float)totalSets) * 100);

                    // Check if we've crossed a 10% threshold (only notify at multiples of 10%)
                    if (Instance.Challenge.Sets >= 10)
                    {
                        if (percent / 10 > prevSets / 10)
                        {
                            PacketSender.SendChatMsg(Player,
                                Strings.Player.ChallengeProgressPercent.ToString(challengeName, percent.ToString("N1")),
                                Enums.ChatMessageType.Experience,
                                sendToast: true);
                        }
                    }
                    else
                    {
                        // Same thing for 25% for challenges with smaller sets
                        if (percent / 25 > prevSets / 25)
                        {
                            PacketSender.SendChatMsg(Player,
                                Strings.Player.ChallengeProgressPercent.ToString(challengeName, percent.ToString("N1")),
                                Enums.ChatMessageType.Experience,
                                sendToast: true);
                        }
                    }
                    break;
                }
                default:
                {
                    PacketSender.SendChatMsg(Player,
                        Strings.Player.ChallengeProgress.ToString(challengeName, currSets, required),
                        Enums.ChatMessageType.Experience,
                        sendToast: true);
                    break;
                }
            }

            
        }
    }
}
