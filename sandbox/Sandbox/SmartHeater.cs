public class SmartHeater : SmartDevice
{
    public SmartHeater(string name) : base(name)
    {
    }

    public override string GetDeviceType()
    {
        return "Smart Heater";
    }
}
