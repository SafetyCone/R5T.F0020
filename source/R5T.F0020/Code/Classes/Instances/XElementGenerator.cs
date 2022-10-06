using System;


namespace R5T.F0020
{
	public class XElementGenerator : IXElementGenerator
	{
		#region Infrastructure

	    public static IXElementGenerator Instance { get; } = new XElementGenerator();

	    private XElementGenerator()
	    {
        }

	    #endregion
	}
}