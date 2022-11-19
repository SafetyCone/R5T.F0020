using System;


namespace R5T.F0020
{
	public class ProjectSdkStrings : IProjectSdkStrings
	{
		#region Infrastructure

	    public static IProjectSdkStrings Instance { get; } = new ProjectSdkStrings();

	    private ProjectSdkStrings()
	    {
        }

	    #endregion
	}
}