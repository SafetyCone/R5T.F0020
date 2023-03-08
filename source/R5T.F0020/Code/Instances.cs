using System;


namespace R5T.F0020
{
    public static class Instances
    {
        public static IBooleanOperator BooleanOperator => F0020.BooleanOperator.Instance;
        public static IElementNames ElementNames => F0020.ElementNames.Instance;
        public static F0002.IEnumerationHelper EnumerationHelper => F0002.EnumerationHelper.Instance;
        public static F0000.IFileSystemOperator FileSystemOperator => F0000.FileSystemOperator.Instance;
        public static IOutputTypeStrings OutputTypeStrings => F0020.OutputTypeStrings.Instance;
        public static F0002.IPathOperator PathOperator => F0002.PathOperator.Instance;
        public static IProjectElementRelativeXPaths ProjectElementRelativeXPaths => F0020.ProjectElementRelativeXPaths.Instance;
        public static IProjectFileGenerator ProjectFileGenerator => F0020.ProjectFileGenerator.Instance;
        public static IProjectFileOperator ProjectFileOperator => F0020.ProjectFileOperator.Instance;
        public static IProjectFileXDocumentOperator ProjectFileXDocumentOperator => F0020.ProjectFileXDocumentOperator.Instance;
        public static N000.IProjectFileXmlOperator ProjectFileXmlOperator => N000.ProjectFileXmlOperator.Instance;
        public static N000.IProjectFileXPathOperator ProjectFileXPathOperator => N000.ProjectFileXPathOperator.Instance;
        public static IProjectOperator ProjectOperator => F0020.ProjectOperator.Instance;
        public static F0040.F000.IProjectPathsOperator ProjectPathsOperator => F0040.F000.ProjectPathsOperator.Instance;
        public static IProjectSdkStringOperations ProjectSdkStringOperations => F0020.ProjectSdkStringOperations.Instance;
        public static IProjectSdkStrings ProjectSdkStrings => F0020.ProjectSdkStrings.Instance;
        public static IProjectXmlOperations ProjectXmlOperations => F0020.ProjectXmlOperations.Instance;
        public static IProjectXmlOperator ProjectXmlOperator => F0020.ProjectXmlOperator.Instance;
        public static Internal.IProjectXmlOperator ProjectXmlOperator_Internal => Internal.ProjectXmlOperator.Instance;
        public static IStrings Strings => F0020.Strings.Instance;
        public static ITargetFrameworkMonikerStrings TargetFrameworkMonikerStrings => F0020.TargetFrameworkMonikerStrings.Instance;
        public static IWarnings Warnings => F0020.Warnings.Instance;
        public static F0000.IWasFoundOperator WasFoundOperator => F0000.WasFoundOperator.Instance;
        public static IXDocumentRelativeXPaths XDocumentRelativeXPaths => F0020.XDocumentRelativeXPaths.Instance;
        public static F0000.IXElementOperator XElementOperator => F0000.XElementOperator.Instance;
        public static IXElementGenerator XElementGenerator => F0020.XElementGenerator.Instance;
        public static F0000.IXmlFileOperator XmlFileOperator => F0000.XmlFileOperator.Instance;
        public static F0002.IXmlOperator XmlOperator => F0002.XmlOperator.Instance;
    }
}