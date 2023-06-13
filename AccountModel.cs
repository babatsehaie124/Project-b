using System.Text.Json.Serialization;

class AccountModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("Email")]
    public string Email { get; set; }

    [JsonPropertyName("Wachtwoord")]
    public string Wachtwoord { get; set; }

    [JsonPropertyName("fName")]
    public string FName { get; set; }

    [JsonPropertyName("lName")]
    public string LName { get; set; }

    public AccountModel(int id, string email, string wachtwoord, string fName, string lName)
    {
        Id = id;
        Email = email;
        Wachtwoord = wachtwoord;
        FName = fName;
        LName = lName;
    }
}