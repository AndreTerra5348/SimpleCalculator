using System;
using System.Globalization;

namespace SimpleCalculator.WPF.Formatter;

public class CultureNumberFormatter : ICultureNumberFormatter
{
    public string Format(string entry)
    {
        if (entry.EndsWith(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator))
            return entry;

        if (!double.TryParse(entry, NumberStyles.Number, CultureInfo.CurrentCulture, out var value))
            throw new ArgumentException($"Invalid entry: {entry}");

        var decimalSeparatorIndex = entry.IndexOf(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);

        var decimalCount = decimalSeparatorIndex == -1 ? 0 : entry.Length - decimalSeparatorIndex - 1;
        return value.ToString($"N{decimalCount}", CultureInfo.CurrentCulture);
    }
}


