using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using R5T.F0000;
using R5T.T0132;

using R5T.F0020.Extensions;


namespace R5T.F0020
{
    [FunctionalityMarker]
	public partial interface IProjectXmlOperator : IFunctionalityMarker
	{
		private static Internal.IProjectXmlOperator Internal => F0020.Internal.ProjectXmlOperator.Instance;


		public XElement AcquireOutputTypeElement(XElement projectElement)
		{
			var outputTypeElement = Internal.AcquirePropertyGroupChildElement(
				projectElement,
				Instances.ElementNames.OutputType);

			return outputTypeElement;
		}

		public XElement AcquirePackageReferencesItemGroup(XElement projectElement)
		{
			var wasFound = this.HasPackageReferencesItemGroup(projectElement);
			if (!wasFound)
			{
				var itemGroup = this.AddItemGroup(projectElement);
				return itemGroup;
			}

			return wasFound.Result;
		}

		public XElement AcquireProjectReferencesItemGroup(XElement projectElement)
		{
			var wasFound = this.HasProjectReferencesItemGroup(projectElement);
			if (!wasFound)
			{
				var itemGroup = this.AddItemGroup(projectElement);
				return itemGroup;
			}

			return wasFound.Result;
		}

		/// <summary>
		/// <inheritdoc cref="Documentation.MainPropertyGroup" path="/summary"/>
		/// </summary>
		public XElement AcquireMainPropertyGroup(XElement projectElement)
        {
			var mainPropertyGroup = Internal.AcquirePropertyGroupChildElement(projectElement,
				Instances.ElementNames.TargetFramework);

			return mainPropertyGroup;
        }

		/// <summary>
		/// <inheritdoc cref="Documentation.PackagePropertyGroup" path="/summary"/>
		/// </summary>
		public XElement AcquirePackagePropertyGroup(XElement projectElement)
		{
			var packagePropertyGroup = Internal.AcquirePropertyGroupChildElement(projectElement,
				Instances.ElementNames.Version);

			return packagePropertyGroup;
		}

		public XElement AcquireTargetFrameworkPropertyGroup(XElement projectElement)
		{
			var wasFound = this.HasTargetFrameworkPropertyGroup(projectElement);
			if (!wasFound)
			{
				var itemGroup = this.AddItemGroup(projectElement);
				return itemGroup;
			}

			return wasFound.Result;
		}

		public XElement AcquireTargetFrameworkPropertyGroupChildElement(XElement projectElement,
			string targetFrameworkPropertyGroupChildElementName)
		{
			var targetFrameworkPropertyGroup = this.AcquireTargetFrameworkPropertyGroup(projectElement);

			var childWasFound = targetFrameworkPropertyGroup.HasChild(targetFrameworkPropertyGroupChildElementName);
			if (!childWasFound)
			{
				var childElement = Instances.XElementOperator.AddChild(targetFrameworkPropertyGroup, targetFrameworkPropertyGroupChildElementName);
				return childElement;
			}

			return childWasFound.Result;
		}

		public XElement AddItemGroup(XElement projectElement)
		{
			var itemGroup = Instances.XElementGenerator.CreateItemGroup();

			projectElement.Add(itemGroup);

			return itemGroup;
		}

		public void AddPackageReference_Idempotent(XElement projectElement,
			string packageIdentity,
			Version version)
        {
			var versionString = VersionOperator.Instance.ToString_Major_Minor_Build(version);

			this.AddPackageReference_Idempotent(
				projectElement,
				packageIdentity,
				versionString);
        }

		public void AddPackageReference_Idempotent(XElement projectElement,
			string packageIdentity,
			string version)
        {
			// Short-circuit if already present.
			var alreadyHasReference = this.HasPackageReferenceElement(
				projectElement,
				packageIdentity,
				version);

			if (alreadyHasReference)
			{
				return;
			}

			var projectReferencesItemGroup = this.AcquirePackageReferencesItemGroup(projectElement);

			ItemGroupXmlOperator.Instance.AddPackageReference(
				projectReferencesItemGroup,
				packageIdentity,
				version);
		}

