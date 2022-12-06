using System;
using System.Xml.Linq;

using R5T.T0132;


namespace R5T.F0020
{
	[FunctionalityMarker]
	public partial interface ISupportedPlatformsOperations : IFunctionalityMarker
	{
		public bool Is_ForBrowser(string supportedPlaform)
		{
			var isForBrowser = SupportedPlatforms.Instance.Browser == supportedPlaform;
			return isForBrowser;
		}

        public bool Is_ForBrowser(XElement supportedPlatformElement)
		{
            var includeAttributeValue = ProjectXmlOperator.Instance.GetIncludeAttributeValue(
                supportedPlatformElement);

            var isForBrowser = this.Is_ForBrowser(includeAttributeValue);
			return isForBrowser;
        }
    }
}