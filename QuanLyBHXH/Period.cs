using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBHXH
{
    internal class Period
    {
        public int StartMonth { get; set; }
        public int StartYear { get; set; }
        public int EndMonth { get; set; }
        public int EndYear { get; set; }
        public double MonthlySalary { get; set; }

        public Period(int sm, int sy, int em, int ey, double sal)
        {
            StartMonth = sm; 
            StartYear = sy;
            EndMonth = em; 
            EndYear = ey;
            MonthlySalary = sal;
        }
        public int MonthsCounted()
        {
            return (EndYear - StartYear) * 12 + (EndMonth - StartMonth) + 1;
        }
        public bool IsValid()
        {
            if (StartYear > EndYear) return false;
            if (StartYear == EndYear && StartMonth > EndMonth) return false;
            if (StartMonth < 1 || StartMonth > 12) return false;
            if (EndMonth < 1 || EndMonth > 12) return false;
            if (MonthlySalary < 0) return false;
            return MonthsCounted() > 0;
        }
        public bool Overlaps(Period other)
        {
            DateTime thisStart = new DateTime(StartYear, StartMonth, 1);
            DateTime thisEnd = new DateTime(EndYear, EndMonth, 1);
            DateTime otherStart = new DateTime(other.StartYear, other.StartMonth, 1);
            DateTime otherEnd = new DateTime(other.EndYear, other.EndMonth, 1);
            return thisStart <= otherEnd && otherStart <= thisEnd;
        }
        public IEnumerable<(int year, int month)> IterateMonths()
        {
            int y = StartYear;
            int m = StartMonth;
            while (y < EndYear || (y == EndYear && m <= EndMonth))
            {
                yield return (y, m);
                m++;
                if (m > 12) { m = 1; y++; }
            }
        }
        public override string ToString()
        {
            return $"{StartMonth}/{StartYear} - đến {EndMonth}/{EndYear}, Lương: {MonthlySalary}";
        }
    }
}
