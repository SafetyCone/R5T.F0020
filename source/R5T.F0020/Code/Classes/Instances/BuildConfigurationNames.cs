using System;


namespace R5T.F0020
{
	public class BuildConfigurationNames : IBuildConfigurationNames
	{
		#region Infrastructure

	    public static IBuildConfigurationNames Instance { get; } = new BuildConfigurationNames();

	    private BuildConfigurationNames()
	    {
        }

	    #endregion
	}
}