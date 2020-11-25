using Septa.PayamGostar.Domain.Model.Enumeration.BaseInfo.InlineFormula;
using System;

namespace Septa.PayamGostar.Domain.Dto.BaseInfo.InlineFormula
{
	public class OperandInlineFormulaEntryInfo : BaseInlineFormulaEntryInfo
	{
		#region Instance Members

		public OperandInlineFormulaEntryInfo(
			string rawToken,
			InlineFormulaOperandType operandType) : base(rawToken)
		{
			this.OperandType = operandType;
		}

		public override string Key => this.GetOperandKeyForUseInCalculation();

		public InlineFormulaOperandType OperandType { get; private set; }

		private string GetOperandKeyForUseInCalculation()
		{
			switch (this.OperandType)
			{
				case InlineFormulaOperandType.ConstantValue:
					return this.RawToken;
				case InlineFormulaOperandType.Variable:
					return this.RawToken.Remove(this.RawToken.Length - 1, 1).Remove(0, 1);
				default:
					throw new NotSupportedException();
			}
		}

		#endregion

		#region static Members

		public static bool IsOperandEntryOfType(
			string entryToken,
			InlineFormulaOperandType operandType)
		{
			switch (operandType)
			{
				case InlineFormulaOperandType.ConstantValue:
					return IsConstantValueEntry(entryToken);

				case InlineFormulaOperandType.Variable:
					return IsVariableEntry(entryToken);

				default:
					throw new NotSupportedException();
			}
		}

		private static bool IsVariableEntry(string entryToken)
		{
			var toReturn = false;

			var trimmedEntry = entryToken?.Trim();

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

		private static bool IsConstantValueEntry(string entryToken)
		{
			var toReturn = false;

			var trimmedEntry = entryToken?.Trim();

			if (!string.IsNullOrEmpty(trimmedEntry))
			{
				toReturn = decimal.TryParse(trimmedEntry, out _);
			}

			return toReturn;
		}

		#endregion
	}
}
