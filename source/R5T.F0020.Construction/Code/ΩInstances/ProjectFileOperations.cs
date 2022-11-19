using System;


namespace R5T.F0020.Construction
{
	public class ProjectFileOperations : IProjectFileOperations
	{
		#region Infrastructure

	    public static IProjectFileOperations Instance { get; } = new ProjectFileOperations();

	    private ProjectFileOperations()
	    {
        }

	    #endregion
	}
}