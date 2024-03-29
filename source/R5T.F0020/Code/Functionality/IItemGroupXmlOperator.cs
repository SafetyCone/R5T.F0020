using System;
using System.Xml.Linq;

using R5T.T0132;
using R5T.T0152.N001;


namespace R5T.F0020
{
	[FunctionalityMarker]
	public partial interface IItemGroupXmlOperator : IFunctionalityMarker
	{
        public void AddSupportedPlatform(XElement supportedPlatformItemGroup,
            string supportedPlatform)
        {
            var projectReferenceElement = XElementGenerator.Instance.CreateSupportedPlatformElement(
                supportedPlatform);

            supportedPlatformItemGroup.Add(projectReferenceElement);
        }

        public void AddPackageReference(XElement projectReferencesItemGroup,
			string packageIdentity,
			string version)
		{
			var projectReferenceElement = XElementGenerator.Instance.CreatePackageReferenceElement(
				packageIdentity,
				version);

			projectReferencesItemGroup.Add(projectReferenceElement);
		}

		public void AddPackageReference(XElement projectReferencesItemGroup,
			PackageReference packageReference)
		{
			this.AddPackageReference(projectReferencesItemGroup,
				packageReference.Identity,
				packageReference.Version);
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