using System;
using System.Collections.Generic;

namespace Lissajous
{
    public class LissajousModel
    {
        public IEnumerable<(double X, double Y)> GeneratePoints(
            double ax, double ay,
            double fx, double fy,
            double phiXDeg, double phiYDeg)
        {
            var points = new List<(double X, double Y)>();

            double phiX = phiXDeg * Math.PI / 180.0;
            double phiY = phiYDeg * Math.PI / 180.0;

            double maxF = Math.Max(fx, fy);
            double minT = 1.0 / maxF;
            double dt = minT / 100.0;

            double minF = Math.Min(fx, fy);
            double maxT = 1.0 / minF;
            double totalTime = maxT * 20;

            int pointCount = (int)(totalTime / dt);
            if (pointCount > 20000) pointCount = 20000;

            for (int i = 0; i <= pointCount; i++)
            {
                double t = i * dt;
                double x = ax * Math.Sin(2 * Math.PI * fx * t + phiX);
                double y = ay * Math.Sin(2 * Math.PI * fy * t + phiY);
                points.Add((x, y));
            }

            return points;
        }
    }
}