
using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SpaceInvaders.Presentation
{
    /// <summary>
    /// Converts an integer value to an enumerable collection of integers,
    /// useful for creating loops in XAML.
    /// </summary>
    /// <remarks>
    /// For example, if the input value is 5, it will return a collection of [0, 1, 2, 3, 4].
    /// </remarks>
    public class IntToLoopConverter : IValueConverter
    {
        /// <summary>
        /// Converts an integer value to a sequence of integers from 0 up to (value - 1).
        /// </summary>
        /// <param name="value">The integer value to convert.</param>
        /// <param name="targetType">The type of the binding target property (not used in this implementation).</param>
        /// <param name="parameter">An optional parameter (not used in this implementation).</param>
        /// <param name="language">The language to use for the conversion (not used in this implementation).</param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> of integers from 0 up to (value - 1) if the value is an integer;
        /// otherwise, returns an empty collection.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is int count)
            {
                return Enumerable.Range(0, count);
            }
            return Enumerable.Empty<int>();
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
}
