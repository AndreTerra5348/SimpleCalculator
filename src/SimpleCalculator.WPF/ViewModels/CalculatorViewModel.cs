using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SimpleCalculator.Core;
using SimpleCalculator.Core.Contexties;
using SimpleCalculator.Core.Models;
using SimpleCalculator.Core.Operations;
using SimpleCalculator.WPF.Formatter;
using System;
using System.Globalization;
using System.Windows.Input;

namespace SimpleCalculator.WPF.ViewModels;


public class CalculatorViewModel : ObservableObject
{
    private const string DefaultEntryValue = "0";

    private readonly IOperationContext _operationContext;
    private readonly ICultureNumberFormatter _cultureNumberFormatter;
    private readonly ICalculator _calculator;

    private string _symbol = "";
    private string _assignedEntry = "";

    private string _entry;
    public string Entry
    {
        get => _entry;
        private set
        {
            var formattedValue = _cultureNumberFormatter.Format(value);
            SetProperty(ref _entry, formattedValue);
        }
    }

    private string _calculation;
    public string Calculation
    {
        get => _calculation;
        private set
        {
            SetProperty(ref _calculation, value);
        }
    }

    public string NumberDecimalSeparator => 
        CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

    public bool IsEntryDirty { get; private set; }
    public bool IsCalculationDirty { get; private set; }

    public ICommand OperationCommand { get; private set; }
    public ICommand EqualCommand { get; private set; }
    public ICommand DigitCommand { get; private set; }
    public ICommand DecimalCommand { get; private set; }
    public ICommand ClearCommand { get; private set; }        

    public CalculatorViewModel(ICalculator calculator,
        IOperationContext operationContext,
        ICultureNumberFormatter cultureNumberFormatter)
    {
        _calculator = calculator;
        _operationContext = operationContext;
        _cultureNumberFormatter = cultureNumberFormatter;

        Entry = DefaultEntryValue;
        IsEntryDirty = true;

        DigitCommand = new RelayCommand<string>(digit =>
        {
            Entry = IsEntryDirty ? Entry + digit : digit;
            IsEntryDirty = true;
        });

        OperationCommand = new RelayCommand<string>(symbol =>
        {
            if(IsEntryDirty)
                EqualCommand.Execute(null);

            IOperation operation = symbol switch
            {
                OperationsSymbol.Add => new AddOperation(),
                OperationsSymbol.Subtract => new SubtractOperation(),
                OperationsSymbol.Multiply => new MultiplyOperation(),
                OperationsSymbol.Divide => new DivideOperation(),
                _ => throw new InvalidOperationException($"Operation {symbol} not found")
            };

            _operationContext.SetOperation(operation);
            Calculation = $"{Entry} {_symbol = symbol}";
        });

        EqualCommand = new RelayCommand(() =>
        {
            if (IsEntryDirty)                
                AssignEntry();

            try
            {
                Entry = _calculator.Calculate().ToString();
                Calculation = _symbol.Length > 0 ? $"{Entry} {_symbol} {_assignedEntry} =" : $"{Entry} =";
            }
            catch (Exception ex)
            {
                SetEntryError(ex.Message);
            }
            finally
            {
                IsEntryDirty = false;
            }

        });

        DecimalCommand = new RelayCommand(() =>
        {
            Entry += NumberDecimalSeparator;
            UpdateCommandsCanExecuteState();
        }, 
        () => !Entry.Contains(NumberDecimalSeparator));

        ClearCommand = new RelayCommand(() =>
        {
            ClearEntry();
            ClearCalculation();
            _calculator.Clear();
        });    
    }

    void AssignEntry()
    {
        if (!double.TryParse(Entry, NumberStyles.Number, CultureInfo.CurrentCulture, out var value))
        {
            SetEntryError("Invalid Entry!");
            return;
        }

        _calculator.AssignValue(value);
        _assignedEntry = Entry;
    }

    void ClearEntry()
    {
        IsEntryDirty = false;
        Entry = DefaultEntryValue;
        UpdateCommandsCanExecuteState();
    }
    void ClearCalculation() => Calculation = String.Empty;

    void SetEntryError(string message)
    {
        Entry = message;
        ClearCalculation();
        _calculator.Clear();
    }
    
    void UpdateCommandsCanExecuteState()
    {
        (DecimalCommand as RelayCommand).NotifyCanExecuteChanged();
    }
}



