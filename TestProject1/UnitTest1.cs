namespace ConsoleAppRestaurantTableReservationManager
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void BookTable_SuccessfullyBooksTable_ReturnsTrue()
        {
            TableReservationManager manager = new TableReservationManager();
            manager.AddRestaurant("A", 10);

            bool result = manager.BookTable("A", new DateTime(2023, 12, 25), 3);

            Assert.IsTrue(result);
        }
        [Test]
        public void BookTable_TableAlreadyBooked_ReturnsFalse()
        {
            TableReservationManager manager = new TableReservationManager();
            manager.AddRestaurant("A", 10);

            manager.BookTable("A", new DateTime(2023, 12, 25), 3);
            bool result = manager.BookTable("A", new DateTime(2023, 12, 25), 3);

            Assert.IsFalse(result);
        }

        [Test]
        public void FindAllFreeTables_ReturnsAvailableTables()
        {
            TableReservationManager manager = new TableReservationManager();
            manager.AddRestaurant("A", 10);
            manager.BookTable("A", new DateTime(2023, 12, 25), 3);

            var freeTables = manager.FindAllFreeTables(new DateTime(2023, 12, 25));

            Assert.AreEqual(9, freeTables.Count); // Only one table is booked, so 9 should be available
        }
    }
}