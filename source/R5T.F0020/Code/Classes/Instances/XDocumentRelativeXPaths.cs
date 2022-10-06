using System;


namespace R5T.F0020
{
	public class XDocumentRelativeXPaths : IXDocumentRelativeXPaths
	{
		#region Infrastructure

	    public static IXDocumentRelativeXPaths Instance { get; } = new XDocumentRelativeXPaths();

	    private XDocumentRelativeXPaths()
	    {
        }

	    #endregion
	}
}