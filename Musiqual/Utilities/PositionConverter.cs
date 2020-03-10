using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Musiqual.Utilities
{

    /// <summary>
    /// Scross - Parameter Position converter.
    /// </summary>
    public class PositionConverter : IValueConverter
    {

        /// <summary>
        /// Convert Parameter Position to Scross Position.
        /// </summary>
        /// <param name="value">The Parameter Position.</param>
        /// <param name="targetType">Double Position in Scross.</param>
        /// <param name="parameter">Null.</param>
        /// <param name="culture">Current culture.</param>
        /// <returns>The Scross Position.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null || targetType != typeof(double)) return null;
            // TODO
            throw new NotImplementedException();
        }

        /// <summary>
        /// Convert Scross Position to Parameter Position.
        /// </summary>
        /// <param name="value">The Scross Position.</param>
        /// <param name="targetType">Double Position in Parameter.</param>
        /// <param name="parameter">Null</param>
        /// <param name="culture">Current culture.</param>
        /// <returns>The Parameter Position.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null || targetType != typeof(double)) return null;
            // TODO
            throw new NotImplementedException();
        }

        /// <summary>
        /// The current converter.
        /// </summary>
        public static PositionConverter Current { get; } = new PositionConverter();

    }

}
