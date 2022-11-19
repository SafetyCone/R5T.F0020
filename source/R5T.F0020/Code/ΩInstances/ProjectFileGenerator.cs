using System;


namespace R5T.F0020
{
	public class ProjectFileGenerator : IProjectFileGenerator
	{
		#region Infrastructure

	    public static IProjectFileGenerator Instance { get; } = new ProjectFileGenerator();

	    private ProjectFileGenerator()
	    {
        }

	    #endregion
	}
}