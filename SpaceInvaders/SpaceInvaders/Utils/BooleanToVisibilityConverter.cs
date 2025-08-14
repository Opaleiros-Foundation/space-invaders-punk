using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;

namespace SpaceInvaders.Utils;

/// <summary>
/// Converts a boolean value to a <see cref="Visibility"/> value,
/// allowing control over the UI element's visibility based on a true/false condition.
/// </summary>
/// <remarks>
/// Can be used to hide or show elements. If the parameter is "Inverse",
/// the visibility is inverted (true becomes Collapsed, false becomes Visible).
/// </remarks>
public class BooleanToVisibilityConverter : IValueConverter
{
    /// <summary>
    /// Converts a boolean value to a <see cref="Visibility"/> value.
    /// </summary>
    /// <param name="value">The boolean value to convert.</param>
    /// <param name="targetType">The type of the binding target property (expected to be <see cref="Visibility"/>).</param>
    /// <param name="parameter">An optional parameter. If it's the string "Inverse" (case-insensitive),
    /// the visibility is inverted.</param>
    /// <param name="language">The language to use for the conversion (not used in this implementation).</param>
    /// <returns>
    /// <see cref="Visibility.Visible"/> if the value is true, <see cref="Visibility.Collapsed"/> if false.
    /// If the "Inverse" parameter is provided, the behavior is inverted.
    /// Returns <see cref="Visibility.Collapsed"/> by default if the value is not a boolean.
    /// </returns>
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

    /// <summary>
    /// Not implemented. Throws <see cref="NotImplementedException"/>.
    /// </summary>
    /// <param name="value">The value to convert back.</param>
    /// <param name="targetType">The type of the binding target property.</param>
    /// <param name="parameter">An optional parameter.</param>
    /// <param name="language">The language to use for the conversion.</param>
    /// <returns>Does not return.</returns>
    /// <exception cref="NotImplementedException">Always thrown, as this method is not implemented.</exception>
    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
