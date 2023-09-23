// File: Utils/UnitConverters.cs
public static class UnitConversion
{
    public static double HoursToMinutes(double hours) 
    {
        return hours * 60;
    }
    public static double HoursToSeconds(double hours) 
    {
        return hours * 3600;
    }
    public static double HoursToDays(double hours) 
    {
        return hours / 24;
    }
    public static double MinutesToHours(double minutes) 
    {
        return minutes / 60;
    }
    public static double MinutesToSeconds(double minutes) 
    {
        return minutes * 60;
    }
    public static double MinutesToDays(double minutes) 
    {
        return minutes / 1440;
    }
    public static double SecondsToHours(double seconds) 
    {
        return seconds / 3600;
    }
    public static double SecondsToMinutes(double seconds) 
    {
        return seconds / 60;
    }
    public static double SecondsToDays(double seconds) 
    {
        return seconds / 86400;
    }
    public static double DaysToHours(double days) 
    {
        return days * 24;
    }
    public static double DaysToMinutes(double days) 
    {
        return days * 1440;
    }
    public static double DaysToSeconds(double days) 
    {
        return days * 86400;
    }
}