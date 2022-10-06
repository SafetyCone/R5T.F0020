using System;

using R5T.T0141;


namespace R5T.F0020.Construction
{
	[DemonstrationsMarker]
	public partial interface IProjectFileGenerationDemonstrations : IDemonstrationsMarker
	{
		public void CreateNew_Standard()
		{
			var project = F0020.Instances.ProjectOperator.CreateNew();

			//project.Modify(F0020.Instances.ProjectXmlOperations.EmptyToMinimal_Console_NET_5);
			project
				.Modify(F0020.Instances.ProjectXmlOperations.EmptyToMinimal_Console_NET_6)
				.Modify(F0020.Instances.ProjectXmlOperations.AddStandardFunctionality)
				;

			F0020.Instances.ProjectFileXmlOperator.Save(
				Instances.ProjectFilePaths.Test,
				project);
		}

		public void CreateNew_Minimal()
        {
			var project = F0020.Instances.ProjectOperator.CreateNew();

			//project.Modify(F0020.Instances.ProjectXmlOperations.EmptyToMinimal_Console_NET_5);
			project.Modify(F0020.Instances.ProjectXmlOperations.EmptyToMinimal_Console_NET_6);

			F0020.Instances.ProjectFileXmlOperator.Save(
				Instances.ProjectFilePaths.Test,
				project);
		}

		public void CreateNew_Empty()
		{
			var project = F0020.Instances.ProjectOperator.CreateNew();

			F0020.Instances.ProjectFileXmlOperator.Save(
				Instances.ProjectFilePaths.Test,
				project);
		}

		public void CreateNewConsole()
		{
			var projectFilePath =
				//@"C:\Temp\ProjectFile-Console.csproj"
				@"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.Z0008\source\R5T.Z0008\Files\ExampleConsole.csproj"
				;

			Instances.ProjectFileGenerator.CreateNewConsole(projectFilePath);
		}

		public void CreateNewLibrary()
        {
			var projectFilePath =
				//@"C:\Temp\ProjectFile-Library.csproj"
				@"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.Z0008\source\R5T.Z0008\Files\ExampleLibrary.csproj"
				;

			Instances.ProjectFileGenerator.CreateNewLibrary(projectFilePath);
        }

		public void CreateNewTest()
		{
			var projectFilePath =
				//@"C:\Temp\ProjectFile-Console.csproj"
				@"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.Z0008\source\R5T.Z0008\Files\ExampleTest.csproj"
				;

			Instances.ProjectFileGenerator.CreateNewTest(projectFilePath);
		}
	}
}