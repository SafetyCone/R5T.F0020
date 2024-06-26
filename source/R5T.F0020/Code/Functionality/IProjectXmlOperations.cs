using System;
using System.Xml.Linq;

using R5T.T0132;


namespace R5T.F0020
{
	[FunctionalityMarker]
	public partial interface IProjectXmlOperations : IFunctionalityMarker
	{
        public Func<XElement> CreateProjectElement(
            Func<XElement> constructor,
            params Action<XElement>[] modifiers)
        {
			return () =>
			{
				var projectElement = Instances.ConstructionOperator.Create(
					constructor,
					modifiers);

				return projectElement;
			};
        }

        public XElement NewProjectElement()
        {
            var output = Instances.XElementGenerator.CreateProject();
            return output;
        }

  //      /// <summary>
  //      /// <inheritdoc cref="IXElementGenerator.NewProjectElement"/>
		///// Then runs the provided project element action.
  //      /// </summary>
  //      public async Task<XElement> CreateNewProjectElement(
		//	Func<XElement, Task> projectElementAction = default)
  //      {
  //          var projectElement = XElementGenerator.Instance.NewProjectElement();

		//	await F0000.ActionOperator.Instance.Run(
		//		projectElementAction,
		//		projectElement);

  //          return projectElement;
  //      }

        /// <summary>
        /// Upgrades an empty project to a minimal project.
        /// </summary>
        public void EmptyToMinimal(XElement projectElement,
			string targetFrameworkMonikerString,
			string outputTypeString)
		{
			Instances.ProjectXmlOperator.SetTargetFramework(projectElement, targetFrameworkMonikerString);
			Instances.ProjectXmlOperator.SetOutputType(projectElement, outputTypeString);
		}

		public void EmptyToMinimal_Console_NET_5(XElement projectElement)
        {
			this.EmptyToMinimal(projectElement,
				Instances.TargetFrameworkMonikerStrings.NET_5,
				Instances.OutputTypeStrings.Exe);
        }

		public void EmptyToMinimal_Console_NET_6(XElement projectElement)
		{
			this.EmptyToMinimal(projectElement,
				Instances.TargetFrameworkMonikerStrings.NET_6,
				Instances.OutputTypeStrings.Exe);
		}

		public void AddStandardFunctionality(XElement projectElement)
        {
			Instances.ProjectXmlOperator.SetGenerateDocumentationFile(projectElement, true);
#pragma warning disable CS0618 // Type or member is obsolete
            Instances.ProjectXmlOperator.SetDisabledWarnings(projectElement, new[]
			{
				Instances.Warnings.CS1573,
				Instances.Warnings.CS1587,
				Instances.Warnings.CS1591,
			});
#pragma warning restore CS0618 // Type or member is obsolete
        }

		public Action<XElement> EmptyToStandard(
			string targetFrameworkMonikerString,
			string outputTypeString)
        {
			void Internal(XElement projectElement)
            {
				this.EmptyToMinimal(projectElement,
					targetFrameworkMonikerString,
					outputTypeString);

				this.AddStandardFunctionality(projectElement);
            }

			return Internal;
        }
	}
}