namespace CertificateRequest;

static class RequestHandler
{
  class Request
  {
    int _id;
    public Contact _contact;
    public bool _numbersAreChecked;
    public bool _moneyWillBeSent;

    Request(int id, Contact contact, bool numbersChecked, bool moneyWillBeSent)
    {
      _id = id;
      _contact = contact;
      _numbersAreChecked = numbersChecked;
      _moneyWillBeSent = moneyWillBeSent;
    }
  }

  static IEnumerable<Request> GetRequests()
  {
    string? line;
    using (var reader = File.OpenText(file))
    {
      while ((line = reader.ReadLine()) != null)    
      {
        yield return ReadRequest(line.Split('\t'));
      }
    }
  }

  static Request ReadRequest(string[] rl)
  {
    return new Request(rl[0], new Contact(rl[1], rl[2], new Adress(rl[3], rl[4], rl[])))
    

  }
}

public record struct Contact
{
  public required string Name { get; set; }
  public required string Email { get; set; }
  public required Adress Adress { get; set; } 
  public required string Sailclub;

  public Contact(string name, string email, Adress adress, string sailclub)
  {
    Name = name;
    Email = email;
    Adress = adress;
    Sailclub = sailclub;
  }
}

public record struct Adress
{
  public required string Streetname;
  public required int Housenumber;
  public required int Postcode;
  public string? Place;
  public string? Country;

  public Adress(string streetname, int housenumber, int postcode, string? place = null, string? country = null)
  {
    Streetname = streetname;
    Housenumber = housenumber;
    Postcode = postcode;
    Place = place;
    Country = country;
  }

}

