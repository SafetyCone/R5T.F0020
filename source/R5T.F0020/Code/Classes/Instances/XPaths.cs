using System;


namespace R5T.F0020
{
	public class XPaths : IXPaths
	{
		#region Infrastructure

	    public static IXPaths Instance { get; } = new XPaths();

	    private XPaths()
	    {
        }

	    #endregion
	}
}