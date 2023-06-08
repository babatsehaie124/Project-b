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
        // get data from HuidigeReservering.json
    }

    public void SaveAsCurrent()
    {
        //write to HuidigeReservering.json
    }
}