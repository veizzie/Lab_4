using System;
using System.Formats.Asn1;

class Par
{
    protected double sideA;
    protected double sideB;
    protected double angle;

    public Par(double sideA, double sideB, double angle)
    {
        this.sideA = sideA;
        this.sideB = sideB;
        this.angle = angle * (Math.PI / 180.0); // Convert degrees to radians
    }

    public void Print()
    {
        double angleDegrees = angle * (180.0 / Math.PI); // Convert radians to degrees
        Console.WriteLine($"\na = {sideA}, b = {sideB}, angle = {angleDegrees}");
    }

    public virtual double Sqr()
    {
        double area = Math.Round(sideA * sideB * Math.Sin(angle), 2);
        Console.WriteLine("Площа паралелограма S = " + area);
        return area;
    }

    public void Diag(out double diagonal1, out double diagonal2)
    {
        // Using the Law of Cosines to find diagonals
        diagonal1 = Math.Round(Math.Sqrt(sideA * sideA + sideB * sideB - 2 * sideA * sideB * Math.Cos(angle)), 2);
        diagonal2 = Math.Round(Math.Sqrt(sideA * sideA + sideB * sideB + 2 * sideA * sideB * Math.Cos(angle)), 2);
        Console.WriteLine($"Діагоналі d1 = {diagonal1}, d2 = {diagonal2}");
    }
}

class PryamRectangle : Par
{
    public PryamRectangle(double sideA, double sideB) : base(sideA, sideB, 90)
    {
    }

    public override double Sqr()
    {
        double area = sideA * sideB;
        Console.WriteLine("Площа прямокутника S = " + area);
        return area;
    }

    public double Radius()
    {
        return Math.Sqrt(sideA * sideA + sideB * sideB) / 2;
    }
}


class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Random random = new Random();

        for (int i = 0; i < 3; i++)
        {
            double s1 = random.Next(1, 10); // Випадкова довжина сторони 1
            double s2 = random.Next(1, 10); // Випадкова довжина сторони 2
            double a = random.Next(80, 90); // Випадковий кут від 80 до 90 градусів

            Par shape;

            if (random.Next(0, 2) == 0) // Випадково вибираємо між паралелограмом і прямокутником
            {
                shape = new Par(s1, s2, a);
            }
            else
            {
                shape = new PryamRectangle(s1, s2);
            }

            shape.Print();
            double area = shape.Sqr();
            if (shape is PryamRectangle)
            {
                PryamRectangle rectangle = (PryamRectangle)shape;

                Console.WriteLine($"R = {Math.Round(rectangle.Radius(), 2)}");
            }
            shape.Diag(out double diagonal1, out double diagonal2);
        }

        Console.ReadKey();
    }
}
