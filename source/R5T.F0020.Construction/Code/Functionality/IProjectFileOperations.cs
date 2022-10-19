using System;
using System.Collections.Generic;

using R5T.T0132;


namespace R5T.F0020.Construction
{
	[FunctionalityMarker]
	public partial interface IProjectFileOperations : IFunctionalityMarker
	{
		public void FindOldStyleCsprojFile()
        {
			var projectTuplesFilePath = @"C:\Temp\Project File Tuples.json";

			var projectTuples = Instances.JsonOperator.Deserialize_Synchronous<ProjectFilesTuple[]>(
				projectTuplesFilePath);

			var projectFilePathsOfInterest = new List<string>();

            foreach (var projectTuple in projectTuples)
            {
				var projectFilePath = projectTuple.ProjectFilePath;

				var projectFileText = Instances.FileOperator.ReadText(projectFilePath);

				if(projectFileText.Contains("xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\""))
                {
					projectFilePathsOfInterest.Add(projectFilePath);
                }
			}

			// Write output.
			Instances.FileOperator.WriteLines(
				Instances.FilePaths.OutputTextFilePath,
				projectFilePathsOfInterest);

			Instances.NotepadPlusPlusOperator.Open(
				Instances.FilePaths.OutputTextFilePath);
		}
	}
}