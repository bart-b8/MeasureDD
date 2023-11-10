namespace MeasureDD;

public static class Context
{
  // public const string docPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
  //  const string docPath = @"C:\Users\bartd\Documents\0. Files\SoftwareDevelopment\Projects\MeasureDD\";
  static string rootPath = GetProjectRoot();
  const string filePath = @"files/" ;
  const string testFilePath = @"MeasureDD.Tests/files/";
  public enum DocumentType
  {
    Requests,
    Template
  }

  public static string GetTestFilePath(DocumentType doctype)
  {
    return Path.Combine(rootPath, testFilePath, doctype switch 
        {
        DocumentType.Requests => "requests.txt",
        DocumentType.Template => "template.docx",
        _ => throw new ArgumentOutOfRangeException(nameof(doctype), $"Not excpeted DocumentType value: {doctype}"),
        });
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
