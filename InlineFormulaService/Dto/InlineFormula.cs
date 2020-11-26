using Septa.PayamGostar.Domain.Dto.BaseInfo.InlineFormula;
using Septa.PayamGostar.Domain.Model.Enumeration.BaseInfo.InlineFormula;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InlineFormulaService.Dto
{
	public class InlineFormula : IComparer<InlineFormulaEntry>, IEnumerable
	{
		#region Properties & Indexers

		public InlineFormulaEntry this[int index] => _underlyingCollection.ElementAtOrDefault(index);

		private IEnumerable<InlineFormulaEntry> VariableOperandEntries
		{
			get
			{
				return _underlyingCollection?
					.Where(x => x.EntryType == InlineFormulaEntryType.Operand &&
					(x.EntryInfo as OperandInlineFormulaEntryInfo).OperandType == InlineFormulaOperandType.Variable);
			}
		}

		#endregion

		#region Fields

		private readonly SortedSet<InlineFormulaEntry> _underlyingCollection;

		#endregion

		#region Constructor

		public InlineFormula(IEnumerable<InlineFormulaEntry> formulaEntries)
		{
			if (formulaEntries is null)
				throw new ArgumentNullException(nameof(formulaEntries));

			this._underlyingCollection = new SortedSet<InlineFormulaEntry>(formulaEntries, this);
		}

		#endregion

		#region Public Methods

		public int Compare(InlineFormulaEntry x, InlineFormulaEntry y)
		{
			return x.CompareTo(y);
		}

		public IEnumerator GetEnumerator()
		{
			return _underlyingCollection.GetEnumerator();
		}

		public override string ToString()
		{
			return this.GetFormulaText();
		}

		public List<string> GetListOfUsedVariables()
		{
			return this.VariableOperandEntries?
				.Select((x) => x.EntryInfo.Key)
				.ToList();
		}

		public decimal CalculateFormula(Dictionary<string, decimal> formulaVariableValues)
		{
			var usedVariables = this.GetListOfUsedVariables();

			if (formulaVariableValues is null)
				throw new ArgumentNullException(nameof(formulaVariableValues));

			if (usedVariables.Any(v => !formulaVariableValues.ContainsKey(v)))
			{
			}

			throw new NotImplementedException();
		}

		#endregion

		#region Private Methods

		private string GetFormulaText()
		{
			var formulaBuilder = new StringBuilder();

			foreach (var entry in _underlyingCollection)
			{
				formulaBuilder.Append($"{entry.ToString()} ");
			}

			return formulaBuilder.ToString().Trim();
		}

		#endregion
	}
}
