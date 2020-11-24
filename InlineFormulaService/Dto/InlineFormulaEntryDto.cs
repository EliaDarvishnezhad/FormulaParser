using Septa.PayamGostar.Domain.Model.Enumeration.BaseInfo.InlineFormula;

namespace Septa.PayamGostar.Domain.Dto.BaseInfo.InlineFormula
{
    public class InlineFormulaEntryDto
    {
        public byte EntryIndex { get; set; }
        public InlineFormulaEntryType FormulaEntryType { get; set; }
        public BaseInlineFormulaEntryInfoDto FormulaEntryInfo { get; set; }
    }
}
