using System;
using System.Collections.Generic;
using System.Xml.Linq;

using R5T.F0000;

using R5T.T0132;


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
				var childElement = Instances.XmlOperator.AddChild(targetFrameworkPropertyGroup, targetFrameworkPropertyGroupChildElementName);
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

		public XElement AddPropertyGroup(XElement projectElement)
		{
			var propertyGroup = Instances.XElementGenerator.CreatePropertyGroup();

			projectElement.Add(propertyGroup);

			return propertyGroup;
		}

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

		public string GetPackageTagsTokenSeparator()
        {
			var tokenSeparator = Z0000.Instances.Strings.Semicolon;
			return tokenSeparator;
        }

		public WasFound<XElement> HasProjectReferencesItemGroup(XElement projectElement)
		{
			// Assume just one project references item group.
			var wasFound = projectElement.HasElement(Instances.ProjectElementRelativeXPaths.ItemGroupWithProjectReference);
			return wasFound;
		}

		public WasFound<XElement> HasTargetFrameworkPropertyGroup(XElement projectElement)
		{
			// Assume just one project references item group.
			var wasFound = projectElement.HasElement(Instances.ProjectElementRelativeXPaths.PropertyGroupWithProjectReference);
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

		public void SetAuthors(XElement projectElement, string authorsString)
        {
			this.SetPackagePropertyGroupChildElementValue(
				projectElement,
				Instances.ElementNames.Authors,
				authorsString);
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

			this.SetMainPropertyGroupChildElementValue(projectElement,
				Instances.ElementNames.ItemGroup,
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
			var valueString = F0000.Instances.BooleanOperator.ToString_Camelcase(requireLicenseAcceptance);

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

		public string GetTargetFramework(XElement projectElement)
		{
			var hasTargetFramework = this.HasTargetFramework(projectElement);
			if(!hasTargetFramework)
            {
				throw new Exception("Unable to find target framework element in project.");
            }

			return hasTargetFramework;
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

		public void SetTargetFramework(XElement projectElement, string targetFrameworkMonikerString)
		{
			Internal.SetPropertyGroupChildElementValue(projectElement,
				Instances.ElementNames.TargetFramework,
				targetFrameworkMonikerString);
		}
	}
}