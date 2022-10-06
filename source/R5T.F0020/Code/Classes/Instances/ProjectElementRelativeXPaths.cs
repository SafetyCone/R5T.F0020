using System;


namespace R5T.F0020
{
	public class ProjectElementRelativeXPaths : IProjectElementRelativeXPaths
	{
		#region Infrastructure

	    public static IProjectElementRelativeXPaths Instance { get; } = new ProjectElementRelativeXPaths();

	    private ProjectElementRelativeXPaths()
	    {
        }

	    #endregion
	}
}