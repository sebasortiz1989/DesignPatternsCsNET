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

        public static class PointFactory
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

        public class Point
        {
            private double x, y;

            public Point(double x, double y)
            {
                this.x = x;
                this.y = y;
            }

            public override string ToString()
            {
                return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
            }
        }

        public void Main(string[] args)
        {
            var point = PointFactory.NewPointPolar(1, Math.PI / 2);
            Console.WriteLine(point);
        }
    }
}