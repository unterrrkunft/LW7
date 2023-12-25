using System;
using System.Collections.Generic;
using System.Linq;

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

        if (restaurant == null)
        {
            throw new InvalidOperationException("Restaurant not found");
        }

        if (tableNumber < 0 || tableNumber >= restaurant.Tables.Count)
        {
            throw new ArgumentException("Invalid table number");
        }

        return restaurant.BookTable(date, tableNumber);
    }

    public void SortRestaurantsByAvailability(DateTime date)
    {
        Restaurants = Restaurants
            .OrderByDescending(restaurant => restaurant.CountAvailableTables(date))
            .ToList();
    }
}