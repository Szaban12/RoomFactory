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
        /// <summary>
        /// Test to see if the used .xml is missing
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof (System.IO.FileNotFoundException))]
        public void XMLFileMissingTest()
        {
            DataReader dr = new DataReader();
            dr.getRooms();
        }
        /// <summary>
        /// Tests if the user is trying to index out of array
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void CreateRoomOutoofIndex()
        {
            DataReader dr = new DataReader();
            RoomCreator rc = new RoomCreator();
            rc.CreateRoom("1",dr);
        }
        /// <summary>
        /// Test to see if ID is null
        /// </summary>
        public void VariableViabilityTest_ID()
        {
            Room r = new Room(null, -20, -30);
            Assert.IsTrue(r.Id is null, "Id is null");
        }
        /// <summary>
        /// Testing if the height is not 0 or negative
        /// </summary>
        public void VariableViabilityTest_Height()
        {
            Room r = new Room(null, -20, -30);
            Assert.IsTrue(r.Height >= 0, "Height is negative or 0");
        }
        /// <summary>
        /// Testing if the width is not 0 or negative
        /// </summary>
        public void VariableViabilityTest_Width()
        {
            Room r = new Room(null, -20, -30);
            Assert.IsTrue(r.Width >= 0, "Width is negative or 0");
        }
    }
}
