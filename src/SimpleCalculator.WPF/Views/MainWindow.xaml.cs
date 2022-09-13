using SimpleCalculator.WPF.ViewModels;
using System.Windows;

namespace SimpleCalculator.WPF.Views;

public partial class MainWindow : Window
{
    public CalculatorViewModel ViewModel { get; private set; }
    public MainWindow(CalculatorViewModel viewModel)
    {
        ViewModel = viewModel;
        InitializeComponent();
    }
}
