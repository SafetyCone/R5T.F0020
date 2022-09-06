using System;


namespace R5T.F0020
{
	public class ProjectOperator : IProjectOperator
	{
		#region Infrastructure

	    public static ProjectOperator Instance { get; } = new();

	    private ProjectOperator()
	    {
        }

	    #endregion
	}
}