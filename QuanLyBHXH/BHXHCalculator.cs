using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBHXH
{
    internal class BHXHCalculator
    {
        private static double MonthsToYeartsRounded(int months)
        {
            int years = months / 12;
            int rem = months % 12;
            double extra = 0.0;
            if (rem >= 1 && rem <= 6) extra = 0.5;
            else if (rem >= 7) extra = 1.0;
            return years + extra;
        }
        public static double ComputeMBQTL(Employee e, Dictionary<int, double> factorByYear = null)
        {
            double totalWeighted = 0.0;
            int totalMonths = 0;
            factorByYear = factorByYear ?? new Dictionary<int, double>();
            foreach (var p in e.Periods)
            {
                foreach (var ym in p.IterateMonths())
                {
                    int y = ym.year;
                    double factor = factorByYear.ContainsKey(y) ? factorByYear[y] : 1.0;
                    totalWeighted += p.MonthlySalary * factor;
                    totalMonths++;
                }
            }
            if (totalMonths == 0) return 0.0;
            return totalWeighted / totalMonths;
        }

        // Tính BHXH 1 lần theo quy tắc đã mô tả (đơn giản hóa: không có bảng hệ số trượt khi tính thời điểm)
        public static double ComputeOneTime(Employee e, Dictionary<int, double> factorByYear = null)
        {
            if (!e.HasAnyPeriod()) return 0.0;
            int totalMonths = e.TotalMonths();
            double totalContributed = e.TotalContributedSimple();
            double mbqtl = ComputeMBQTL(e, factorByYear);
            if (totalMonths < 12)
            {
                double result = 0.22 * totalContributed;
                double cap = 2.0 * mbqtl;
                if (result > cap) result = cap;
                return Math.Round(result, 0);
            }

            // Đếm tháng trước 2014 và từ 2014
            int monthsBefore2014 = 0, monthsFrom2014 = 0;
            foreach (var p in e.Periods)
            {
                foreach (var ym in p.IterateMonths())
                {
                    if (ym.year < 2014) monthsBefore2014++;
                    else monthsFrom2014++;
                }
            }

            double yearsBefore2014 = MonthsToYeartsRounded(monthsBefore2014);
            double yearsFrom2014 = MonthsToYeartsRounded(monthsFrom2014);
            double amount = mbqtl * (1.5 * yearsBefore2014 + 2.0 * yearsFrom2014);
            double cap2 = 2.0 * mbqtl;
            if (amount > cap2) amount = cap2;
            return Math.Round(amount, 0);
        }

    }
}
