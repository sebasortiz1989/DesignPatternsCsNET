using System;

namespace Factories
{
    public class PointExample
    {
        public enum CoordinateSystem
        {
            Cartesian,
            Polar,
        }

        public class Point
        {
            private double x, y;

            private Point(double x, double y)
            {
                this.x = x;
                this.y = y;
            }

            public static Point NewPointCartesian(double x, double y)
            {
                return new Point(x, y);
            }

            public static Point NewPointPolar(double rho, double theta)
            {
                return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
            }

            public override string ToString()
            {
                return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
            }
        }

        public static void Main(string[] args)
        {
            var point = Point.NewPointPolar(1, Math.PI / 2);
            Console.WriteLine(point);
        }
    }
}