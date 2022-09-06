using System;

using R5T.F0000;
using R5T.F0002;


namespace R5T.F0020
{
    public static class Instances
    {
        public static IXDocumentRelativeXPaths XDocumentRelativeXPaths { get; } = F0020.XDocumentRelativeXPaths.Instance;
        public static IEnumerationHelper EnumerationHelper { get; } = F0002.EnumerationHelper.Instance;
        public static IFileSystemOperator FileSystemOperator { get; } = F0000.FileSystemOperator.Instance;
        public static IPathOperator PathOperator { get; } = F0002.PathOperator.Instance;
        public static IProjectFileGenerator ProjectFileGenerator { get; } = F0020.ProjectFileGenerator.Instance;
        public static IProjectFileOperator ProjectFileOperator { get; } = F0020.ProjectFileOperator.Instance;
        public static N000.IProjectFileXmlOperator ProjectFileXmlOperator { get; } = N000.ProjectFileXmlOperator.Instance;
        public static N000.IProjectFileXPathOperator ProjectFileXPathOperator { get; } = N000.ProjectFileXPathOperator.Instance;
        public static IProjectOperator ProjectOperator { get; } = F0020.ProjectOperator.Instance;
        public static IProjectXDocumentOperator ProjectXDocumentOperator { get; } = F0020.ProjectXDocumentOperator.Instance;
        public static IStrings Strings { get; } = F0020.Strings.Instance;
    }
}