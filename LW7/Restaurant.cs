using System;
using System.Collections.Generic;
using System.Linq;

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