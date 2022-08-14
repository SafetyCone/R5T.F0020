using System;


namespace R5T.F0020.N000
{
	public class ProjectFileXmlOperator : IProjectFileXmlOperator
	{
		#region Infrastructure

	    public static ProjectFileXmlOperator Instance { get; } = new();

	    private ProjectFileXmlOperator()
	    {
        }

	    #endregion
	}
}