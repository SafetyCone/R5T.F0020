using System;


namespace R5T.F0020
{
	public class ItemGroupXmlOperator : IItemGroupXmlOperator
	{
		#region Infrastructure

	    public static IItemGroupXmlOperator Instance { get; } = new ItemGroupXmlOperator();

	    private ItemGroupXmlOperator()
	    {
        }

	    #endregion
	}
}