using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;

namespace SpaceInvaders.Utils;

public class BooleanToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is bool boolValue)
        {
            if (parameter is string param && param.Equals("Inverse", StringComparison.OrdinalIgnoreCase))
            {
                return boolValue ? Visibility.Collapsed : Visibility.Visible;
            }
            return boolValue ? Visibility.Visible : Visibility.Collapsed;
        }
        return Visibility.Collapsed; // Default to collapsed if not a boolean
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
