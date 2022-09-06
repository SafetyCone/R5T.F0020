using System;

using R5T.T0132;


namespace R5T.F0020
{
	/// <summary>
	/// Operator for project-related functionality (at a higher level of abstraction than project *file* functionality).
	/// </summary>
	[FunctionalityMarker]
	public partial interface IProjectOperator : IFunctionalityMarker
	{
		public string GetProjectName(string projectFilePath)
        {
			var projectName = Instances.PathOperator.GetFileNameStem(projectFilePath);
			return projectName;
        }

		/// <summary>
		/// The default version for projects that do not specify a version is 1.0.0.
		/// </summary>
		public Version GetDefaultVersion()
		{
			var output = F0000.Instances.Versions._1_0_0;
			return output;
		}
	}
}