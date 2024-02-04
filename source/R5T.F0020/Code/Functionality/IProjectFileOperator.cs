using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

using R5T.F0000;
using R5T.L0089.T000;
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
            var xProjectFile = await Instances.ProjectFileXmlOperator.LoadProjectDocument(projectFilePath);

            var projectDirectoryRelativeProjectReferenceFilePath = Instances.ProjectPathsOperator.GetProjectDirectoryRelativePath(
                projectFilePath,
                projectReferenceFilePath);

            Instances.ProjectFileXPathOperator.AddProjectReference_Idempotent(
                xProjectFile,
                projectDirectoryRelativeProjectReferenceFilePath);

            await Instances.ProjectFileXmlOperator.SaveProjectDocument(
                projectFilePath,
                xProjectFile);
        }

        public void AddProjectReference_Idempotent_Synchronous(
           string projectFilePath,
           string projectReferenceFilePath)
        {
            var xProjectFile = Instances.ProjectFileXmlOperator.LoadProjectDocument_Synchronous(projectFilePath);

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
            var xProjectFile = Instances.ProjectFileXmlOperator.LoadProjectDocument_Synchronous(projectFilePath);

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

        public void AddProjectReferences_Idempotent_Synchronous(
           string projectFilePath,
           params string[] projectReferenceFilePaths)
        {
            this.AddProjectReferences_Idempotent_Synchronous(
                projectFilePath,
                projectReferenceFilePaths.AsEnumerable());
        }

        public async Task AddProjectReferences_Idempotent(
           string projectFilePath,
           IEnumerable<string> projectReferenceFilePaths)
        {
            var xProjectFile = await Instances.ProjectFileXmlOperator.LoadProjectDocument(projectFilePath);

            var projectDirectoryRelativeProjectReferenceFilePaths = Instances.ProjectPathsOperator.GetProjectDirectoryRelativePaths(
                projectFilePath,
                projectReferenceFilePaths);

            Instances.ProjectFileXPathOperator.AddProjectReferences_Idempotent(
                xProjectFile,
                projectDirectoryRelativeProjectReferenceFilePaths.Values);

            await Instances.ProjectFileXmlOperator.SaveProjectFile(
                projectFilePath,
                xProjectFile);
        }

        public Task AddProjectReferences_Idempotent(
           string projectFilePath,
           params string[] projectReferenceFilePaths)
        {
            return this.AddProjectReferences_Idempotent(
                projectFilePath,
                projectReferenceFilePaths.AsEnumerable());
        }

        public async Task CreateProjectFile(
            string projectFilePath,
            Func<XElement> projectElementConstructor)
        {
            var projectElement = projectElementConstructor();

            await Instances.ProjectFileXmlOperator.SaveProjectElement(
                projectFilePath,
                projectElement);
        }

        public async Task CreateProjectFile(
            string projectFilePath,
            IEnumerable<Action<XElement>> projectElementActions)
        {
            var projectElement = ProjectXmlOperator.Instance.CreateProjectElement(
                projectElementActions);

            await Instances.ProjectFileXmlOperator.SaveProjectElement(
                projectFilePath,
                projectElement);
        }

        public void CreateProjectFile_Synchronous(
            string projectFilePath,
            IEnumerable<Action<XElement>> projectElementActions)
        {
            var projectElement = ProjectXmlOperator.Instance.CreateProjectElement(
                projectElementActions);

            Instances.ProjectFileXmlOperator.SaveProjectElement_Synchronous(
                projectFilePath,
                projectElement);
        }

        public void CreateProjectFile_Synchronous(
            string projectFilePath,
            params Action<XElement>[] projectElementActions)
        {
            this.CreateProjectFile_Synchronous(
                projectFilePath,
                projectElementActions.AsEnumerable());
        }

        public async Task CreateProjectFile(
            string projectFilePath,
            Func<string, IEnumerable<Action<XElement>>> projectElementActionsConstructor)
        {
            var projectElementActions = projectElementActionsConstructor(projectFilePath);

            await this.CreateProjectFile(
                projectFilePath,
                projectElementActions);
        }

        public async Task CreateProjectFile(
            string projectFilePath,
            Func<Task<XElement>> projectElementConstructor)
        {
            var projectElement = await projectElementConstructor();

            await Instances.ProjectFileXmlOperator.SaveProjectElement(
                projectFilePath,
                projectElement);
        }

        public async Task CreateProjectFile(
            string projectFilePath,
            Func<Task<XElement>> projectElementConstructor,
            params Func<XElement, Task>[] modifiers)
        {
            await this.CreateProjectFile(
                projectFilePath,
                ConstructionOperations.Instance.New(
                    projectElementConstructor,
                    modifiers));
        }

        public async Task<string[]> GetDirectProjectReferenceFilePaths(string projectFilePath)
        {
            var projectReferenceXDocumentRelativeXPath = "//Project/ItemGroup/ProjectReference";
            var projectReferenceIncludeAttributeName = "Include";

            var projectDirectoryPath = Instances.ProjectPathsOperator.GetProjectDirectoryPath(projectFilePath);

            var projectXDocument = await Instances.ProjectFileXmlOperator.LoadProjectDocument(projectFilePath);

            var projectReferenceXElements = projectXDocument.XPathSelectElements(projectReferenceXDocumentRelativeXPath);

            var projectReferenceProjectDirectoryRelativeFilePaths = projectReferenceXElements
                .Select(xElement => xElement.Attribute(projectReferenceIncludeAttributeName).Value)
                .ToArray();

            var output = projectReferenceProjectDirectoryRelativeFilePaths
                .Select(relativeFilePath =>
                {
                    var unresolvedPath = projectDirectoryPath + relativeFilePath;

                    var resolvedPath = Instances.PathOperator.Resolve_Path(unresolvedPath);
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

            var projectXDocument = Instances.ProjectFileXmlOperator.LoadProjectDocument_Synchronous(projectFilePath);

            var projectReferenceXElements = projectXDocument.XPathSelectElements(projectReferenceXDocumentRelativeXPath);

            var projectReferenceProjectDirectoryRelativeFilePaths = projectReferenceXElements
                .Select(xElement => xElement.Attribute(projectReferenceIncludeAttributeName).Value)
                .ToArray();

            var output = projectReferenceProjectDirectoryRelativeFilePaths
                .Select(relativeFilePath =>
                {
                    var unresolvedPath = projectDirectoryPath + relativeFilePath;

                    var resolvedPath = Instances.PathOperator.Resolve_Path(unresolvedPath);
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

            var defaultNamespace = hasDefaultNamespace.Get_Result_OrIfNotFound(
                F0040.F000.ProjectNamespacesOperator.Instance.Get_DefaultNamespaceName(projectFilePath));

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

        public string GetSdk(string projectFilePath)
        {
            var hasSdk = this.HasSdk(projectFilePath);
            if (!hasSdk)
            {
                throw new Exception("No SDK element found in project.");
            }

            return hasSdk;
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

        public bool HasAnyCOMReferences(string projectFilePath)
        {
            var hasAnyCOMReferences = this.InQueryProjectFileContext_Synchronous(projectFilePath,
                projectElement => Instances.ProjectXmlOperator.HasAnyCOMReferences(projectElement));

            return hasAnyCOMReferences;
        }

        public WasFound<string> HasDefaultNamespace(string projectFilePath)
        {
            var hasDefaultNamespace = this.InQueryProjectFileContext_Synchronous(projectFilePath,
                projectElement =>
                {
                    // Note: the default namespace in the Visual Studio UI is the <RootNamespace> element in the project file.
                    var hasRootNamespace = Instances.ProjectXmlOperator.HasRootNamespace(projectElement);
                    return hasRootNamespace;
                });

            return hasDefaultNamespace;
        }

        public WasFound<string> HasSdk(string projectFilePath)
        {
            var hasSdk = this.InQueryProjectFileContext_Synchronous(projectFilePath,
                projectElement =>
                {
                    var hasSdk = Instances.ProjectXmlOperator.HasSdk(projectElement);
                    return hasSdk;
                });

            return hasSdk;
        }

        public WasFound<string> HasTargetFramework(string projectFilePath)
        {
            var hasTargetFramework = this.InQueryProjectFileContext_Synchronous(projectFilePath,
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

            var output = hasVersion.Get_Result_OrIfNotFound(
                () => Instances.ProjectOperator.GetDefaultVersion());

            return output;
        }

        public bool HasProjectReference(
            string projectFilePath,
            string projectReferenceFilePath)
        {
            var xmlProjectFile = Instances.ProjectFileXmlOperator.LoadProjectDocument_Synchronous(projectFilePath);

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

        public async Task InModifyProjectFileContext(
            string projectFilePath,
            Action<XElement> projectElementModifier)
        {
            var projectElement = await this.Load(projectFilePath);

            projectElementModifier(projectElement);

            await this.Save(
                projectFilePath,
                projectElement);
        }

        public async Task InModifyProjectFileContext(
            string projectFilePath,
            Func<XElement, Task> projectElementModifier)
        {
            var projectElement = await this.Load(projectFilePath);

            await projectElementModifier(projectElement);

            await this.Save(
                projectFilePath,
                projectElement);
        }

        public void InModifyProjectFileContext_Synchronous(
            string projectFilePath,
            Action<XElement> projectElementModifier)
        {
            var projectElement = this.Load_Synchronous(projectFilePath);

            projectElementModifier(projectElement);

            this.Save_Synchronous(
                projectFilePath,
                projectElement);
        }

        public async Task<TOutput> InQueryProjectFileContext<TOutput>(
            string projectFilePath,
            Func<XElement, TOutput> projectElementFunction)
        {
            var projectDocument = await Instances.ProjectFileXmlOperator.LoadProjectDocument(projectFilePath);

            var projectElement = Instances.ProjectFileXPathOperator.GetProjectElement(projectDocument);

            var output = projectElementFunction(projectElement);
            return output;
        }

        public TOutput InQueryProjectFileContext_Synchronous<TOutput>(
            string projectFilePath,
            Func<XElement, TOutput> projectElementFunction)
        {
            var projectDocument = Instances.ProjectFileXmlOperator.LoadProjectDocument_Synchronous(projectFilePath);

            var projectElement = Instances.ProjectFileXPathOperator.GetProjectElement(projectDocument);

            var output = projectElementFunction(projectElement);
            return output;
        }

        public void InReadonlyProjectFileContext_Synchronous(
            string projectFilePath,
            Action<XElement> projectElementAction)
        {
            var projectDocument = Instances.ProjectFileXmlOperator.LoadProjectDocument_Synchronous(projectFilePath);

            var projectElement = Instances.ProjectFileXPathOperator.GetProjectElement(projectDocument);

            projectElementAction(projectElement);
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
            Instances.FileSystemOperator.Verify_File_Exists(possibleProjectFilePath);

            // Is the file an XML file?
            var isXml = Instances.XmlFileOperator.IsXmlFile(
                possibleProjectFilePath);

            if(!isXml)
            {
                return false;
            }

            // Does the XML file have a root Project element?
            var xmlDocument = Instances.XmlOperator.Load_XDocument_Synchronous(possibleProjectFilePath);

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

        public async Task<XElement> Load(string projectFilePath)
        {
            var projectElement = await N000.ProjectFileXmlOperator.Instance.LoadProjectElement(
                projectFilePath);

            return projectElement;
        }

        public XElement Load_Synchronous(string projectFilePath)
        {
            var projectElement = N000.ProjectFileXmlOperator.Instance.LoadProjectElement_Synchronous(
                projectFilePath);

            return projectElement;
        }

        public void RemoveProjectReference_Synchronous(
            string projectFilePath,
            string projectReferenceFilePath)
        {
            this.RemoveProjectReferences_Synchronous(
                projectFilePath,
                EnumerableOperator.Instance.From(projectReferenceFilePath));
        }

        public void RemoveProjectReferences_Synchronous(
            string projectFilePath,
            IEnumerable<string> projectReferenceFilePaths)
        {
            var projectDirectoryRelativeProjectReferenceFilePaths = projectReferenceFilePaths
                .Select(projectReferenceFilePath => Instances.ProjectPathsOperator.GetProjectDirectoryRelativePath(
                    projectFilePath,
                    projectReferenceFilePath))
                .Now();

            ProjectFileOperator.Instance.InModifyProjectFileContext_Synchronous(
                projectFilePath,
                projectElement =>
                {
                    Instances.ProjectXmlOperator.RemoveProjectReferences(
                        projectElement,
                        projectDirectoryRelativeProjectReferenceFilePaths);
                });
        }

        public async Task Save(
            string projectFilePath,
            XElement projectElement)
        {
            await N000.ProjectFileXmlOperator.Instance.SaveProjectElement(
                projectFilePath,
                projectElement);
        }

        public void Save_Synchronous(
            string projectFilePath,
            XElement projectElement)
        {
            N000.ProjectFileXmlOperator.Instance.SaveProjectElement_Synchronous(
                projectFilePath,
                projectElement);
        }

        public async Task SetCheckEolTargetFramework(
            string projectFilePath,
            bool value)
        {
            await ProjectFileOperator.Instance.InModifyProjectFileContext(
                projectFilePath,
                projectElement =>
                {
                    ProjectXmlOperator.Instance.SetCheckEolTargetFramework(
                        projectElement,
                        value);
                });
        }
    }
}