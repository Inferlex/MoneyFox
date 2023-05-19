namespace MoneyFox.Core.Queries.BudgetList;

public class BudgetListData
{
    public BudgetListData(int id, string name, decimal spendingLimit, decimal currentSpending)
    {
        Id = id;
        Name = name;
        SpendingLimit = spendingLimit;
        CurrentSpending = currentSpending;
    }

    public int Id { get; }

    public string Name { get; }

    public decimal SpendingLimit { get; }

    public decimal CurrentSpending { get; }
}
