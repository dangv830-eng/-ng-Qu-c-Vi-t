// =========================
// BAI TAP 2: PHUONG TRINH BAC 2
// =========================
static void BaiTap2()
{
    Console.Clear();
    Console.WriteLine("===== BAI TAP 2: PHUONG TRINH BAC 2 =====");

    Console.Write("Nhap a = ");
    double a = double.Parse(Console.ReadLine());

    Console.Write("Nhap b = ");
    double b = double.Parse(Console.ReadLine());

    Console.Write("Nhap c = ");
    double c = double.Parse(Console.ReadLine());

    if (a == 0)
    {
        if (b == 0)
        {
            if (c == 0)
                Console.WriteLine("Phuong trinh co vo so nghiem.");
            else
                Console.WriteLine("Phuong trinh vo nghiem.");
        }
        else
        {
            double x = -c / b;
            Console.WriteLine($"Phuong trinh co nghiem x = {x}");
        }
    }
    else
    {
        double delta = b * b - 4 * a * c;

        Console.WriteLine($"Delta = {delta}");

        if (delta < 0)
        {
            Console.WriteLine("Phuong trinh vo nghiem.");
        }
        else if (delta == 0)
        {
            double x = -b / (2 * a);
            Console.WriteLine($"Phuong trinh co nghiem kep x1 = x2 = {x}");
        }
        else
        {
            double x1 = (-b + Math.Sqrt(delta)) / (2 * a);
            double x2 = (-b - Math.Sqrt(delta)) / (2 * a);

            Console.WriteLine("Phuong trinh co 2 nghiem phan biet:");
            Console.WriteLine($"x1 = {x1}");
            Console.WriteLine($"x2 = {x2}");
        }
    }

    Console.WriteLine("\nNhan phim bat ky de quay lai Menu...");
    Console.ReadKey();
}
