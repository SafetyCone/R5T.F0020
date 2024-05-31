using System;

using R5T.T0132;


namespace R5T.F0020
{
	[FunctionalityMarker]
	public partial interface IBooleanOperator : IFunctionalityMarker,
		L0066.IBooleanOperator
	{
		/// <summary>
		/// Returns the boolean representation needed for project files, which is the values "true" or "false" (lower values).
		/// </summary>
		public string ToString_ForProjectFile(bool value)
        {
			var representation = Instances.BooleanOperator.ToString_Lower(value);
			return representation;
        }
	}
}