using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace R5T.F0020.V000
{
    [TestClass]
    public class ProjectFileGeneratorTests
    {
        [TestMethod]
        public void CreateConsoleProjectFile()
        {
            var projectFilePath = Instances.ProjectFilePaths.ForTestingOutput;

            var expectedProjectFilePath = Instances.ExampleFilePaths.ExampleConsoleProject;

            Instances.ProjectFileGenerator.CreateNewConsole(projectFilePath);

            Instances.FileEqualityVerifier.VerifyFileByteLevelEquality(
                projectFilePath,
                expectedProjectFilePath);
        }
    }
}
