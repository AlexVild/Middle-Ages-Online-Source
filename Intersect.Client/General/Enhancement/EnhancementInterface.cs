using Intersect.Client.Items;
using Intersect.GameObjects;
using Intersect.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.General.Enhancement
{
    public sealed class EnhancementInterface
    {
        public Guid CurrencyId { get; set; }
        public ItemBase Currency => ItemBase.Get(CurrencyId);
        public bool RefreshUi { get; set; }
        public bool IsOpen { get; set; }

        public ItemBase ItemDescriptor { get; set; }
        public float CostMultiplier { get; set; }

        public ObservableCollection<EnhancementItem> EnhancementsApplied { get; set; } = new ObservableCollection<EnhancementItem>();

        public EnhancementItem[] NewEnhancements => EnhancementsApplied.Where(en => en.Removable).ToArray();

        public int EPFree => ItemDescriptor == default ? 0 : ItemDescriptor.EnhancementThreshold - EPSpent;

        Item EnhancingItem { get; set; }

        public int EPSpent => ItemDescriptor == default ? 0 : EnhancementsApplied
            .Select(e => EnhancementDescriptor.Get(e.EnhancementId))
            .Where(e => e != default)
            .Aggregate(0, (prev, next) => prev + next.RequiredEnhancementPoints);

        public EnhancementInterface()
        {
            EnhancementsApplied.CollectionChanged += EnhancementsApplied_CollectionChanged;
        }

        private void EnhancementsApplied_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            RefreshUi = true;
        }

        public void Open(Guid currencyId, float costMulti, Item item)
        {
            EnhancementsApplied.Clear();
            CurrencyId = currencyId;
            CostMultiplier = costMulti;

            IsOpen = true;
            Interface.Interface.GameUi?.EnhancementWindow?.Show();
            RefreshUi = true;
            EnhancingItem = item;
            ItemDescriptor = item?.Base;

            InitializeEnhancementList();
        }

        void InitializeEnhancementList()
        {
            if (EnhancingItem == default || EnhancingItem.ItemProperties == default)
            {
                return;
            }

            foreach (var enhancementId in EnhancingItem.ItemProperties.AppliedEnhancementIds)
            {
                EnhancementsApplied.Add(new EnhancementItem(enhancementId, false));
            }
        }

        public void Close()
        {
            RefreshUi = false;
            IsOpen = false;
        }

        public bool CanAddEnhancement(EnhancementDescriptor enhancement, out string failureReason)
        {
            failureReason = string.Empty;

            if (ItemDescriptor == null || enhancement == null)
            {
                return false;
            }

            // Do we have enough EP free currently?
            if (EPFree < enhancement.RequiredEnhancementPoints)
            {
                failureReason = "There's not enough remaining EP to apply this enhancement.";
                return false;
            }

            // Does the weapon meet minimum level requirements for any of its weapon types?
            if (!EnhancementHelper.WeaponLevelRequirementMet(ItemDescriptor.MaxWeaponLevels, enhancement.ValidWeaponTypes, out failureReason))
            {
                return false;
            }

            return true;
        }

        public bool TryAddEnhancement(EnhancementDescriptor enhancement)
        {
            if (enhancement == default || !CanAddEnhancement(enhancement, out _))
            {
                return false;
            }

            EnhancementsApplied.Add(new EnhancementItem(enhancement.Id, true));
            return true;
        }

        public bool TryRemoveEnhancementAt(int idx)
        {
            if (!CanRemoveEnhancementAt(idx, out _))
            {
                return false;
            }

            EnhancementsApplied.RemoveAt(idx);
            return true;
        }

        public bool CanRemoveEnhancementAt(int idx, out string failureReason)
        {
            failureReason = string.Empty;

            var enhancement = EnhancementsApplied.ElementAtOrDefault(idx);
            if (enhancement == default)
            {
                return false;
            }

            if (!enhancement.Removable)
            {
                failureReason = "You can not remove previously applied enhancements unless you reset this weapon.";
                return false;
            }

            return true;
        }
    }

    public class EnhancementItem
    {
        public Guid EnhancementId { get; set; }
        public bool Removable { get; set; }

        public EnhancementItem(Guid enhancementId, bool removable)
        {
            EnhancementId = enhancementId;
            Removable = removable;
        }
    }
}
