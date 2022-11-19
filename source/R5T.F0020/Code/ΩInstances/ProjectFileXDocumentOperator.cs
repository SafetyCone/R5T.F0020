using System;


namespace R5T.F0020
{
	public class ProjectFileXDocumentOperator : IProjectFileXDocumentOperator
	{
		#region Infrastructure

	    public static IProjectFileXDocumentOperator Instance { get; } = new ProjectFileXDocumentOperator();

	    private ProjectFileXDocumentOperator()
	    {
        }

	    #endregion
	}
}