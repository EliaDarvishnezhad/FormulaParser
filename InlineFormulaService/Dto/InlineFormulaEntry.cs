using Septa.PayamGostar.Domain.Model.Enumeration.BaseInfo.InlineFormula;
using System;

namespace Septa.PayamGostar.Domain.Dto.BaseInfo.InlineFormula
{
	public class InlineFormulaEntry
	{
		/// <summary>
		/// Initializes an instance of <b>InlineFormulaEntry</b> Class For Operand Entry Type
		/// </summary>
		/// <param name="operandInfo">An Instance of OperandInlineFormulaEntryInfo Class Containing Information Of Formula Entry</param>
		/// <param name="entryIndex">Index of the entry in formula</param>
		public InlineFormulaEntry(
			OperandInlineFormulaEntryInfo operandInfo,
			int entryIndex)
		{
			if (operandInfo is null)
				throw new ArgumentNullException(nameof(operandInfo));

			if (entryIndex < 0)
				throw new ArgumentOutOfRangeException(nameof(entryIndex));

			this.EntryType = InlineFormulaEntryType.Operand;
			this.EntryInfo = operandInfo;
			this.EntryIndex = entryIndex;
		}

		/// <summary>
		/// Initializes an instance of <b>InlineFormulaEntry</b> Class For Operator Entry Type
		/// </summary>
		/// <param name="operatorInfo">An Instance of OperatorInlineFormulaEntryInfo Class Containing Information Of Formula Entry</param>
		/// <param name="entryIndex">Index of the entry in formula</param>
		public InlineFormulaEntry(
			OperatorInlineFormulaEntryInfo operatorInfo,
			int entryIndex)
		{
			if (operatorInfo is null)
				throw new ArgumentNullException(nameof(operatorInfo));

			if (entryIndex < 0)
				throw new ArgumentOutOfRangeException(nameof(entryIndex));

			this.EntryType = InlineFormulaEntryType.Operator;
			this.EntryInfo = operatorInfo;
			this.EntryIndex = entryIndex;
		}

		public int EntryIndex { get; private set; }
		public InlineFormulaEntryType EntryType { get; private set; }
		public BaseInlineFormulaEntryInfo EntryInfo { get; private set; }
	}
}
