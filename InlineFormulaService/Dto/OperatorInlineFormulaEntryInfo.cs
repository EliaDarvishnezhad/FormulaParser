using Septa.PayamGostar.Domain.Model.Enumeration.BaseInfo.InlineFormula;
using System.Collections.Generic;
using System.Linq;

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

		public override string Key => SupportedOperatorTypeSignPairDictionary[this.OperatorType].ToString();
		public InlineFormulaOperatorType OperatorType { get; private set; }

		#region Static Members

		public const char PlusSign = '+';
		public const char MinusSign = '-';
		public const char AsteriskSign = '*';
		public const char SlashSign = '/';

		private static Dictionary<InlineFormulaOperatorType, char> _operatorTypeSignPairDictionary;
		public static Dictionary<InlineFormulaOperatorType, char> SupportedOperatorTypeSignPairDictionary
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

		public static bool IsOperatorEntry(string entryToken)
		{
			var toReturn = false;

			var trimmedEntry = entryToken?.Trim();

			if (!string.IsNullOrEmpty(trimmedEntry))
				toReturn = SupportedOperatorTypeSignPairDictionary.Values.Any(x => x.ToString().Equals(trimmedEntry));

			return toReturn;
		}

		#endregion
	}
}