using Newtonsoft.Json;
public class Reservering
{
    public string Emailaddress;
    public int RoosterId;
    public List<string> Stoelen;
    public Dictionary<string, int> Snacks;

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
}