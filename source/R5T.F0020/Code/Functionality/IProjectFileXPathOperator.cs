using System;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

using R5T.Magyar;

using R5T.T0132;


namespace R5T.F0020
{
	[FunctionalityMarker]
	public partial interface IProjectFileXPathOperator : IFunctionalityMarker
	{
		public XElement AcquireProjectReferencesItemGroup(XDocument projectFile)
        {
			var wasFound = this.HasProjectReferencesItemGroup(projectFile);
			if (!wasFound)
			{
				var output = this.AddItemGroup(projectFile);
				return output;
			}

			return wasFound.Result;
		}

		public void AddProjectReference(XDocument projectFile,
			string projectDirectoryRelativeProjectFilePath)
        {
			// Short-circuit if already present.
			var alreadyHasReference = this.HasProjectReferenceElement(
				projectFile,
				projectDirectoryRelativeProjectFilePath);
			if(alreadyHasReference)
            {
				return;
            }

			var projectReferencesItemGroup = this.AcquireProjectReferencesItemGroup(projectFile);

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

		public XElement AddItemGroup(XDocument projectFile)
        {
			var itemGroup = this.CreateItemGroup();

			var projectElement = this.GetProjectElement(projectFile);

			projectElement.Add(itemGroup);

			return itemGroup;
		}

		public XElement CreateItemGroup()
        {
			var itemGroup = new XElement("ItemGroup");
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

		public XElement GetProjectElement(XDocument projectFile)
        {
			var wasFound = this.HasProjectElement(projectFile);
			if (!wasFound)
			{
				throw new Exception("Projet item not found.");
			}

			return wasFound.Result;
		}

		public XElement GetProjectReferenceElement(XDocument projectFile,
			string projectDirectoryRelativeProjectFilePath)
		{
			var wasFound = this.HasProjectReferenceElement(
				projectFile,
				projectDirectoryRelativeProjectFilePath);

			if (!wasFound)
			{
				throw new Exception($"Project references element not found for:\n{projectDirectoryRelativeProjectFilePath}");
			}

			return wasFound.Result;
		}

		public XElement GetProjectReferencesItemGroup(XDocument projectFile)
        {
			var wasFound = this.HasProjectReferencesItemGroup(projectFile);
            if (!wasFound)
            {
                throw new Exception("Project references item group not found.");
            }

			return wasFound.Result;
        }

		public WasFound<XElement> HasProjectElement(XDocument projectFile)
        {
			var projectReferencesXDocumentRelativeXPath = "//Project";

			var wasFound = this.HasElement(projectFile, projectReferencesXDocumentRelativeXPath);
			return wasFound;
		}

		public WasFound<XElement> HasProjectReferenceElement(XDocument projectFile,
			string projectDirectoryRelativeProjectFilePath)
		{
			var hasProjectReferencesItemGroup = this.HasProjectReferencesItemGroup(projectFile);
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
			var elementOrDefault = projectReferencesItemGroup.XPathSelectElements("//ProjectReference")
				.Where(element => this.IsProjectReferenceTo(
					element,
					projectDirectoryRelativeProjectFilePath))
				.SingleOrDefault()
				;

			var output = WasFound.From(elementOrDefault);
			return output;
        }

		public WasFound<XElement> HasProjectReferencesItemGroup(XDocument projectFile)
        {
			var projectReferencesXDocumentRelativeXPath = "//Project/ItemGroup[ProjectReference]";

			// Assume just one project references item group.
			var wasFound = this.HasElement(projectFile, projectReferencesXDocumentRelativeXPath);
			return wasFound;
		}

		public WasFound<XElement> HasElement(XDocument projectFile, string xPath)
        {
			var itemOrDefault = projectFile.XPathSelectElement(xPath);

			var wasFound = WasFound.From(itemOrDefault);
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

		public void RemoveProjectReference(XDocument projectFile,
			string projectDirectoryRelativeProjectFilePath)
		{
			// Short-circuit if not already present.
			var hasReference = this.HasProjectReferenceElement(
				projectFile,
				projectDirectoryRelativeProjectFilePath);
			if (!hasReference)
			{
				return;
			}

			var projectReferenceElement = this.GetProjectReferenceElement(
				projectFile,
				projectDirectoryRelativeProjectFilePath);

			projectReferenceElement.Remove();
		}

		//public void RemoveProjectReference(XElement projectReferencesItemGroup,
		//	string projectDirectoryRelativeProjectFilePath)
		//{
		//	var projectReferenceElement = this.GetProjectReferenceElement()

		//	projectReferencesItemGroup.Add(projectReferenceElement);
		//}
	}
}