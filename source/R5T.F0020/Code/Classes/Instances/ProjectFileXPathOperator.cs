using System;


namespace R5T.F0020.N000
{
	public class ProjectFileXPathOperator : IProjectFileXPathOperator
	{
		#region Infrastructure

	    public static ProjectFileXPathOperator Instance { get; } = new();

	    private ProjectFileXPathOperator()
	    {
        }

	    #endregion
	}
}