		public void AddProjectReference_Idempotent(XElement projectElement,
			string projectFilePath,
			string referenceProjectFilePath)
        {
			var projectDirectoryRelativeProjectReferenceFilePath = Instances.ProjectPathsOperator.GetProjectDirectoryRelativePath(
				projectFilePath,
				referenceProjectFilePath);

			this.AddProjectReference_Idempotent(
				projectElement,
				projectDirectoryRelativeProjectReferenceFilePath);
		}

		public void AddProjectReference_Idempotent(XElement projectElement,
			string projectDirectoryRelativeReferenceProjectFilePath)
		{
			// Short-circuit if already present.
			var alreadyHasReference = this.HasProjectReferenceElement(
				projectElement,
				projectDirectoryRelativeReferenceProjectFilePath);

			if (alreadyHasReference)
			{
				return;
			}

			var projectReferencesItemGroup = this.AcquireProjectReferencesItemGroup(projectElement);

			ItemGroupXmlOperator.Instance.AddProjectReference(
				projectReferencesItemGroup,
				projectDirectoryRelativeReferenceProjectFilePath);
		}

		public XElement AddPropertyGroup(XElement projectElement)
		{
			var propertyGroup = Instances.XElementGenerator.CreatePropertyGroup();

			projectElement.Add(propertyGroup);

			return propertyGroup;
		}

		/// <inheritdoc cref="IXElementGenerator.CreateProject(bool)"/>
		public XElement CreateNew()
        {
			var output = Instances.XElementGenerator.CreateProject();
			return output;
        }

		public string GetAuthorsTokenSeparator()
        {
			var tokenSeparator = Z0000.Instances.Strings.Comma;
			return tokenSeparator;
        }

		public IEnumerable<XElement> GetItemGroups(XElement projectElement)
        {
			var output = projectElement.Children()
				.WhereNameIs(Instances.ElementNames.ItemGroup)
				;

			return output;
        }

		public string GetPackageTagsTokenSeparator()
        {
			var tokenSeparator = Z0000.Instances.Strings.Semicolon;
			return tokenSeparator;
        }

		public WasFound<XElement> HasPackageReferenceElement(XElement projectElement,
			string packageIdentity,
			string version)
        {
			var hasPackageReferencesItemGroup = this.HasPackageReferencesItemGroup(projectElement);
			if(!hasPackageReferencesItemGroup)
            {
				return WasFound.NotFound<XElement>();
            }

			var output = this.HasPackageReferenceElement_ForPackageReferencesItemGroup(
				hasPackageReferencesItemGroup.Result,
				packageIdentity,
				version);

			return output;
        }

		public WasFound<XElement> HasProjectReferenceElement(XElement projectElement,
			string projectDirectoryRelativeProjectFilePath)
		{
			var hasProjectReferencesItemGroup = this.HasProjectReferencesItemGroup(projectElement);
			if (!hasProjectReferencesItemGroup)
			{
				return WasFound.NotFound<XElement>();
			}

			var output = this.HasProjectReferenceElement_ForProjectReferencesItemGroup(
				hasProjectReferencesItemGroup.Result,
				projectDirectoryRelativeProjectFilePath);

			return output;
		}

		public WasFound<XElement> HasPackageReferenceElement_ForPackageReferencesItemGroup(XElement packageReferencesItemGroup,
			string packageIdentity,
			string version)
        {
			var elementOrDefault = packageReferencesItemGroup.Elements()
				.WhereNameIs(Instances.ElementNames.PackageReference)
				.Where(element => this.IsPackageReferenceTo(
					element,
					packageIdentity,
					version))
				.SingleOrDefault()
				;

			var output = WasFound.From(elementOrDefault);
			return output;
		}

