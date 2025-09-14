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
            Console.Write("Nhap ma BHXH (10 chu so): ");
            string id = Console.ReadLine()?.Trim();
            if (!InputHelper.IsValidBhxhId(id))
            {
                Console.WriteLine("Ma BHXH khong hop le (phai 10 chu so).");
                return;
            }
            if (FindById(id) != null)
            {
                Console.WriteLine("Ma BHXH da ton tai.");
                return;
            }
            Console.Write("Ho va ten: ");
            string name = Console.ReadLine()?.Trim();
            Console.Write("Gioi tinh (Nam/Nu): ");
            string gender = Console.ReadLine()?.Trim();
            Console.Write("Ngay sinh (yyyy-MM-dd): ");
            if (!DateTime.TryParse(Console.ReadLine()?.Trim(), out DateTime dob))
            {
                Console.WriteLine("Ngay sinh khong hop le.");
                return;
            }
            var e = new Employee(id, name, gender.Equals("Nam", StringComparison.OrdinalIgnoreCase) ? "Nam" : "Nu", dob);
            employees.Add(e);
            Console.WriteLine("Them thanh cong.");
        }

        public void AddPeriodsInteractive()
        {
            Console.Write("Nhap ma BHXH de them giai doan: ");
            var id = Console.ReadLine()?.Trim();
            var e = FindById(id);
            if (e == null)
            {
                Console.WriteLine("Khong tim thay ma BHXH.");
                return;
            }
            Console.WriteLine($"Thong tin: {e}");
            Console.Write("Nhap so giai doan K (>0): ");
            if (!int.TryParse(Console.ReadLine()?.Trim(), out int k) || k <= 0)
            {
                Console.WriteLine("K khong hop le.");
                return;
            }
            for (int i = 1; i <= k; i++)
            {
                Console.WriteLine($"Giai doan {i}:");
                try
                {
                    int sm = InputHelper.ReadInt("Tu thang (1-12): ");
                    int sy = InputHelper.ReadInt("Tu nam: ");
                    int em = InputHelper.ReadInt("Den thang (1-12): ");
                    int ey = InputHelper.ReadInt("Den nam: ");
                    double sal = InputHelper.ReadDouble("Muc luong dong (VND/thang): ");
                    var p = new Period(sm, sy, em, ey, sal);
                    if (!p.IsValid())
                    {
                        Console.WriteLine("Giai doan khong hop le (thoi gian). Nhap lai.");
                        i--; continue;
                    }
                    if (!e.AddPeriod(p))
                    {
                        Console.WriteLine("Giai doan trung lap voi giai doan da co. Nhap lai.");
                        i--; continue;
                    }
                    Console.WriteLine("Them giai doan thanh cong.");
                }
                catch (Exception)
                {
                    Console.WriteLine("Du lieu giai doan khong hop le. Nhap lai.");
                    i--; continue;
                }
            }
        }

        public void PrintAllEmployees()
        {
            Console.WriteLine("\n--- Danh sach nguoi lao dong ---");
            int nam = 0, nu = 0;
            foreach (var e in employees)
            {
                Console.WriteLine(e);
                if (e.Gender.Equals("Nam", StringComparison.OrdinalIgnoreCase)) nam++;
                else nu++;
            }
            Console.WriteLine($"Tong Nam: {nam} | Tong Nu: {nu}");
        }

        public void PrintParticipants()
        {
            Console.WriteLine("\n--- Danh sach nguoi co tham gia BHXH ---");
            foreach (var e in employees)
            {
                if (e.HasAnyPeriod())
                {
                    Console.WriteLine($"{e.ID} | {e.Name} | Tong thang: {e.TotalMonths()} | Tong dong (don gian): {e.TotalContributedSimple():N0}");
                }
            }
        }

        public void PrintComputeOneTimeAll()
        {
            Console.WriteLine("\n--- Tinh tien BHXH 1 lan (approx) ---");
            foreach (var e in employees)
            {
                if (!e.HasAnyPeriod()) continue;
                var val = BHXHCalculator.ComputeOneTime(e);
                Console.WriteLine($"{e.ID} | {e.Name} | Thang dong: {e.TotalMonths()} | BHXH 1 lan (approx): {val:N0}");
            }
            Console.WriteLine("Luu y: Ket qua la tham khao (khong ap he so truot gia chi tiet).");
        }

        private Employee FindById(string id) => employees.Find(x => x.ID == id);
    }
}
