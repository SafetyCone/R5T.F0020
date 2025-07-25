using System;
using System.IO;
using System.Linq;

using R5T.T0141;


namespace R5T.F0020.Construction
{
	[DemonstrationsMarker]
	public partial interface IProjectFileOperatorDemonstrations : IDemonstrationsMarker
	{
		public void TestHasVersion()
        {
			var projectWithVersionFilePath = Instances.ProjectFilePaths.R5T_S0013;

			var projectWithVersionHasVersion = Instances.ProjectFileOperator.Has_VersionString_Synchronous(projectWithVersionFilePath);

			var versionOrNot = projectWithVersionHasVersion.Exists
				? projectWithVersionHasVersion.Result.ToString()
				: "<No version>"
				;

			Console.WriteLine($"{versionOrNot}: Project with version has version?");

			var projectWithoutVersionFilePath = Instances.ProjectFilePaths.R5T_F0020_Construction;

			var projectWithoutVersionHasVersion = Instances.ProjectFileOperator.Has_VersionString_Synchronous(projectWithoutVersionFilePath);

			versionOrNot = projectWithoutVersionHasVersion.Exists
				? projectWithoutVersionHasVersion.Result.ToString()
				: "<No version>"
				;

			Console.WriteLine($"{versionOrNot}: Project with version has version?");
		}

		public void TestIsLibraryProject()
        {
			var libraryProjectFilePath = Instances.ProjectFilePaths.R5T_F0020;

			var libraryProjectIsLibrary = Instances.ProjectFileOperator.IsLibrary_Synchronous(libraryProjectFilePath);

			Console.WriteLine($"{libraryProjectIsLibrary}: is library project a library?");

			var nonLibraryProjectFilePath = Instances.ProjectFilePaths.R5T_F0020_Construction;

			var nonLibraryProjectIsLibrary = Instances.ProjectFileOperator.IsLibrary_Synchronous(nonLibraryProjectFilePath);

			Console.WriteLine($"{nonLibraryProjectIsLibrary}: is non-library project a library?");
		}

		public void AddProjectReference()
        {
			var projectFilePath = Instances.ProjectFilePaths.R5T_F0020_Construction;

			var referenceProjectFilePath = Instances.ProjectFilePaths.R5T_A0003;

			Instances.ProjectFileOperator.AddProjectReference_Idempotent_Synchronous(
				projectFilePath,
				referenceProjectFilePath);
		}

		public void HasProjectReference_True()
        {
			var projectFilePath = Instances.ProjectFilePaths.R5T_S0030;

			var referenceProjectFilePath = Instances.ProjectFilePaths.R5T_A0003;

			var hasProjectReference = Instances.ProjectFileOperator.HasProjectReference(
				projectFilePath,
				referenceProjectFilePath);

			Console.WriteLine($"Has project reference: {hasProjectReference}");
		}

		public void HasProjectReference_False()
		{
			var projectFilePath = Instances.ProjectFilePaths.R5T_S0030;

			var referenceProjectFilePath = Instances.ProjectFilePaths.R5T_S0031;

			var hasProjectReference = Instances.ProjectFileOperator.HasProjectReference(
				projectFilePath,
				referenceProjectFilePath);

			Console.WriteLine($"Has project reference: {hasProjectReference}");
		}

		public void ListDirectProjectReferenceFilePaths()
        {
			var projectFilePath = Instances.ProjectFilePaths.R5T_S0030;

			var projectReferenceFilePaths = Instances.ProjectFileOperator.GetDirectProjectReferenceFilePaths_Synchronous(projectFilePath);

			var outputFilePath = @"C:\Temp\Project References.txt";

			var lines = F0000.Instances.EnumerableOperator.From($"Project references of project:\n{projectFilePath}\n")
				.Append(projectReferenceFilePaths
					.OrderAlphabetically())
				;

			F0000.Instances.FileOperator.Write_Lines_Synchronous(
				outputFilePath,
				lines);
        }

		public void RemoveProjectReference()
		{
			var projectFilePath = Instances.ProjectFilePaths.R5T_F0020_Construction;

			var referenceProjectFilePath = Instances.ProjectFilePaths.R5T_A0003;

			Instances.ProjectFileOperator.RemoveProjectReference_Synchronous(
				projectFilePath,
				referenceProjectFilePath);
		}
	}
}