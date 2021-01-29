using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        private static double ReadDouble() => double.Parse(Console.ReadLine().Replace(".", ","));

        private static void GetInput(out Line line1, out Line line2)
        {
            line1 = new Line(ReadDouble(), ReadDouble());
            line2 = new Line(ReadDouble(), ReadDouble());
        }
      
        private static void Main()
        {
            GetInput(out Line line1, out Line line2);

            Point point = line1.GetIntersection(line2);

            Presenter.Visualize(line1, line2, point);
        }
    }
}
