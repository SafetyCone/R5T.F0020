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
			var projectElement = Instances.ProjectXmlOperator.CreateNew();

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
			var output = F0000.Instances.Versions._1_0_0;
			return output;
		}

		public string GetProjectName(string projectFilePath)
        {
			var projectName = Instances.PathOperator.GetFileNameStem(projectFilePath);
			return projectName;
        }

		public string GetWarningsConcatentation(IEnumerable<int> warnings)
        {
			var output = F0000.Instances.StringOperator.Join(
				Z0000.Instances.Characters.Semicolon,
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