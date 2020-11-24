using InlineFormulaService.Exceptions;
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
		public const char PlusSign = '+';
		public const char MinusSign = '-';
		public const char AsteriskSign = '*';
		public const char SlashSign = '/';


		private Dictionary<InlineFormulaOperatorType, char> _operatorTypeSignPairDictionary;
		public Dictionary<InlineFormulaOperatorType, char> OperatorTypeSignPairDictionary
		{
			get
			{
				if (_operatorTypeSignPairDictionary is null)
				{
					_operatorTypeSignPairDictionary = new Dictionary<InlineFormulaOperatorType, char>(){
					{ InlineFormulaOperatorType.Add, PlusSign},
					{ InlineFormulaOperatorType.Subtract, MinusSign },
					{ InlineFormulaOperatorType.Multiply, AsteriskSign },
					{ InlineFormulaOperatorType.Divide, SlashSign }};
				}
				return _operatorTypeSignPairDictionary;
			}
		}


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
			catch (Exception)
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


		private List<InlineFormulaEntry> ParseFormula(string formula)
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
							entry = new InlineFormulaEntry
							{
								EntryIndex = entryIndexCounter,
								FormulaEntryType = InlineFormulaEntryType.Operand,
								FormulaEntryInfo = new OperandInlineFormulaEntryInfo(splitedEntries[i], InlineFormulaOperandType.Variable)
							};
						}
						else if (this.IsConstantValueEntry(splitedEntries[i]))
						{
							entry = new InlineFormulaEntry
							{
								EntryIndex = entryIndexCounter,
								FormulaEntryType = InlineFormulaEntryType.Operand,
								FormulaEntryInfo = new OperandInlineFormulaEntryInfo(splitedEntries[i], InlineFormulaOperandType.ConstantValue)
							};
						}
						else if (this.IsOperatorEntry(splitedEntries[i]))
						{
							var revertedDictionary = this.OperatorTypeSignPairDictionary.ToDictionary(k => k.Value, v => v.Key);

							var operatorType = revertedDictionary[splitedEntries[i].Trim().FirstOrDefault()];

							entry = new InlineFormulaEntry
							{
								EntryIndex = entryIndexCounter,
								FormulaEntryType = InlineFormulaEntryType.Operator,
								FormulaEntryInfo = new OperatorInlineFormulaEntryInfo(splitedEntries[i], operatorType)
							};
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
			{
				toReturn = this.OperatorTypeSignPairDictionary.Values.Any(x => x.Equals(trimmedEntry));
			}

			return toReturn;
		}

		private void ProcessPostParsingValidation(List<InlineFormulaEntry> formulaEntries)
		{
			if (formulaEntries is null)
				throw new ArgumentNullException(nameof(formulaEntries));

			if (formulaEntries.Any())
			{
				throw new NotImplementedException();
			}
		}

		#endregion
	}
}
