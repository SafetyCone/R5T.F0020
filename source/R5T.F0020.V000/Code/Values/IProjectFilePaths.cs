using System;

using R5T.T0131;


namespace R5T.F0020.V000
{
	[ValuesMarker]
	public partial interface IProjectFilePaths : IValuesMarker
	{
		//public string ForTestingOutput => @"C:\Temp\Console Project.csproj";
		public string ForTestingOutput => Environment.CurrentDirectory + "\\" + @"Files\Project.csproj";

		public string F0002 => @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.F0002\source\R5T.F0002\R5T.F0002.csproj";
		public string F0020 => @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.F0020\source\R5T.F0020\R5T.F0020.csproj";
		public string Magyar => @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.Magyar\source\R5T.Magyar\R5T.Magyar.csproj";
		public string T0132 => @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.T0132\source\R5T.T0132\R5T.T0132.csproj";
		public string Z0008 => @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.Z0008\source\R5T.Z0008\R5T.Z0008.csproj";
	}
}