using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema
{
    /// <summary>
    /// Class for calculating statistics about cinema visitors.
    /// </summary>
    public static class Statisztika
    {
        /// <summary>
        /// Static list of strings, staticstics about cinema visitors.
        /// </summary>
        public static List<String> list;
        /// <summary>
        /// Constructor
        /// </summary>
        static Statisztika()
        {
            list = new List<string>();
        }
        /// <summary>
        /// Function to calculate the statistics.
        /// </summary>
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
    /// <summary>
    /// Class of cinemaVisitors
    /// </summary>
    class CinemaVisitor
    {

        public  int TX { get; set; }
        public int TY { get; set; }
        public int AX { get; set; }
        public int AY { get; set; }
        public VisitorType VType { get; set; }
        /// <summary>
        /// Empty constructor
        /// </summary>
        public CinemaVisitor()
        {

        }
        /// <summary>
        /// Normal constructor
        /// </summary>
        /// <param name="tX"></param>
        /// <param name="tY"></param>
        /// <param name="aX"></param>
        /// <param name="aY"></param>
        /// <param name="vType"></param>

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
        /// <summary>
        /// Function to draw the characters current position.
        /// </summary>
        public void Draw()
        {
            Console.SetCursorPosition(AX, 2 * AY);
            Console.Write('*');
        }
        /// <summary>
        /// Function to move the character.
        /// </summary>
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
        /// <summary>
        /// Enum, the type of visitor, coming or leaving.
        /// </summary>
        public enum VisitorType
        {
            In,
            Out
        }
    }
}
