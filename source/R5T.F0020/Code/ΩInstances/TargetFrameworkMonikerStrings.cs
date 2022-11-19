using System;


namespace R5T.F0020
{
	public class TargetFrameworkMonikerStrings : ITargetFrameworkMonikerStrings
	{
		#region Infrastructure

	    public static ITargetFrameworkMonikerStrings Instance { get; } = new TargetFrameworkMonikerStrings();

	    private TargetFrameworkMonikerStrings()
	    {
        }

	    #endregion
	}
}