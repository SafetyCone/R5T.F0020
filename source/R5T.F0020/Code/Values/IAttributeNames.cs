using System;

using R5T.T0131;


namespace R5T.F0020
{
	[ValuesMarker]
	public partial interface IAttributeNames : IValuesMarker
	{
		public string Include => "Include";
		public string Version => "Version";
	}
}