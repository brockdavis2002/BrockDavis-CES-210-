using System;
using System.Collections.Generic;
using System.Linq;

public class Room
{
    public string Name { get; private set; }
    private List<SmartDevice> devices;

    public Room(string name)
    {
        Name = name;
        devices = new List<SmartDevice>();
    }

    public void TurnOffAllDevices()
    {
        foreach (var device in devices)
        {
            device.TurnOff();
        }
    }

        public void TurnOnAllDevices()
    {
        foreach (var device in devices)
        {
            device.TurnOn();
        }
    }

    public void AddDevice(SmartDevice device)
    {
        devices.Add(device);
        Console.WriteLine($"{device.GetDeviceType()} '{device.Name}' added to room '{Name}'.");
    }

    public void RemoveDevice(SmartDevice device)
    {
        if (devices.Remove(device))
        {
            Console.WriteLine($"{device.GetDeviceType()} '{device.Name}' removed from room '{Name}'.");
        }
        else
        {
            Console.WriteLine($"{device.GetDeviceType()} '{device.Name}' not found in room '{Name}'.");
        }
    }

        public List<SmartDevice> GetDevices()
    {
        return devices;
    }

    public void ToggleAllDevices()
    {
        foreach (var device in devices)
        {
            if (device.IsOn())
            {
                device.TurnOff();
            }
            else
            {
                device.TurnOn();
            }
        }
    }

    public void ReportStatusOfAllDevices()
    {
        foreach (var device in devices)
        {
            Console.WriteLine($"{device.GetDeviceType()} '{device.Name}' is {(device.IsOn() ? "on" : "off")}.");
        }
    }

    public void ReportDevicesOn()
    {
        foreach (var device in devices)
        {
            if (device.IsOn())
            {
                Console.WriteLine($"{device.GetDeviceType()} '{device.Name}' is on in room '{Name}'.");
            }
        }
    }

    public List<SmartDevice> Devices
    {
        get { return devices; }
    }
}
