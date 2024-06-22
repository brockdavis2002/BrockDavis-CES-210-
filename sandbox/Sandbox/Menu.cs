using System;
using System.Collections.Generic;
using System.Linq;

public class Menu
{
    private House house;

    public Menu()
    {
        house = new House(); // Initialize a new house

        // Sample data setup (can be replaced with user input or dynamic setup)
        Room livingRoom = new Room("Living Room");
        Room bedroom = new Room("Bedroom");

        SmartLight light1 = new SmartLight("Living Room Light 1");
        SmartLight light2 = new SmartLight("Living Room Light 2");
        SmartHeater heater = new SmartHeater("Bedroom Heater");
        SmartTV tv = new SmartTV("Living Room TV");

        livingRoom.AddDevice(light1);
        livingRoom.AddDevice(light2);
        livingRoom.AddDevice(tv);
        bedroom.AddDevice(heater);

        house.AddRoom(livingRoom);
        house.AddRoom(bedroom);
    }

    public void RunMenu()
    {
        while (true)
        {
            Console.Clear(); // Clear the console
            Console.WriteLine("===== Smart Home Menu =====");
            Console.WriteLine("1. Turn on/off all lights");
            Console.WriteLine("2. Turn on/off a device");
            Console.WriteLine("3. Turn on/off all devices in a room");
            Console.WriteLine("4. Report all items in a room and their status");
            Console.WriteLine("5. Report items that are on");
            Console.WriteLine("6. Report item that has been on the longest");
            Console.WriteLine("7. Add a device");
            Console.WriteLine("8. Remove a device");
            Console.WriteLine("9. Assign a device to a room");
            Console.WriteLine("0. Exit");
            Console.Write("Enter your choice: ");

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        TurnOnOffLights();
                        break;
                    case 2:
                        TurnOnOffDevice();
                        break;
                    case 3:
                        TurnOnOffAllDevicesInRoom();
                        break;
                    case 4:
                        ReportAllItemsInRoom();
                        break;
                    case 5:
                        ReportItemsOn();
                        break;
                    case 6:
                        ReportLongestOnItem();
                        break;
                    case 7:
                        AddDevice();
                        break;
                    case 8:
                        RemoveDevice();
                        break;
                    case 9:
                        AssignDeviceToRoom();
                        break;
                    case 0:
                        ExitProgram();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }

            Console.WriteLine(); // Blank line for separation
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }

