using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using R5T.T0132;


namespace R5T.F0020
{
	/// <summary>
	/// Operator for project-related functionality (at a higher level of abstraction than project *file* functionality).
	/// </summary>
	[FunctionalityMarker]
	public partial interface IProjectOperator : IFunctionalityMarker
	{
		public Project CreateNew()
        {
			var projectElement = Instances.ProjectXmlOperations.NewProjectElement();

			var project = new Project()
			{
				Element = projectElement,
			};

			return project;
        }

		public Project CreateNew(Action<XElement> modifier)
		{
			var project = this.CreateNew(
				this.CreateNew,
				modifier);

			return project;
		}

		public Project CreateNew(
			Func<Project> projectConstructor,
			Action<XElement> modifier)
		{
			var project = projectConstructor();

			modifier(project.Element);

			return project;
		}

		/// <summary>
		/// The default version for projects that do not specify a version is 1.0.0.
		/// </summary>
		public Version GetDefaultVersion()
		{
			var output = Instances.Versions._Raw._1_0_0;
			return output;
		}

		/// <summary>
		/// Gets the project name from the path of the project only (does not examine project file contents, so does not require the project file to exist).
		/// </summary>
		public string GetProjectName(string projectFilePath)
        {
			var projectName = Instances.PathOperator.Get_FileNameStem(projectFilePath);
			return projectName;
        }

		public string GetWarningsConcatentation(IEnumerable<int> warnings)
        {
			var output = Instances.StringOperator.Join(
				Instances.Characters.Semicolon,
				warnings
					.Select(warning => warning.ToString()));

			return output;
        }

		public Project Modify(
			Project project,
			Action<XElement> projectElementModifier)
        {
			projectElementModifier(project.Element);

			return project;
        }
	}
}