using System.Diagnostics.CodeAnalysis;

namespace CertificateRequest;

static class RequestHandler
{
  class Request
  {
    int _id;
    public Contact _contact;
    public bool _numbersAreChecked;
    public bool _moneyWillBeSent;

    public Request(int id, Contact contact, bool numbersChecked, bool moneyWillBeSent)
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
    using (var reader = File.OpenText(Context.Context.filePath))
    {
      while ((line = reader.ReadLine()) != null)    
      {
        yield return ReadRequest(line.Split('\t'));
      }
    }
  }

  static Request ReadRequest(string[] rl)
  {
    if (rl.Length != 10)
    {
      throw new ArgumentException("Number of fields in the read input is incorrect.", nameof(rl));
    }
    
    int id;
    int housenumber;
    int postcode;

    try 
    {
      id = Convert.ToInt32(rl[0]);
      housenumber = Convert.ToInt32(rl[4]);
      postcode = Convert.ToInt32(rl[5]);
    }
    catch (FormatException)
    {
      Console.WriteLine("Input string is not a sequence of digits.");
      goto Found;
    }
    catch (OverflowException)
    {
      Console.WriteLine("The number cannot fit in an Int32");
      goto Found;
    }

    string name = rl[1];
    string email = rl[2];
    
    string streetname = rl[3];
    string? place = rl[6] != "" ? rl[6] : null;
    string? country = rl[7] != "" ? rl[7] : null;

    string sailclub = rl[8];

    bool numbersChecked = false;
    bool moneyWillBeSent = false;

    try
    {
      numbersChecked = Convert.ToBoolean(rl[9]);
      moneyWillBeSent = Convert.ToBoolean(rl[10]);
    }
    catch (FormatException)
    {
      Console.WriteLine("Unable to convert input to boolean.", (rl[9], rl[10]));
      goto Found;
    }

    Adress adress = new(streetname, housenumber, postcode, place, country);
    Contact contact = new(name, email, adress, sailclub);

    return new Request(id, contact, numbersChecked, moneyWillBeSent);

  Found:
    throw new ArgumentException("Input data for Request is corrupted.", nameof(rl));
  }
}

public record struct Contact
{
  public required string Name { get; set; }
  public required string Email { get; set; }
  public required Adress Adress { get; set; } 
  public required string Sailclub;

  [SetsRequiredMembers]
  public Contact(string name, string email, Adress adress, string sailclub)
  {
    this.Name = name;
    this.Email = email;
    this.Adress = adress;
    this.Sailclub = sailclub;
  }
}

public record struct Adress
{
  public required string Streetname;
  public required int Housenumber;
  public required int Postcode;
  public string? Place;
  public string? Country;

  [SetsRequiredMembers]
  public Adress(string streetname, int housenumber, int postcode, string? place = null, string? country = null)
  {
    this.Streetname = streetname;
    this.Housenumber = housenumber;
    this.Postcode = postcode;
    this.Place = place;
    this.Country = country;
  }

}

