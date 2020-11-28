using InlineFormulaService.Exceptions;
using InlineFormulaService.Service;
using Septa.PayamGostar.Domain.Dto.BaseInfo.InlineFormula;
using Septa.PayamGostar.Domain.Model.Enumeration.BaseInfo.InlineFormula;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InlineFormulaService.Dto
{
	public class InlineFormula : IComparer<InlineFormulaEntry>, IEnumerable<InlineFormulaEntry>
	{
		#region Properties & Indexers

		public InlineFormulaEntry this[int index] => _underlyingCollection.ElementAtOrDefault(index);

		public int Count => this._underlyingCollection.Count;

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

			InlineFormulaBuilder.SortAndValidateEntryOrder(formulaEntries.ToList());

			this._underlyingCollection = new SortedSet<InlineFormulaEntry>(formulaEntries, this);
		}

		#endregion

		#region Public Methods

		public int Compare(InlineFormulaEntry x, InlineFormulaEntry y)
		{
			return x.CompareTo(y);
		}

		public override string ToString()
		{
			return this.GetFormulaText();
		}

		public List<string> GetListOfUsedVariables()
		{
			return this.VariableOperandEntries?
				.Select((x) => x.EntryInfo.Key)
				.Distinct()
				.ToList();
		}

		public decimal CalculateFormula(Dictionary<string, decimal> formulaVariableValues)
		{
			var usedVariables = this.GetListOfUsedVariables();

			if (formulaVariableValues is null)
				throw new ArgumentNullException(nameof(formulaVariableValues));

			if (usedVariables.Any(v => !formulaVariableValues.ContainsKey(v)))
			{
				var emptyVariableName = usedVariables.FirstOrDefault(v => !formulaVariableValues.ContainsKey(v));
				throw new VariableValueNotSpecifiedException(emptyVariableName);
			}

			decimal result = 0;

			if (this.Any())
			{
				foreach (var item in this.OfType<InlineFormulaEntry>())
				{
					if (item.EntryType == InlineFormulaEntryType.Operand &&
						(item.EntryInfo as OperandInlineFormulaEntryInfo).OperandType == InlineFormulaOperandType.Variable)
					{
						(item.EntryInfo as OperandInlineFormulaEntryInfo).Value = formulaVariableValues[item.EntryInfo.Key];
					}
				}

				if (this[0].EntryType == InlineFormulaEntryType.Operand)
				{
					result = (this[0].EntryInfo as OperandInlineFormulaEntryInfo).Value;

					for (int i = 1; i < this.Count; i += 2)
					{
						var nextOperandIndex = i + 1;

						if (nextOperandIndex < this.Count)
						{
							switch ((this[i].EntryInfo as OperatorInlineFormulaEntryInfo)?.OperatorType)
							{
								case InlineFormulaOperatorType.Add:
									result += this[nextOperandIndex].EntryInfo as OperandInlineFormulaEntryInfo;
									break;
								case InlineFormulaOperatorType.Subtract:
									result -= this[nextOperandIndex].EntryInfo as OperandInlineFormulaEntryInfo;
									break;
								case InlineFormulaOperatorType.Multiply:
									result *= this[nextOperandIndex].EntryInfo as OperandInlineFormulaEntryInfo;
									break;
								case InlineFormulaOperatorType.Divide:
									result /= this[nextOperandIndex].EntryInfo as OperandInlineFormulaEntryInfo;
									break;
								default:
									throw new NotSupportedException();
							}
						}
					}
				}
			}

			return result;
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

		IEnumerator<InlineFormulaEntry> IEnumerable<InlineFormulaEntry>.GetEnumerator()
		{
			return _underlyingCollection.GetEnumerator();
		}

		public IEnumerator GetEnumerator()
		{
			return _underlyingCollection.GetEnumerator();
		}

		#endregion
	}
}
