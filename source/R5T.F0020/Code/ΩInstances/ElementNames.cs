using System;


namespace R5T.F0020
{
	public class ElementNames : IElementNames
	{
		#region Infrastructure

	    public static IElementNames Instance { get; } = new ElementNames();

	    private ElementNames()
	    {
        }

	    #endregion
	}
}