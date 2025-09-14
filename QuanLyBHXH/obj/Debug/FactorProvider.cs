using System;
using System.Collections.Generic;

namespace QuanLyBHXH
{
    internal static class FactorProvider
    {
        // Giá trị là "mức điều chỉnh" (factor) cho từng năm.
        public static Dictionary<int, double> DefaultFactors { get; } = CreateDefault();

        private static Dictionary<int, double> CreateDefault()
        {
            var d = new Dictionary<int, double>();
            
            d[1995] = 4.78;
            d[1996] = 4.51;
            d[1997] = 4.37;
            d[1998] = 4.06;
            d[1999] = 3.89;
            d[2000] = 3.95;
            // ...
            d[2008] = 2.21;
            d[2009] = 2.07;
            d[2010] = 1.90;
            d[2011] = 1.60;
            d[2012] = 1.47;
            d[2013] = 1.37;
            d[2014] = 1.32;
            d[2015] = 1.31;
            d[2016] = 1.28;
            d[2017] = 1.23;
            d[2018] = 1.19;
            d[2019] = 1.16;
            d[2020] = 1.12;
            d[2021] = 1.10;
            d[2022] = 1.07;
            d[2023] = 1.04;
            d[2024] = 1.00;
            d[2025] = 1.00;

            // Thêm các năm khác (ví dụ "trước 1995") -> bạn có thể map là một key -1 hoặc dùng trị cố định
            // d[-1] = 5.63; // nếu muốn dùng

            return d;
        }
        
    // Nếu muốn load từ file JSON/CSV, thêm method LoadFromJson(path) ở đây.
}
}
