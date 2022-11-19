using System;
using System.Collections.Generic;
using System.Linq;

using R5T.T0132;


namespace R5T.F0020.Construction
{
	[FunctionalityMarker]
	public partial interface IProjectFileOperations : IFunctionalityMarker
	{
		public void DeleteBAKProjects()
        {
			var backupProjectFilePaths = this.DetermineBAKProjects();

            foreach (var filePath in backupProjectFilePaths)
            {
				F0000.FileSystemOperator.Instance.DeleteFile_OkIfNotExists(
					filePath);
            }
		}

		public string[] DetermineBAKProjects()
        {
			var projectTuplesFilePath = @"C:\Temp\Project File Tuples.json";

			var projectTuples = Instances.JsonOperator.Deserialize_Synchronous<ProjectFilesTuple[]>(
				projectTuplesFilePath);

			var backupProjectFilePaths = projectTuples
				.Select(projectTuple => projectTuple.ProjectFilePath)
				.Where(projectFilePath =>
				{
					var projectfileNameStem = F0002.PathOperator.Instance.GetFileNameStem(projectFilePath);

					var isBackupProjectFile = F0000.StringOperator.Instance.EndsWith(
						projectfileNameStem,
						F0000.FileNameAffixes.Instance.BAK);

					return isBackupProjectFile;
				})
				.OrderAlphabetically()
				.ToArray();

			return backupProjectFilePaths;
		}

		public void FindBAKProjects()
        {
			var backupProjectFilePaths = this.DetermineBAKProjects();

			// Write output.
			Instances.FileOperator.WriteLines(
				Instances.FilePaths.OutputTextFilePath,
				backupProjectFilePaths);

			Instances.NotepadPlusPlusOperator.Open(
				Instances.FilePaths.OutputTextFilePath);
		}

		public void FindOldStyleCsprojFile()
        {
			var projectTuplesFilePath = @"C:\Temp\Project File Tuples.json";

			var projectTuples = Instances.JsonOperator.Deserialize_Synchronous<ProjectFilesTuple[]>(
				projectTuplesFilePath);

			var projectFilePathsOfInterest = new List<string>();

            foreach (var projectTuple in projectTuples)
            {
				var projectFilePath = projectTuple.ProjectFilePath;

				var projectFileText = Instances.FileOperator.ReadText_Synchronous(projectFilePath);

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