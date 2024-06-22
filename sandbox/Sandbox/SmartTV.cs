public class SmartTV : SmartDevice
{
    public SmartTV(string name) : base(name)
    {
    }

    public override string GetDeviceType()
    {
        return "Smart TV";
    }
}
