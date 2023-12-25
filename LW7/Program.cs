using System;
using System.Collections.Generic;

// Main Application Class
public class TableReservationApp
{
    static void Main(string[] args)
    {
        TableReservationManager m = new TableReservationManager();
        m.AddRestaurant("A", 10);
        m.AddRestaurant("B", 5);

        Console.WriteLine(m.BookTable("A", new DateTime(2023, 12, 25), 3)); // True
        Console.WriteLine(m.BookTable("A", new DateTime(2023, 12, 25), 3)); // False
    }
}

// Reservation Manager Class
public class TableReservationManager
{
    public List<Restaurant> Restaurants;

    public TableReservationManager()
    {
        Restaurants = new List<Restaurant>();
    }

    public void AddRestaurant(string name, int tableCount)
    {
        try
        {
            Restaurant restaurant = new Restaurant();
            restaurant.Name = name;
            restaurant.Tables = new RestaurantTable[tableCount];

            for (int i = 0; i < tableCount; i++)
            {
                restaurant.Tables[i] = new RestaurantTable();
            }

            Restaurants.Add(restaurant);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error");
        }
    }

    public List<string> FindAllFreeTables(DateTime date)
    {
        try
        {
            List<string> free = new List<string>();

            foreach (var restaurant in Restaurants)
            {
                for (int i = 0; i < restaurant.Tables.Length; i++)
                {
                    if (!restaurant.Tables[i].IsBooked(date))
                    {
                        free.Add($"{restaurant.Name} - Table {i + 1}");
                    }
                }
            }

            return free;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error");
            return new List<string>();
        }
    }

    public bool BookTable(string restaurantName, DateTime date, int tableNumber)
    {
        foreach (var restaurant in Restaurants)
        {
            if (restaurant.Name == restaurantName)
            {
                if (tableNumber < 0 || tableNumber >= restaurant.Tables.Length)
                {
                    throw new Exception(null); // Invalid table number
                }

                return restaurant.Tables[tableNumber].Book(date);
            }
        }

        throw new Exception(null); // Restaurant not found
    }

    public void SortRestaurantsByAvailability(DateTime date)
    {
        try
        {
            bool swapped;
            do
            {
                swapped = false;
                for (int i = 0; i < Restaurants.Count - 1; i++)
                {
                    int avTc = CountAvailableTables(Restaurants[i], date);
                    int avTn = CountAvailableTables(Restaurants[i + 1], date);

                    if (avTc < avTn)
                    {
                        // Swap restaurants
                        var temp = Restaurants[i];
                        Restaurants[i] = Restaurants[i + 1];
                        Restaurants[i + 1] = temp;
                        swapped = true;
                    }
                }
            } while (swapped);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error");
        }
    }

    public int CountAvailableTables(Restaurant restaurant, DateTime date)
    {
        try
        {
            int count = 0;
            foreach (var table in restaurant.Tables)
            {
                if (!table.IsBooked(date))
                {
                    count++;
                }
            }
            return count;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error");
            return 0;
        }
    }
}

// Restaurant Class
public class Restaurant
{
    public string Name;
    public RestaurantTable[] Tables;
}

// Table Class
public class RestaurantTable
{
    private List<DateTime> BookedDates;

    public RestaurantTable()
    {
        BookedDates = new List<DateTime>();
    }

    public bool Book(DateTime date)
    {
        try
        {
            if (BookedDates.Contains(date))
            {
                return false;
            }

            BookedDates.Add(date);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error");
            return false;
        }
    }

    public bool IsBooked(DateTime date)
    {
        return BookedDates.Contains(date);
    }
}