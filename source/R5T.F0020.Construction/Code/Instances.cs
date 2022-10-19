using System;

using R5T.F0000;
using R5T.F0032;
using R5T.F0033;
using R5T.Z0015;


namespace R5T.F0020.Construction
{
    public static class Instances
    {
        public static IProjectFileGenerationDemonstrations ProjectFileGenerationDemonstrations { get; } = Construction.ProjectFileGenerationDemonstrations.Instance;
        public static IProjectFileOperatorDemonstrations ProjectFileOperatorDemonstrations { get; } = Construction.ProjectFileOperatorDemonstrations.Instance;

        public static IFileOperator FileOperator { get; } = F0000.FileOperator.Instance;
        public static IFilePaths FilePaths { get; } = Z0015.FilePaths.Instance;
        public static IJsonOperator JsonOperator { get; } = F0032.JsonOperator.Instance;
        public static INotepadPlusPlusOperator NotepadPlusPlusOperator { get; } = F0033.NotepadPlusPlusOperator.Instance;
        public static IProjectFileGenerator ProjectFileGenerator { get; } = F0020.ProjectFileGenerator.Instance;
        public static IProjectFileOperations ProjectFileOperations { get; } = Construction.ProjectFileOperations.Instance;
        public static IProjectFileOperator ProjectFileOperator { get; } = F0020.ProjectFileOperator.Instance;
        public static IProjectFilePaths ProjectFilePaths { get; } = Construction.ProjectFilePaths.Instance;
    }
}