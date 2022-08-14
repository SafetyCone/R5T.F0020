using System;
using System.IO;

using R5T.T0132;


namespace R5T.F0020
{
	[FunctionalityMarker]
	public partial interface IProjectFileGenerator : IFunctionalityMarker
	{
		public void CreateNew(
			string projectFilePath,
			ProjectType projectType)
        {
            switch (projectType)
            {
				case ProjectType.Console:
					this.CreateNewConsole(projectFilePath);
					break;

				case ProjectType.Library:
					this.CreateNewLibrary(projectFilePath);
					break;

				case ProjectType.Test:
					this.CreateNewTest(projectFilePath);
					break;

				default:
					throw Instances.EnumerationHelper.UnexpectedEnumerationValueException(projectType);
			}
        }

        public void CreateNewConsole(string projectFilePath)
		{
			var text =
@"
<Project Sdk=""Microsoft.NET.Sdk"">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net5.0</TargetFramework>
  </PropertyGroup>

</Project>
";
			this.WriteText(
				projectFilePath,
				text);
		}

		public void CreateNewLibrary(string projectFilePath)
        {
			var text =
@"
<Project Sdk=""Microsoft.NET.Sdk"">

  <PropertyGroup>
    <TargetFramework>net5.0</TargetFramework>
  </PropertyGroup>

</Project>
";
			this.WriteText(
				projectFilePath,
				text);
        }

		public void CreateNewTest(string projectFilePath)
		{
			var text =
@"
<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <TargetFramework>net5.0</TargetFramework>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include=""Microsoft.NET.Test.Sdk"" Version=""17.0.0"" />
    <PackageReference Include=""MSTest.TestAdapter"" Version=""2.2.8"" />
  </ItemGroup>
</Project>
";
			this.WriteText(
				projectFilePath,
				text);
		}

		public void WriteText(
			string filePath,
			string text)
		{
			// Ensure the directory exists.
			var directoryPath = Instances.PathOperator.GetParentDirectoryPath(filePath);
			
			Instances.FileSystemOperator.CreateDirectory_OkIfAlreadyExists(directoryPath);

			// Trim text.
			var outputText = text.Trim();

			// Write text synchronously.
			File.WriteAllText(
				filePath,
				outputText);
		}
	}
}