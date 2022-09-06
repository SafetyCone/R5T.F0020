using System;
using System.Xml.Linq;

using R5T.Magyar;

using R5T.T0132;


namespace R5T.F0020
{
	[FunctionalityMarker]
	public partial interface IProjectXDocumentOperator : IFunctionalityMarker
	{
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

        public bool IsLibraryProject(XDocument projectXDocument)
        {
            // If project does *not* have an output type element, then it is a class library.
            var hasOutputTypeElement = Instances.ProjectFileXPathOperator.HasOutputTypeElement(projectXDocument);
            if (!hasOutputTypeElement)
            {
                return true;
            }

            var outputTypeElement = hasOutputTypeElement.Result;

            var outputTypeValue = outputTypeElement.Value;

            var isLibrary = outputTypeValue == Instances.Strings.LibraryOutputTypeValue;
            return isLibrary;
        }
	}
}