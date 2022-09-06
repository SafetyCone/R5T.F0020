using System;


namespace R5T.F0020
{
	public class ProjectXDocumentOperator : IProjectXDocumentOperator
	{
		#region Infrastructure

	    public static ProjectXDocumentOperator Instance { get; } = new();

	    private ProjectXDocumentOperator()
	    {
        }

	    #endregion
	}
}