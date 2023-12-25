using System;

public class TableReservationApp
{
    static void Main(string[] args)
    {
        try
        {
            TableReservationManager manager = new TableReservationManager();
            manager.AddRestaurant("A", 10);
            manager.AddRestaurant("B", 5);

            Console.WriteLine(manager.BookTable("A", new DateTime(2023, 12, 25), 3)); // True
            Console.WriteLine(manager.BookTable("A", new DateTime(2023, 12, 25), 3)); // False
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}