using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        private static double xStart = -10 * 16.0 / 9;
        private static double xFinish = 10 * 16.0 / 9;
        private static double yStart = -10;
        private static double yFinish = 10;

        private static void ToScreenCoords(double x, double y, out int screenX, out int screenY)
        {
            double centerX = (xFinish + xStart) / 2;
            double centerY = (yFinish + yStart) / 2;

            double widthScale = Console.WindowWidth / Math.Abs(xFinish - xStart);
            screenX = (int)(Math.Round((x - centerX) * widthScale)) + Console.WindowWidth / 2;

            double heightScale = Console.WindowHeight / Math.Abs(yFinish - yStart);
            screenY = -(int)(Math.Round((y - centerY) * heightScale)) + Console.WindowHeight / 2;
        }

        private static void DrawDot(double x, double y, char symbol = 'x', ConsoleColor color = ConsoleColor.White)
        {
            ToScreenCoords(x, y, out int screenX, out int screenY);

            try
            {
                if (!(screenX < 0 || screenX > Console.WindowWidth || screenY < 0 || screenY > Console.WindowHeight))
                {
                    ConsoleColor oldColor = Console.ForegroundColor;
                    Console.ForegroundColor = color;

                    Console.SetCursorPosition(screenX, screenY);
                    Console.Write('x');

                    Console.ForegroundColor = oldColor;
                }
            }
            catch (Exception e)
            {

            }
        }

        private static (double, double) Intersection(double k1, double b1, double k2, double b2)
        {
            double x = (b2 - b1) / (k1 - k2);
            double y = k1 * x + b1;
            return (x, y);
        }

        private static void DrawStraight(double k, double b)
        {
            for (double x = -10; x < 10; x += 0.2)
            {
                double y = k * x + b;
                DrawDot(x, y);
                y = 0;
            }
        }

        private static void LGBTBackight(double x, double y)
        {
            DrawDot(x, y, 'x', ConsoleColor.Red);
            for (double a = x - 3; a < x + 3; a += 0.05)
            {
                double b = Math.Sqrt(9 - (a - x) * (a - x)) + y;
                DrawDot(a, b, 'x', ConsoleColor.Green);
                b = -(Math.Sqrt(9 - (a - x) * (a - x))) + y;
                DrawDot(a, b, 'x', ConsoleColor.Green);

                double centerX = (xFinish + xStart) / 2;
                double centerY = (yFinish + yStart) / 2;
                double widthScale = Console.WindowWidth / Math.Abs(xFinish - xStart);
                int screenX = (int)(Math.Round((x - centerX) * widthScale)) + Console.WindowWidth / 2;

                double heightScale = Console.WindowHeight / Math.Abs(yFinish - yStart);
                int screenY = -(int)(Math.Round((y - centerY) * heightScale)) + Console.WindowHeight / 2;
                Console.SetCursorPosition(screenX + 10, screenY);


                ConsoleColor oldColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("(" + x + ";" + y + ")");

            }
        }

        private static void Input(out double k1, out double b1, out double k2, out double b2)
        {
            k1 = double.Parse(Console.ReadLine());
            b1 = double.Parse(Console.ReadLine());
            k2 = double.Parse(Console.ReadLine());
            b2 = double.Parse(Console.ReadLine());
        }


        private static void Main()
        {
            Console.Clear();
            double k1 = 0;
            double k2 = 0;
            double b1 = 0;
            double b2 = 0;
            Input(out k1, out b1, out k2, out b2);

            (double, double) IntersectionDot = Intersection(k1, b1, k2, b2);

            DrawStraight(k1, b1);
            DrawStraight(k2, b2);

            LGBTBackight(IntersectionDot.Item1, IntersectionDot.Item2);

            Console.ReadKey();
        }
    }
}
