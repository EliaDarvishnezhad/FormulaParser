using InlineFormulaService.Exceptions;
using Septa.PayamGostar.Domain.Dto.BaseInfo.InlineFormula;
using Septa.PayamGostar.Domain.Model.Enumeration.BaseInfo.InlineFormula;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InlineFormulaService.Service
{
	public static class InlineFormulaHelper
	{
		#region Constant Values

		public const char TokenDelimiter = ' ';

		#endregion

		public static InlineFormulaEntry ParseAndBuildInlineFormulaEntry(string entryToken, int entryIndex)
		{
			InlineFormulaEntry entry = null;

			if (!string.IsNullOrEmpty(entryToken))
			{
				InlineFormulaOperandType? operandType = null;

				foreach (var item in Enum.GetValues(typeof(InlineFormulaOperandType)).OfType<InlineFormulaOperandType>())
				{
					if (OperandInlineFormulaEntryInfo.IsOperandEntryOfType(entryToken, item))
					{
						operandType = item;
						break;
					}
				}

				if (operandType.HasValue)
				{
					var entryInfo = new OperandInlineFormulaEntryInfo(entryToken, operandType.Value);

					entry = new InlineFormulaEntry(entryInfo, entryIndex);
				}
				else if (OperatorInlineFormulaEntryInfo.IsOperatorEntry(entryToken))
				{
					var revertedDictionary = OperatorInlineFormulaEntryInfo
						.SupportedOperatorTypeSignPairDictionary
						.ToDictionary(k => k.Value, v => v.Key);

					var operatorType = revertedDictionary[entryToken.Trim().FirstOrDefault()];

					var entryInfo = new OperatorInlineFormulaEntryInfo(entryToken, operatorType);

					entry = new InlineFormulaEntry(entryInfo, entryIndex);
				}
				else
				{
					throw new InvalidFormulaSyntaxException($"Unrecognized Formula Entry Token \"{entryToken}\" at Index \"{entryIndex}\" (Zero-Based).");
				}
			}

			return entry;
		}

		public static InlineFormulaEntry ParseAndBuildInlineFormulaEntry(InlineFormulaTokenIndexDto inlineFormulaEntryToken)
		{
			if (inlineFormulaEntryToken is null)
				throw new ArgumentNullException(nameof(inlineFormulaEntryToken));

			return ParseAndBuildInlineFormulaEntry(inlineFormulaEntryToken.Token, inlineFormulaEntryToken.Index);
		}

		public static void SortAndValidateEntryOrder(List<InlineFormulaEntry> formulaEntries)
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
	}
}
