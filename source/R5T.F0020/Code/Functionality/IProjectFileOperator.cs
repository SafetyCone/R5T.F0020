using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.XPath;

using R5T.Magyar;

using R5T.T0132;


namespace R5T.F0020
{
	[FunctionalityMarker]
	public partial interface IProjectFileOperator : IFunctionalityMarker
	{
        public async Task AddProjectReference(
            string projectFilePath,
            string projectReferenceFilePath)
        {
            var xProjectFile = await Instances.ProjectFileXmlOperator.LoadProjectFile(projectFilePath);

            var projectDirectoryRelativeProjectReferenceFilePath = this.GetProjectDirectoryRelativePath(
                projectFilePath,
                projectReferenceFilePath);

            Instances.ProjectFileXPathOperator.AddProjectReference(
                xProjectFile,
                projectDirectoryRelativeProjectReferenceFilePath);

            await Instances.ProjectFileXmlOperator.SaveProjectFile(
                projectFilePath,
                xProjectFile);
        }

        public void AddProjectReference_Synchronous(
           string projectFilePath,
           string projectReferenceFilePath)
        {
            var xProjectFile = Instances.ProjectFileXmlOperator.LoadProjectFile_Synchronous(projectFilePath);

            var projectDirectoryRelativeProjectReferenceFilePath = this.GetProjectDirectoryRelativePath(
                projectFilePath,
                projectReferenceFilePath);

            Instances.ProjectFileXPathOperator.AddProjectReference(
                xProjectFile,
                projectDirectoryRelativeProjectReferenceFilePath);

            Instances.ProjectFileXmlOperator.SaveProjectFile_Synchronous(
                projectFilePath,
                xProjectFile);
        }

        public void Create_New(string projectFilePath, ProjectType projectType)
        {
            Instances.ProjectFileGenerator.CreateNew(projectFilePath, projectType);
        }

        public async Task<string[]> GetDirectProjectReferenceFilePaths(string projectFilePath)
        {
            var projectReferenceXDocumentRelativeXPath = "//Project/ItemGroup/ProjectReference";
            var projectReferenceIncludeAttributeName = "Include";

            var projectDirectoryPath = this.GetProjectDirectoryPath(projectFilePath);

            var projectXDocument = await Instances.ProjectFileXmlOperator.LoadProjectFile(projectFilePath);

            var projectReferenceXElements = projectXDocument.XPathSelectElements(projectReferenceXDocumentRelativeXPath);

            var projectReferenceProjectDirectoryRelativeFilePaths = projectReferenceXElements
                .Select(xElement => xElement.Attribute(projectReferenceIncludeAttributeName).Value)
                .ToArray();

            var output = projectReferenceProjectDirectoryRelativeFilePaths
                .Select(relativeFilePath =>
                {
                    var unresolvedPath = projectDirectoryPath + relativeFilePath;

                    var resolvedPath = Instances.PathOperator.ResolvePath(unresolvedPath);
                    return resolvedPath;
                })
                .ToArray();

            return output;
        }

        public string[] GetDirectProjectReferenceFilePaths_Synchronous(string projectFilePath)
        {
            var projectReferenceXDocumentRelativeXPath = "//Project/ItemGroup/ProjectReference";
            var projectReferenceIncludeAttributeName = "Include";

            var projectDirectoryPath = this.GetProjectDirectoryPath(projectFilePath);

            var projectXDocument = Instances.ProjectFileXmlOperator.LoadProjectFile_Synchronous(projectFilePath);

            var projectReferenceXElements = projectXDocument.XPathSelectElements(projectReferenceXDocumentRelativeXPath);

            var projectReferenceProjectDirectoryRelativeFilePaths = projectReferenceXElements
                .Select(xElement => xElement.Attribute(projectReferenceIncludeAttributeName).Value)
                .ToArray();

            var output = projectReferenceProjectDirectoryRelativeFilePaths
                .Select(relativeFilePath =>
                {
                    var unresolvedPath = projectDirectoryPath + relativeFilePath;

                    var resolvedPath = Instances.PathOperator.ResolvePath(unresolvedPath);
                    return resolvedPath;
                })
                .ToArray();

            return output;
        }

        public string GetProjectDirectoryPath(string projectFilePath)
        {
            var projectDirectoryPath = Instances.PathOperator.GetParentDirectoryPath_ForFile(projectFilePath);
            return projectDirectoryPath;
        }

        public string GetProjectDirectoryRelativePath(
            string projectFilePath,
            string path)
        {
            var projectDirectoryPath = this.GetProjectDirectoryPath(projectFilePath);

            var projectDirectoryRelativeFilePath = Instances.PathOperator.GetRelativePath(
                projectDirectoryPath,
                path);

            return projectDirectoryRelativeFilePath;
        }

        /// <summary>
        /// Gets the version specified by the project file, or if none is specified, the value provided by <see cref="IProjectOperator.GetDefaultVersion"/> (1.0.0).
        /// </summary>
        public Version GetVersionOrDefault(string projectFilePath)
        {
            var hasVersion = this.HasVersion(projectFilePath);

            var output = hasVersion.ResultOrIfNotFound(
                () => Instances.ProjectOperator.GetDefaultVersion());

            return output;
        }

        public bool HasProjectReference(
            string projectFilePath,
            string projectReferenceFilePath)
        {
            var xmlProjectFile = Instances.ProjectFileXmlOperator.LoadProjectFile_Synchronous(projectFilePath);

            var projectDirectoryRelativeProjectReferenceFilePath = this.GetProjectDirectoryRelativePath(
                projectFilePath,
                projectReferenceFilePath);

            var output = Instances.ProjectFileXPathOperator.HasProjectReferenceElement(
                xmlProjectFile,
                projectDirectoryRelativeProjectReferenceFilePath);

            return output;
        }

        public WasFound<Version> HasVersion(string projectFilePath)
        {
            var hasVersionString = Instances.ProjectFileXmlOperator.InProjectFileXDocumentContext_Synchronous(
                projectFilePath,
                Instances.ProjectXDocumentOperator.HasVersionString);

            var output = hasVersionString.Convert(versionString => Version.Parse(versionString));

            return output;
        }

        /// <summary>
        /// Determines whether a project file is a library (as opposed to a an executable).
        /// </summary>
        public bool IsLibrary_Synchronous(string projectFilePath)
        {
            var output = Instances.ProjectFileXmlOperator.InProjectFileXDocumentContext_Synchronous(
                projectFilePath,
                Instances.ProjectXDocumentOperator.IsLibraryProject);

            return output;
        }

        /// <summary>
        /// Eases construction of a new <see cref="FileStream"/> for reading.
        /// </summary>
        public FileStream NewRead(string filePath)
        {
            var fileStream = new FileStream(filePath, FileMode.Open);
            return fileStream;
        }

        public void RemoveProjectReference_Synchronous(
            string projectFilePath,
            string projectReferenceFilePath)
        {
            var xProjectFile = Instances.ProjectFileXmlOperator.LoadProjectFile_Synchronous(projectFilePath);

            var projectDirectoryRelativeProjectReferenceFilePath = this.GetProjectDirectoryRelativePath(
                projectFilePath,
                projectReferenceFilePath);

            Instances.ProjectFileXPathOperator.RemoveProjectReference(
                xProjectFile,
                projectDirectoryRelativeProjectReferenceFilePath);

            Instances.ProjectFileXmlOperator.SaveProjectFile_Synchronous(
                projectFilePath,
                xProjectFile);
        }
    }
}