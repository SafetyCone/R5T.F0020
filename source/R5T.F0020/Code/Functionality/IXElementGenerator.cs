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
				project.SetValue(Z0000.Instances.Strings.Empty);
            }

			var sdk = new XAttribute(Instances.ElementNames.Sdk, sdkString);

			project.Add(sdk);

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
			var itemGroup = Instances.XElementOperator.CreateElement(Instances.ElementNames.PropertyGroup);
			return itemGroup;
		}

		public XElement CreateOutputType()
		{
			var itemGroup = Instances.XElementOperator.CreateElement(Instances.ElementNames.OutputType);
			return itemGroup;
		}
	}
}