using System;


namespace R5T.F0020.Construction
{
	public class ProjectFileOperatorDemonstrations : IProjectFileOperatorDemonstrations
	{
		#region Infrastructure

	    public static ProjectFileOperatorDemonstrations Instance { get; } = new();

	    private ProjectFileOperatorDemonstrations()
	    {
        }

	    #endregion
	}
}