using System.Diagnostics.CodeAnalysis;
// using System.Runtime.CompilerServices;

// using MeasureDD;

// [assembly: InternalsVisibleTo("MeasureDD.Tests")]

namespace MeasureDD.Requests;

public class Request
{
  internal int _id;
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

  public static string Header()
  {
    return "ID\tName Requestor\tEmail Requestor\tAdress\t\tPostCode\tPlace\tCountry\tSail Club\tNumbersChecked\tMoneywillbesent";
  }

  public override string ToString()
  {
    return $"{this._id}\t{this._contact.Name}\t{this._contact.Email}\t{this._contact.Adress.Streetname}\t{this._contact.Adress.Housenumber}\t{this._contact.Adress.Postcode}\t{this._contact.Adress.Place}\t{this._contact.Adress.Country}\t{this._contact.Sailclub}\t{this._numbersAreChecked}\t{this._moneyWillBeSent}";
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

public static class RequestHandler
{
  public static Request ReadRequestString(string[] rl)
  {
    if (rl.Length != 11)
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

  public static void WriteRequest(Request rq, string path)
  {
    using (StreamWriter file = File.AppendText(path))
    {
      file.WriteLine(rq.ToString());
    }
  }

  public static IEnumerable<Request> GetRequests()
  {
    string? line;
    using (var reader = File.OpenText(Context.GetFilePath()))
    {
      while ((line = reader.ReadLine()) != null)
      {
        yield return ReadRequestString(line.Split('\t'));
      }
    }
  }
}
