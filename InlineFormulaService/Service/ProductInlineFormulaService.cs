using InlineFormulaService.Exceptions;
using InlineFormulaService.Service;
using Septa.PayamGostar.Domain.Dto.BaseInfo.InlineFormula;
using Septa.PayamGostar.Domain.Model.Enumeration.BaseInfo.InlineFormula;
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
			throw new NotImplementedException();
		}

		public decimal CalculateFormula(IEnumerable<InlineFormulaEntry> inlineFormulaEntries, Dictionary<string, decimal> variableValues)
		{
			throw new NotImplementedException();
		}

		public bool TryParseFormulaEntries(
			string formula,
			out IEnumerable<InlineFormulaEntry> inlineFormulaEntries)
		{
			try
			{
				inlineFormulaEntries = this.ParseFormulaEntries(formula);
				return true;
			}
			catch (Exception ex)
			{
				inlineFormulaEntries = null;
				return false;
			}
		}

		public bool TryParseFormulaEntries(
			IEnumerable<InlineFormulaEntryTokenDto> inlineFormulaTokenCollection,
			out IEnumerable<InlineFormulaEntry> inlineFormulaEntries)
		{
			try
			{
				inlineFormulaEntries = this.ParseFormulaEntries(inlineFormulaTokenCollection);
				return true;
			}
			catch (Exception ex)
			{
				inlineFormulaEntries = null;
				return false;
			}
		}

		public IEnumerable<InlineFormulaEntry> ParseFormulaEntries(string formula)
		{
			if (formula is null)
				throw new ArgumentNullException(nameof(formula));

			return ParseFormula(this.ParseFormulaEntryTokens(formula));
		}

		public IEnumerable<InlineFormulaEntry> ParseFormulaEntries(IEnumerable<InlineFormulaEntryTokenDto> inlineFormulaTokenCollection)
		{
			if (!inlineFormulaTokenCollection?.Any() ?? true)
				throw new ArgumentNullException(nameof(inlineFormulaTokenCollection));

			return this.ParseFormula(inlineFormulaTokenCollection);
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
				toReturn = inlineFormulaTokenCollection.Select(entry => InlineFormulaEntryBuilder.ParseAndBuildInlineFormulaEntry(entry)).ToList();

			this.SortAndValidateEntryOrder(toReturn);

			return toReturn;
		}

		private IEnumerable<InlineFormulaEntryTokenDto> ParseFormulaEntryTokens(string formula)
		{
			var toReturn = new List<InlineFormulaEntryTokenDto>();

			if (formula is null)
				toReturn = null;
			else if (!string.IsNullOrEmpty(formula))
			{
				var splitedEntryTokens = formula.Split(InlineFormulaEntryBuilder.TokenDelimiter);

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

		private void SortAndValidateEntryOrder(List<InlineFormulaEntry> formulaEntries)
		{
			if (formulaEntries is null)
				throw new ArgumentNullException(nameof(formulaEntries));

			if (formulaEntries.Any())
			{
				formulaEntries = formulaEntries.OrderBy(x => x.EntryIndex).ToList();

				var firstEntry = formulaEntries.Single(x => x.EntryIndex == 0);

				if (firstEntry.EntryType == InlineFormulaEntryType.Operator)
					throw new UnexpectedEntryTokenException(
						firstEntry.EntryType,
						firstEntry.EntryInfo.RawToken,
						firstEntry.EntryIndex);

				for (int i = 0; i < formulaEntries.Count; i++)
				{
					var nextEntryIndex = i + 1;
					if (nextEntryIndex < formulaEntries.Count)
					{
						if (formulaEntries[i].EntryType == formulaEntries[nextEntryIndex].EntryType)
							throw new UnexpectedEntryTokenException(
								formulaEntries[nextEntryIndex].EntryType,
								formulaEntries[nextEntryIndex].EntryInfo.RawToken,
								formulaEntries[nextEntryIndex].EntryIndex);
					}
				}
			}
		}

		#endregion
	}
}
