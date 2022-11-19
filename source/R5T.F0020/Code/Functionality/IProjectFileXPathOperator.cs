using System;
using System.Collections.Generic;
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

		public void AddProjectReference_Idempotent(XDocument projectXDocument,
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

			ItemGroupXmlOperator.Instance.AddProjectReference(
				projectReferencesItemGroup,
				projectDirectoryRelativeProjectFilePath);
		}

		public void AddProjectReferences_Idempotent(XDocument projectXDocument,
			IEnumerable<string> projectDirectoryRelativeProjectFilePaths)
		{
			// Short-circuit if already present.
			var hasReferences = this.HasProjectReferenceElements(
				projectXDocument,
				projectDirectoryRelativeProjectFilePaths);

			var projectReferencesItemGroup = this.AcquireProjectReferencesItemGroup(projectXDocument);

            foreach (var pair in hasReferences)
            {
				if(pair.Value.NotFound())
                {
					var projectDirectoryRelativeProjectFilePath = pair.Key;

					ItemGroupXmlOperator.Instance.AddProjectReference(
						projectReferencesItemGroup,
						projectDirectoryRelativeProjectFilePath);
				}
            }
		}

		public XElement AddItemGroup(XDocument projectXDocument)
        {
			var itemGroup = Instances.XElementGenerator.CreateItemGroup();

			var projectElement = this.GetProjectElement(projectXDocument);

			projectElement.Add(itemGroup);

			return itemGroup;
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

			var output = ProjectXmlOperator.Instance.HasProjectReferenceElement_ForProjectReferencesItemGroup(
				hasProjectReferencesItemGroup,
				projectDirectoryRelativeProjectFilePath);

			return output;
		}

		public Dictionary<string, WasFound<XElement>> HasProjectReferenceElements(XDocument projectXDocument,
			IEnumerable<string> projectDirectoryRelativeProjectFilePaths)
		{
			var hasProjectReferencesItemGroup = this.HasProjectReferencesItemGroup(projectXDocument);
			if (!hasProjectReferencesItemGroup)
			{
				return projectDirectoryRelativeProjectFilePaths
					.ToDictionary(
						x => x,
						x => WasFound.NotFound<XElement>());
			}

			var projectReferencesItemGroup = hasProjectReferencesItemGroup.Result;

			var output = projectDirectoryRelativeProjectFilePaths
				.ToDictionary(
					x => x,
					x => ProjectXmlOperator.Instance.HasProjectReferenceElement_ForProjectReferencesItemGroup(
						projectReferencesItemGroup,
						x));

			return output;
		}

		public WasFound<XElement> HasProjectReferencesItemGroup(XDocument projectXDocument)
        {
			// Assume just one project references item group.
			var wasFound = ProjectXmlOperator.Instance.HasProjectReferencesItemGroup(
				projectXDocument.Root);

			return wasFound;
		}

		public WasFound<XElement> HasVersionElement(XDocument projectXDocument)
		{
			var wasFound = projectXDocument.Root.HasChildOfChild_Single(
				Instances.ElementNames.PropertyGroup,
				Instances.ElementNames.Version);

			return wasFound;
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