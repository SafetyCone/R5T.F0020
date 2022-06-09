using System;


namespace R5T.F0020.Construction
{
    public static class Instances
    {
        public static IProjectFileGenerationDemonstrations ProjectFileGenerationDemonstrations { get; } = Construction.ProjectFileGenerationDemonstrations.Instance;
        public static IProjectFileOperatorDemonstrations ProjectFileOperatorDemonstrations { get; } = Construction.ProjectFileOperatorDemonstrations.Instance;

        public static IProjectFileGenerator ProjectFileGenerator { get; } = F0020.ProjectFileGenerator.Instance;
        public static IProjectFileOperator ProjectFileOperator { get; } = F0020.ProjectFileOperator.Instance;
    }
}