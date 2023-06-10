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
        // string jsonData = File.ReadAllText("HuidigeReservering.json");
        // List<Reservering> data = JsonConvert.DeserializeObject<List<Reservering>>(jsonData);
    }

    public void SaveAsCurrent()
    {
        // string jsonData = File.ReadAllText("HuidigeReservering.json");
        // List<dynamic> data = JsonConvert.DeserializeObject<List<dynamic>>(jsonData);

        //dynamic current
    }
}