using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace jarvisMarch
{
    internal class Program
    {
        public static int iteration; 
        public static void Main(string[] args)
        {
            var streamReader = 
                new StreamReader("C:\\Users\\deter\\RiderProjects\\jarvisMarch\\Generator\\values.txt");
            
            string line;
            var lineList = new List<string>();
            while ((line = streamReader.ReadLine()) != null)
                lineList.Add(line);
            
            streamReader.Close();
            
            var times = new List<TimeSpan>();
            var iterations = new List<int>();

            var pointsList = new List<Point>();
            for (int j = 0; j < lineList.Count; j++)
            {
                foreach (var item in lineList[j].Split(' '))
                {
                    var m = item.Split('.');
                    if (string.IsNullOrEmpty(m[0]))
                        break;
                    pointsList.Add(new Point(int.Parse(m[0]), int.Parse(m[1])));
                }
                
                Stopwatch x = new Stopwatch();
                x.Start();
                var p = JarvisMarch(pointsList);
                x.Stop();
                
                times.Add(x.Elapsed);
                iterations.Add(iteration);
                
                Console.WriteLine((j + 1) + ": complete");
            }
            
            var streamWriter =
                new StreamWriter("C:\\Users\\deter\\RiderProjects\\jarvisMarch\\jarvisMarch\\result.txt");
            var countEl = 100;
            for (int i = 0; i < times.Count; i++)
            {
                streamWriter.WriteLine("{0} elements: Time: {1:00}.{2} iterations: {3}", 
                    countEl, times[i].Seconds,  times[i].TotalMilliseconds, iterations[i]);
                countEl += 100;
            }
            streamWriter.Close();
        }
        
        private static int FindAngle(Point p1, Point p2, Point p3)
        {
            int val = (p2.Y - p1.Y) * (p3.X - p2.X) -
                      (p2.X - p1.X) * (p3.Y - p2.Y);
      
            if (val == 0) return 0; 
            return (val > 0)? 1: 2; 
        }
        
        private static List<Point> JarvisMarch(List<Point> points)
        {
            iteration = 0;
            List<Point> hull = new List<Point>();
            var n = points.Count;
            int startPointId = 0;
            for (int i = 1; i < n; i++)
            {
                iteration++;
                if (points[i].X < points[startPointId].X)
                    startPointId = i;
            }

            int currentPointId = startPointId;
            do
            {
                iteration++;
                hull.Add(points[currentPointId]);
                
                var nextPointId = (currentPointId + 1) % n;
              
                for (int i = 0; i < n; i++)
                {
                    iteration++;
                    if (FindAngle(points[currentPointId], points[i], points[nextPointId]) == 2)
                        nextPointId = i;
                }
                currentPointId = nextPointId;
      
            } while (currentPointId != startPointId);
            
            hull.Add(points[startPointId]);
            return hull;
        }
    }

    internal class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}