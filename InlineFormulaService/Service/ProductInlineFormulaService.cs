using Septa.PayamGostar.Domain.Dto.BaseInfo.InlineFormula;
using Septa.PayamGostar.Domain.Service.ProductManagement;
using System;
using System.Collections.Generic;

namespace Septa.PayamGostar.CrmService.ProductManagement
{
	public class ProductInlineFormulaService : IProductInlineFormulaService
	{
		public const string PlusSign = "+";
		public const string MinusSign = "-";
		public const string AsteriskSign = "*";
		public const string SlashSign = "/";

		private string[] ReservedOperatorSigns
		{
			get
			{
				return new string[] { PlusSign, MinusSign, AsteriskSign, SlashSign };
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

		public decimal CalculateFormula(IEnumerable<InlineFormulaEntryDto> inlineFormulaEntries, Dictionary<string, decimal> variableValues)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<OperandInlineFormulaEntryInfoDto> GetListOfUsedVariablesInFormula(string formula)
		{
			throw new NotImplementedException();
		}

		public bool ValidateFormulaSyntax(string formula, IEnumerable<string> supportedVariableCollection)
		{

			throw new NotImplementedException();
		}

		#endregion

		#region Private Methods


		private List<InlineFormulaEntryDto> ParseFormula(string formula)
		{
			if (!string.IsNullOrEmpty(formula))
			{
				var toReturn = new List<InlineFormulaEntryDto>();

				return toReturn;
			}
			else
				return null;
		}

		#endregion
	}
}
