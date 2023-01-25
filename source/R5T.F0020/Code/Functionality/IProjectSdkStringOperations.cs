using System;

using R5T.T0132;


namespace R5T.F0020
{
	[FunctionalityMarker]
	public partial interface IProjectSdkStringOperations : IFunctionalityMarker
	{
		public bool Is_RazorSdk(string projectSdkString)
		{
			var isRazorSdk = ProjectSdkStrings.Instance.Razor == projectSdkString;
			return isRazorSdk;
		}

		public bool Is_WebSdk(string projectSdkString)
		{
			var isWeb = ProjectSdkStrings.Instance.Web == projectSdkString;
			return isWeb;
		}
	}
}