using System;


namespace R5T.F0020
{
	public class ProjectFileOperator : IProjectFileOperator
	{
		#region Infrastructure

	    public static ProjectFileOperator Instance { get; } = new();

	    private ProjectFileOperator()
	    {
        }

	    #endregion
	}
}