using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoomFactory;

namespace Cinema
{
    class Program
    {
        static void Main(string[] args)
        {
            Room r = new Room();
            DataReader dr = new DataReader();
            dr.getRooms();
            
        }
    }
}
