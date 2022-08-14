using System;


namespace R5T.F0020.V000
{
	public class ProjectFilePaths : IProjectFilePaths
	{
		#region Infrastructure

	    public static ProjectFilePaths Instance { get; } = new();

	    private ProjectFilePaths()
	    {
        }

	    #endregion
	}
}