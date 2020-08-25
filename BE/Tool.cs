using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BE
{
    public static class Tool
    {
        public static XElement faltten2<T>(this T[] arr)
        {
            XElement diary = new XElement("diary");
            foreach (var item in arr)
            {
                diary.Add(new XElement("bool", item));
            }
            return diary;
        }

        public static XElement Flatten<T>(this bool[,] arr)
        {
            int rows = arr.GetLength(0);
            int columns = arr.GetLength(1);
            bool[] arrFlattened = new bool[rows * columns];
            for (int j = 0; j < rows; j++)
            {
                for (int i = 0; i < columns; i++)
                {
                    var test = arr[j, i];
                    arrFlattened[j * columns + i] = arr[j, i];
                }
            }
            return faltten2(arrFlattened);
        }


        public static bool[,] Expand<T>(this XElement arr, int rows)
        {
            int length = arr.Elements().Count();
            int columns = length / rows;
            bool[,] arrExpanded = new bool[rows, columns];
            IEnumerable<XElement> diaty = arr.Elements();
            for (int j = 0; j < rows; j++)
            {
                for (int i = 0; i < columns; i++)
                {
                    arrExpanded[j, i] = bool.Parse(diaty.ToArray()[j * columns + i].Value);
                }
            }
            return arrExpanded;
        }
    }
}
