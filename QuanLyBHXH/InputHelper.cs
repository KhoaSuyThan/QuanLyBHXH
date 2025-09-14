using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBHXH
{
    internal class InputHelper
    {
        public static bool IsValidBhxhId(string id)
        {
            if (string.IsNullOrEmpty(id)) return false;
            if (id.Length != 10) return false;
            foreach (var c in id) if (!char.IsDigit(c)) return false;
            return true;
        }

        public static int ReadInt(string prompt)
        {
            Console.Write(prompt);
            var s = Console.ReadLine();
            if (!int.TryParse(s, out int r)) throw new Exception("Invalid int");
            return r;
        }

        public static double ReadDouble(string prompt)
        {
            {
                Console.Write(prompt);
                var s = Console.ReadLine();
                if (!double.TryParse(s, out double r)) throw new Exception("Invalid double");
                return r;
            }
        }
    }
}
