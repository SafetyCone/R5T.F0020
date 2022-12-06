using System;


namespace R5T.F0020
{
	public class SupportedPlatformsOperations : ISupportedPlatformsOperations
	{
		#region Infrastructure

	    public static ISupportedPlatformsOperations Instance { get; } = new SupportedPlatformsOperations();

	    private SupportedPlatformsOperations()
	    {
        }

	    #endregion
	}
}