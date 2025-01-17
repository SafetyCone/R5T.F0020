using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

using R5T.Extensions;
using R5T.L0089.T000;
using R5T.T0132;
using R5T.T0152.N001;

using R5T.F0020.Extensions;


namespace R5T.F0020
{
    [FunctionalityMarker]
	public partial interface IProjectXmlOperator : IFunctionalityMarker
	{
#pragma warning disable IDE1006 // Naming Styles
        public Internal.IProjectXmlOperator _Internal => Internal.ProjectXmlOperator.Instance;
#pragma warning restore IDE1006 // Naming Styles


        public XElement AcquireOutputTypeElement(XElement projectElement)
		{
			var outputTypeElement = _Internal.AcquirePropertyGroupChildElement(
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

        public XElement AcquireSupportedPlatformItemGroup(XElement projectElement)
        {
            var wasFound = this.HasSupportedPlatformItemGroup(projectElement);
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
			var mainPropertyGroup = _Internal.AcquirePropertyGroupWithChildElement(projectElement,
				Instances.ElementNames.TargetFramework);

			return mainPropertyGroup;
        }

		/// <summary>
		/// <inheritdoc cref="Documentation.PackagePropertyGroup" path="/summary"/>
		/// </summary>
		public XElement AcquirePackagePropertyGroup(XElement projectElement)
		{
			var packagePropertyGroup = _Internal.AcquirePropertyGroupChildElement(projectElement,
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

			var childWasFound = targetFrameworkPropertyGroup.HasChild(
				targetFrameworkPropertyGroupChildElementName,
				out var childElement);

			if (!childWasFound)
			{
				childElement = Instances.XElementOperator.Add_Child(
					targetFrameworkPropertyGroup,
					targetFrameworkPropertyGroupChildElementName);

				return childElement;
			}

			return childElement;
		}

		public XElement AddItemGroup(XElement projectElement)
		{
			var itemGroup = Instances.XElementGenerator.CreateItemGroup();

			projectElement.Add(itemGroup);

			return itemGroup;
		}

        public void AddProjectReferences_Idempotent(
			XElement projectElement,
			string projectFilePath,
			IEnumerable<string> referenceProjectFilePaths)
        {
            var projectDirectoryRelativeProjectReferenceFilePaths = Instances.ProjectPathsOperator.GetProjectDirectoryRelativePaths(
                projectFilePath,
                referenceProjectFilePaths);

			this.AddProjectReferences_Idempotent(
				projectElement,
				projectDirectoryRelativeProjectReferenceFilePaths.Values);
        }

        public void AddProjectReferences_Idempotent(
            XElement projectElement,
            string projectFilePath,
            params string[] referenceProjectFilePaths)
		{
			this.AddProjectReferences_Idempotent(
				projectElement,
				projectFilePath,
				referenceProjectFilePaths.AsEnumerable());
		}

        /// <inheritdoc cref="Documentation.SetProjectReferences"/>
        public void SetProjectReferences(
            XElement projectElement,
            string projectFilePath,
            IEnumerable<string> referenceProjectFilePaths)
        {
            var projectDirectoryRelativeProjectReferenceFilePaths = Instances.ProjectPathsOperator.GetProjectDirectoryRelativePaths(
                projectFilePath,
                referenceProjectFilePaths);

            this.SetProjectReferences(
                projectElement,
                projectDirectoryRelativeProjectReferenceFilePaths.Values);
        }

        /// <inheritdoc cref="Documentation.SetProjectReferences"/>
        public void SetProjectReferences(
            XElement projectElement,
            string projectFilePath,
            params string[] referenceProjectFilePaths)
        {
            this.SetProjectReferences(
                projectElement,
                projectFilePath,
                referenceProjectFilePaths.AsEnumerable());
        }

        public void AddProjectReferences_Idempotent(
            XElement projectElement,
            IEnumerable<string> projectDirectoryRelativeProjectFilePaths)
        {
            var hasReferences = this.HasProjectReferenceElements(
                projectElement,
                projectDirectoryRelativeProjectFilePaths);

            var projectReferencesItemGroup = this.AcquireProjectReferencesItemGroup(projectElement);

            foreach (var pair in hasReferences)
            {
                if (pair.Value.Is_NotFound())
                {
                    var projectDirectoryRelativeProjectFilePath = pair.Key;

                    ItemGroupXmlOperator.Instance.AddProjectReference(
                        projectReferencesItemGroup,
                        projectDirectoryRelativeProjectFilePath);
                }
            }
        }

        /// <inheritdoc cref="Documentation.SetProjectReferences"/>
        public void SetProjectReferences(
            XElement projectElement,
            IEnumerable<string> projectDirectoryRelativeProjectFilePaths)
        {
            var projectReferencesItemGroup = this.AcquireProjectReferencesItemGroup(projectElement);

			// Remove all.
			projectReferencesItemGroup.RemoveNodes();

			// Add new.
            foreach (var projectDirectoryRelativeProjectFilePath in projectDirectoryRelativeProjectFilePaths)
            {
                ItemGroupXmlOperator.Instance.AddProjectReference(
                    projectReferencesItemGroup,
                    projectDirectoryRelativeProjectFilePath);
            }
        }

        public void AddPackageReference_Idempotent(XElement projectElement,
			string packageIdentity,
			Version version)
        {
			var versionString = Instances.VersionOperator.ToString_Major_Minor_Build(version);

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

		public void AddBrowserSupportedPlatform(XElement projectElement)
		{
			var alreadyHasSupportedPlatform = this.HasSupportedPlatformElement(projectElement);
			if(alreadyHasSupportedPlatform)
			{
				var supportedPlatformElement = alreadyHasSupportedPlatform.Result;

				var supportedPlatformIsBrowser = this.IncludeAttributeValueIs(
					supportedPlatformElement,
					SupportedPlatforms.Instance.Browser);

				if(!supportedPlatformIsBrowser)
				{
					var includeAttributeValue = this.GetIncludeAttributeValueOrNull(supportedPlatformElement);

					throw new Exception($"Will not overwrite supported platform with value \"{SupportedPlatforms.Instance.Browser}\". Existing supported platform value found: {includeAttributeValue}.");
				}

				// Else, already set.
				return;
			}

			var supportedPlatformItemGroup = this.AcquireSupportedPlatformItemGroup(projectElement);

            ItemGroupXmlOperator.Instance.AddSupportedPlatform(supportedPlatformItemGroup,
                SupportedPlatforms.Instance.Browser);
        }

		public void AddPackageReferences_Idempotent(XElement projectElement,
			IEnumerable<PackageReference> packageReferences)
		{
			// Short-circuit if already present.
			var hasPackageReferenceByPackageReference = this.HasPackageReferenceElements(
				projectElement,
				packageReferences);

			var packageReferencesItemGroup = this.AcquirePackageReferencesItemGroup(projectElement);

			foreach (var pair in hasPackageReferenceByPackageReference)
			{
				if (pair.Value.Is_NotFound())
				{
					var packageReference = pair.Key;

					ItemGroupXmlOperator.Instance.AddPackageReference(
						packageReferencesItemGroup,
						packageReference);
				}
			}
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

		///// <inheritdoc cref="IXElementGenerator.CreateProject(bool)"/>
		//public XElement CreateNew()
  //      {
		//	var output = Instances.XElementGenerator.CreateProject();
		//	return output;
  //      }

  //      /// <summary>
  //      /// <inheritdoc cref="IXElementGenerator.NewProjectElement"/>
		///// Then runs the provided action.
  //      /// </summary>
  //      public async Task<XElement> CreateNewProjectElement(
		//	Func<XElement, Task> projectElementAction = default)
  //      {
  //          var output = Instances.XElementGenerator.NewProjectElement();

		//	await ActionOperator.Instance.Run(
		//		projectElementAction,
		//		output);

  //          return output;
  //      }

		public XElement CreateProjectElement(
			params Action<XElement>[] modifiers)
		{
			var projectElement = this.CreateProjectElement(
				modifiers.AsEnumerable());

			return projectElement;
		}

        public XElement CreateProjectElement(
            IEnumerable<Action<XElement>> modifiers)
        {
            var projectElement = Instances.ConstructionOperator.Create(
                ProjectXmlOperations.Instance.NewProjectElement,
                modifiers);

            return projectElement;
        }

        public async Task<XElement> CreateProjectElement(
            Func<Task<XElement>> constructor,
            params Func<XElement, Task>[] modifiers)
        {
			var projectElement = await constructor();

			await Instances.ActionOperator.Run(
				projectElement,
				modifiers);

			return projectElement;
        }

        public async Task<XElement> CreateProjectElement(
            Func<XElement> constructor,
            params Func<XElement, Task>[] modifiers)
        {
            var projectElement = constructor();

            await Instances.ActionOperator.Run(
                projectElement,
                modifiers);

            return projectElement;
        }

        public XElement CreateProjectElement(
            Func<XElement> constructor,
            params Action<XElement>[] modifiers)
        {
			var projectElement = this.CreateProjectElement(
				constructor,
				modifiers.AsEnumerable());

			return projectElement;
        }

        public XElement CreateProjectElement(
            Func<XElement> constructor,
            IEnumerable<Action<XElement>> modifiers)
        {
            var projectElement = constructor();

            Instances.ActionOperator.Run_Actions(
                projectElement,
                modifiers);

            return projectElement;
        }

        public string GetAuthorsTokenSeparator()
        {
			var tokenSeparator = Instances.Strings.Comma;
			return tokenSeparator;
        }

		public IEnumerable<XElement> GetItemGroups(XElement projectElement)
        {
			var output = projectElement.Enumerate_Children()
				.Where_NameIs(Instances.ElementNames.ItemGroup)
				;

			return output;
        }

		public string GetPackageTagsTokenSeparator()
        {
			var tokenSeparator = Instances.Strings.Semicolon;
			return tokenSeparator;
        }

		public WasFound<XElement> HasSupportedPlatformElement(XElement projectElement)
		{
			var hasSupportedPlatformItemGroup = this.HasSupportedPlatformItemGroup(projectElement);
			if(!hasSupportedPlatformItemGroup)
			{
                return WasFound.NotFound<XElement>();
            }

            var hasSupportedPlatform = this.HasSupportedPlatform_ForSupportedPlatformItemGroup(
                hasSupportedPlatformItemGroup.Result);

            return hasSupportedPlatform;
        }

        public bool HasFrameworkReference(XElement projectElement,
            string frameworkName)
        {
            var hasFrameworkReferencesItemGroup = this.HasFrameworkReferencesItemGroup(projectElement);
            if (!hasFrameworkReferencesItemGroup)
            {
                return WasFound.NotFound<XElement>();
            }

            var output = this.HasFrameworkReferenceElement_ForFrameworkReferencesItemGroup(
                hasFrameworkReferencesItemGroup.Result,
                frameworkName);

            return output.Exists;
        }

        public Dictionary<PackageReference, WasFound<XElement>> HasPackageReferenceElements(
			XElement projectElement,
			IEnumerable<PackageReference> packageReferences)
		{
			var hasPackageReferencesItemGroup = this.HasPackageReferencesItemGroup(projectElement);
			if (!hasPackageReferencesItemGroup)
			{
				// None were found.
				return packageReferences
					.ToDictionary(
						x => x,
						x => WasFound.NotFound<XElement>());
			}

			var packageReferencesItemGroup = hasPackageReferencesItemGroup.Result;

			var output = packageReferences
				.ToDictionary(
					x => x,
					x => this.HasPackageReferenceElement_ForPackageReferencesItemGroup(
						packageReferencesItemGroup,
						x));

			return output;
		}

        public bool HasPackageReferenceElement(XElement projectElement,
            string packageIdentity)
		{
            var hasPackageReferencesItemGroup = this.HasPackageReferencesItemGroup(projectElement);
            if (!hasPackageReferencesItemGroup)
            {
                return WasFound.NotFound<XElement>();
            }

            var output = this.HasPackageReferenceElement_ForPackageReferencesItemGroup(
                hasPackageReferencesItemGroup.Result,
                packageIdentity);

            return output;
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

        public Dictionary<string, WasFound<XElement>> HasProjectReferenceElements(
			XElement projectElement,
            IEnumerable<string> projectDirectoryRelativeProjectFilePaths)
        {
            var hasProjectReferencesItemGroup = this.HasProjectReferencesItemGroup(projectElement);
            if (!hasProjectReferencesItemGroup)
            {
				// None were found.
                return projectDirectoryRelativeProjectFilePaths
                    .ToDictionary(
                        x => x,
                        x => WasFound.NotFound<XElement>());
            }

            var projectReferencesItemGroup = hasProjectReferencesItemGroup.Result;

            var output = projectDirectoryRelativeProjectFilePaths
                .ToDictionary(
                    x => x,
                    x => this.HasProjectReferenceElement_ForProjectReferencesItemGroup(
                        projectReferencesItemGroup,
                        x));

            return output;
        }

		public WasFound<XElement> HasSupportedPlatform_ForSupportedPlatformItemGroup(XElement supportedPlatformItemGroup)
		{
            var elementOrDefault = supportedPlatformItemGroup.Elements()
                .Where_NameIs(Instances.ElementNames.SupportedPlatform)
                .SingleOrDefault()
                ;

            var output = WasFound.From(elementOrDefault);
            return output;
        }

        public WasFound<XElement> HasFrameworkReferenceElement_ForFrameworkReferencesItemGroup(XElement frameworkReferencesItemGroup,
            string frameworkName)
        {
            var elementOrDefault = frameworkReferencesItemGroup.Elements()
                .Where_NameIs(Instances.ElementNames.FrameworkReference)
                .Where(element => this.IncludeAttributeValueIs(
                    element,
                    frameworkName))
                .SingleOrDefault()
                ;

            var output = WasFound.From(elementOrDefault);
            return output;
        }

        public WasFound<XElement> HasPackageReferenceElement_ForPackageReferencesItemGroup(XElement packageReferencesItemGroup,
            string packageIdentity)
        {
            var elementOrDefault = packageReferencesItemGroup.Elements()
                .Where_NameIs(Instances.ElementNames.PackageReference)
                .Where(element => this.IsPackageReferenceTo(
                    element,
                    packageIdentity))
                .SingleOrDefault()
                ;

            var output = WasFound.From(elementOrDefault);
            return output;
        }

        public WasFound<XElement> HasPackageReferenceElement_ForPackageReferencesItemGroup(XElement packageReferencesItemGroup,
			string packageIdentity,
			string version)
        {
			var elementOrDefault = packageReferencesItemGroup.Elements()
				.Where_NameIs(Instances.ElementNames.PackageReference)
				.Where(element => this.IsPackageReferenceTo(
					element,
					packageIdentity,
					version))
				.SingleOrDefault()
				;

			var output = WasFound.From(elementOrDefault);
			return output;
		}

		public WasFound<XElement> HasPackageReferenceElement_ForPackageReferencesItemGroup(XElement packageReferencesItemGroup,
			PackageReference packageReference)
		{
			var elementOrDefault = packageReferencesItemGroup.Elements()
				.Where_NameIs(Instances.ElementNames.PackageReference)
				.Where(element => this.IsPackageReferenceTo(
					element,
					packageReference))
				.SingleOrDefault()
				;

			var output = WasFound.From(elementOrDefault);
			return output;
		}

		public WasFound<XElement> HasProjectReferenceElement_ForProjectReferencesItemGroup(XElement projectReferencesItemGroup,
			string projectDirectoryRelativeProjectFilePath)
		{
			var elementOrDefault = projectReferencesItemGroup.Elements()
				.Where_NameIs(Instances.ElementNames.ProjectReference)
				.Where(element => this.IsProjectReferenceTo(
					element,
					projectDirectoryRelativeProjectFilePath))
				.SingleOrDefault()
				;

			var output = WasFound.From(elementOrDefault);
			return output;
		}

        public WasFound<XElement> HasSupportedPlatformItemGroup(XElement projectElement)
        {
            // Assume just one package item group.
            var exists = projectElement.HasChildWithChild_Single(
                Instances.ElementNames.ItemGroup,
                Instances.ElementNames.SupportedPlatform,
				out var result);

			var output = WasFound.From(
				exists,
				result);

			return output;
        }

        public WasFound<XElement> HasFrameworkReferencesItemGroup(XElement projectElement)
        {
            // Assume just one package item group.
            var exists = projectElement.HasChildWithChild_Single(
                Instances.ElementNames.ItemGroup,
                Instances.ElementNames.FrameworkReference,
                out var result);

            var output = WasFound.From(
                exists,
                result);

            return output;
        }

        public WasFound<XElement> HasPackageReferencesItemGroup(XElement projectElement)
        {
			// Assume just one package item group.
			var exists = projectElement.HasChildWithChild_Single(
				Instances.ElementNames.ItemGroup,
				Instances.ElementNames.PackageReference,
                out var result);

            var output = WasFound.From(
                exists,
                result);

            return output;
        }

		public WasFound<XElement> HasProjectReferencesItemGroup(XElement projectElement)
		{
			// Assume just one project references item group.
			var exists = projectElement.HasChildWithChild_Single(
				Instances.ElementNames.ItemGroup,
				Instances.ElementNames.ProjectReference,
                out var result);

            var output = WasFound.From(
                exists,
                result);

            return output;
        }

		public WasFound<XElement> HasTargetFrameworkPropertyGroup(XElement projectElement)
		{
			// Assume just one project references item group.
			var exists = projectElement.HasChildWithChild_Single(
				Instances.ElementNames.PropertyGroup,
				Instances.ElementNames.TargetFramework,
                out var result);

            var output = WasFound.From(
                exists,
                result);

            return output;
        }

		public void SetAuthors(XElement projectElement, string[] authors)
        {
			var authorsTokenSeparator = this.GetAuthorsTokenSeparator();

			var authorsString = Instances.StringOperator.Join(
				authorsTokenSeparator,
				authors);

			this.SetAuthors(
				projectElement,
				authorsString);
        }

		public string GetIncludeAttributeValueOrNull(XElement elementWithIncludeAttribute)
		{
            var includeAttributeValue = elementWithIncludeAttribute.Attribute(AttributeNames.Instance.Include)?.Value;
			return includeAttributeValue;
        }

		public bool IncludeAttributeValueIs(XElement elementWithIncludeAttribute,
			string value)
		{
			var includeAttributeValue = this.GetIncludeAttributeValueOrNull(elementWithIncludeAttribute);

			var output = includeAttributeValue == value;
			return output;
        }

        public bool IsPackageReferenceTo(XElement element,
            string packageIdentity)
        {
            var includeAttributeValue = element.Attribute(AttributeNames.Instance.Include)?.Value;

            var output = includeAttributeValue == packageIdentity;
            return output;
        }

        public bool IsPackageReferenceTo(XElement element,
			string packageIdentity,
			string version)
        {
			var isPackageReferenceToPackage = this.IsPackageReferenceTo(element,
				packageIdentity);

			if(!isPackageReferenceToPackage)
			{
				return false;
			}

			var versionAttributeValue = element.Attribute(AttributeNames.Instance.Include)?.Value;

			var output = versionAttributeValue == version;
			return output;
		}

		public bool IsPackageReferenceTo(XElement element,
			PackageReference packageReference)
		{
			var output = this.IsPackageReferenceTo(element,
				packageReference.Identity,
				packageReference.Version);

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
        public void SetDisabledWarnings(
			XElement projectElement,
			IEnumerable<int> warnings)
		{
			var valueString = Instances.ProjectOperator.GetWarningsConcatentation(warnings);

			this.SetMainPropertyGroupChildElementValue(projectElement,
				Instances.ElementNames.NoWarn,
				valueString);
		}

		/// <inheritdoc cref="SetDisabledWarnings(XElement, IEnumerable{int})"/>
		public void SetDisabledWarnings(
			XElement projectElement,
			params int[] warnings)
			=> this.SetDisabledWarnings(
				projectElement,
				warnings.AsEnumerable());


        public void SetGenerateDocumentationFile(XElement projectElement, bool value)
		{
			var valueString = Instances.BooleanOperator.ToString_ForProjectFile(value);

			// Put the property in the packages property group since the documentation file is generated by default in Visual Studio, but not during packaging.
			// Thus the generate documentation file property is only relevant to the packaging process, and so should go with the other packaging properties.
			this.SetPackagePropertyGroupChildElementValue(projectElement,
				Instances.ElementNames.GenerateDocumentationFile,
				valueString);
		}

		public WasFound<XElement> HasMainPropertyGroupChildElement(XElement projectElement,
            string mainPropertyGroupChildElementName)
		{
			var wasFound = _Internal.HasPropertyGroupChildElement(
				projectElement,
				Instances.ElementNames.TargetFramework,
				mainPropertyGroupChildElementName);

			return wasFound;
		}

		/// <summary>
		/// <inheritdoc cref="Documentation.MainPropertyGroup" path="/summary"/>
		/// </summary>
		public void SetMainPropertyGroupChildElementValue(XElement projectElement,
			string mainPropertyGroupChildElementName,
			string value)
        {
			_Internal.SetPropertyGroupChildElementValue(projectElement,
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
			_Internal.SetPropertyGroupChildElementValue(projectElement,
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
			var valueString = Instances.BooleanOperator.ToString_PascalCase(requireLicenseAcceptance);

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

			var packageTagsString = Instances.StringOperator.Join(
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

		public bool Has_OutputType(
			XElement projectElement,
			out string outputType)
		{
            var hasOutputType = _Internal.HasPropertyGroupChildElementValue(projectElement,
                Instances.ElementNames.OutputType);

			outputType = hasOutputType.Result;

            return hasOutputType.Exists;
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
			var versionString = Instances.VersionOperator.ToString_Major_Minor_Build(version);

			this.SetVersion(projectElement, versionString);
        }

		public void SetVersion(XElement projectElement, string versionString)
        {
			this.SetPackagePropertyGroupChildElementValue(
				projectElement,
				Instances.ElementNames.Version,
				versionString);
        }

        public string Get_OutuputType(XElement projectElement)
        {
            var hasOutputType = this.Has_OutputType(projectElement);
            if (!hasOutputType)
            {
				// Return the default.
				return Instances.OutputTypeStrings.Library;
            }

            return hasOutputType;
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

		public string GetSdk(XElement projectElement)
		{
			var hasSdk = this.HasSdk(projectElement);

			var sdk = Instances.WasFoundOperator.Get_Result_OrExceptionIfNotFound(
				hasSdk,
				"SDK attribute not found.");

			return sdk;
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
				.SelectMany(itemGroup => itemGroup.Enumerate_Children()
					.Where_NameIs(Instances.ElementNames.COMReference))
				.Any();

			return hasAnyCOMReferences;
        }

        public WasFound<XElement> HasCheckEolTargetFrameworkElement(XElement projectElement)
        {
            var hasCheckEolTargetFrameworkElement = _Internal.HasPropertyGroupChildElement(projectElement,
                Instances.ElementNames.CheckEolTargetFramework);

            return hasCheckEolTargetFrameworkElement;
        }

        public WasFound<string> HasRootNamespace(XElement projectElement)
		{
			var hasRootNamespace = _Internal.HasPropertyGroupChildElementValue(projectElement,
				Instances.ElementNames.RootNamespace);

			return hasRootNamespace;
		}

        public WasFound<string> Has_OutputType(XElement projectElement)
        {
            var hasTargetFramework = _Internal.HasPropertyGroupChildElementValue(projectElement,
                Instances.ElementNames.OutputType);

            return hasTargetFramework;
        }

        public WasFound<string> HasTargetFramework(XElement projectElement)
        {
			var hasTargetFramework = _Internal.HasPropertyGroupChildElementValue(projectElement,
				Instances.ElementNames.TargetFramework);

			return hasTargetFramework;
		}

        public WasFound<string> Has_VersionString(XElement projectElement)
        {
            var has_VersionString = _Internal.HasPropertyGroupChildElementValue(projectElement,
                Instances.ElementNames.Version);

            return has_VersionString;
        }

        public WasFound<Version> Has_Version(XElement projectElement)
        {
			var has_VersionString = this.Has_VersionString(projectElement);

			var has_version = has_VersionString.Convert(
				Instances.VersionOperator.Parse);

            return has_version;
        }

        public WasFound<string> HasTargetFrameworkVersion(XElement projectElement)
		{
			var hasTargetFramework = _Internal.HasPropertyGroupChildElementValue(projectElement,
				Instances.ElementNames.TargetFrameworkVersion);

			return hasTargetFramework;
		}

		public void SetTargetFramework(XElement projectElement, string targetFrameworkMonikerString)
		{
			_Internal.SetPropertyGroupChildElementValue(projectElement,
				Instances.ElementNames.TargetFramework,
				targetFrameworkMonikerString);
		}

		public bool Has_UseWindowsForms(
			XElement projectElement,
			out bool value)
		{
            var hasTargetFramework = _Internal.HasPropertyGroupChildElementValue(projectElement,
                Instances.ElementNames.UseWindowsForms);

			value = hasTargetFramework
				? Instances.BooleanOperator.From(hasTargetFramework.Result)
				: false
				;

			return hasTargetFramework.Exists;
        }

        public void SetUseWindowsForms(XElement projectElement, bool value)
        {
			var valueString = BooleanOperator.Instance.ToString_ForProjectFile(value);

            this.SetMainPropertyGroupChildElementValue(projectElement,
                Instances.ElementNames.UseWindowsForms,
                valueString);
        }

        public XAttribute AcquireSdkAttribute(XElement projectElement)
		{
			var hasSdkAttribute = this.HasSdkAttribute(projectElement);

			var sdkAttribute = hasSdkAttribute
				? hasSdkAttribute.Result
				: this.AddSdkAttribute(projectElement)
				;

			return sdkAttribute;
		}

		public XAttribute AddSdkAttribute(XElement projectElement)
		{
			var sdkAttribute = XElementGenerator.Instance.NewSdkAttribute();

			projectElement.Add(sdkAttribute);

			return sdkAttribute;
		}

		public WasFound<XAttribute> HasSdkAttribute(XElement projectElement)
		{
            var sdkAttributeOrDefault = projectElement.Attributes()
                .Where(xAttribute => xAttribute.Name.LocalName == Instances.ElementNames.Sdk)
                .SingleOrDefault();

			var output = WasFound.From(sdkAttributeOrDefault);
			return output;
        }

		public void SetSdk(XElement projectElement, string sdk)
        {
			var sdkAttribute = this.AcquireSdkAttribute(projectElement);

			sdkAttribute.Value = sdk;
        }

		public WasFound<string> HasSdk(XElement projectElement)
		{
			var hasSdkAttribute = this.HasSdkAttribute(projectElement);
			if(!hasSdkAttribute)
			{
				return WasFound.NotFound<string>();
			}

			var sdkAttribute = hasSdkAttribute.Result;

			var sdk = sdkAttribute.Value;

			var output = WasFound.Found(sdk);
			return output;
		}

		public WasFound<XAttribute> HasIncludeAttribute(XElement includeAttributedElement)
		{
            var includeAttributeOrDefault = includeAttributedElement.Attributes()
                .Where(xAttribute => xAttribute.Name.LocalName == AttributeNames.Instance.Include)
                .SingleOrDefault();

            var output = WasFound.From(includeAttributeOrDefault);
            return output;
        }

        public XAttribute GetIncludeAttribute(XElement includeAttributedElement)
        {
            var hasIncludeAttribute = this.HasIncludeAttribute(includeAttributedElement);

            hasIncludeAttribute.Throw_ExceptionIfNotFound("Include attribute not found.");

			var includeAttribute = hasIncludeAttribute.Result;
			return includeAttribute;
        }

        public string GetIncludeAttributeValue(XElement includeAttributedElement)
		{
			var includeAttribute = this.GetIncludeAttribute(includeAttributedElement);

			var value = includeAttribute.Value;
			return value;
		}
	}
}