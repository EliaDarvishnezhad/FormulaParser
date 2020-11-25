namespace Septa.PayamGostar.Domain.Dto.BaseInfo.InlineFormula
{
	public class InlineFormulaEntryTokenDto
	{
		public InlineFormulaEntryTokenDto(string token, int index)
		{
			this.Token = token;
			this.Index = index;
		}

		public string Token { get; set; }
		public int Index { get; set; }
	}
}
