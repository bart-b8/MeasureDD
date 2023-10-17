using MeasureDD.Requests;
using MeasureDD;

namespace CertificateRequest.Tests;

public class UnitTest1
{
    [Fact]
    public void PassingTest()
    {
      Assert.Equal(4, Add(2,2));
    }
    
    [Fact]
    public void FailingTest()
    {
      Assert.NotEqual(5, Add(2,2));
    }

    int Add(int x, int y)
    {
      return x + y;
    }
}

public class UnitTest_ReadRequests
{
  static string input1 =  "1\tBart Decoutere\temail@email.email\tAdress Street\t42\t3232\tCity\tCountry\tHZC\ttrue\ttrue";
  static string input2 =  "1\tBart Decoutere\temail@email.email\tAdress Street\t42\t3232\t\t\tHZC\ttrue\ttrue";
  static Adress adress1 = new("Adress Street", 42, 3232, "City", "Country");
  static Adress adress2 = new("Adress Street", 42, 3232, null, null);
  static Contact contact1 = new("Bart Decoutere", "email@email.email", adress1, "HZC");
  static Contact contact2 = new("Bart Decoutere", "email@email.email", adress2, "HZC");
  static Request rq = new(1, contact1, true, true);
  
  [Fact]
  public void ChecksInput_InputCount10()
  {
    string[] rl = {"1", "Bart Decoutere"};
    var wasExceptionReceived = Record.Exception(() => RequestHandler.ReadRequestString(rl));
    Assert.IsType<ArgumentException>(wasExceptionReceived);
  }

  [Fact]
  public void CheckUse_RequestBasicWithOptional()
  {
    Assert.Equal(1, rq._id);
    Assert.Equal(contact1, rq._contact);
    Assert.Equal("Bart Decoutere", rq._contact.Name);
    Assert.Equal("email@email.email", rq._contact.Email);
    Assert.Equal(42, rq._contact.Adress.Housenumber);
    Assert.Equal("Adress Street", rq._contact.Adress.Streetname);
    Assert.True(rq._numbersAreChecked);
    Assert.True(rq._moneyWillBeSent);
  }

  [Fact]
  public void CheckUse_RequestBasicNoOptional()
  {
    Request rq = new(1, contact2, true, true);
    Assert.Equal(1, rq._id);
    Assert.Equal(contact2, rq._contact);
    Assert.Equal("Bart Decoutere", rq._contact.Name);
    Assert.Equal("email@email.email", rq._contact.Email);
    Assert.Equal(42, rq._contact.Adress.Housenumber);
    Assert.Equal("Adress Street", rq._contact.Adress.Streetname);
    Assert.True(rq._numbersAreChecked);
    Assert.True(rq._moneyWillBeSent);
  }
    // Request request = new(1, contact, true, true);

  [Fact]
  public void CheckUse_ReadRequest()
  {
    string[] input = input1.Split("\t");
    Request rq = new(1, contact1, true, true);
    Request rq2 = RequestHandler.ReadRequestString(input);

    Assert.Equal(rq.ToString(), rq2.ToString());
  }

   [Fact]
   public void CheckUse_WriteRequest()
   {
     string? output;
     string filePath = Context.GetTestFilePath();
     RequestHandler.WriteRequest(rq, filePath);

     using (StreamReader file = new(filePath))
     {
       output = file.ReadLine();
     }
kjakk
     Assert.Equal(rq.ToString(), (RequestHandler.ReadRequestString(output)).ToString());
   }
}
