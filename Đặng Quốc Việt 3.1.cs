using System;

class Program
{
    // =========================
    // BAI TAP 1: CALCULATOR
    // =========================
    static void BaiTap1()
    {
        Console.Clear();
        Console.WriteLine("===== BAI TAP 1: CALCULATOR =====");

        Console.Write("Nhap so thu nhat a = ");
        double a = double.Parse(Console.ReadLine());

        Console.Write("Nhap so thu hai b = ");
        double b = double.Parse(Console.ReadLine());

        Console.Write("Nhap phep toan (+, -, *, /, %) = ");
        char op = char.Parse(Console.ReadLine());

        try
        {
            double result = op switch
            {
                '+' => a + b,
                '-' => a - b,
                '*' => a * b,

                '/' => b == 0
                    ? throw new DivideByZeroException()
                    : a / b,

                '%' => b == 0
                    ? throw new DivideByZeroException()
                    : a % b,

                _ => throw new InvalidOperationException("Phep toan khong hop le!")
            };

            Console.WriteLine($"Ket qua: {a} {op} {b} = {result}");
        }
        catch (DivideByZeroException)
        {
            Console.WriteLine("Loi: Khong the chia cho 0!");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine(ex.Message);
        }

        Console.WriteLine("\nNhan phim bat ky de quay lai Menu...");
        Console.ReadKey();
    }


    