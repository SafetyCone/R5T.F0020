using System;


namespace R5T.F0020
{
	public class ProjectSdkStringOperations : IProjectSdkStringOperations
	{
		#region Infrastructure

	    public static IProjectSdkStringOperations Instance { get; } = new ProjectSdkStringOperations();

	    private ProjectSdkStringOperations()
	    {
        }

	    #endregion
	}
}