using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema
{
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
