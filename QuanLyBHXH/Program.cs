using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBHXH
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var manager = new EmployeeManager();
            manager.CreateData();
            while (true)
            {
                Console.WriteLine("\n=== QUAN LY BHXH ===");
                Console.WriteLine("1. Them moi nguoi lao dong");
                Console.WriteLine("2. Them giai doan dong BHXH cho nguoi lao dong");
                Console.WriteLine("3. Xuat danh sach nguoi lao dong (thong ke Nam/Nu)");
                Console.WriteLine("4. Xuat danh sach nguoi lao dong co tham gia BHXH va tong tien da dong");
                Console.WriteLine("5. Tinh tien BHXH 1 lan cho nhung nguoi co tham gia (2024 - don gian)");
                Console.WriteLine("6. Thoat");
                Console.Write("Chon: ");
                var opt = Console.ReadLine()?.Trim();
                switch (opt)
                {
                    case "1":
                        manager.AddEmployeeInteractive();
                        break;
                    case "2":
                        manager.AddPeriodsInteractive();
                        break;
                    case "3":
                        manager.PrintAllEmployees();
                        break;
                    case "4":
                        manager.PrintParticipants();
                        break;
                    case "5":
                        manager.PrintComputeOneTimeAll();
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Lua chon khong hop le.");
                        break;
                }
            }
        }
    }
}
