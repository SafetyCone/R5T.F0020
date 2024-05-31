using System;
using System.Xml.Linq;

using R5T.T0132;


namespace R5T.F0020
{
	[FunctionalityMarker]
	public partial interface IXElementGenerator : IFunctionalityMarker
	{
		public XElement CreateItemGroup()
		{
			var itemGroup = new XElement(Instances.ElementNames.ItemGroup);
			return itemGroup;
		}

		/// <summary>
		/// Creates a new project with the specified SDK.
		/// </summary>
		public XElement CreateProject(string sdkString, bool selfClosing = true)
        {
			var project = new XElement(Instances.ElementNames.Project);

			if(!selfClosing)
            {
				// Adding an empty string of content will make the *not* self-closing.
				project.SetValue(Instances.Strings.Empty);
            }

			var sdkAttribute = this.NewSdkAttribute(sdkString);

			project.Add(sdkAttribute);

			return project;
        }

		/// <summary>
		/// Creates a new project with the <see cref="IProjectSdkStrings.NET"/> SDK.
		/// </summary>
		public XElement CreateProject(bool selfClosing = true)
        {
			var projectElement = this.CreateProject(
				Instances.ProjectSdkStrings.NET,
				selfClosing);

			return projectElement;
        }

        public XElement CreateSupportedPlatformElement(
            string supportedPlatform)
        {
            var includeAttribute = new XAttribute(AttributeNames.Instance.Include, supportedPlatform);

            var output = new XElement(ElementNames.Instance.SupportedPlatform);

            output.Add(includeAttribute);

            return output;
        }

        public XElement CreatePackageReferenceElement(
			string packageIdentity,
			string version)
		{
			var includeAttribute = new XAttribute(AttributeNames.Instance.Include, packageIdentity);
			var versionAttribute = new XAttribute(AttributeNames.Instance.Version, version);

			var output = new XElement(ElementNames.Instance.PackageReference);

			output.Add(
				includeAttribute,
				versionAttribute);

			return output;
		}

		public XElement CreateProjectReferenceElement(
			string projectDirectoryRelativeProjectFilePath)
		{
			var projectDirectoryRelativeProjectFilePathWindows = projectDirectoryRelativeProjectFilePath.Replace('/', '\\');

			var includeAttribute = new XAttribute(AttributeNames.Instance.Include, projectDirectoryRelativeProjectFilePathWindows);

			var output = new XElement(ElementNames.Instance.ProjectReference);

			output.Add(includeAttribute);

			return output;
		}

		public XElement CreatePropertyGroup()
		{
			var itemGroup = Instances.XElementOperator.Create_Element_FromName(Instances.ElementNames.PropertyGroup);
			return itemGroup;
		}

		public XElement CreateOutputType()
		{
			var itemGroup = Instances.XElementOperator.Create_Element_FromName(Instances.ElementNames.OutputType);
			return itemGroup;
		}

        public XAttribute NewSdkAttribute(string sdkString)
        {
            var sdkAttribute = new XAttribute(Instances.ElementNames.Sdk, sdkString);
			return sdkAttribute;
        }

        public XAttribute NewSdkAttribute()
		{
			var sdkAttribute = this.NewSdkAttribute(
				ProjectSdkStrings.Instance.NET);

			return sdkAttribute;
        }

		/// <summary>
		/// Creates a new project element with no attributes (not even the SDK).
		/// </summary>
		public XElement NewProjectElement()
		{
            var projectElement = new XElement(Instances.ElementNames.Project);
			return projectElement;
        }
	}
}