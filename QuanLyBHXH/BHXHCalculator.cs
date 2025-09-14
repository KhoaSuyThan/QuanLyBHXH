using System;
using System.Collections.Generic;

namespace QuanLyBHXH
{
    internal class BHXHCalculator
    {
        private static double MonthsToYearsRounded(int months)
        {
            int years = months / 12;
            int rem = months % 12;
            double extra = 0.0;
            if (rem >= 1 && rem <= 6) extra = 0.5;
            else if (rem >= 7) extra = 1.0;
            return years + extra;
        }

        // Tính mức bình quân tiền lương có hệ số trượt giá
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

        // Tính BHXH một lần (đã áp công thức hệ số trượt giá)
        public static double ComputeOneTime(Employee e, Dictionary<int, double> factorByYear = null)
        {
            if (!e.HasAnyPeriod()) return 0.0;

            // Tổng số tháng
            int totalMonths = e.TotalMonths();
            if (totalMonths == 0) return 0.0;

            // Tính MBQTL có hệ số trượt giá
            double mbqtl = Math.Round(ComputeMBQTL(e, factorByYear), 0);

            // Đếm số tháng trước và sau 2014
            int monthsBefore2014 = 0, monthsFrom2014 = 0;
            foreach (var p in e.Periods)
            {
                foreach (var ym in p.IterateMonths())
                {
                    if (ym.year < 2014) monthsBefore2014++;
                    else monthsFrom2014++;
                }
            }

            // Quy đổi thành số năm (làm tròn 0.5/1)
            double yearsBefore2014 = MonthsToYearsRounded(monthsBefore2014);
            double yearsFrom2014 = MonthsToYearsRounded(monthsFrom2014);

            // Công thức tính BHXH một lần
            double amount = mbqtl * (1.5 * yearsBefore2014 + 2.0 * yearsFrom2014);

            return Math.Round(amount, 0);
        }
    }
}
