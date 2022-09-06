using System;


namespace R5T.F0020
{
	public class XPaths : IXPaths
	{
		#region Infrastructure

	    public static XPaths Instance { get; } = new();

	    private XPaths()
	    {
        }

	    #endregion
	}
}