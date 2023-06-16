using Newtonsoft.Json;
public class Reservering
{
    public string Emailaddress { get; set; }
    public int RoosterId { get; set; }
    public List<string> Stoelen { get; set; }
    // public Dictionary<string, int> Snacks { get; set; }
    public List<SnackModel> Snacks { get; set; }

    public Reservering(int roosterId)
    {
        RoosterId = roosterId;
        Stoelen = new();
        Snacks = new();
    }

    public void LoadFromCurrent()
    {
        string jsonData = File.ReadAllText("HuidigeReservering.json");
        Reservering data = JsonConvert.DeserializeObject<Reservering>(jsonData);
        RoosterId = data.RoosterId;
        Stoelen = data.Stoelen;
        Snacks = data.Snacks;
        Emailaddress = data.Emailaddress;
    }

    public void SaveAsCurrent()
    {
        string jsonData = JsonConvert.SerializeObject(this);
        File.WriteAllText("HuidigeReservering.json", jsonData);
    }

    public void Clear()
    {
        RoosterId = 0;
        Stoelen = new();
        Snacks = new();
        string jsonData = JsonConvert.SerializeObject(this);
        File.WriteAllText("HuidigeReservering.json", jsonData);
    }
}