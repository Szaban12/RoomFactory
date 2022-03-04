using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using RoomFactory;
//Tesztek:
//1.xml file kezelése
//          beolvasás
//          fájl felbontása
//                  szobák száma
//                  id ellenőrzés
//                  szélesség
//                  magasság
//          rossz id kezelése
//          létezik a room, dimenzió szám
namespace RoomFactoryTest
{
    [TestClass]
    public class RoomFactoryUnitTest1
    {

        [TestMethod]
        [ExpectedException(typeof (System.IO.FileNotFoundException))]
        public void XMLFileMissingTest()
        {
            DataReader dr = new DataReader();
            dr.getRooms();
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void CreateRoomOutoofIndex()
        {
            DataReader dr = new DataReader();
            RoomCreator rc = new RoomCreator();
            rc.CreateRoom("1",dr);
        }
        public void VariableViabilityTest_ID()
        {
            Room r = new Room(null, -20, -30);
            Assert.IsTrue(r.Id is null, "Id is null");
        }
        public void VariableViabilityTest_Height()
        {
            Room r = new Room(null, -20, -30);
            Assert.IsTrue(r.Height >= 0, "Height is negative or 0");
        }
        public void VariableViabilityTest_Width()
        {
            Room r = new Room(null, -20, -30);
            Assert.IsTrue(r.Width >= 0, "Width is negative or 0");
        }
    }
}
