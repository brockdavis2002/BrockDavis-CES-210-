public class SmartLight : SmartDevice
{
    public SmartLight(string name) : base(name)
    {
    }

    public override string GetDeviceType()
    {
        return "Smart Light";
    }
}
