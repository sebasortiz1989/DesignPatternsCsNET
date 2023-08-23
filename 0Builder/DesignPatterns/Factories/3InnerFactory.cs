using System;
using System.Threading.Tasks;

namespace Factories;

public class InnerFactory
{
    public class Point
    {
        private double x, y;

        private Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
        }

        public static Point Origin1 => new Point(0, 0);

        public static Point Origin2 = new Point(0, 0); // This one is better
        public static class Factory
        {
            public static Point NewPointCartesian(double x, double y)
            {
                return new Point(x, y);
            }

            public static Point NewPointPolar(double rho, double theta)
            {
                return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
            }
        }
    }

    public class Demo
    {
        public static void Main(string[] args)
        {
            var point = Point.Factory.NewPointPolar(1, Math.PI / 2);
            Console.WriteLine(point);

            var origin1 = Point.Origin1;
            var origin2 = Point.Origin2;
        }
    }
}