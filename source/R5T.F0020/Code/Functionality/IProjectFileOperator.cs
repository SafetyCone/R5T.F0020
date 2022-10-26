using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

using R5T.F0000;
using R5T.T0132;


namespace R5T.F0020
{
    [FunctionalityMarker]
	public partial interface IProjectFileOperator : IFunctionalityMarker
	{
        public async Task AddProjectReference_Idempotent(
            string projectFilePath,
            string projectReferenceFilePath)
        {
            var xProjectFile = await Instances.ProjectFileXmlOperator.LoadProjectFile(projectFilePath);

            var projectDirectoryRelativeProjectReferenceFilePath = Instances.ProjectPathsOperator.GetProjectDirectoryRelativePath(
                projectFilePath,
                projectReferenceFilePath);

            Instances.ProjectFileXPathOperator.AddProjectReference_Idempotent(
                xProjectFile,
                projectDirectoryRelativeProjectReferenceFilePath);

            await Instances.ProjectFileXmlOperator.SaveProject(
                projectFilePath,
                xProjectFile);
        }

        public void AddProjectReference_Idempotent_Synchronous(
           string projectFilePath,
           string projectReferenceFilePath)
        {
            var xProjectFile = Instances.ProjectFileXmlOperator.LoadProjectFile_Synchronous(projectFilePath);

            var projectDirectoryRelativeProjectReferenceFilePath = Instances.ProjectPathsOperator.GetProjectDirectoryRelativePath(
                projectFilePath,
                projectReferenceFilePath);

            Instances.ProjectFileXPathOperator.AddProjectReference_Idempotent(
                xProjectFile,
                projectDirectoryRelativeProjectReferenceFilePath);

            Instances.ProjectFileXmlOperator.SaveProjectFile_Synchronous(
                projectFilePath,
                xProjectFile);
        }

        public void AddProjectReferences_Idempotent_Synchronous(
           string projectFilePath,
           IEnumerable<string> projectReferenceFilePaths)
        {
            var xProjectFile = Instances.ProjectFileXmlOperator.LoadProjectFile_Synchronous(projectFilePath);

            var projectDirectoryRelativeProjectReferenceFilePaths = Instances.ProjectPathsOperator.GetProjectDirectoryRelativePaths(
                projectFilePath,
                projectReferenceFilePaths);

            Instances.ProjectFileXPathOperator.AddProjectReferences_Idempotent(
                xProjectFile,
                projectDirectoryRelativeProjectReferenceFilePaths.Values);

            Instances.ProjectFileXmlOperator.SaveProjectFile_Synchronous(
                projectFilePath,
                xProjectFile);
        }

        public void Create_New(string projectFilePath, ProjectType projectType)
        {
            Instances.ProjectFileGenerator.CreateNew(projectFilePath, projectType);
        }

        public void Create(string projectFilePath,
            Action<XElement> projectElementModifer)
        {
            var project = Instances.ProjectOperator.CreateNew();

            project.Modify(projectElementModifer);

            Instances.ProjectFileXmlOperator.Save(
                projectFilePath,
                project);
        }

