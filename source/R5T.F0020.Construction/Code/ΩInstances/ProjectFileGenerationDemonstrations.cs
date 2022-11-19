using System;


namespace R5T.F0020.Construction
{
	public class ProjectFileGenerationDemonstrations : IProjectFileGenerationDemonstrations
	{
		#region Infrastructure

	    public static ProjectFileGenerationDemonstrations Instance { get; } = new();

	    private ProjectFileGenerationDemonstrations()
	    {
        }

	    #endregion
	}
}