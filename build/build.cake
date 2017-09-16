var target = Argument("target", "Default");

var rootLocation = "./..";

Task("Restore")
	.Does(() =>
{
	DotNetCoreRestore(rootLocation);
});

Task("Build")
	.IsDependentOn("Restore")
	.Does(() =>
{
	DotNetCoreBuild(rootLocation);
});

Task("Tests")
	.IsDependentOn("Build")
	.Does(() =>
{
	var projectFiles = GetFiles($"{rootLocation}/test/*.Tests/*.csproj");
    foreach(var file in projectFiles)
    {
        DotNetCoreTest(file.FullPath);
    }
});

Task("Default")
	.IsDependentOn("Tests")
	.Does(() =>
{
	
});

RunTarget(target);