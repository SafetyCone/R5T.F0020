using System;


namespace R5T.F0020
{
	public class ProjectFileGenerator : IProjectFileGenerator
	{
		#region Infrastructure

	    public static ProjectFileGenerator Instance { get; } = new();

	    private ProjectFileGenerator()
	    {
        }

	    #endregion
	}
}