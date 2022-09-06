using System;


namespace R5T.F0020
{
	public class XDocumentRelativeXPaths : IXDocumentRelativeXPaths
	{
		#region Infrastructure

	    public static XDocumentRelativeXPaths Instance { get; } = new();

	    private XDocumentRelativeXPaths()
	    {
        }

	    #endregion
	}
}