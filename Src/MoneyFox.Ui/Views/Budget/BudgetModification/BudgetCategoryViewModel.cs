namespace MoneyFox.Ui.Views.Budget.BudgetModification;

using CommunityToolkit.Mvvm.ComponentModel;

public sealed class BudgetCategoryViewModel : ObservableViewModelBase
{
    private string name = string.Empty;

    public BudgetCategoryViewModel(int categoryId, string name)
    {
        CategoryId = categoryId;
        Name = name;
    }

    public int CategoryId { get; }

    public string Name
    {
        get => name;
        set => SetProperty( ref name,   value);
    }
}
