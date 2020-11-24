using Septa.PayamGostar.Domain.Dto.BaseInfo.InlineFormula;
using System.Collections.Generic;

namespace Septa.PayamGostar.Domain.Service.ProductManagement
{
	public interface IProductInlineFormulaService
	{
		IEnumerable<InlineFormulaEntry> ParseFormulaEntries(string formula);
		bool TryParseFormulaEntries(string formula, out IEnumerable<InlineFormulaEntry> inlineFormulaEntries);

		IEnumerable<OperandInlineFormulaEntryInfo> GetListOfUsedVariablesInFormula(string formula);

		decimal CalculateFormula(string formula, Dictionary<string, decimal> variableValues);
		decimal CalculateFormula(IEnumerable<InlineFormulaEntry> inlineFormulaEntryCollection, Dictionary<string, decimal> variableValues);
	}
}
