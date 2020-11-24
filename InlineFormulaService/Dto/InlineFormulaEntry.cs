using Septa.PayamGostar.Domain.Model.Enumeration.BaseInfo.InlineFormula;

namespace Septa.PayamGostar.Domain.Dto.BaseInfo.InlineFormula
{
	public class InlineFormulaEntry
	{
		public int EntryIndex { get; set; }
		public InlineFormulaEntryType FormulaEntryType { get; set; }
		public BaseInlineFormulaEntryInfo FormulaEntryInfo { get; set; }
	}
}
