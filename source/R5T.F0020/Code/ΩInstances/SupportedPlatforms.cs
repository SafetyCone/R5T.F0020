using System;


namespace R5T.F0020
{
	public class SupportedPlatforms : ISupportedPlatforms
	{
		#region Infrastructure

	    public static ISupportedPlatforms Instance { get; } = new SupportedPlatforms();

	    private SupportedPlatforms()
	    {
        }

	    #endregion
	}
}