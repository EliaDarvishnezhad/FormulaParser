using Septa.PayamGostar.Domain.Model.Enumeration.BaseInfo.InlineFormula;

namespace Septa.PayamGostar.Domain.Dto.BaseInfo.InlineFormula
{
    public class OperatorInlineFormulaEntryInfoDto : BaseInlineFormulaEntryInfoDto
    {
        InlineFormulaOperatorType OperatorType { get; set; }
    }
}