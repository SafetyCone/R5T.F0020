using System;

using R5T.T0131;


namespace R5T.F0020
{
	[ValuesMarker]
	public partial interface IElementNames : IValuesMarker
	{
		public string Authors => "Authors";
		public string CheckEolTargetFramework => "CheckEolTargetFramework";
        public string Company => "Company";
		public string COMReference => "COMReference";
		public string Copyright => "Copyright";
		public string Description => "Description";
		public string GenerateDocumentationFile => "GenerateDocumentationFile";
		public string ItemGroup => "ItemGroup";
		public string NoWarn => "NoWarn";
		public string OutputType => "OutputType";
		public string PackageLicenseExpression => "PackageLicenseExpression";
		public string PackageReadmeFile => "PackageReadmeFile";
		public string PackageReference => "PackageReference";
		public string PackageRequireLicenseAcceptance => "PackageRequireLicenseAcceptance";
		public string PackageTags => "PackageTags";
		public string Project => "Project";
		public string ProjectReference => "ProjectReference";
		public string PropertyGroup => "PropertyGroup";
		public string RepositoryUrl => "RepositoryUrl";
		public string RootNamespace => "RootNamespace";
		public string Sdk => "Sdk";
		public string SupportedPlatform => "SupportedPlatform";
		public string TargetFramework => "TargetFramework";
		public string TargetFrameworkVersion => "TargetFrameworkVersion";
		public string UseWindowsForms => "UseWindowsForms";
		public string Version => "Version";
	}
}