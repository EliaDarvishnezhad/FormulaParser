using Septa.PayamGostar.Domain.Model.Enumeration.BaseInfo.InlineFormula;

namespace Septa.PayamGostar.Domain.Dto.BaseInfo.InlineFormula
{
	public class OperandInlineFormulaEntryInfo : BaseInlineFormulaEntryInfo
	{
		public OperandInlineFormulaEntryInfo(
			string rawToken,
			InlineFormulaOperandType operandType) : base(rawToken)
		{
			this.OperandType = operandType;
		}

		public override string Key => throw new System.NotImplementedException();

		public InlineFormulaOperandType OperandType { get; private set; }
	}
}
