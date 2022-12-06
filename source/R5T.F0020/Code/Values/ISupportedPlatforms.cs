using System;

using R5T.T0131;


namespace R5T.F0020
{
	[ValuesMarker]
	public partial interface ISupportedPlatforms : IValuesMarker
	{
		public string Browser => "browser";
	}
}