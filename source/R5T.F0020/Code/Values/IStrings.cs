using System;

using R5T.T0131;


namespace R5T.F0020
{
	[ValuesMarker]
	public partial interface IStrings : IValuesMarker,
		L0066.IStrings
	{
		public string LibraryOutputTypeValue => "Library";
	}
}