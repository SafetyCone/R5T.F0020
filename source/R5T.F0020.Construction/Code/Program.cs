using System;

namespace R5T.F0020.Construction
{
    class Program
    {
        static void Main()
        {
            // Demonstrations.
            //Instances.ProjectFileGenerationDemonstrations.CreateNew_Empty();
            //Instances.ProjectFileGenerationDemonstrations.CreateNew_Minimal();
            //Instances.ProjectFileGenerationDemonstrations.CreateNew_Standard();

            //Instances.ProjectFileGenerationDemonstrations.CreateNewConsole();
            //Instances.ProjectFileGenerationDemonstrations.CreateNewLibrary();
            //Instances.ProjectFileGenerationDemonstrations.CreateNewTest();
            //Instances.ProjectFileOperatorDemonstrations.AddProjectReference();
            //Instances.ProjectFileOperatorDemonstrations.HasProjectReference_False();
            //Instances.ProjectFileOperatorDemonstrations.HasProjectReference_True();
            //Instances.ProjectFileOperatorDemonstrations.ListDirectProjectReferenceFilePaths();
            //Instances.ProjectFileOperatorDemonstrations.RemoveProjectReference();
            //Instances.ProjectFileOperatorDemonstrations.TestIsLibraryProject();
            //Instances.ProjectFileOperatorDemonstrations.TestHasVersion();

            Instances.ProjectFileOperations.FindOldStyleCsprojFile();
        }
    }
}