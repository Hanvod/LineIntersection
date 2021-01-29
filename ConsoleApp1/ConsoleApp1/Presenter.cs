using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Presenter
    {
        private static double xStart = -10 * 16.0 / 9;
        private static double xFinish = 10 * 16.0 / 9;
        private static double yStart = -10;
        private static double yFinish = 10;

        private static ScreenPoint ConvertToScreenCoords(Point point)
        {
            double centerX = (xFinish + xStart) / 2;
            double centerY = (yFinish + yStart) / 2;

            double widthScale = Console.WindowWidth / Math.Abs(xFinish - xStart);
            int screenX = (int)(Math.Round((point.X - centerX) * widthScale)) + Console.WindowWidth / 2;

            double heightScale = Console.WindowHeight / Math.Abs(yFinish - yStart);
            int screenY = -(int)(Math.Round((point.Y - centerY) * heightScale)) + Console.WindowHeight / 2;

            return new ScreenPoint(screenX, screenY);
        }

        private static void WriteAtPosition(ScreenPoint screenPosition, char symbol = 'x', ConsoleColor color = ConsoleColor.White)
        {
            if (!(screenPosition.X < 0 || screenPosition.X > Console.WindowWidth || screenPosition.Y < 0 || screenPosition.Y > Console.WindowHeight))
            {
                ConsoleColor oldColor = Console.ForegroundColor;
                Console.ForegroundColor = color;

                Console.SetCursorPosition(screenPosition.X, screenPosition.Y);
                Console.Write(symbol);

                Console.ForegroundColor = oldColor;
            }
        }

        private static void DrawDot(Point point, char symbol = 'x', ConsoleColor color = ConsoleColor.White)
        {
            ScreenPoint screenPoint = ConvertToScreenCoords(point);
            WriteAtPosition(screenPoint, symbol, color);
        }

        private static void DrawLine(Line line)
        {
            for (double x = xStart; x < xFinish; x += 0.01)
            {
                double y = line.K * x + line.B;
                DrawDot(new Point(x, y));
            }
        }

        private static void DrawCircle(Point center, double radius, char symbol = 'x', ConsoleColor color = ConsoleColor.Red)
        {
            for (double t = 0; t < 2 * Math.PI; t += 0.01)
            {
                double x = radius * Math.Sin(t) + center.X;
                double y = radius * Math.Cos(t) + center.Y;

                ScreenPoint screenPoint = ConvertToScreenCoords(new Point(x, y));

                WriteAtPosition(screenPoint, symbol, color);
            }
        }

        private static void DrawHightlightedDot(Point point) => DrawDot(point, 'O', ConsoleColor.Red);

        private static void FocusOnPoint(Point point, double xFov = 10, double yFov = double.NaN)
        {
            if(double.IsNaN(yFov))
            {
                yFov = xFov * Console.WindowHeight / Console.WindowWidth;
            }

            xStart = point.X - xFov / 2;
            xFinish = point.X + xFov / 2;
            
            yStart = point.Y - yFov / 2;
            yFinish = point.Y + yFov / 2;
        }

        public static void Visualize(Line line1, Line line2, Point point)
        {
            Console.Clear();

            FocusOnPoint(point, 10);

            DrawLine(line1);
            DrawLine(line2);

            DrawHightlightedDot(point);

            ScreenPoint screenPoint = ConvertToScreenCoords(point);

            DrawCircle(point, 0.5);

            Console.SetCursorPosition(screenPoint.X + 5, screenPoint.Y);
            Console.Write($"({point.X}, {point.Y})");

            Console.ReadKey();
        }
    }
}
