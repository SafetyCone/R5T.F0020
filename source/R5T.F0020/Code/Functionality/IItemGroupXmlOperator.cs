using System;
using System.Xml.Linq;

using R5T.T0132;


namespace R5T.F0020
{
	[FunctionalityMarker]
	public partial interface IItemGroupXmlOperator : IFunctionalityMarker
	{
		public void AddPackageReference(XElement projectReferencesItemGroup,
			string packageIdentity,
			string version)
		{
			var projectReferenceElement = XElementGenerator.Instance.CreatePackageReferenceElement(
				packageIdentity,
				version);

			projectReferencesItemGroup.Add(projectReferenceElement);
		}

		public void AddProjectReference(XElement projectReferencesItemGroup,
			string projectDirectoryRelativeProjectFilePath)
		{
			var projectReferenceElement = XElementGenerator.Instance.CreateProjectReferenceElement(
				projectDirectoryRelativeProjectFilePath);

			projectReferencesItemGroup.Add(projectReferenceElement);
		}
	}
}