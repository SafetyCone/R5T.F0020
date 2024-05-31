using System;
using System.IO;
using System.Xml.Linq;

using R5T.T0132;


namespace R5T.F0020
{
	[FunctionalityMarker]
	public partial interface IProjectFileGenerator : IFunctionalityMarker
	{
		public Project CreateNew()
        {
			var project = Instances.ProjectOperator.CreateNew();
			return project;
        }

		public Project CreateNew(Action<XElement> modifier)
        {
			var project = Instances.ProjectOperator.CreateNew(modifier);
			return project;
        }

		public Project CreateNew(
			Func<Project> projectConstructor,
			Action<XElement> modifier)
        {
			var project = Instances.ProjectOperator.CreateNew(
				projectConstructor,
				modifier);

			return project;
        }

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
					throw Instances.EnumerationOperator.Get_UnexpectedEnumerationValueException(projectType);
			}
        }

		public void CreateNewWinForms(string projectFilePath)
        {
			var text = 
			@"
<Project Sdk=""Microsoft.NET.Sdk"">

  <PropertyGroup>
    <OutputType>WinExe</OutputType>
    <TargetFramework>net6.0-windows</TargetFramework>
    <Nullable>enable</Nullable>
    <UseWindowsForms>true</UseWindowsForms>
  </PropertyGroup>

</Project>
";
			this.WriteText(
				projectFilePath,
				text);
		}

		/// <summary>
		/// Creates a new console project (using .NET 6.0).
		/// </summary>
        public void CreateNewConsole(string projectFilePath)
		{
			var text =
@"
<Project Sdk=""Microsoft.NET.Sdk"">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net6.0</TargetFramework>
  </PropertyGroup>

</Project>
";
			this.WriteText(
				projectFilePath,
				text);
		}

		/// <summary>
		/// Creates a new library project (using .NET Standard 2.1).
		/// </summary>
		public void CreateNewLibrary(string projectFilePath)
        {
			var text =
@"
<Project Sdk=""Microsoft.NET.Sdk"">

  <PropertyGroup>
    <TargetFramework>netstandard2.1</TargetFramework>
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
			var directoryPath = Instances.PathOperator.Get_ParentDirectoryPath_ForFile(filePath);
			
			Instances.FileSystemOperator.Create_Directory_OkIfAlreadyExists(directoryPath);

			// Trim text.
			var outputText = text.Trim();

			// Write text synchronously.
			File.WriteAllText(
				filePath,
				outputText);
		}
	}
}