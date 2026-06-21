using IntegrityInMicrosoftGraph.Security;
using IntegrityInMicrosoftGraph.Services;

var file = new FileService();
var basePath = file.CreateFile("testFile", 1024);

File.WriteAllBytes(basePath, new byte[1024]);

var newHashService = new HashService();
newHashService.ComputeHash(basePath);

Console.WriteLine(newHashService.ComputeHash(basePath));