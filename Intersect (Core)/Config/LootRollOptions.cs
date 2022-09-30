using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Config
{
    public class LootRollOptions
    {
        public int MaximumLootItems = 30;

        public string DismissSound = "al_book_drop.wav";
        
        public string DismissAllSound = "al_forging.wav";

        public string TakeSound = "al_cloth-heavy.wav";

        public string TakeAllSound = "al_cloth-heavy.wav";

        public string BankSound = "al_book_turn.wav";
        
        public string BankAllSound = "al_sort_bank.wav";
    }
}
