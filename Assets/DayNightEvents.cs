using System;

public static class DayNightEvents
{
    public static Action OnNightStart;
    public static Action OnDayStart;

    public static void NightStarted()
    {
        OnNightStart?.Invoke();
    }

    public static void DayStarted()
    {
        OnDayStart?.Invoke();
    }
}