using System;

using R5T.F0000;
using R5T.F0002;
using R5T.F0040.F000;


namespace R5T.F0020
{
    public static class Instances
    {
        public static IBooleanOperator BooleanOperator { get; } = F0020.BooleanOperator.Instance;
        public static IElementNames ElementNames { get; } = F0020.ElementNames.Instance;
        public static IEnumerationHelper EnumerationHelper { get; } = F0002.EnumerationHelper.Instance;
        public static F0000.IFileSystemOperator FileSystemOperator { get; } = F0000.FileSystemOperator.Instance;
        public static IOutputTypeStrings OutputTypeStrings { get; } = F0020.OutputTypeStrings.Instance;
        public static F0002.IPathOperator PathOperator { get; } = F0002.PathOperator.Instance;
        public static IProjectElementRelativeXPaths ProjectElementRelativeXPaths { get; } = F0020.ProjectElementRelativeXPaths.Instance;
        public static IProjectFileGenerator ProjectFileGenerator { get; } = F0020.ProjectFileGenerator.Instance;
        public static IProjectFileOperator ProjectFileOperator { get; } = F0020.ProjectFileOperator.Instance;
        public static IProjectFileXDocumentOperator ProjectFileXDocumentOperator { get; } = F0020.ProjectFileXDocumentOperator.Instance;
        public static N000.IProjectFileXmlOperator ProjectFileXmlOperator { get; } = N000.ProjectFileXmlOperator.Instance;
        public static N000.IProjectFileXPathOperator ProjectFileXPathOperator { get; } = N000.ProjectFileXPathOperator.Instance;
        public static IProjectOperator ProjectOperator { get; } = F0020.ProjectOperator.Instance;
        public static IProjectPathsOperator ProjectPathsOperator { get; } = F0040.F000.ProjectPathsOperator.Instance;
        public static IProjectSdkStrings ProjectSdkStrings { get; } = F0020.ProjectSdkStrings.Instance;
        public static IProjectXmlOperations ProjectXmlOperations { get; } = F0020.ProjectXmlOperations.Instance;
        public static IProjectXmlOperator ProjectXmlOperator { get; } = F0020.ProjectXmlOperator.Instance;
        public static IStrings Strings { get; } = F0020.Strings.Instance;
        public static ITargetFrameworkMonikerStrings TargetFrameworkMonikerStrings { get; } = F0020.TargetFrameworkMonikerStrings.Instance;
        public static IWarnings Warnings { get; } = F0020.Warnings.Instance;
        public static IXDocumentRelativeXPaths XDocumentRelativeXPaths { get; } = F0020.XDocumentRelativeXPaths.Instance;
        public static IXElementGenerator XElementGenerator { get; } = F0020.XElementGenerator.Instance;
        public static F0002.IXmlOperator XmlOperator { get; } = F0002.XmlOperator.Instance;
    }
}