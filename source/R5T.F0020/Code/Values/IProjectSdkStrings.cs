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
		/// The base SDK. (Microsoft.NET.Sdk)
		/// </summary>
		public string NET => "Microsoft.NET.Sdk";

		/// <summary>
		/// The web SDK. (Microsoft.NET.Sdk.Web)
		/// </summary>
		public string Web => "Microsoft.NET.Sdk.Web";

        /// <summary>
        /// The Blazor WebAssembly SDK. (Microsoft.NET.Sdk.BlazorWebAssembly)
        /// </summary>
		public const string BlazorWebAssembly_Constant = "Microsoft.NET.Sdk.BlazorWebAssembly";

		/// <inheritdoc cref="BlazorWebAssembly_Constant"/>
        public string BlazorWebAssembly => IProjectSdkStrings.BlazorWebAssembly_Constant;

		/// <summary>
		/// The Razor SDK. (Microsoft.NET.Sdk.Razor)
		/// </summary>
		public string Razor => "Microsoft.NET.Sdk.Razor";
    }
}