using System;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

using R5T.F0000;

using R5T.T0132;


namespace R5T.F0020.N000
{
	[FunctionalityMarker]
	public partial interface IProjectFileXPathOperator : IFunctionalityMarker
	{
		public XElement AcquireProjectReferencesItemGroup(XDocument projectXDocument)
        {
			var wasFound = this.HasProjectReferencesItemGroup(projectXDocument);
			if (!wasFound)
			{
				var output = this.AddItemGroup(projectXDocument);
				return output;
			}

			return wasFound.Result;
		}

		public void AddProjectReference(XDocument projectXDocument,
			string projectDirectoryRelativeProjectFilePath)
        {
			// Short-circuit if already present.
			var alreadyHasReference = this.HasProjectReferenceElement(
				projectXDocument,
				projectDirectoryRelativeProjectFilePath);
			if(alreadyHasReference)
            {
				return;
            }

			var projectReferencesItemGroup = this.AcquireProjectReferencesItemGroup(projectXDocument);

			this.AddProjectReference(
				projectReferencesItemGroup,
				projectDirectoryRelativeProjectFilePath);
		}

		public void AddProjectReference(XElement projectReferencesItemGroup,
			string projectDirectoryRelativeProjectFilePath)
        {
			var projectReferenceElement = this.CreateProjectReferenceElement(projectDirectoryRelativeProjectFilePath);

			projectReferencesItemGroup.Add(projectReferenceElement);
        }

		public XElement AddItemGroup(XDocument projectXDocument)
        {
			var itemGroup = Instances.XElementGenerator.CreateItemGroup();

			var projectElement = this.GetProjectElement(projectXDocument);

			projectElement.Add(itemGroup);

			return itemGroup;
		}

		public XElement CreateProjectReferenceElement(
			string projectDirectoryRelativeProjectFilePath)
		{
			var projectDirectoryRelativeProjectFilePathWindows = projectDirectoryRelativeProjectFilePath.Replace('/', '\\');

			var includeAttribute = new XAttribute("Include", projectDirectoryRelativeProjectFilePathWindows);

			var output = new XElement("ProjectReference");

			output.Add(includeAttribute);

			return output;
		}

		public XElement GetProjectElement(XDocument projectXDocument)
        {
			var wasFound = Instances.ProjectFileXmlOperator.HasProjectElement(projectXDocument);
			if (!wasFound)
			{
				throw new Exception("Project item not found.");
			}

			return wasFound.Result;
		}

		public XElement GetProjectReferenceElement(XDocument projectXDocument,
			string projectDirectoryRelativeProjectFilePath)
		{
			var wasFound = this.HasProjectReferenceElement(
				projectXDocument,
				projectDirectoryRelativeProjectFilePath);

			if (!wasFound)
			{
				throw new Exception($"Project references element not found for:\n{projectDirectoryRelativeProjectFilePath}");
			}

			return wasFound.Result;
		}

		public XElement GetProjectReferencesItemGroup(XDocument projectXDocument)
        {
			var wasFound = this.HasProjectReferencesItemGroup(projectXDocument);
            if (!wasFound)
            {
                throw new Exception("Project references item group not found.");
            }

			return wasFound.Result;
        }

		public WasFound<XElement> HasOutputTypeElement(XDocument projectXDocument)
        {
			var wasFound = projectXDocument.Root.HasChildOfChild_Single(
				Instances.ElementNames.PropertyGroup,
				Instances.ElementNames.OutputType);

			return wasFound;
		}

		public WasFound<XElement> HasProjectReferenceElement(XDocument projectXDocument,
			string projectDirectoryRelativeProjectFilePath)
		{
			var hasProjectReferencesItemGroup = this.HasProjectReferencesItemGroup(projectXDocument);
			if (!hasProjectReferencesItemGroup)
			{
				return WasFound.NotFound<XElement>();
			}

			var output = this.HasProjectReferenceElement(hasProjectReferencesItemGroup.Result,
				projectDirectoryRelativeProjectFilePath);

			return output;
		}

		public WasFound<XElement> HasProjectReferenceElement(XElement projectReferencesItemGroup,
			string projectDirectoryRelativeProjectFilePath)
        {
			var elementOrDefault = projectReferencesItemGroup.Elements()
				.WhereNameIs(Instances.ElementNames.ProjectReference)
				.Where(element => this.IsProjectReferenceTo(
					element,
					projectDirectoryRelativeProjectFilePath))
				.SingleOrDefault()
				;

			var output = WasFound.From(elementOrDefault);
			return output;
        }

		public WasFound<XElement> HasProjectReferencesItemGroup(XDocument projectXDocument)
        {
			// Assume just one project references item group.
			var wasFound = projectXDocument.Root.HasChildWithChild_Single(
				Instances.ElementNames.ItemGroup,
				Instances.ElementNames.ProjectReference);

			return wasFound;
		}

		public WasFound<XElement> HasVersionElement(XDocument projectXDocument)
		{
			var wasFound = projectXDocument.Root.HasChildOfChild_Single(
				Instances.ElementNames.PropertyGroup,
				Instances.ElementNames.Version);

			return wasFound;
		}

		public bool IsProjectReferenceTo(XElement element,
			string projectDirectoryRelativeProjectFilePath)
        {
			var projectDirectoryRelativeProjectFilePathNonWindows = projectDirectoryRelativeProjectFilePath.Replace('\\', '/');
			var projectDirectoryRelativeProjectFilePathWindows = projectDirectoryRelativeProjectFilePath.Replace('/', '\\');

			var includeAttributeValue = element.Attribute("Include")?.Value;

			var output = false
				|| includeAttributeValue == projectDirectoryRelativeProjectFilePathNonWindows
				|| includeAttributeValue == projectDirectoryRelativeProjectFilePathWindows
				;

			return output;
        }

		public void RemoveProjectReference(XDocument projectXDocument,
			string projectDirectoryRelativeProjectFilePath)
		{
			// Short-circuit if not already present.
			var hasReference = this.HasProjectReferenceElement(
				projectXDocument,
				projectDirectoryRelativeProjectFilePath);
			if (!hasReference)
			{
				return;
			}

			var projectReferenceElement = this.GetProjectReferenceElement(
				projectXDocument,
				projectDirectoryRelativeProjectFilePath);

			projectReferenceElement.Remove();
		}
	}
}