using System;
using System.Collections.Generic;

namespace BudgetTracker
{
    public class ExpenseManager
    {
        private List<ExpenseItem> _items;
        private readonly FileService _fileService;

        public ExpenseManager()
        {
            _fileService = new FileService();
            // Adatok betoltese
            _items = _fileService.LoadData();
        }

        public void AddExpense(string name, int amount, string category)
        {
            // 0-nal kisebb osszeg ervenytelen
            if (amount <= 0)
            {
                throw new ArgumentException("Az összegnek pozitívnak kell lennie!");
            }

            var newItem = new ExpenseItem(name, amount, category, DateTime.Now);
            _items.Add(newItem);

            // Frissitjuk fajlban is
            _fileService.SaveData(_items);
        }

        public List<ExpenseItem> GetAllExpenses()
        {
            return _items;
        }
    }
}