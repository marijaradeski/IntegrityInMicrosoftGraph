var basePath = Directory.GetCurrentDirectory();
var filePath = Path.Combine(basePath, "testFile");

File.WriteAllBytes(filePath, new byte[1024]);

var newHashService = new IntegrityInMicrosoftGraph.Security.HashService();
newHashService.ComputeHash(filePath);

Console.WriteLine(newHashService.ComputeHash(filePath));