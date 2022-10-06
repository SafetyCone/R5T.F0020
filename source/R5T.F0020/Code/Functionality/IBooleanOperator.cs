using System;

using R5T.T0132;


namespace R5T.F0020
{
	[FunctionalityMarker]
	public partial interface IBooleanOperator : IFunctionalityMarker
	{
		/// <summary>
		/// Returns the boolean representation needed for project files.
		/// </summary>
		public string ToString_ForProjectFile(bool value)
        {
			var representation = F0000.Instances.BooleanOperator.ToString_Lower(value);
			return representation;
        }
	}
}