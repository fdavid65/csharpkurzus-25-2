using System;

namespace BudgetTracker
{
    internal class Program
    {
        static ExpenseManager manager = new ExpenseManager();

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== Budget Tracker ===");
            Console.ResetColor();
            bool running = true;

            while (running)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nFOMENU:");
                Console.ResetColor();
                Console.WriteLine("1. Uj kiadas hozzaadasa");
                Console.WriteLine("2. Kiadasok listazasa");
                Console.WriteLine("3. Statisztikak");
                Console.WriteLine("4. Kereses nev alapjan");
                Console.WriteLine("5. Kilepes");
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
                            MenuSearch();
                            break;
                        case "5":
                            running = false;
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine("Tovabbi szep napot!");
                            Console.ResetColor();
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Ismeretlen parancs, kerlek 1-5 kozotti szamot valassz!");
                            Console.ResetColor();
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
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n--- Uj kiadas ---");
            Console.ResetColor();
            Console.Write("Megnevezes: ");
            string name = Console.ReadLine();

            Console.Write("Kategoria: ");
            string category = Console.ReadLine();

            Console.Write("Osszeg (Ft): ");
            int amount = int.Parse(Console.ReadLine());

            manager.AddExpense(name, amount, category);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(">> Sikeresen rogzites");
            Console.ResetColor();
        }

        static void MenuListExpenses()
        {
            var list = manager.GetAllExpenses();
            PrintList(list);
        }

        static void MenuShowStatistics()
        {
            var list = manager.GetAllExpenses();

            if (list.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Nincs eleg adat a statisztikahoz.");
                Console.ResetColor();
                return;
            }

            int total = StatisticsService.CalculateTotal(list);
            Console.Write("\nTeljes koltes: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{total} Ft");
            Console.ResetColor();

            var expensive = StatisticsService.GetMostExpensiveItem(list);
            Console.Write("Legdragabb tetel: ");
            Console.Write($"{expensive.Name} ");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"({expensive.Amount} Ft)");
            Console.ResetColor();

            StatisticsService.PrintCategoryBreakdown(list);
        }

        static void MenuSearch()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n--- Keresés ---");
            Console.ResetColor();

            Console.Write("Mit keresel?: ");
            string text = Console.ReadLine();

            var results = manager.SearchExpenses(text);

            if (results.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Találatok száma: {results.Count}");
                Console.ResetColor();
                PrintList(results);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Nincs találat.");
                Console.ResetColor();
            }
        }

        static void PrintList(List<ExpenseItem> list)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n--- Lista ({list.Count} tétel) ---");
            Console.ResetColor();

            if (list.Count == 0)
            {
                Console.WriteLine("Nincs megjeleníthető adat.");
                return;
            }

            foreach (var item in list)
            {
                Console.Write($"[{item.Date.ToShortDateString()}] {item.Name} ({item.Category}): ");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{item.Amount} Ft");
                Console.ResetColor();
            }
        }
    }
}