        public async Task<string[]> GetDirectProjectReferenceFilePaths(string projectFilePath)
        {
            var projectReferenceXDocumentRelativeXPath = "//Project/ItemGroup/ProjectReference";
            var projectReferenceIncludeAttributeName = "Include";

            var projectDirectoryPath = Instances.ProjectPathsOperator.GetProjectDirectoryPath(projectFilePath);

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

            var projectDirectoryPath = Instances.ProjectPathsOperator.GetProjectDirectoryPath(projectFilePath);

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

        /// <summary>
        /// Gets the default namespace from the project file, or throws if not found.
        /// Throws an <see cref="Exception"/> if the project file path does not have the <see cref="IElementNames.RootNamespace"/> element.
        /// </summary>
        public string GetDefaultNamespaceName_OrThrowIfNotFound(string projectFilePath)
        {
            var hasDefaultNamespace = this.HasDefaultNamespace(projectFilePath);
            if(!hasDefaultNamespace)
            {
                throw new Exception($"No default namespace ({ElementNames.Instance.RootNamespace} element) found in project.");
            }

            return hasDefaultNamespace;
        }

        /// <summary>
        /// Gets the default namespace from the project file, or throws if not found.
        /// Returns the default project namespace name (i.e. the project name) if the project file path does not have the <see cref="IElementNames.RootNamespace"/> element.
        /// </summary>
        public string GetDefaultNamespaceName_OrDefaultIfNotFound(string projectFilePath)
        {
            var hasDefaultNamespace = this.HasDefaultNamespace(projectFilePath);

            var defaultNamespace = hasDefaultNamespace.ResultOrIfNotFound(
                F0040.F000.ProjectNamespacesOperator.Instance.GetDefaultNamespaceName(projectFilePath));

            return defaultNamespace;
        }

        /// <summary>
        /// Chooses <see cref="GetDefaultNamespaceName_OrDefaultIfNotFound(string)"/> as the default.
        /// </summary>
        public string GetDefaultNamespaceName(string projectFilePath)
        {
            var output = this.GetDefaultNamespaceName_OrDefaultIfNotFound(projectFilePath);
            return output;
        }

        public string GetTargetFramework(string projectFilePath)
        {
            var hasTargetFramework = this.HasTargetFramework(projectFilePath);
            if(!hasTargetFramework)
            {
                throw new Exception("No target framework element found in project.");
            }

            return hasTargetFramework;
        }

        public WasFound<string> HasDefaultNamespace(string projectFilePath)
        {
            var hasDefaultNamespace = this.InQueryProjectFileContext(projectFilePath,
                projectElement =>
                {
                    // Note: the default namespace in the Visual Studio UI is the <RootNamespace> element in the project file.
                    var hasRootNamespace = Instances.ProjectXmlOperator.HasRootNamespace(projectElement);
                    return hasRootNamespace;
                });

            return hasDefaultNamespace;
        }

        public WasFound<string> HasTargetFramework(string projectFilePath)
        {
            var hasTargetFramework = this.InQueryProjectFileContext(projectFilePath,
                projectElement =>
                {
                    var hasTargetFramework = Instances.ProjectXmlOperator.HasTargetFramework(projectElement);
                    return hasTargetFramework;
                });

            return hasTargetFramework;
        }

        public string GetProjectName(string projectFilePath)
        {
            var projectName = Instances.ProjectOperator.GetProjectName(projectFilePath);
            return projectName;
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

            var projectDirectoryRelativeProjectReferenceFilePath = Instances.ProjectPathsOperator.GetProjectDirectoryRelativePath(
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
                Instances.ProjectFileXDocumentOperator.HasVersionString);

            var output = hasVersionString.Convert(versionString => Version.Parse(versionString));

            return output;
        }

        public void InModifyProjectFileContext(
            string projectFilePath,
            Action<XElement> projectElementModifier)
        {
            var projectDocument = Instances.ProjectFileXmlOperator.LoadProjectFile_Synchronous(projectFilePath);

            var projectElement = Instances.ProjectFileXPathOperator.GetProjectElement(projectDocument);

            projectElementModifier(projectElement);

            Instances.ProjectFileXmlOperator.SaveProjectFile_Synchronous(
                projectFilePath,
                projectDocument);
        }

        public TOutput InQueryProjectFileContext<TOutput>(
            string projectFilePath,
            Func<XElement, TOutput> projectElementFunction)
        {
            var projectDocument = Instances.ProjectFileXmlOperator.LoadProjectFile_Synchronous(projectFilePath);

            var projectElement = Instances.ProjectFileXPathOperator.GetProjectElement(projectDocument);

            var output = projectElementFunction(projectElement);
            return output;
        }

        /// <summary>
        /// Determines whether a project file is a library (as opposed to a an executable).
        /// </summary>
        public bool IsLibrary_Synchronous(string projectFilePath)
        {
            var output = Instances.ProjectFileXmlOperator.InProjectFileXDocumentContext_Synchronous(
                projectFilePath,
                Instances.ProjectFileXDocumentOperator.IsLibraryProject);

            return output;
        }

        /// <summary>
		/// Examines file context to determine if a file is a solution file.
		/// </summary>
        public bool IsProjectFile(string possibleProjectFilePath)
        {
            // File exists?
            Instances.FileSystemOperator.VerifyFileExists(possibleProjectFilePath);

            // Is the file an XML file?
            var isXml = Instances.XmlFileOperator.IsXmlFile(
                possibleProjectFilePath);

            if(!isXml)
            {
                return false;
            }

            // Does the XML file have a root Project element?
            var xmlDocument = Instances.XmlOperator.Load(possibleProjectFilePath);

            var hasProjectElement = Instances.ProjectFileXmlOperator.HasProjectElement(xmlDocument);
            if(!hasProjectElement)
            {
                return false;
            }

            var projectElement = hasProjectElement.Result;

            // Does the project element have a property group with a target framework element?
            var hasTargetElement = Instances.ProjectXmlOperator.HasTargetFramework(projectElement);
            if(!hasTargetElement)
            {
                // If no target element, does it have an (old-stype) target framework version element?
                var hasTargetElementVersion = Instances.ProjectXmlOperator.HasTargetFrameworkVersion(projectElement);
                if(!hasTargetElementVersion)
                {
                    return false;
                }
            }

            return true;
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

            var projectDirectoryRelativeProjectReferenceFilePath = Instances.ProjectPathsOperator.GetProjectDirectoryRelativePath(
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