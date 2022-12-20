using System;

using R5T.T0131;


namespace R5T.F0020
{
	/// <summary>
	/// See: <see href="https://learn.microsoft.com/en-us/dotnet/standard/frameworks"/>
	/// And for what frameworks support what frameworks: https://learn.microsoft.com/en-us/dotnet/standard/net-standard?tabs=net-standard-2-0#select-net-standard-version
	/// </summary>
	[ValuesMarker]
	public partial interface ITargetFrameworkMonikerStrings : IValuesMarker
    {
		/// <summary>
		/// When you need a target framework moniker, but don't want to think about it.
		/// Chooses <see cref="NET_6"/> as the default.
		/// </summary>
		public string Default => this.NET_6;
		/// <summary>
		/// When you want to use the standard target framework moniker.
		/// /// Chooses <see cref="NET_6"/> as the standard.
		/// </summary>
		public string Standard => this.NET_6;


        public string NET_6 => "net6.0";
        public string NET_6_Windows => "net6.0-windows";
        public string NET_5 => "net5.0";
		/// <summary>
		/// Useful if you want to allow old .NET Framework (4.6.2 and higher) support.
		/// </summary>
		public string NET_Standard_2_0 => "netstandard2.0";
		/// <summary>
		/// Note: as of this version of .NET Standard, the old .NET framework is no longer supported.
		/// </summary>
		public string NET_Standard2_1 => "netstandard2.1";
		public string NET_Core3_1 => "netcoreapp3.1";
		public string NET_Framework4_8 => "net48";
    }
}