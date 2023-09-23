// File: Utils/TimeConversion.cs
public static class TimeConversion
{
    public static double ConvertTime(double value, string currentUnit, string desiredUnit)
    {
        TimeUnit currentUnitValue = (TimeUnit)Enum.Parse(typeof(TimeUnit), currentUnit, true);
        TimeUnit desiredUnitValue = (TimeUnit)Enum.Parse(typeof(TimeUnit), desiredUnit, true);

        double result = value;

        switch (currentUnitValue)
        {
            case TimeUnit.Days:
                result = UnitConversion.DaysToSeconds(value);
                break;
            case TimeUnit.Hours:
                result = UnitConversion.HoursToSeconds(value);
                break;
            case TimeUnit.Minutes:
                result = UnitConversion.MinutesToSeconds(value);
                break;
            case TimeUnit.Seconds:
                break;
        }

        switch (desiredUnitValue)
        {
            case TimeUnit.Days:
                result = UnitConversion.SecondsToDays(result);
                break;
            case TimeUnit.Hours:
                result = UnitConversion.SecondsToHours(result);
                break;
            case TimeUnit.Minutes:
                result = UnitConversion.SecondsToMinutes(result);
                break;
            case TimeUnit.Seconds:
                break;
        }

        return result;
    }
}