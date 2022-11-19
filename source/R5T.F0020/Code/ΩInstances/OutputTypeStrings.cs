using System;


namespace R5T.F0020
{
	public class OutputTypeStrings : IOutputTypeStrings
	{
		#region Infrastructure

	    public static IOutputTypeStrings Instance { get; } = new OutputTypeStrings();

	    private OutputTypeStrings()
	    {
        }

	    #endregion
	}
}