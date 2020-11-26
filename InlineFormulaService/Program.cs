using Septa.PayamGostar.CrmService.ProductManagement;
using Septa.PayamGostar.Domain.Service.ProductManagement;
using System;
using System.Linq;

namespace InlineFormulaService
{
	class Program
	{
		static void Main(string[] args)
		{
			IProductInlineFormulaService formulaService = new ProductInlineFormulaService();

			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.Black;

			do
			{
				Console.Clear();

				WriteHeader();
				Console.Write("Insert formula: ");
				var formulaText = Console.ReadLine();

				formulaText = string.IsNullOrEmpty(formulaText) ? "[Price] * 2 + [Quantity] - 5" : formulaText.Trim();

				try
				{
					if (!string.IsNullOrEmpty(formulaText))
					{
						var formula = formulaService.ParseFormulaEntries(formulaText);

						var fomulaVaribaleValues = formula.GetListOfUsedVariables().ToDictionary(x => x, x => 1m);

						var formulaResult = formula.CalculateFormula(fomulaVaribaleValues);

						Console.ForegroundColor = ConsoleColor.DarkCyan;
						Console.WriteLine(formula);
						Console.WriteLine(string.Join(", ", fomulaVaribaleValues.Select(x => $"{x.Key}={x.Value}")));
						Console.WriteLine(formulaResult);
						Console.ForegroundColor = ConsoleColor.Black;
					}
				}
				catch (Exception ex)
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine($"\r\n{ex.Message}");
					Console.ForegroundColor = ConsoleColor.Black;
				}

				Console.ForegroundColor = ConsoleColor.DarkGreen;
				Console.WriteLine("\r\nPress ESC to exit or any other key to try again...");
				Console.ForegroundColor = ConsoleColor.Black;
			}
			while (Console.ReadKey().Key != ConsoleKey.Escape);
		}

		private static void WriteHeader()
		{
			var bannerChar = '*';
			var bannerLine = string.Join(string.Empty, Enumerable.Repeat(bannerChar, Console.BufferWidth - 2));

			var headerTitle = "--InLine Formula Parser--";
			var sideBarWidth = (Console.BufferWidth - "--InLine Formula Parser--".Length - 2) / 2;
			Console.ForegroundColor = ConsoleColor.DarkMagenta;

			Console.WriteLine(bannerLine);
			Console.WriteLine(bannerLine);
			Console.WriteLine($"{string.Join(string.Empty, Enumerable.Repeat("*", sideBarWidth))}{headerTitle}{string.Join(string.Empty, Enumerable.Repeat("*", sideBarWidth))}");
			Console.WriteLine(bannerLine);
			Console.WriteLine(bannerLine);

			Console.ForegroundColor = ConsoleColor.Black;
		}
	}
}
