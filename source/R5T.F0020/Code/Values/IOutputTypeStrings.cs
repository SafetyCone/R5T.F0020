using System;

using R5T.T0131;


namespace R5T.F0020
{
    /// <summary>
    /// Find "OutputType" in: <see href="https://learn.microsoft.com/en-us/visualstudio/msbuild/common-msbuild-project-properties?view=vs-2022"/>
    /// </summary>
    /// <remarks>
    /// Deprecated. See F10Y.Z0002.IOutputTypes.
    /// </remarks>
    [ValuesMarker]
	public partial interface IOutputTypeStrings : IValuesMarker
	{
		/// <summary>
		/// For class libraries.
		/// </summary>
		public string Library => "Library";

		/// <summary>
		/// Used for console applications (and if you want your WinForms or WPF application to show a console window).
		/// </summary>
		public string Exe => "Exe";

		/// <summary>
		/// Note really used? Unclear what a module is, and no good source online.
		/// </summary>
		public string Module => "Module";

		/// <summary>
		/// Used for WinForms and WPF applications.
		/// See also: <see href="https://learn.microsoft.com/en-us/dotnet/core/compatibility/sdk/5.0/automatically-infer-winexe-output-type"/>
		/// </summary>
		public string WinExe => "WinExe";
	}
}