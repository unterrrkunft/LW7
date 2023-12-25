using System;
using System.Collections.Generic;
using System.Linq;

public class TableReservationApp
{
    static void Main(string[] args)
    {
        TableReservationManager manager = new TableReservationManager();
        manager.AddRestaurant("A", 10);
        manager.AddRestaurant("B", 5);

        Console.WriteLine(manager.BookTable("A", new DateTime(2023, 12, 25), 3)); // True
        Console.WriteLine(manager.BookTable("A", new DateTime(2023, 12, 25), 3)); // False
    }
}

public class TableReservationManager
{
    public List<Restaurant> Restaurants { get; private set; }

    public TableReservationManager()
    {
        Restaurants = new List<Restaurant>();
    }

    public void AddRestaurant(string name, int tableCount)
    {
        Restaurant restaurant = new Restaurant(name, tableCount);
        Restaurants.Add(restaurant);
    }

    public List<string> FindAllFreeTables(DateTime date)
    {
        return Restaurants
            .SelectMany(restaurant => restaurant.GetAvailableTables(date))
            .ToList();
    }

    public bool BookTable(string restaurantName, DateTime date, int tableNumber)
    {
        var restaurant = Restaurants.FirstOrDefault(r => r.Name == restaurantName);

        if (restaurant != null)
        {
            return restaurant.BookTable(date, tableNumber);
        }

        throw new InvalidOperationException("Restaurant not found");
    }

    public void SortRestaurantsByAvailability(DateTime date)
    {
        Restaurants = Restaurants
            .OrderByDescending(restaurant => restaurant.CountAvailableTables(date))
            .ToList();
    }
}

public class Restaurant
{
    public string Name { get; private set; }
    public List<RestaurantTable> Tables { get; private set; }

    public Restaurant(string name, int tableCount)
    {
        Name = name;
        Tables = Enumerable.Range(1, tableCount)
            .Select(i => new RestaurantTable())
            .ToList();
    }

    public List<string> GetAvailableTables(DateTime date)
    {
        return Tables
            .Where(table => !table.IsBooked(date))
            .Select(table => $"{Name} - Table {Tables.IndexOf(table) + 1}")
            .ToList();
    }

    public bool BookTable(DateTime date, int tableNumber)
    {
        if (tableNumber < 0 || tableNumber >= Tables.Count)
        {
            throw new ArgumentException("Invalid table number");
        }

        return Tables[tableNumber].Book(date);
    }

    public int CountAvailableTables(DateTime date)
    {
        return Tables.Count(table => !table.IsBooked(date));
    }
}

public class RestaurantTable
{
    private List<DateTime> BookedDates;

    public RestaurantTable()
    {
        BookedDates = new List<DateTime>();
    }

    public bool Book(DateTime date)
    {
        if (BookedDates.Contains(date))
        {
            return false;
        }

        BookedDates.Add(date);
        return true;
    }

    public bool IsBooked(DateTime date)
    {
        return BookedDates.Contains(date);
    }
}