using System;
using System.Collections.Generic;

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