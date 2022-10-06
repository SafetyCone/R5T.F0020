using System;

using R5T.T0131;


namespace R5T.F0020
{
	/// <summary>
	/// See: https://learn.microsoft.com/en-us/dotnet/core/project-sdk/overview
	/// </summary>
	[ValuesMarker]
	public partial interface IProjectSdkStrings : IValuesMarker
	{
		/// <summary>
		/// The base SDK ("Microsoft.NET.Sdk").
		/// </summary>
		public string NET => "Microsoft.NET.Sdk";
	}
}