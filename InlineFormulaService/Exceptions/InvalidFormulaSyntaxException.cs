using System;

namespace InlineFormulaService.Exceptions
{
	public class InvalidFormulaSyntaxException : InvalidOperationException
	{
		public const string InvalidFormulaSyntaxDefaultErrorMessageFormat = "Cannot parse Inline Formula due to Incorrect Syntax.\r\n{0}";

		public InvalidFormulaSyntaxException(string message) :
			base(string.Format(InvalidFormulaSyntaxDefaultErrorMessageFormat, message ?? string.Empty))
		{

		}
	}
}
