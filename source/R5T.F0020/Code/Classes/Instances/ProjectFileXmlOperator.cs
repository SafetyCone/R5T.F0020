using System;


namespace R5T.F0020.N000
{
	public class ProjectFileXmlOperator : IProjectFileXmlOperator
	{
		#region Infrastructure

	    public static IProjectFileXmlOperator Instance { get; } = new ProjectFileXmlOperator();

	    private ProjectFileXmlOperator()
	    {
        }

	    #endregion
	}
}