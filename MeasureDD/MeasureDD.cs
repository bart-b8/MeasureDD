// See https://aka.ms/new-console-template for more information
using MeasureDD.Requests;

string a =  "1\tBart Decoutere\temail@email.email\tAdress Street\t42\t3232\tCity\tCountry\tHZC\ttrue\ttrue";
Console.WriteLine(a);
string[] b = a.Split("\t");

foreach (string c in b)
{
  Console.WriteLine(c);
}

Request rq = RequestHandler.ReadRequestString(b);
Console.WriteLine(Request.Header());
Console.WriteLine(rq);



