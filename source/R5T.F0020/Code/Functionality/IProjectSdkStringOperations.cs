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
	}
}