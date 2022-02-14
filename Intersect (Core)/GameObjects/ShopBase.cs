using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

using Intersect.Models;

using Newtonsoft.Json;

namespace Intersect.GameObjects
{

    public class ShopBase : DatabaseObject<ShopBase>, IFolderable
    {

        [NotMapped] public List<ShopItem> BuyingItems = new List<ShopItem>();

        [NotMapped] public List<ShopItem> SellingItems = new List<ShopItem>();

        [JsonConstructor]
        public ShopBase(Guid id) : base(id)
        {
            Name = "New Shop";
        }

        //EF is so damn picky about its parameters
        public ShopBase()
        {
            Name = "New Shop";
        }

        public bool BuyingWhitelist { get; set; } = true;
        
        public bool TagWhitelist { get; set; } = true;

        //Spawn Info
        [Column("DefaultCurrency")]
        [JsonProperty]
        public Guid DefaultCurrencyId { get; set; }

        [NotMapped]
        [JsonIgnore]
        public ItemBase DefaultCurrency
        {
            get => ItemBase.Get(DefaultCurrencyId);
            set => DefaultCurrencyId = value?.Id ?? Guid.Empty;
        }

        [Column("BuyingItems")]
        [JsonIgnore]
        public string JsonBuyingItems
        {
            get => JsonConvert.SerializeObject(BuyingItems);
            set => BuyingItems = JsonConvert.DeserializeObject<List<ShopItem>>(value);
        }

        [Column("SellingItems")]
        [JsonIgnore]
        public string JsonSellingItems
        {
            get => JsonConvert.SerializeObject(SellingItems);
            set => SellingItems = JsonConvert.DeserializeObject<List<ShopItem>>(value);
        }

        public string BuySound { get; set; } = null;

        public string SellSound { get; set; } = null;

        /// <inheritdoc />
        public string Folder { get; set; } = "";

        public float BuyMultiplier { get; set; } = 1.0f;

        [NotMapped] public List<string> BuyingTags = new List<string>();

        [Column("BuyingTags")]
        [JsonIgnore]
        public string JsonBuyingTags
        {
            get => JsonConvert.SerializeObject(BuyingTags);
            set => BuyingTags = JsonConvert.DeserializeObject<List<string>>(value);
        }

        public bool BuysItem(ItemBase item)
        {
            if (item == null) return false;

            // Check to see if this item contains a tag in the tag white/blacklist
            bool validTag = false;
            if (TagWhitelist)
            {
                validTag = item.Tags.FindAll(tag => BuyingTags?.Contains(tag) == true).Count > 0;
            }
            else
            {
                validTag = item.Tags.FindAll(tag => BuyingTags?.Contains(tag) == true).Count == 0;
            }

            bool validBuyItem = false;
            if (BuyingWhitelist)
            {
                validBuyItem = BuyingItems.FindAll(buyItem =>
                {
                    return (item.Id == buyItem.ItemId) == BuyingWhitelist;
                }).Count > 0;
            }
            else
            {
                validBuyItem = BuyingItems.FindAll(buyItem => buyItem.ItemId == item.Id).Count == 0;
            }

            // If item is in whitelist, do not care about tag list
            return (validBuyItem && BuyingWhitelist) || validTag && validBuyItem;
        }
    }

    public class ShopItem
    {

        public Guid CostItemId;

        public int CostItemQuantity;

        public Guid ItemId;

        [JsonConstructor]
        public ShopItem(Guid itemId, Guid costItemId, int costVal)
        {
            ItemId = itemId;
            CostItemId = costItemId;
            CostItemQuantity = costVal;
        }

        [NotMapped]
        [JsonIgnore]
        public ItemBase Item => ItemBase.Get(ItemId);

    }

}
