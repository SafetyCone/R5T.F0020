using System;


namespace R5T.F0020
{
	public class Warnings : IWarnings
	{
		#region Infrastructure

	    public static IWarnings Instance { get; } = new Warnings();

	    private Warnings()
	    {
        }

	    #endregion
	}
}