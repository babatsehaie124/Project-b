using System.Text.Json.Serialization;


class AccountModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("wachtwoord")]
    public string Wachtwoord { get; set; }

    [JsonPropertyName("fName")]
    public string fName { get; set; }
    [JsonPropertyName("lName")]
    public string lName { get; set; }

    public AccountModel(int id, string emailAddress, string wachtwoord, string fname, string lname)
    {
        Id = id;
        Email = emailAddress;
        Wachtwoord = wachtwoord;
        fName = fname;
        lName = lname;
    }

}




