using System;

using R5T.T0131;


namespace R5T.F0020
{
	[DraftValuesMarker]
	public partial interface IWarnings : IDraftValuesMarker
	{
		/// <summary>
		/// Parameter 'parameter' has no matching param tag in the XML comment for 'parameter' (but other parameters do)
		/// See <see href="https://learn.microsoft.com/en-us/dotnet/csharp/misc/cs1573"/>
		/// </summary>
		public int CS1573 => 1573;
		/// <summary>
		/// XML comment is not placed on a valid language element
		/// See <see href="https://learn.microsoft.com/en-us/dotnet/csharp/misc/cs1587"/>
		/// </summary>
		public int CS1587 => 1587;
		/// <summary>
		/// Missing XML comment for publicly visible type or member 'Type_or_Member'
		/// See <see href="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-messages/cs1591"/>
		/// </summary>
		public int CS1591 => 1591;
	}
}