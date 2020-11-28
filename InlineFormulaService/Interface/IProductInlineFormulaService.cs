using InlineFormulaService.Dto;
using Septa.PayamGostar.Domain.Dto.BaseInfo.InlineFormula;
using System.Collections.Generic;

namespace Septa.PayamGostar.Domain.Service.ProductManagement
{
	public interface IProductInlineFormulaService
	{
		InlineFormula ParseFormulaEntries(string formula);
		InlineFormula ParseFormulaEntries(IEnumerable<InlineFormulaEntryTokenDto> inlineFormulaTokenCollection);

		bool TryParseFormulaEntries(
			string formula,
			out InlineFormula inlineFormula);
		bool TryParseFormulaEntries(
			IEnumerable<InlineFormulaEntryTokenDto> inlineFormulaTokenCollection,
			out InlineFormula inlineFormula);

		decimal CalculateFormula(string formula, Dictionary<string, decimal> variableValues);
		decimal CalculateFormula(IEnumerable<InlineFormulaEntryTokenDto> inlineFormulaEntryTokens, Dictionary<string, decimal> variableValues);
	}
}
