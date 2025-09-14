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
                Console.WriteLine("\n=== QUẢN LÝ BHXH - Console ===");
                Console.WriteLine("1. Thêm mới người lao động");
                Console.WriteLine("2. Thêm giai đoạn đóng BHXH cho người lao động");
                Console.WriteLine("3. Xuất danh sách người lao động (thống kê Nam/Nữ)");
                Console.WriteLine("4. Xuất danh sách người lao động có tham gia BHXH và tổng tiền đã đóng");
                Console.WriteLine("5. Tính tiền BHXH 1 lần cho những người có tham gia (2024 - đơn giản)");
                Console.WriteLine("6. Thoát");
                Console.Write("Chọn: ");
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
                        Console.WriteLine("Lựa chọn không hợp lệ.");
                        break;
                }
            }
        }
    }
}
