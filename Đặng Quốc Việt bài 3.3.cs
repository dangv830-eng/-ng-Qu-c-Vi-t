// =========================
// BAI TAP 3: SO NGUYEN TO
// =========================
static bool IsPrime(int n)
{
    if (n < 2)
        return false;

    for (int i = 2; i <= Math.Sqrt(n); i++)
    {
        if (n % i == 0)
            return false;
    }

    return true;
}


// =========================
// BAI TAP 3: SO HOAN HAO
// =========================
static bool IsPerfectNumber(int n)
{
    if (n <= 1)
        return false;

    int sum = 1;

    for (int i = 2; i <= n / 2; i++)
    {
        if (n % i == 0)
            sum += i;
    }

    return sum == n;
}
// =========================
// BAI TAP 3
// =========================
static void BaiTap3()
{
    Console.Clear();
    Console.WriteLine("===== BAI TAP 3: SO NGUYEN TO & FIBONACCI =====");

    int N;

    do
    {
        Console.Write("Nhap so nguyen duong N = ");
        N = int.Parse(Console.ReadLine());
    }
    while (N <= 0);

    // Kiem tra so nguyen to
    if (IsPrime(N))
        Console.WriteLine($"{N} la so nguyen to.");
    else
        Console.WriteLine($"{N} khong phai la so nguyen to.");

    // Kiem tra so hoan hao
    if (IsPerfectNumber(N))
        Console.WriteLine($"{N} la so hoan hao.");
    else
        Console.WriteLine($"{N} khong phai la so hoan hao.");

    // In N so Fibonacci dau tien
    Console.WriteLine($"\n{N} so dau tien cua day Fibonacci:");

    long f1 = 0;
    long f2 = 1;

    for (int i = 1; i <= N; i++)
    {
        Console.Write(f1 + " ");

        long f3 = f1 + f2;
        f1 = f2;
        f2 = f3;
    }

    Console.WriteLine();

    Console.WriteLine("\nNhan phim bat ky de quay lai Menu...");
    Console.ReadKey();
}


// =========================
// MENU CHINH
// =========================
static void Main()
{
    int choice;

    do
    {
        Console.Clear();

        Console.WriteLine("========================================");
        Console.WriteLine("              MENU CHINH");
        Console.WriteLine("========================================");
        Console.WriteLine("1. Chay Bai tap 1 (Calculator)");
        Console.WriteLine("2. Chay Bai tap 2 (Phuong trinh bac 2)");
        Console.WriteLine("3. Chay Bai tap 3 (So nguyen to & Fibonacci)");
        Console.WriteLine("0. Thoat chuong trinh");
        Console.WriteLine("========================================");

        Console.Write("Nhap lua chon cua ban: ");
        choice = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case 1:
                BaiTap1();
                break;

            case 2:
                BaiTap2();
                break;

            case 3:
                BaiTap3();
                break;

            case 0:
                Console.Clear();
                Console.WriteLine("Cam on ban da su dung chuong trinh!");
                break;

            default:
                Console.WriteLine("Lua chon khong hop le!");

                Console.WriteLine("\nNhan phim bat ky de tiep tuc...");
                Console.ReadKey();
                break;
        }

    } while (choice != 0);
}
}