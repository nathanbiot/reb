namespace reb.Models;

public class Motor
{
    public DateTime AddedOn { get; set; }
    public DateTime? CompletedOn { get; set; }
    public string Brand { get; set; }
    public MotorNameplate Nameplate { get; set; }
    public int NotchesNumber { get; set; }
    public int WindingsNumber { get; set; }
    public float WireDiameter { get; set; }
    public float SheetMetalLength { get; set; }
    public float Bore { get; set; }
}

public class MotorNameplate
{
    public string SerialNumber { get; set; }
    public float Weight { get; set; }
    public int PhasesNumber { get; set; }    
    public List<MotorSetup> MotorSetups { get; set; }
}

public class MotorSetup
{
    public int Voltage { get; set; }
    public int Frequency { get; set; }
    public float PowerFactor { get; set; }
    public float Power { get; set; }
    public float Current { get; set; }
    public int Speed { get; set; }
    public ConnectionType ConnectionType { get; set; }
}

public enum ConnectionType
{
    Star,
    Delta
}