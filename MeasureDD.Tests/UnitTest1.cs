using MeasureDD.Requests;

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
  string[] rl = {"1", "Bart Decoutere"};
  static Adress adress = new("Adress Street", 42, 3232, null, null);
  static Contact contact = new("Bart Decoutere", "email@email.email", adress, "HZC");
  
  [Fact]
  public void ChecksInput_InputCount10()
  {
    var wasExceptionReceived = Record.Exception(() => RequestHandler.ReadRequestString(rl));
    Assert.IsType<ArgumentException>(wasExceptionReceived);
  }

  [Fact]
  public void CheckUse_Basic()
  {
    Request rq = new(1, contact, true, true);
    Assert.Equal(1, rq._id);
    Assert.Equal(contact, rq._contact);
    Assert.Equal("Bart Decoutere", rq._contact.Name);
    Assert.Equal("email@email.email", rq._contact.Email);
    Assert.Equal(42, rq._contact.Adress.Housenumber);
    Assert.Equal("Adress Street", rq._contact.Adress.Streetname);
    Assert.True(rq._numbersAreChecked);
    Assert.True(rq._moneyWillBeSent);
  }
    // Request request = new(1, contact, true, true);
}
