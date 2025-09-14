using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBHXH
{
    internal class EmployeeManager
    {
        private List<Employee> employees = new List<Employee>();
        
        public void CreateData()
        {
            Console.WriteLine("Taoooo là Nammmmmmmmmm");
            var e1 = new Employee("1234567890", "Nguyen Van A", "Nam", new DateTime(1988, 5, 20));
            e1.AddPeriod(new Period(8, 2020, 7, 2021, 34000000));
            e1.AddPeriod(new Period(9, 2021, 7, 2024, 44000000));
            employees.Add(e1);

            var e2 = new Employee("0987654321", "Tran Thi B", "Nu", new DateTime(1990, 3, 2));
            e2.AddPeriod(new Period(1, 2010, 12, 2013, 8000000));
            e2.AddPeriod(new Period(1, 2014, 12, 2018, 10000000));
            employees.Add(e2);

            var e3 = new Employee("1111111111", "Le Van C", "Nam", new DateTime(1995, 7, 10));
            employees.Add(e3);
        }
        public void AddEmployeeInteractive()
        {
            Console.Write("Nhập mã BHXH (10 chữ số): ");
            string id = Console.ReadLine()?.Trim();
            if (!InputHelper.IsValidBhxhId(id))
            {
                Console.WriteLine("Mã BHXH không hợp lệ (phải 10 chữ số).");
                return;
            }
            if (FindById(id) != null)
            {
                Console.WriteLine("Mã BHXH đã tồn tại.");
                return;
            }
            Console.Write("Họ và tên: ");
            string name = Console.ReadLine()?.Trim();
            Console.Write("Giới tính (Nam/Nu): ");
            string gender = Console.ReadLine()?.Trim();
            Console.Write("Ngày sinh (yyyy-MM-dd): ");
            if (!DateTime.TryParse(Console.ReadLine()?.Trim(), out DateTime dob))
            {
                Console.WriteLine("Ngày sinh không hợp lệ.");
                return;
            }
            var e = new Employee(id, name, gender.Equals("Nam", StringComparison.OrdinalIgnoreCase) ? "Nam" : "Nu", dob);
            employees.Add(e);
            Console.WriteLine("Thêm thành công.");
        }

        public void AddPeriodsInteractive()
        {
            Console.Write("Nhập mã BHXH để thêm giai đoạn: ");
            var id = Console.ReadLine()?.Trim();
            var e = FindById(id);
            if (e == null)
            {
                Console.WriteLine("Không tìm thấy mã BHXH.");
                return;
            }
            Console.WriteLine($"Thông tin: {e}");
            Console.Write("Nhập số giai đoạn K (>0): ");
            if (!int.TryParse(Console.ReadLine()?.Trim(), out int k) || k <= 0)
            {
                Console.WriteLine("K không hợp lệ.");
                return;
            }
            for (int i = 1; i <= k; i++)
            {
                Console.WriteLine($"Giai đoạn {i}:");
                try
                {
                    int sm = InputHelper.ReadInt("Từ tháng (1-12): ");
                    int sy = InputHelper.ReadInt("Từ năm: ");
                    int em = InputHelper.ReadInt("Đến tháng (1-12): ");
                    int ey = InputHelper.ReadInt("Đến năm: ");
                    double sal = InputHelper.ReadDouble("Mức lương đóng (VNĐ/tháng): ");
                    var p = new Period(sm, sy, em, ey, sal);
                    if (!p.IsValid())
                    {
                        Console.WriteLine("Giai đoạn không hợp lệ (thời gian). Nhập lại.");
                        i--; continue;
                    }
                    if (!e.AddPeriod(p))
                    {
                        Console.WriteLine("Giai đoạn trùng lặp với giai đoạn đã có. Nhập lại.");
                        i--; continue;
                    }
                    Console.WriteLine("Thêm giai đoạn thành công.");
                }
                catch (Exception)
                {
                    Console.WriteLine("Dữ liệu giai đoạn không hợp lệ. Nhập lại.");
                    i--; continue;
                }
            }
        }

        public void PrintAllEmployees()
        {
            Console.WriteLine("\n--- Danh sách người lao động ---");
            int nam = 0, nu = 0;
            foreach (var e in employees)
            {
                Console.WriteLine(e);
                if (e.Gender.Equals("Nam", StringComparison.OrdinalIgnoreCase)) nam++;
                else nu++;
            }
            Console.WriteLine($"Tổng Nam: {nam} | Tổng Nữ: {nu}");
        }

        public void PrintParticipants()
        {
            Console.WriteLine("\n--- Danh sách người có tham gia BHXH ---");
            foreach (var e in employees)
            {
                if (e.HasAnyPeriod())
                {
                    Console.WriteLine($"{e.ID} | {e.Name} | Tổng tháng: {e.TotalMonths()} | Tổng đóng (đơn giản): {e.TotalContributedSimple():N0}");
                }
            }
        }

        public void PrintComputeOneTimeAll()
        {
            Console.WriteLine("\n--- Tính tiền BHXH 1 lần (approx) ---");
            foreach (var e in employees)
            {
                if (!e.HasAnyPeriod()) continue;
                var val = BHXHCalculator.ComputeOneTime(e);
                Console.WriteLine($"{e.ID} | {e.Name} | Tháng đóng: {e.TotalMonths()} | BHXH 1 lần (approx): {val:N0}");
            }
            Console.WriteLine("Lưu ý: Kết quả là tham khảo (không áp hệ số trượt giá chi tiết).");
        }

        private Employee FindById(string id) => employees.Find(x => x.ID == id);
    }
}
