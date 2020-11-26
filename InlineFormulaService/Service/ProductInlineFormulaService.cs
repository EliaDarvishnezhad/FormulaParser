using InlineFormulaService.Dto;
using InlineFormulaService.Service;
using Septa.PayamGostar.Domain.Dto.BaseInfo.InlineFormula;
using Septa.PayamGostar.Domain.Service.ProductManagement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Septa.PayamGostar.CrmService.ProductManagement
{
	public class ProductInlineFormulaService : IProductInlineFormulaService
	{
		public ProductInlineFormulaService()
		{

		}

		#region Service Methods

		public decimal CalculateFormula(string formula, Dictionary<string, decimal> variableValues)
		{
			var inlineFormula = this.ParseFormulaEntries(formula);

			return inlineFormula.CalculateFormula(variableValues);
		}

		public decimal CalculateFormula(IEnumerable<InlineFormulaEntryTokenDto> inlineFormulaEntryTokens, Dictionary<string, decimal> variableValues)
		{
			var inlineFormula = this.ParseFormulaEntries(inlineFormulaEntryTokens);

			return inlineFormula.CalculateFormula(variableValues);
		}

		public bool TryParseFormulaEntries(
			string formula,
			out InlineFormula inlineFormula)
		{
			try
			{
				inlineFormula = this.ParseFormulaEntries(formula);
				return true;
			}
			catch (Exception ex)
			{
				inlineFormula = null;
				return false;
			}
		}

		public bool TryParseFormulaEntries(
			IEnumerable<InlineFormulaEntryTokenDto> inlineFormulaTokenCollection,
			out InlineFormula inlineFormula)
		{
			try
			{
				inlineFormula = this.ParseFormulaEntries(inlineFormulaTokenCollection);
				return true;
			}
			catch (Exception ex)
			{
				inlineFormula = null;
				return false;
			}
		}

		public InlineFormula ParseFormulaEntries(string formula)
		{
			if (formula is null)
				throw new ArgumentNullException(nameof(formula));

			return new InlineFormula(ParseFormula(this.ParseFormulaEntryTokens(formula)));
		}

		public InlineFormula ParseFormulaEntries(IEnumerable<InlineFormulaEntryTokenDto> inlineFormulaTokenCollection)
		{
			if (!inlineFormulaTokenCollection?.Any() ?? true)
				throw new ArgumentNullException(nameof(inlineFormulaTokenCollection));

			return new InlineFormula(this.ParseFormula(inlineFormulaTokenCollection));
		}

		public IEnumerable<OperandInlineFormulaEntryInfo> GetListOfUsedVariablesInFormula(string formula)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region Private Methods

		private IEnumerable<InlineFormulaEntry> ParseFormula(IEnumerable<InlineFormulaEntryTokenDto> inlineFormulaTokenCollection)
		{
			List<InlineFormulaEntry> toReturn = null;

			if (inlineFormulaTokenCollection != null)
				toReturn = inlineFormulaTokenCollection.Select(entry => InlineFormulaBuilder.ParseAndBuildInlineFormulaEntry(entry)).ToList();

			InlineFormulaBuilder.SortAndValidateEntryOrder(toReturn);

			return toReturn;
		}

		private IEnumerable<InlineFormulaEntryTokenDto> ParseFormulaEntryTokens(string formula)
		{
			var toReturn = new List<InlineFormulaEntryTokenDto>();

			if (formula is null)
				toReturn = null;
			else if (!string.IsNullOrEmpty(formula))
			{
				var splitedEntryTokens = formula.Split(InlineFormulaBuilder.TokenDelimiter);

				var entryTokenIndexCounter = 0;

				for (int i = 0; i < splitedEntryTokens.Length; i++)
				{
					if (!string.IsNullOrEmpty(splitedEntryTokens[i]))
					{
						toReturn.Add(new InlineFormulaEntryTokenDto(splitedEntryTokens[i], entryTokenIndexCounter));
						entryTokenIndexCounter++;
					}
				}
			}

			return toReturn;
		}

		#endregion
	}
}
