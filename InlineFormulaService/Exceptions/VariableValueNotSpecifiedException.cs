using System;

namespace InlineFormulaService.Exceptions
{
	public class VariableValueNotSpecifiedException : ArgumentException
	{
		public const string VariableValueNotSpecifiedMessageDefaultFormat = "There is no value present for variable \"0\".";

		public VariableValueNotSpecifiedException(string variableName)
			: base(string.Format(VariableValueNotSpecifiedMessageDefaultFormat, variableName))
		{
		}
	}
}
