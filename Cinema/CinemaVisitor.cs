using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema
{
    public static class Statisztika
    {
        public static List<String> list;
        static Statisztika()
        {
            list = new List<string>();
        }
        public static void Stat_calc()
        {
            foreach (var line in list.GroupBy(info => info)
                .Select(group => new
                {
                    metric=group.Key,
                    Count=group.Count()
                })
                .OrderBy(x=> x.metric)
                )
            {
                Console.WriteLine("hely {}: {1}db",line.metric,line.Count);
            }
        }
    }
    class CinemaVisitor
    {
        public  int TX { get; set; }
        public int TY { get; set; }
        public int AX { get; set; }
        public int AY { get; set; }
        public VisitorType VType { get; set; }
        public CinemaVisitor()
        {

        }

        public CinemaVisitor(int tX, int tY, int aX, int aY, VisitorType vType)
        {
            TX = tX;
            TY = tY;
            AX = aX;
            AY = aY;
            VType = vType;
            if (VType==VisitorType.In)
            {
                Statisztika.list.Add(String.Concat(TY.ToString(),' ',TX.ToString()));
            }
        }

        public void Draw()
        {
            Console.SetCursorPosition(AX, 2 * AY);
            Console.Write('*');
        }
        public void Move()
        {
            if (AX!=TX||AY!=TY)
            {
                if (VType==VisitorType.In)
                {
                    if (AY!=TY) AY++;
                    else AX++;
                }
                if (VType==VisitorType.Out)
                {
                    if (AX != TX) AX++;
                    else AY++;
                }
            }
        }

        public enum VisitorType
        {
            In,
            Out
        }
    }
}
