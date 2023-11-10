namespace MeasureDD;

public static class Context
{
  // public const string docPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
  //  const string docPath = @"C:\Users\bartd\Documents\0. Files\SoftwareDevelopment\Projects\MeasureDD\";
  static string docPath = GetProjectRoot();
  const string filePath = @"files/requests.txt" ;
  const string testFilePath = @"MeasureDD.Tests/files/requests.txt";

  public static string GetTestFilePath()
  {
    return Path.Combine(docPath, testFilePath);
  }

  /*
  public static string GetFilePath()
  {
    return Path.Combine(docPath, filePath);
  }
  */

  static string GetProjectRoot()
  {
    string dir = Directory.GetCurrentDirectory();
    while (!(Directory.GetDirectories(dir, "*.git*", SearchOption.TopDirectoryOnly).Length > 0))
    {
      dir = Directory.GetParent(dir)!.ToString();
      if (dir == null)
      {
        throw new DirectoryNotFoundException();
      }
      dir = dir.ToString();
    }
    return dir;
  }

}