		public WasFound<XElement> HasProjectReferenceElement_ForProjectReferencesItemGroup(XElement projectReferencesItemGroup,
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

		public WasFound<XElement> HasPackageReferencesItemGroup(XElement projectElement)
        {
			// Assume just one package item group.
			var wasFound = projectElement.HasChildWithChild_Single(
				Instances.ElementNames.ItemGroup,
				Instances.ElementNames.PackageReference);

			return wasFound;
		}

		public WasFound<XElement> HasProjectReferencesItemGroup(XElement projectElement)
		{
			// Assume just one project references item group.
			var wasFound = projectElement.HasChildWithChild_Single(
				Instances.ElementNames.ItemGroup,
				Instances.ElementNames.ProjectReference);

			return wasFound;
		}

		public WasFound<XElement> HasTargetFrameworkPropertyGroup(XElement projectElement)
		{
			// Assume just one project references item group.
			var wasFound = projectElement.HasChildWithChild_Single(
				Instances.ElementNames.PropertyGroup,
				Instances.ElementNames.TargetFramework);

			return wasFound;
		}

		public void SetAuthors(XElement projectElement, string[] authors)
        {
			var authorsTokenSeparator = this.GetAuthorsTokenSeparator();

			var authorsString = F0000.Instances.StringOperator.Join(
				authorsTokenSeparator,
				authors);

			this.SetAuthors(
				projectElement,
				authorsString);
        }

		public bool IsPackageReferenceTo(XElement element,
			string packageIdentity,
			string version)
        {
			var includeAttributeValue = element.Attribute(AttributeNames.Instance.Include)?.Value;
			var versionAttributeValue = element.Attribute(AttributeNames.Instance.Include)?.Value;

			var output = true
				&& includeAttributeValue == packageIdentity
				&& versionAttributeValue == version
				;

			return output;
		}

		public bool IsProjectReferenceTo(XElement element,
			string projectDirectoryRelativeProjectFilePath)
		{
			var projectDirectoryRelativeProjectFilePathNonWindows = projectDirectoryRelativeProjectFilePath.Replace('\\', '/');
			var projectDirectoryRelativeProjectFilePathWindows = projectDirectoryRelativeProjectFilePath.Replace('/', '\\');

			var includeAttributeValue = element.Attribute(AttributeNames.Instance.Include)?.Value;

			var output = false
				|| includeAttributeValue == projectDirectoryRelativeProjectFilePathNonWindows
				|| includeAttributeValue == projectDirectoryRelativeProjectFilePathWindows
				;

			return output;
		}

		public void RemoveProjectReference(XElement projectElement,
			string projectDirectoryRelativeProjectFilePath)
		{
			// Short-circuit if not already present.
			var hasReference = this.HasProjectReferenceElement(
				projectElement,
				projectDirectoryRelativeProjectFilePath);

			if (!hasReference)
			{
				return;
			}

			var projectReferenceElement = this.GetProjectReferenceElement(
				projectElement,
				projectDirectoryRelativeProjectFilePath);

			projectReferenceElement.Remove();
		}

		public void RemoveProjectReferences(XElement projectElement,
			IEnumerable<string> projectDirectoryRelativeProjectFilePath)
        {
            foreach (var path in projectDirectoryRelativeProjectFilePath)
            {
				this.RemoveProjectReference(
					projectElement,
					path);
            }
        }

		public void SetAuthors(XElement projectElement, string authorsString)
        {
			this.SetPackagePropertyGroupChildElementValue(
				projectElement,
				Instances.ElementNames.Authors,
				authorsString);
        }

		public void SetCheckEolTargetFramework(XElement projectElement,
			bool value)
		{
			var valueString = BooleanOperator.Instance.ToString_ForProjectFile(value);

			this.SetMainPropertyGroupChildElementValue(
				projectElement,
				ElementNames.Instance.CheckEolTargetFramework,
				valueString);
		}

		public void SetCompany(XElement projectElement, string company)
        {
			this.SetPackagePropertyGroupChildElementValue(
				projectElement,
				Instances.ElementNames.Company,
				company);
        }

		public void SetCopyright(XElement projectElement, string copyright)
        {
			this.SetPackagePropertyGroupChildElementValue(
				projectElement,
				Instances.ElementNames.Copyright,
				copyright);
        }

        public void SetDescription(XElement projectElement, string description)
        {
			this.SetPackagePropertyGroupChildElementValue(
				projectElement,
				Instances.ElementNames.Description,
				description);
        }

        /// <summary>
        /// Sets the <see cref="IElementNames.NoWarn"/> property value.
        /// </summary>
        public void SetDisabledWarnings(XElement projectElement,
			IEnumerable<int> warnings)
		{
			var valueString = Instances.ProjectOperator.GetWarningsConcatentation(warnings);

			this.SetMainPropertyGroupChildElementValue(projectElement,
				Instances.ElementNames.NoWarn,
				valueString);
		}

		public void SetGenerateDocumentationFile(XElement projectElement, bool value)
		{
			var valueString = Instances.BooleanOperator.ToString_ForProjectFile(value);

			// Put the property in the packages property group since the documentation file is generated by default in Visual Studio, but not during packaging.
			// Thus the generate documentation file property is only relevant to the packaging process, and so should go with the other packaging properties.
			this.SetPackagePropertyGroupChildElementValue(projectElement,
				Instances.ElementNames.GenerateDocumentationFile,
				valueString);
		}

		/// <summary>
		/// <inheritdoc cref="Documentation.MainPropertyGroup" path="/summary"/>
		/// </summary>
		public void SetMainPropertyGroupChildElementValue(XElement projectElement,
			string mainPropertyGroupChildElementName,
			string value)
        {
			Internal.SetPropertyGroupChildElementValue(projectElement,
				Instances.ElementNames.TargetFramework,
				mainPropertyGroupChildElementName,
				value);
        }

		/// <summary>
		/// <inheritdoc cref="Documentation.PackagePropertyGroup" path="/summary"/>
		/// </summary>
		public void SetPackagePropertyGroupChildElementValue(XElement projectElement,
			string packagePropertyGroupChildElementName,
			string value)
		{
			Internal.SetPropertyGroupChildElementValue(projectElement,
				Instances.ElementNames.Version,
				packagePropertyGroupChildElementName,
				value);
		}

		/// <summary>
		/// One of the SPDX (Software Package Data Exchange) license identifiers: <see href="https://spdx.org/licenses/"/>.
		/// See also: <see href="https://learn.microsoft.com/en-us/nuget/reference/nuspec#license"/>
		/// </summary>
		public void SetPackageLicenseExpression(XElement projectElement, string spdxPackageLicenseExpression)
        {
			this.SetPackagePropertyGroupChildElementValue(
				projectElement,
				Instances.ElementNames.PackageLicenseExpression,
				spdxPackageLicenseExpression);
        }

		public void SetPackageRequireLicenseAcceptance(XElement projectElement, bool requireLicenseAcceptance)
        {
			var valueString = F0000.Instances.BooleanOperator.ToString_PascalCase(requireLicenseAcceptance);

			this.SetPackagePropertyGroupChildElementValue(
				projectElement,
				Instances.ElementNames.PackageRequireLicenseAcceptance,
				valueString);
        }

		public void SetPackageReadmeFile(XElement projectElement, string packageReadmeFileProjectDirectoryRelativePath)
        {
			this.SetPackagePropertyGroupChildElementValue(
				projectElement,
				Instances.ElementNames.PackageReadmeFile,
				packageReadmeFileProjectDirectoryRelativePath);
        }

		public void SetPackageTags(XElement projectElement, string[] packageTags)
        {
			var tokenSeparator = this.GetPackageTagsTokenSeparator();

			var packageTagsString = F0000.Instances.StringOperator.Join(
				tokenSeparator,
				packageTags);

			this.SetPackageTags(
				projectElement,
				packageTagsString);
        }

		public void SetPackageTags(XElement projectElement, string packageTagsString)
        {
			this.SetPackagePropertyGroupChildElementValue(
				projectElement,
				Instances.ElementNames.PackageTags,
				packageTagsString);
        }

		public void SetOutputType(XElement projectElement, string outputTypeString)
		{
			this.SetMainPropertyGroupChildElementValue(projectElement,
				Instances.ElementNames.OutputType,
				outputTypeString);
		}

		public void SetRepositoryUrl(XElement projectElement, string repositoryUrl)
        {
			this.SetPackagePropertyGroupChildElementValue(
				projectElement,
				Instances.ElementNames.RepositoryUrl,
				repositoryUrl);
        }

		public void SetGenerateDocumentationFile_True(XElement projectElement)
        {
			var value = true;

			this.SetGenerateDocumentationFile(
				projectElement,
				value);
        }

		public void SetVersion(XElement projectElement, Version version)
        {
			var versionString = F0000.Instances.VersionOperator.ToString_Major_Minor_Build(version);

			this.SetVersion(projectElement, versionString);
        }

		public void SetVersion(XElement projectElement, string versionString)
        {
			this.SetPackagePropertyGroupChildElementValue(
				projectElement,
				Instances.ElementNames.Version,
				versionString);
        }

		public XElement GetProjectReferenceElement(XElement projectElement,
			string projectDirectoryRelativeProjectFilePath)
		{
			var wasFound = this.HasProjectReferenceElement(
				projectElement,
				projectDirectoryRelativeProjectFilePath);

			if (!wasFound)
			{
				throw new Exception($"Project references element not found for:\n{projectDirectoryRelativeProjectFilePath}");
			}

			return wasFound.Result;
		}

		public string GetTargetFramework(XElement projectElement)
		{
			var hasTargetFramework = this.HasTargetFramework(projectElement);
			if(!hasTargetFramework)
            {
				throw new Exception("Unable to find target framework element in project.");
            }

			return hasTargetFramework;
		}

		public bool HasAnyCOMReferences(XElement projectElement)
        {
			var hasAnyCOMReferences = projectElement.ItemGroups()
				.SelectMany(itemGroup => itemGroup.Children()
					.WhereNameIs(Instances.ElementNames.COMReference))
				.Any();

			return hasAnyCOMReferences;
        }

		public WasFound<string> HasRootNamespace(XElement projectElement)
		{
			var hasRootNamespace = Internal.HasPropertyGroupChildElementValue(projectElement,
				Instances.ElementNames.RootNamespace);

			return hasRootNamespace;
		}

		public WasFound<string> HasTargetFramework(XElement projectElement)
        {
			var hasTargetFramework = Internal.HasPropertyGroupChildElementValue(projectElement,
				Instances.ElementNames.TargetFramework);

			return hasTargetFramework;
		}

		public WasFound<string> HasTargetFrameworkVersion(XElement projectElement)
		{
			var hasTargetFramework = Internal.HasPropertyGroupChildElementValue(projectElement,
				Instances.ElementNames.TargetFrameworkVersion);

			return hasTargetFramework;
		}

		public void SetTargetFramework(XElement projectElement, string targetFrameworkMonikerString)
		{
			Internal.SetPropertyGroupChildElementValue(projectElement,
				Instances.ElementNames.TargetFramework,
				targetFrameworkMonikerString);
		}

		public void SetSdk(XElement projectElement, string sdk)
        {
			var sdkAttribute = projectElement.Attributes()
				.Where(xAttribute => xAttribute.Name.LocalName == Instances.ElementNames.Sdk)
				.Single();


			sdkAttribute.Value = sdk;
        }
	}
}