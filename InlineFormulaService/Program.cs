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

				var formulaText = Console.ReadLine();

				formulaText = string.IsNullOrEmpty(formulaText) ? "[Price] * 2 + [Quantity] - 5" : formulaText.Trim();

				if (!string.IsNullOrEmpty(formulaText))
				{
					var entries = formulaService.TryParseFormulaEntries(formulaText,out var result);
				}

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
