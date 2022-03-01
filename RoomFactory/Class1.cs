using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RoomFactory
{
    public class Room
    {
        public string Id { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Room()
        {

        }
        public Room(string _id, int _width, int _height)
        {
            this.Id = _id;
            this.Width = _width;
            this.Height = _height;
        }
    }
    public interface IDataReader
    {
        List<Room> getRooms();
    }
    public class DataReader:IDataReader
    {
        public List<Room> getRooms()
        {
            List<Room> rooms = new List<Room>();
            XElement xdoc = XElement.Load("moziterem.xml");
            var items = from item in xdoc.Descendants("oneRoom")
                        select new Room
                        {
                            Id = (string)item.Attribute("id"),
                            Width = Convert.ToInt32(item.Element("width").Value),
                            Height = Convert.ToInt32(item.Element("height").Value)

                        };
            rooms = items.ToList<Room>();
            return rooms;
        }
    }
    public class RoomCreator
    {
        public static char[,] CreateRoom(string RoomId,IDataReader dataSource)
        {
            Room r = dataSource.getRooms().Find(room => RoomId == room.Id);
            char[,] cin = new char[r.Width, r.Height];
            for (int i = 0; i < cin.GetLength(0); i++)
            {
                for (int j = 0; j < cin.GetLength(1); j++)
                {
                    cin[i, j] ='0';
                }
            }
            return cin;
        }
    }
}
