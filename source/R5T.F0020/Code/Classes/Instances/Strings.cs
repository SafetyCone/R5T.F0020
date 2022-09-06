using System;


namespace R5T.F0020
{
	public class Strings : IStrings
	{
		#region Infrastructure

	    public static Strings Instance { get; } = new();

	    private Strings()
	    {
        }

	    #endregion
	}
}