using System;

using R5T.T0132;


namespace R5T.F0020.Construction
{
	[FunctionalityMarker]
	public partial interface ITry : IFunctionalityMarker
	{
		public void DetermineIfHasComReference()
        {
			var expectation =
				//Instances.ExpectationOperator.From(
				//	Instances.ExampleProjectFilePaths.WithCOMReference,
				//	true)
				Instances.ExpectationOperator.From(
					Instances.ExampleProjectFilePaths.WithoutCOMReference,
					false)
				;

			var hasAnyCOMReferences = ProjectFileOperator.Instance.HasAnyCOMReferences(
				expectation);

			expectation.Verify_OrThrow(hasAnyCOMReferences);
        }
	}
}