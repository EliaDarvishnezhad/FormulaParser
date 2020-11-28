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
	/// <summary>
	/// <b>InlineFormula</b> Class is a container of formula entries .</br>
	/// Handles Iterating Over Formula Entries.
	/// Handles Operations Such as Validating, Evaluating, And Retrieving Formula Entries
	/// </summary>
	public class InlineFormula : IComparer<InlineFormulaEntry>, IEnumerable<InlineFormulaEntry>
	{
		#region Properties & Indexers

		/// <summary>
		/// Indexer of Formula Entries
		/// </summary>
		/// <param name="index">Index of entry</param>
		/// <returns cref="InlineFormulaEntry">Returns <b>InlineFormulaEntry</b> at Specified Index</returns>
		public InlineFormulaEntry this[int index] => _underlyingCollection.ElementAtOrDefault(index);

		/// <summary>
		/// Count of formula entries (Operands and Operators Between Them)
		/// </summary>
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

		/// <summary>
		/// Initializes an Instance of FormulaEntry with Given FormulaEntries
		/// </summary>
		/// <param name="formulaEntries">Collection Of <b>InlineFormulaEntry</b> to Initialize Formula with
		/// <seealso cref="InlineFormulaEntry"/></param>
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

		/// <summary>
		/// Returns the List of Variable Keys</b>
		/// </summary>
		/// <returns>List of String Containing List of Variable Keys.</returns>
		public List<string> GetListOfUsedVariables()
		{
			return this.VariableOperandEntries?
				.Select((x) => x.EntryInfo.Key)
				.Distinct()
				.ToList();
		}

		/// <summary>
		/// Calculates formula and returns the result number in decimal type
		/// </summary>
		/// <param name="formulaVariableValues">A Dictionary of Variables and Their Corresponding Value</param>
		/// <returns>Returns Result of Formula Expression</returns>
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
								case InlineFormulaOperatorType.Power:
									result ^= this[nextOperandIndex].EntryInfo as OperandInlineFormulaEntryInfo;
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

		IEnumerator<InlineFormulaEntry> IEnumerable<InlineFormulaEntry>.GetEnumerator()
		{
			return _underlyingCollection.GetEnumerator();
		}

		public IEnumerator GetEnumerator()
		{
			return _underlyingCollection.GetEnumerator();
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
