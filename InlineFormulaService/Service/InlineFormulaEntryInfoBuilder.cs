using Septa.PayamGostar.Domain.Dto.BaseInfo.InlineFormula;
using Septa.PayamGostar.Domain.Model.Enumeration.BaseInfo.InlineFormula;
using System;
using System.Collections.Generic;

namespace InlineFormulaService.Service
{
	public static class InlineFormulaEntryInfoBuilder
	{
		public const char PlusSign = '+';
		public const char MinusSign = '-';
		public const char AsteriskSign = '*';
		public const char SlashSign = '/';

		private static Dictionary<InlineFormulaOperatorType, char> _operatorTypeSignPairDictionary;
		public static Dictionary<InlineFormulaOperatorType, char> OperatorTypeSignPairDictionary
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

		public static BaseInlineFormulaEntryInfo CreateFormulaEntryInfo(string formulaEntry)
		{
			throw new NotImplementedException();

			if (!string.IsNullOrEmpty(formulaEntry))
			{
				
			}
		}
	}
}
