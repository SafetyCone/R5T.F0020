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

		public XElement CreateProject(bool selfClosing = true)
        {
			var projectElement = this.CreateProject(
				Instances.ProjectSdkStrings.NET,
				selfClosing);

			return projectElement;
        }

		public XElement CreatePropertyGroup()
		{
			var itemGroup = Instances.XmlOperator.CreateElement(Instances.ElementNames.PropertyGroup);
			return itemGroup;
		}

		public XElement CreateOutputType()
		{
			var itemGroup = Instances.XmlOperator.CreateElement(Instances.ElementNames.OutputType);
			return itemGroup;
		}
	}
}