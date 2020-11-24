using Septa.PayamGostar.Domain.Model.Enumeration.BaseInfo.InlineFormula;
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

	public class UnexpectedEntryTokenException : InvalidFormulaSyntaxException
	{
		public const string UnexpectedEntryTokenDefaultErrorMessageFormat = "Unexpected \"{0}\" Entry Token \"{1}\" at Index \"{2}\" (Zero-Based).";

		public UnexpectedEntryTokenException(
			InlineFormulaEntryType entryType,
			string entryToken,
			int entryIndex)
			: base(string.Format(UnexpectedEntryTokenDefaultErrorMessageFormat, entryType, entryToken, entryIndex))
		{

		}
	}
}
