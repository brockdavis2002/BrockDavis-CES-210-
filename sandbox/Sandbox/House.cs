using System.Collections.Generic;
using System.Linq;

public class House
{
    private List<Room> rooms;

    public House()
    {
        rooms = new List<Room>();
    }

    public void AddRoom(Room room)
    {
        rooms.Add(room);
    }

    public List<Room> Rooms
    {
        get { return rooms; }
    }

    public Room GetRoomByName(string name)
    {
        return rooms.FirstOrDefault(r => r.Name.Equals(name, System.StringComparison.OrdinalIgnoreCase));
    }

    public List<SmartDevice> GetAllDevices()
    {
        List<SmartDevice> allDevices = new List<SmartDevice>();
        foreach (var room in rooms)
        {
            allDevices.AddRange(room.Devices);
        }
        return allDevices;
    }

    public List<SmartDevice> GetDevicesByType(string deviceType)
    {
        List<SmartDevice> devicesOfType = new List<SmartDevice>();
        foreach (var room in rooms)
        {
            devicesOfType.AddRange(room.Devices.Where(d => d.GetDeviceType().Equals(deviceType, System.StringComparison.OrdinalIgnoreCase)));
        }
        return devicesOfType;
    }
    public Room FindRoomWithDevice(SmartDevice device)
    {
        foreach (var room in rooms)
        {
            if (room.GetDevices().Contains(device))
            {
                return room;
            }
        }
        return null; // Device not found in any room
    }
}