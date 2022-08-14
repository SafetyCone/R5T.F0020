using System;

using R5T.F0002;
using R5T.T0119;
using R5T.Z0008;


namespace R5T.F0020.V000
{
    public static class Instances
    {
        public static IAssertion Assertion { get; } = T0119.Assertion.Instance;
        public static IExampleFilePaths ExampleFilePaths { get; } = Z0008.ExampleFilePaths.Instance;
        public static IFileEqualityVerifier FileEqualityVerifier { get; } = F0002.FileEqualityVerifier.Instance;
        public static IProjectFileGenerator ProjectFileGenerator { get; } = F0020.ProjectFileGenerator.Instance;
        public static IProjectFileOperator ProjectFileOperator { get; } = F0020.ProjectFileOperator.Instance;
        public static IProjectFilePaths ProjectFilePaths { get; } = V000.ProjectFilePaths.Instance;
    }
}