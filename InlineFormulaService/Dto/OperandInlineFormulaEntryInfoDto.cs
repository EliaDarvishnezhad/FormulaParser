using Septa.PayamGostar.Domain.Model.Enumeration.BaseInfo.InlineFormula;

namespace Septa.PayamGostar.Domain.Dto.BaseInfo.InlineFormula
{
    public class OperandInlineFormulaEntryInfoDto: BaseInlineFormulaEntryInfoDto
    {
        InlineFormulaOperandType OperandType { get; set; }
    }
}
