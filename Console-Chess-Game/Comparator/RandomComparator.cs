using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Chess_Game.Comparator
{
    internal class RandomComparator : Comparer<int>
    {
        

        public override int Compare(int x, int y)
        {
            return RandomNumber().CompareTo(RandomNumber());
        }

        private int RandomNumber()
        {
            Random random = new Random();
            return random.Next(-5, 6);
        }
    }
}
