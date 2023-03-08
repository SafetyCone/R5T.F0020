using System;
using System.Xml.Linq;

using R5T.F0000;
using R5T.T0132;


namespace R5T.F0020
{
    [FunctionalityMarker]
	public partial interface IProjectFileXDocumentOperator : IFunctionalityMarker
	{
        public XDocument GetProjectDocument(Project project)
        {
            var document = this.GetProjectDocument(project.Element);
            return document;
        }

        public XDocument GetProjectDocument(XElement projectElement)
        {
            var document = new XDocument(projectElement);
            return document;
        }
        
        public WasFound<string> HasVersionString(XDocument projectXDocument)
        {
            // If project does *not* have an output type element, then it is a class library.
            var hasVersionElement = Instances.ProjectFileXPathOperator.HasVersionElement(projectXDocument);
            if (!hasVersionElement)
            {
                return WasFound.NotFound<string>();
            }

            var versionElement = hasVersionElement.Result;

            var versionstring = versionElement.Value;

            var output = WasFound.Found(versionstring);
            return output;
        }

        public bool IsLibraryProject(XDocument projectDocument)
        {
            var projectElement = Instances.ProjectFileXmlOperator.GetProjectElement(projectDocument);

            // If project does *not* have an output type element, then it is a class library.
            var hasOutputTypeElement = Instances.ProjectFileXPathOperator.HasOutputTypeElement(projectDocument);
            if (!hasOutputTypeElement)
            {
                // Unless it is a Web SDK project.
                var sdk = Instances.ProjectXmlOperator.GetSdk(projectElement);

                var isWebSdk = Instances.ProjectSdkStringOperations.Is_WebSdk(sdk);

                if(isWebSdk)
                {
                    return false;
                }

                // Else, return true.
                return true;
            }

            var outputTypeElement = hasOutputTypeElement.Result;

            var outputTypeValue = outputTypeElement.Value;

            var isLibrary = outputTypeValue == Instances.Strings.LibraryOutputTypeValue;
            return isLibrary;
        }
	}
}