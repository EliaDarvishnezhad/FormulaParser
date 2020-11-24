namespace Septa.PayamGostar.Domain.Dto.BaseInfo.InlineFormula
{
	public abstract class BaseInlineFormulaEntryInfo
	{
		public BaseInlineFormulaEntryInfo(string rawToken)
		{
			this.RawToken = rawToken;
		}

		public string RawToken { get; private set; }
		public abstract string Key { get; }
	}
}
