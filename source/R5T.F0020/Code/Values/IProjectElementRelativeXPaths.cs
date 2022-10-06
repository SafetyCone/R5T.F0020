using System;

using R5T.T0131;


namespace R5T.F0020
{
	[ValuesMarker]
	public partial interface IProjectElementRelativeXPaths : IValuesMarker
	{
		public string ItemGroupWithProjectReference => "./ItemGroup[ProjectReference]";
		public string PropertyGroupWithProjectReference => "./PropertyGroup[TargetFramework]";
	}
}