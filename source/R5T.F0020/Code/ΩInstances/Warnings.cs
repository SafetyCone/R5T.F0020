using System;


namespace R5T.F0020
{
    [Obsolete("See R5T.Z0048.")]
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