using InlineFormulaService.Dto;
using Septa.PayamGostar.Domain.Dto.BaseInfo.InlineFormula;
using System.Collections.Generic;

namespace Septa.PayamGostar.Domain.Service.ProductManagement
{
	public interface IProductInlineFormulaService
	{
		InlineFormula ParseFormulaEntries(string formula);
		InlineFormula ParseFormulaEntries(IEnumerable<InlineFormulaTokenIndexDto> inlineFormulaTokenCollection);

		bool TryParseFormulaEntries(
			string formula,
			out InlineFormula inlineFormula);
		bool TryParseFormulaEntries(
			IEnumerable<InlineFormulaTokenIndexDto> inlineFormulaTokenCollection,
			out InlineFormula inlineFormula);

		decimal CalculateFormula(string formula, Dictionary<string, decimal> variableValues);
		decimal CalculateFormula(IEnumerable<InlineFormulaTokenIndexDto> inlineFormulaEntryTokens, Dictionary<string, decimal> variableValues);
	}
}
