using Septa.PayamGostar.Domain.Model.Enumeration.BaseInfo.InlineFormula;

namespace InlineFormulaService.Exceptions
{
	public class UnexpectedEntryTokenException : InvalidFormulaSyntaxException
	{
		public const string UnexpectedEntryTokenDefaultErrorMessageFormat = "Unexpected {0} Entry Token \"{1}\" at Index \"{2}\" (Zero-Based).";

		public UnexpectedEntryTokenException(
			InlineFormulaEntryType entryType,
			string entryToken,
			int entryIndex)
			: base(string.Format(UnexpectedEntryTokenDefaultErrorMessageFormat, entryType, entryToken, entryIndex))
		{

		}
	}
}
