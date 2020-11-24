using Septa.PayamGostar.Domain.Model.Enumeration.BaseInfo.InlineFormula;

namespace Septa.PayamGostar.Domain.Dto.BaseInfo.InlineFormula
{
	public class OperatorInlineFormulaEntryInfo : BaseInlineFormulaEntryInfo
	{
		public OperatorInlineFormulaEntryInfo(
			string rawToken,
			InlineFormulaOperatorType operatorType) : base(rawToken)
		{
			this.OperatorType = operatorType;
		}

		public override string Key => throw new System.NotImplementedException();
		public InlineFormulaOperatorType OperatorType { get; private set; }
	}
}