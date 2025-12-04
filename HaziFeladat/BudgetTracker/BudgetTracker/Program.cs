using System;

namespace BudgetTracker
{
    internal class Program
    {
        static ExpenseManager manager = new ExpenseManager();

        static void Main(string[] args)
        {
            Console.WriteLine("=== Budget Tracker 2025 ===");
            bool running = true;

            while (running)
            {
                Console.WriteLine("\nFOMENU:");
                Console.WriteLine("1. Uj kiadas hozzaadasa");
                Console.WriteLine("2. Kiadasok listazasa");
                Console.WriteLine("3. Statisztikak");
                Console.WriteLine("4. Kilepes");
                Console.Write("Valasztas: ");

                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            MenuAddExpense();
                            break;
                        case "2":
                            MenuListExpenses();
                            break;
                        case "3":
                            MenuShowStatistics();
                            break;
                        case "4":
                            running = false;
                            Console.WriteLine("Tovabbi szep napot!");
                            break;
                        default:
                            Console.WriteLine("Ismeretlen parancs, kerlek 1-4 kozotti szamot valassz!");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nError: {ex.Message}");
                    Console.ResetColor();
                }
            }
        }

        static void MenuAddExpense()
        {
            Console.WriteLine("\n--- Uj kiadas ---");
            Console.Write("Megnevezes: ");
            string name = Console.ReadLine();

            Console.Write("Kategoria: ");
            string category = Console.ReadLine();

            Console.Write("Osszeg (Ft): ");
            int amount = int.Parse(Console.ReadLine());

            manager.AddExpense(name, amount, category);
            Console.WriteLine(">> Sikeresen rogzites");
        }

        static void MenuListExpenses()
        {
            var list = manager.GetAllExpenses();

            Console.WriteLine($"\n--- Lista ({list.Count} tetel) ---");

            if (list.Count == 0)
            {
                Console.WriteLine("Meg nincs rogzitett adat.");
                return;
            }

            foreach (var item in list)
            {
                Console.WriteLine($"[{item.Date.ToShortDateString()}] {item.Name} ({item.Category}): {item.Amount} Ft");
            }
        }

        static void MenuShowStatistics()
        {
            var list = manager.GetAllExpenses();

            if (list.Count == 0)
            {
                Console.WriteLine("Nincs eleg adat a statisztikahoz.");
                return;
            }

            int total = StatisticsService.CalculateTotal(list);
            Console.WriteLine($"\nTeljes koltes: {total} Ft");

            var expensive = StatisticsService.GetMostExpensiveItem(list);
            Console.WriteLine($"Legdragabb tetel: {expensive.Name} ({expensive.Amount} Ft)");

            StatisticsService.PrintCategoryBreakdown(list);
        }
    }
}