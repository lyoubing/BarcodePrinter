using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarcodePrinter
{
    class CRCBuilder
    {
        static Dictionary<long, char> map = new Dictionary<long, char>
        {
            {0, 'Z' },
            {1, 'Y' },
            {2, 'X'},
            {3, 'W'},
            {4, 'V'},
            {5, 'U'},
            {6, 'T'},
            {7, 'S'},
            {8, 'R'},
            {9, 'Q'},
            {10, 'P'},
            {11, 'N'},
            {12, 'M'},
            {13, 'L'},
            {14, 'K'},
            {15, 'J'},
            {16, 'H'},
            {17, 'G'},
            {18, 'F'},
            {19, 'E'},
            {20, 'D'},
            {21, 'C'},
            {22, 'B'},
            {23, 'A'}
        };
        public static string Make(long number)
        {
            var mod = number * 371371377 % 13807;
            var x = mod / 576;
            var y = (mod - x * 576) / 24;
            var z = mod % 24;
            StringBuilder sb = new StringBuilder();
            sb.Append(number);
            sb.Append(map[x]);
            sb.Append(map[y]);
            sb.Append(map[z]);
            return sb.ToString();
        }
    }
}
