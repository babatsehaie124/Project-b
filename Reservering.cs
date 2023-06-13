using Newtonsoft.Json;
public class Reservering
{
    public int RoosterId;
    public List<string> Stoelen;
    public List<string> Snacks;

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
    }

    public void SaveAsCurrent()
    {
        Reservering data = new Reservering(RoosterId);
        data.Stoelen = Stoelen;
        data.Snacks = Snacks;

        string jsonData = JsonConvert.SerializeObject(data);
        File.WriteAllText("HuidigeReservering.json", jsonData);
    }
}