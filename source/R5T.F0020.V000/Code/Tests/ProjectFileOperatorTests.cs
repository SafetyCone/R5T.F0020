using System;
using System.IO;

using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace R5T.F0020.V000
{
    [TestClass]
    public class ProjectFileOperatorTests
    {
        [TestMethod]
        public void AddProjectReference()
        {
            // Copy an example project file to the output location.
            File.Copy(
                Instances.ExampleFilePaths.ExampleConsole,
                Instances.ProjectFilePaths.ForTestingOutput,
                true);

            // Now add a project reference.
            Instances.ProjectFileOperator.AddProjectReference_Synchronous(
                Instances.ProjectFilePaths.ForTestingOutput,
                Instances.ProjectFilePaths.Z0008);

            // Now test equality.
            Instances.FileEqualityVerifier.VerifyFileByteLevelEquality(
                Instances.ProjectFilePaths.ForTestingOutput,
                Instances.ExampleFilePaths.ExampleConsole_WithF0020ProjectReference);
        }

        [TestMethod]
        public void ListProjectReferences()
        {
            var projectFilePath = Instances.ProjectFilePaths.F0020;

            var expectedProjectReferenceFilePaths = new[]
            {
                Instances.ProjectFilePaths.F0002,
                Instances.ProjectFilePaths.Magyar,
                Instances.ProjectFilePaths.T0132,
            };

            var actual = Instances.ProjectFileOperator.GetDirectProjectReferenceFilePaths_Synchronous(projectFilePath);

            Instances.Assertion.AreEqual_ForArray(actual, expectedProjectReferenceFilePaths);
        }
    }
}
