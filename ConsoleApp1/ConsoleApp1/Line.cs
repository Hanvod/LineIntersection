using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Line
    {
        public double K { get; }
        public double B { get; }

        public Line(double k, double b)
        {
            K = k;
            B = b;
        }

        public Point GetIntersection(Line another)
        {
            double x = (another.B - this.B) / (this.K - another.K);
            double y = this.K * x + this.B;
            return new Point(x, y);
        }
    }
}
