namespace EFCoreCodeFirstDemo.Entities
{
    public class Expense
    {
        public int ExpenseId { get; set; }
        public string Department { get; set; }
        public string Purpose { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}