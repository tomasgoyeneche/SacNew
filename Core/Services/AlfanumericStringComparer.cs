using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.Services
{
    public class AlfanumericStringComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            if (x == y)
                return 0;
            if (x == null)
                return -1;
            if (y == null)
                return 1;

            var regex = new Regex(@"(\d+|\D+)");
            var xParts = regex.Matches(x);
            var yParts = regex.Matches(y);

            int max = Math.Max(xParts.Count, yParts.Count);
            for (int i = 0; i < max; i++)
            {
                if (i >= xParts.Count) return -1;
                if (i >= yParts.Count) return 1;

                var xPart = xParts[i].Value;
                var yPart = yParts[i].Value;

                int xNum, yNum;
                bool xIsNumber = int.TryParse(xPart, out xNum);
                bool yIsNumber = int.TryParse(yPart, out yNum);

                if (xIsNumber && yIsNumber)
                {
                    int result = xNum.CompareTo(yNum);
                    if (result != 0)
                        return result;
                }
                else
                {
                    int result = string.Compare(xPart, yPart, System.StringComparison.OrdinalIgnoreCase);
                    if (result != 0)
                        return result;
                }
            }
            return 0;
        }
    }
}
