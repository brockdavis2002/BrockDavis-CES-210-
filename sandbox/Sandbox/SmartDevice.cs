using System;

public abstract class SmartDevice
{
    public string Name { get; private set; }
    private bool isOn;
    private DateTime turnedOnTime;

    public SmartDevice(string name)
    {
        Name = name;
        isOn = false;
    }

    public bool IsOn()
    {
        return isOn;
    }

    public virtual void TurnOn()
    {
        if (!isOn)
        {
            isOn = true;
            turnedOnTime = DateTime.Now;
            Console.WriteLine($"{Name} turned on.");
        }
        else
        {
            Console.WriteLine($"{Name} is already on.");
        }
    }

    public virtual void TurnOff()
    {
        if (isOn)
        {
            isOn = false;
            TimeSpan onDuration = DateTime.Now - turnedOnTime;
            Console.WriteLine($"{Name} turned off. It was on for {onDuration.TotalMinutes} minutes.");
        }
        else
        {
            Console.WriteLine($"{Name} is already off.");
        }
    }

    public DateTime TurnedOnTime
    {
        get { return turnedOnTime; }
    }

    public abstract string GetDeviceType();
}
