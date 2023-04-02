using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Generator
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            StreamWriter sw = new StreamWriter
                ("C:\\Users\\deter\\RiderProjects\\jarvisMarch\\Generator\\values.txt");
            var rnd = new Random();
            var value = new List<string>();
            value.AddRange(Make(100, rnd));
            for (int i = 0; i < 100; i++)
            {
                var strs = value[i];
                sw.WriteLine(strs);
            }
            sw.Close();
        }

        public static List<string> Make(int countStr, Random rnd)
        {
            var countElem = 100;
            var strArray = new List<string>();
            for (int i = 0; i < countStr; i++)
            {
                var str = new StringBuilder();
                for (int j = 0; j < countElem; j++)
                {
                    var x = rnd.Next(-1000, 1000).ToString();
                    var y = rnd.Next(-1000, 1000).ToString();
                    str.Append(x + "." + y + " ");
                }
                strArray.Add(str.ToString());
                countElem += 100;
            }

            return strArray;
        }
    }
}