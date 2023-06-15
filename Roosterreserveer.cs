public class  Roosterreserveer
{
    public static int Zaal { get; set; }
    public static int RoosterId {get; set;}
    public Roosterreserveer(int zaal, int Roosterid)
    {
        Zaal = zaal;
        RoosterId = Roosterid;
    }
    
    public static void Reserve(bool user, int RoosterId)
    {
        if (Zaal == 1)
        {
            ReserveringsManager.Reserveren(user, RoosterId);
        }
        else if (Zaal == 2)
        {
            ReserveringsManagerZaal2.Reserveren(user, RoosterId);
        }
        else if (Zaal == 3)
        {
            ReserveringsManagerZaal3.Reserveren(user, RoosterId);
        }
    }
}
