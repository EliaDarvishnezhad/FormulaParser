using Septa.PayamGostar.Domain.Dto.BaseInfo.InlineFormula;
using System.Collections.Generic;

namespace Septa.PayamGostar.Domain.Service.ProductManagement
{
    public interface IProductInlineFormulaService
    {
        bool ValidateFormulaSyntax(string formula, IEnumerable<string> supportedVariableCollection);
        IEnumerable<OperandInlineFormulaEntryInfoDto> GetListOfUsedVariablesInFormula(string formula);

        decimal CalculateFormula(string formula, Dictionary<string, decimal> variableValues);
        decimal CalculateFormula(IEnumerable<InlineFormulaEntryDto> inlineFormulaEntryCollection, Dictionary<string, decimal> variableValues);
    }
}
