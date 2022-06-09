using System;
using System.IO;
using System.Linq;

using R5T.T0141;


namespace R5T.F0020.Construction
{
	[DemonstrationsMarker]
	public partial interface IProjectFileOperatorDemonstrations : IDemonstrationsMarker
	{
		public void AddProjectReference()
        {
			var projectFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.F0020\source\R5T.F0020.Construction\R5T.F0020.Construction.csproj";

			var referenceProjectFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.A0003\source\R5T.A0003\R5T.A0003.csproj";

			Instances.ProjectFileOperator.AddProjectReference_Synchronous(
				projectFilePath,
				referenceProjectFilePath);
		}

		public void HasProjectReference_True()
        {
			var projectFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.S0030\source\R5T.S0030\R5T.S0030.csproj";

			var referenceProjectFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.A0003\source\R5T.A0003\R5T.A0003.csproj";

			var hasProjectReference = Instances.ProjectFileOperator.HasProjectReference(
				projectFilePath,
				referenceProjectFilePath);

			Console.WriteLine($"Has project reference: {hasProjectReference}");
		}

		public void HasProjectReference_False()
		{
			var projectFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.S0030\source\R5T.S0030\R5T.S0030.csproj";

			var referenceProjectFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.S0031\source\R5T.S0031\R5T.S0031.csproj";

			var hasProjectReference = Instances.ProjectFileOperator.HasProjectReference(
				projectFilePath,
				referenceProjectFilePath);

			Console.WriteLine($"Has project reference: {hasProjectReference}");
		}

		public void ListDirectProjectReferenceFilePaths()
        {
			var projectFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.S0030\source\R5T.S0030\R5T.S0030.csproj";

			var projectReferenceFilePaths = Instances.ProjectFileOperator.GetDirectProjectReferenceFilePaths_Synchronous(projectFilePath);

			var outputFilePath = @"C:\Temp\Project References.txt";

			var lines = EnumerableHelper.From($"Project references of project:\n{projectFilePath}\n")
				.Append(projectReferenceFilePaths
					.OrderAlphabetically())
				;

			FileHelper.WriteAllLines_Synchronous(
				outputFilePath,
				lines);
        }

		public void RemoveProjectReference()
		{
			var projectFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.F0020\source\R5T.F0020.Construction\R5T.F0020.Construction.csproj";

			var referenceProjectFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.A0003\source\R5T.A0003\R5T.A0003.csproj";

			Instances.ProjectFileOperator.RemoveProjectReference_Synchronous(
				projectFilePath,
				referenceProjectFilePath);
		}
	}
}