using System;

class Program
{
    static void Main(string[] args)
    {
        int choice;

        do
        {
            Console.Clear();
            Console.WriteLine("==========================================");
            Console.WriteLine("             MENU CHƯƠNG TRÌNH            ");
            Console.WriteLine("==========================================");
            Console.WriteLine("1. Chạy Bài tập 1 (Calculator)");
            Console.WriteLine("2. Chạy Bài tập 2 (Phương trình bậc 2)");
            Console.WriteLine("3. Chạy Bài tập 3 (Số nguyên tố & Fibonacci)");
            Console.WriteLine("0. Thoát chương trình");
            Console.WriteLine("==========================================");
            Console.Write("Nhập lựa chọn của bạn (0-3): ");

            // Kiểm tra nhập liệu an toàn
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                choice = -1; // Đặt giá trị mặc định nếu nhập không phải số
            }

            Console.WriteLine(); // Xuống dòng cho đẹp mắt

            switch (choice)
            {
                case 1:
                    RunExercise1();
                    PauseScreen();
                    break;
                case 2:
                    RunExercise2();
                    PauseScreen();
                    break;
                case 3:
                    RunExercise3();
                    PauseScreen();
                    break;
                case 0:
                    Console.WriteLine("Cảm ơn bạn đã sử dụng chương trình. Tạm biệt!");
                    break;
                default:
                    Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng nhập từ 0 đến 3!");
                    PauseScreen();
                    break;
            }

        } while (choice != 0);
    }

    // Tạm dừng màn hình để người dùng xem kết quả
    static void PauseScreen()
    {
        Console.WriteLine("\n------------------------------------------");
        Console.Write("Nhấn phím bất kỳ để quay lại Menu...");
        Console.ReadKey();
    }

    // Các hàm tương ứng với chức năng bài tập
    static void RunExercise1()
    {
        Console.WriteLine("--- BÀI TẬP 1: CALCULATOR ---");
        // Đặt code Bài tập 1 tại đây
    }

    static void RunExercise2()
    {
        Console.WriteLine("--- BÀI TẬP 2: PHƯƠNG TRÌNH BẬC 2 ---");
        // Đặt code Bài tập 2 tại đây
    }

    static void RunExercise3()
    {
        Console.WriteLine("--- BÀI TẬP 3: SỐ NGUYÊN TỐ & FIBONACCI ---");
        // Đặt code Bài tập 3 tại đây
    }
}