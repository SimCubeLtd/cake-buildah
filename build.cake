var Project = Directory("./src/Cake.Buildah/");
var TestProject = Directory("./tests/Cake.Buildah.Tests/");
var CakeBuildahProj = Project + File("Cake.Buildah.csproj");
var CakeTestBuildahProj = TestProject + File("Cake.Buildah.Tests.csproj");
var CakeTestBuildahAssembly = TestProject + Directory("bin/Release/net6.0") + File("Cake.Buildah.Tests.dll");
var AssemblyInfo = Project + File("Properties/AssemblyInfo.cs");
var CakeBuildahSln = File("./Cake.Buildah.sln");

var target = Argument("target", "Default");

Task("Default")
	.Does (() =>
	{
		NuGetRestore (CakeBuildahSln);
		DotNetCoreClean(CakeBuildahSln);
		DotNetCoreBuild (CakeBuildahSln, new DotNetCoreBuildSettings {
			Configuration = "Release"
		});
});

Task("UnitTest")
	.IsDependentOn("Default")
	.Does(() =>
	{
		var settings = new DotNetCoreTestSettings
		 {
			 Configuration = "Release"
		 };
		DotNetCoreTest(CakeTestBuildahProj, settings);
	});

RunTarget(target);
