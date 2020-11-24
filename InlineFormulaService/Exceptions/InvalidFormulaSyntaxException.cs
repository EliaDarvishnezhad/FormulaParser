using System;

namespace InlineFormulaService.Exceptions
{
	public class InvalidFormulaSyntaxException : InvalidOperationException
	{
		public const string DefaultErrorMessage = "Cannot parse Inline Formula due to Incorrect Syntax";

		public InvalidFormulaSyntaxException() :
			this(DefaultErrorMessage)
		{

		}

		public InvalidFormulaSyntaxException(string message) :
			base(string.Format("{0}\r\n{1}", DefaultErrorMessage, message ?? string.Empty))
		{

		}
	}
}
