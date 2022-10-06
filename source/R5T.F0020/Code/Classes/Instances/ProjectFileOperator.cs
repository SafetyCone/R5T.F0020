using System;


namespace R5T.F0020
{
	public class ProjectFileOperator : IProjectFileOperator
	{
		#region Infrastructure

	    public static IProjectFileOperator Instance { get; } = new ProjectFileOperator();

	    private ProjectFileOperator()
	    {
        }

	    #endregion
	}
}