using System;


namespace R5T.F0020
{
	public class AttributeNames : IAttributeNames
	{
		#region Infrastructure

	    public static IAttributeNames Instance { get; } = new AttributeNames();

	    private AttributeNames()
	    {
        }

	    #endregion
	}
}