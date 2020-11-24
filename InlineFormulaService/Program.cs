using Septa.PayamGostar.CrmService.ProductManagement;
using Septa.PayamGostar.Domain.Service.ProductManagement;
using System;

namespace InlineFormulaService
{
	class Program
	{
		static void Main(string[] args)
		{

			IProductInlineFormulaService formulaService = new ProductInlineFormulaService();

			do
			{
				Console.Clear();
				WriteHeader();


				var formulaText = Console.ReadLine();

				if (!string.IsNullOrEmpty(formulaText))
				{

				}

			}
			while (Console.ReadKey().Key != ConsoleKey.Escape);
		}

		private static void WriteHeader()
		{
			Console.ForegroundColor = ConsoleColor.DarkMagenta;

			Console.WriteLine("===============================================");
			Console.WriteLine("===============================================");
			Console.WriteLine("================ Formula Parser ===============");
			Console.WriteLine("===============================================");
			Console.WriteLine("===============================================");

			Console.ResetColor();
		}
	}
}
