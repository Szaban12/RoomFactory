using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using RoomFactory;

namespace Cinema
{
    class Program
    {
        public static void Draw(char[,] c)
        {
            Console.CursorVisible = false;
            for (int i = 0; i < c.GetLength(0); i++)
            {
                for (int j = 0; j < c.GetLength(1); j++)
                {
                    Console.SetCursorPosition(i + 1, (2 * j) + 1);
                    Console.Write(c[i, j]);
                }
                Console.Write(" ");
            }
        }
        public static object Lockobject = new object();
        public static char[,] cinema;
        public static List<CinemaVisitor> visitorlist;
        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            Draw(cinema);
            foreach (CinemaVisitor c in visitorlist)
            {c.Draw();}
            lock(Lockobject)
            {
                visitorlist.RemoveAll(x=> x.AX == x.TX && x.AY == x.TY);
            }

        }
        static void Main(string[] args)
        {
            visitorlist = new List<CinemaVisitor>();
            Console.WriteLine("Melyik ID?");
            string cinemaid = Console.ReadLine();
            Console.Clear();
            DataReader r = new DataReader();
            List<Room> list = r.getRooms();
            RoomCreator rc = new RoomCreator();
            CancellationTokenSource source = new CancellationTokenSource();
            cinema = rc.CreateRoom(cinemaid, r);
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 1000;
            aTimer.Enabled = true; //Helyette lehet ==> aTimer.Start();
            ConsoleKeyInfo c; int left = 0, top = 0;
            do
            {
                Console.SetCursorPosition(left + 1, (top * 2) + 1);
                c = Console.ReadKey();
                if (c!=null)
                {
                    Console.CursorVisible = true;
                }
                switch(c.KeyChar)
                {
                    case ' ':
                        {
                            if (cinema[left,top]=='O')
                            {
                                cinema[left, top] = 'X';
                                CinemaVisitor cv = new CinemaVisitor(left, top, 0, 0, CinemaVisitor.VisitorType.In);
                                lock (Lockobject)
                                {
                                    visitorlist.Add(cv);
                                }
                                var task= CancellableTask(cv,source.Token);
                            }
                            else
                            {
                                cinema[left, top] = 'O';
                                CinemaVisitor cv = new CinemaVisitor(cinema.GetLength(1), cinema.GetLength(0), left + 1, top, CinemaVisitor.VisitorType.Out);
                                lock (Lockobject)
                                {
                                    visitorlist.Add(cv);
                                }
                                var task= CancellableTask(cv,source.Token);
                            }
                        }
                        break;
                    case 'w': top = top > 0 ? top - 1 : 0; break;
                    case 's': top = top >= cinema.GetLength(0) ? top : top + 1;break;
                    case 'a':left = left > 0 ? left - 1 : 0;break;
                    case 'd':left = left >= cinema.GetLength(1) ? left : left + 1;break;
                    default:break;
                }
            } while (c.Key != ConsoleKey.Escape);
            source.Cancel();
            source.Dispose();
            aTimer.Stop();
            Console.Clear();
            Statisztika.Stat_calc();
            Console.ReadLine();
        }
        public static void VisitorTask(CinemaVisitor cv,CancellationToken ct)
        {
            while (cv!=null)
            {
                if (ct.IsCancellationRequested)
                {
                    break;
                }
                cv.Move();
                System.Threading.Thread.Sleep(1000);
            }
        }
        public static Task CancellableTask(CinemaVisitor cv, CancellationToken ct)
        {
            return Task.Factory.StartNew(() => VisitorTask(cv, ct), ct);
        }
    }
}
