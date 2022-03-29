using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RoomFactory
{   
    /// <summary>
    /// This is the room class, it contains a string for an id and width and height as integers.
    /// </summary>
    public class Room
    {
        ///text Id string of a room
        public string Id { get; set; }
        ///text Width of a room as integer
        public int Width { get; set; }
        ///text Height of a room as integer
        public int Height { get; set; }
        /// <summary>
        /// Empty constructor
        /// </summary>
        public Room()
        {

        }
        /// <summary>
        ///  Constructor with params for Room class
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="_width"></param>
        /// <param name="_height"></param>
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
        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of Rooms</returns>
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
        public char[,] CreateRoom(string RoomId,IDataReader dataSource)
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