    private void TurnOnOffLights()
    {
        Console.Clear();
        Console.WriteLine("===== Turn On/Off Lights =====");
        List<SmartDevice> lights = house.GetDevicesByType("Smart Light");

        if (lights.Any())
        {
            Console.WriteLine("Select an option:");
            Console.WriteLine("1. Turn on all lights");
            Console.WriteLine("2. Turn off all lights");
            Console.Write("Enter your choice: ");

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        foreach (var light in lights)
                        {
                            if (!light.IsOn())
                            {
                                light.TurnOn();
                            }
                        }
                        Console.WriteLine("All lights turned on.");
                        break;
                    case 2:
                        foreach (var light in lights)
                        {
                            if (light.IsOn())
                            {
                                light.TurnOff();
                            }
                        }
                        Console.WriteLine("All lights turned off.");
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
        }
        else
        {
            Console.WriteLine("No smart lights found.");
        }
    }

private void TurnOnOffDevice()
{
    Console.Clear();
    Console.WriteLine("===== Turn On/Off a Device =====");

    // List all devices to select from
    List<SmartDevice> allDevices = house.GetAllDevices();
    if (allDevices.Any())
    {
        Console.WriteLine("Select a device to turn on/off:");
        for (int i = 0; i < allDevices.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {allDevices[i].Name} ({allDevices[i].GetDeviceType()})");
        }

        Console.Write("Enter your choice: ");
        int deviceChoice;
        if (int.TryParse(Console.ReadLine(), out deviceChoice) && deviceChoice >= 1 && deviceChoice <= allDevices.Count)
        {
            SmartDevice selectedDevice = allDevices[deviceChoice - 1];

            Console.WriteLine($"Selected device: {selectedDevice.Name} ({selectedDevice.GetDeviceType()})");
            Console.WriteLine("Select an action:");
            Console.WriteLine("1. Turn On");
            Console.WriteLine("2. Turn Off");
            Console.Write("Enter your choice: ");

            int actionChoice;
            if (int.TryParse(Console.ReadLine(), out actionChoice))
            {
                switch (actionChoice)
                {
                    case 1:
                        if (!selectedDevice.IsOn())
                        {
                            selectedDevice.TurnOn();
                            Console.WriteLine($"{selectedDevice.Name} has been turned on.");
                        }
                        else
                        {
                            Console.WriteLine($"{selectedDevice.Name} is already on.");
                        }
                        break;
                    case 2:
                        if (selectedDevice.IsOn())
                        {
                            selectedDevice.TurnOff();
                            Console.WriteLine($"{selectedDevice.Name} has been turned off.");
                        }
                        else
                        {
                            Console.WriteLine($"{selectedDevice.Name} is already off.");
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid action choice.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input or device selection.");
        }
    }
    else
    {
        Console.WriteLine("No devices found in the house.");
    }
}
private void TurnOnOffAllDevicesInRoom()
{
    Console.Clear();
    Console.WriteLine("===== Turn On/Off All Devices in a Room =====");

    List<Room> rooms = house.Rooms;
    if (rooms.Any())
    {
        Console.WriteLine("Select a room to turn on/off all devices:");
        for (int i = 0; i < rooms.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {rooms[i].Name}");
        }

        Console.Write("Enter your choice: ");
        int roomChoice;
        if (int.TryParse(Console.ReadLine(), out roomChoice) && roomChoice >= 1 && roomChoice <= rooms.Count)
        {
            Room selectedRoom = rooms[roomChoice - 1];

            Console.WriteLine($"Selected room: {selectedRoom.Name}");
            Console.WriteLine("Select an action:");
            Console.WriteLine("1. Turn On all devices in the room");
            Console.WriteLine("2. Turn Off all devices in the room");
            Console.Write("Enter your choice: ");

            int actionChoice;
            if (int.TryParse(Console.ReadLine(), out actionChoice))
            {
                switch (actionChoice)
                {
                    case 1:
                        selectedRoom.TurnOnAllDevices();
                        Console.WriteLine($"All devices in '{selectedRoom.Name}' have been turned on.");
                        break;
                    case 2:
                        selectedRoom.TurnOffAllDevices();
                        Console.WriteLine($"All devices in '{selectedRoom.Name}' have been turned off.");
                        break;
                    default:
                        Console.WriteLine("Invalid action choice.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input or room selection.");
        }
    }
    else
    {
        Console.WriteLine("No rooms found in the house.");
    }
}


    private void ReportAllItemsInRoom()
    {
        Console.Clear();
        Console.WriteLine("===== Report All Items in a Room =====");

        List<Room> rooms = house.Rooms;
        if (rooms.Any())
        {
            Console.WriteLine("Select a room to report:");
            for (int i = 0; i < rooms.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {rooms[i].Name}");
            }

            Console.Write("Enter your choice: ");
            int roomChoice;
            if (int.TryParse(Console.ReadLine(), out roomChoice) && roomChoice >= 1 && roomChoice <= rooms.Count)
            {
                Room selectedRoom = rooms[roomChoice - 1];
                Console.WriteLine($"===== Devices in '{selectedRoom.Name}' =====");
                selectedRoom.ReportStatusOfAllDevices();
            }
            else
            {
                Console.WriteLine("Invalid input or room selection.");
            }
        }
        else
        {
            Console.WriteLine("No rooms found in the house.");
        }
    }

    private void ReportItemsOn()
    {
        Console.Clear();
        Console.WriteLine("===== Devices that are ON =====");
        foreach (var room in house.Rooms)
        {
            room.ReportDevicesOn();
        }
    }

    private void ReportLongestOnItem()
    {
        Console.Clear();
        TimeSpan maxDuration = TimeSpan.Zero;
        SmartDevice longestOnDevice = null;

        foreach (var room in house.Rooms)
        {
            foreach (var device in room.Devices)
            {
                if (device.IsOn())
                {
                    TimeSpan duration = DateTime.Now - device.TurnedOnTime;
                    if (duration > maxDuration)
                    {
                        maxDuration = duration;
                        longestOnDevice = device;
                    }
                }
            }
        }

        if (longestOnDevice != null)
        {
            Console.WriteLine($"Longest on device: {longestOnDevice.Name} ({longestOnDevice.GetDeviceType()})");
            Console.WriteLine($"Duration: {maxDuration}");
        }
        else
        {
            Console.WriteLine("No devices are currently on.");
        }
    }

private void AddDevice()
{
    Console.Clear();
    Console.WriteLine("===== Add a Device =====");
    Console.WriteLine("Select device category:");
    Console.WriteLine("1. Smart Light");
    Console.WriteLine("2. Smart Heater");
    Console.WriteLine("3. Smart TV");
    Console.Write("Enter your choice: ");

    int categoryChoice;
    if (int.TryParse(Console.ReadLine(), out categoryChoice))
    {
        string deviceType = "";
        switch (categoryChoice)
        {
            case 1:
                deviceType = "Smart Light";
                break;
            case 2:
                deviceType = "Smart Heater";
                break;
            case 3:
                deviceType = "Smart TV";
                break;
            default:
                Console.WriteLine("Invalid choice.");
                return;
        }

        Console.Write("Enter the name of the device: ");
        string deviceName = Console.ReadLine();

        SmartDevice newDevice = null;
        switch (deviceType)
        {
            case "Smart Light":
                newDevice = new SmartLight(deviceName);
                break;
            case "Smart Heater":
                newDevice = new SmartHeater(deviceName);
                break;
            case "Smart TV":
                newDevice = new SmartTV(deviceName);
                break;
        }

        if (newDevice != null)
        {
            Console.WriteLine("Select a room to add the device to:");
            List<Room> rooms = house.Rooms;
            for (int i = 0; i < rooms.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {rooms[i].Name}");
            }
            Console.WriteLine($"{rooms.Count + 1}. Create a New Room");

            Console.Write("Enter your choice: ");
            int roomChoice;
            if (int.TryParse(Console.ReadLine(), out roomChoice) && roomChoice >= 1 && roomChoice <= rooms.Count + 1)
            {
                Room selectedRoom;
                if (roomChoice <= rooms.Count)
                {
                    selectedRoom = rooms[roomChoice - 1];
                }
                else
                {
                    Console.Write("Enter the name of the new room: ");
                    string newRoomName = Console.ReadLine();
                    selectedRoom = new Room(newRoomName);
                    house.AddRoom(selectedRoom);
                }

                selectedRoom.AddDevice(newDevice);
                Console.WriteLine($"{deviceType} '{deviceName}' added to room '{selectedRoom.Name}'.");
            }
            else
            {
                Console.WriteLine("Invalid input or room selection.");
            }
        }
    }
    else
    {
        Console.WriteLine("Invalid input. Please enter a number.");
    }
}
private void RemoveDevice()
{
    Console.Clear();
    Console.WriteLine("===== Remove a Device =====");

    // Get all devices in the house
    List<SmartDevice> allDevices = house.GetAllDevices();

    if (allDevices.Any())
    {
        Console.WriteLine("Select a device to remove:");
        for (int i = 0; i < allDevices.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {allDevices[i].Name} ({allDevices[i].GetDeviceType()})");
        }

        Console.Write("Enter your choice: ");
        int deviceChoice;
        if (int.TryParse(Console.ReadLine(), out deviceChoice) && deviceChoice >= 1 && deviceChoice <= allDevices.Count)
        {
            SmartDevice deviceToRemove = allDevices[deviceChoice - 1];

            // Find the room containing the device
            Room containingRoom = house.FindRoomWithDevice(deviceToRemove);

            if (containingRoom != null)
            {
                containingRoom.RemoveDevice(deviceToRemove);
                Console.WriteLine($"{deviceToRemove.Name} has been removed from room '{containingRoom.Name}'.");
            }
            else
            {
                Console.WriteLine($"Error: Device '{deviceToRemove.Name}' not found in any room.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input or device selection.");
        }
    }
    else
    {
        Console.WriteLine("No devices found in the house.");
    }
}



    private void AssignDeviceToRoom()
    {
        Console.Write("Select device to assign:");
        foreach (var device in house.GetAllDevices())
        {
            Console.WriteLine($"- {device.Name} ({device.GetDeviceType()})");
        }
        Console.Write("Enter device name: ");
        string deviceName = Console.ReadLine();

        SmartDevice deviceToAssign = house.GetAllDevices().FirstOrDefault(d => d.Name.Equals(deviceName, StringComparison.OrdinalIgnoreCase));

        if (deviceToAssign != null)
        {
            Console.WriteLine("Select room to assign the device to:");
            foreach (var room in house.Rooms)
            {
                Console.WriteLine($"- {room.Name}");
            }
            Console.Write("Enter room name: ");
            string roomName = Console.ReadLine();

            Room roomToAssign = house.GetRoomByName(roomName);

            if (roomToAssign != null)
            {
                roomToAssign.AddDevice(deviceToAssign);
                Console.WriteLine($"{deviceToAssign.GetDeviceType()} '{deviceToAssign.Name}' assigned to room '{roomToAssign.Name}'.");
            }
            else
            {
                Console.WriteLine($"Room '{roomName}' not found.");
            }
        }
        else
        {
            Console.WriteLine($"Device '{deviceName}' not found.");
        }
    }

    private void ExitProgram()
    {
        Console.WriteLine("Exiting...");
        Environment.Exit(0);
    }
}
