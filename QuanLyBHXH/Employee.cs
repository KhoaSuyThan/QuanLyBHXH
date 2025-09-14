using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBHXH
{
    internal class Employee
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<Period> Periods { get; set; } = new List<Period>();

        public Employee(string iD, string name, string gender, DateTime dateOfBirth)
        {
            ID = iD;
            Name = name;
            Gender = gender;
            DateOfBirth = dateOfBirth;
        }
        public bool AddPeriod(Period p)
        {
            if (!p.IsValid()) return false;
            foreach (var existing in Periods)
            {
                if (existing.Overlaps(p)) return false;
            }
            Periods.Add(p);
            return true;
        }
        public bool HasAnyPeriod() => Periods.Any();

        public int TotalMonths() => Periods.Sum(p => p.MonthsCounted());
        public double TotalContributedSimple() => Periods.Sum(p => p.MonthlySalary * p.MonthsCounted());

        public override string ToString()
        {
            return $"{ID} | {Name} | {Gender} | {DateOfBirth:yyyy-MM-dd} | Giai doan: {Periods.Count}";
        }
        public double TotalContributedWithFactor(Dictionary<int, double> factorByYear = null)
        {
            double total = 0.0;
            factorByYear = factorByYear ?? new Dictionary<int, double>();

            foreach (var p in Periods)
            {
                foreach (var ym in p.IterateMonths())
                {
                    int y = ym.year;
                    double factor = factorByYear.ContainsKey(y) ? factorByYear[y] : 1.0;
                    total += p.MonthlySalary * factor;
                }
            }

            return total;
        }
    }
}
