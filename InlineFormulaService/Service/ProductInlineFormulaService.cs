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

		public bool TryParseFormulaEntries(string formula, out IEnumerable<InlineFormulaEntry> inlineFormulaEntries)
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

		public IEnumerable<InlineFormulaEntry> ParseFormulaEntries(string formula)
		{
			if (formula is null)
				throw new ArgumentNullException(nameof(formula));

			return this.ParseFormula(formula);
		}

		public IEnumerable<OperandInlineFormulaEntryInfo> GetListOfUsedVariablesInFormula(string formula)
		{
			throw new NotImplementedException();
		}

		public bool ValidateFormulaSyntax(string formula, IEnumerable<string> supportedVariableCollection)
		{

			throw new NotImplementedException();
		}

		#endregion

		#region Private Methods

		private IEnumerable<InlineFormulaEntry> ParseFormula(string formula)
		{
			List<InlineFormulaEntry> toReturn = null;

			if (!string.IsNullOrEmpty(formula))
			{
				toReturn = new List<InlineFormulaEntry>();

				var splitedEntries = formula.Split(' ');

				var entryIndexCounter = 0;

				for (int i = 0; i < splitedEntries.Length; i++)
				{
					InlineFormulaEntry entry = null;
					if (!string.IsNullOrEmpty(splitedEntries[i]))
					{
						if (this.IsVariableEntry(splitedEntries[i]))
						{
							var entryInfo = new OperandInlineFormulaEntryInfo(splitedEntries[i], InlineFormulaOperandType.Variable);

							entry = new InlineFormulaEntry(entryInfo, entryIndexCounter);
						}
						else if (this.IsConstantValueEntry(splitedEntries[i]))
						{
							var entryInfo = new OperandInlineFormulaEntryInfo(splitedEntries[i], InlineFormulaOperandType.ConstantValue);

							entry = new InlineFormulaEntry(entryInfo, entryIndexCounter);
						}
						else if (this.IsOperatorEntry(splitedEntries[i]))
						{
							var revertedDictionary = InlineFormulaEntryInfoBuilder.OperatorTypeSignPairDictionary.ToDictionary(k => k.Value, v => v.Key);

							var operatorType = revertedDictionary[splitedEntries[i].Trim().FirstOrDefault()];

							var entryInfo = new OperatorInlineFormulaEntryInfo(splitedEntries[i], operatorType);

							entry = new InlineFormulaEntry(entryInfo, entryIndexCounter);
						}
						else
						{
							throw new InvalidFormulaSyntaxException($"Invalid formula entry token: \" {entry} \" ");
						}

						toReturn.Add(entry);

						entryIndexCounter++;
					}
				}
			}

			this.ProcessPostParsingValidation(toReturn);

			return toReturn;
		}

		private bool IsVariableEntry(string entry)
		{
			var toReturn = false;

			var trimmedEntry = entry?.Trim();

			if (!string.IsNullOrEmpty(trimmedEntry))
			{
				if (trimmedEntry.StartsWith("[") &&
					trimmedEntry.EndsWith("]") &&
					trimmedEntry.Length > 2)
				{
					toReturn = true;
				}
			}

			return toReturn;
		}

		private bool IsConstantValueEntry(string entry)
		{
			var toReturn = false;

			var trimmedEntry = entry?.Trim();

			if (!string.IsNullOrEmpty(trimmedEntry))
			{
				toReturn = decimal.TryParse(trimmedEntry, out _);
			}

			return toReturn;
		}

		private bool IsOperatorEntry(string entry)
		{
			var toReturn = false;

			var trimmedEntry = entry?.Trim();

			if (!string.IsNullOrEmpty(trimmedEntry))
				toReturn = InlineFormulaEntryInfoBuilder.OperatorTypeSignPairDictionary.Values.Any(x => x.ToString().Equals(trimmedEntry));

			return toReturn;
		}

		private void ProcessPostParsingValidation(List<InlineFormulaEntry> formulaEntries)
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
