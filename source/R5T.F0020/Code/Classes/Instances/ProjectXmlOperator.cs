using System;


namespace R5T.F0020
{
	public class ProjectXmlOperator : IProjectXmlOperator
	{
		#region Infrastructure

	    public static IProjectXmlOperator Instance { get; } = new ProjectXmlOperator();

	    private ProjectXmlOperator()
	    {
        }

        #endregion
    }


    namespace Internal
    {
		public class ProjectXmlOperator : IProjectXmlOperator
		{
			#region Infrastructure

			public static IProjectXmlOperator Instance { get; } = new ProjectXmlOperator();

			private ProjectXmlOperator()
			{
			}

			#endregion
		}
	}
}