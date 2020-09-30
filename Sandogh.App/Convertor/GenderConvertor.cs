using System;
using System.Globalization;
using System.Windows.Data;

namespace Sandogh.App.Convertor
{
    class GenderConvertor : IValueConverter
    {
        enum Gender
        {
             زن,مرد
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
         
            switch (value)
            {
                case false:
                    return Gender.زن;
                case true:
                    return Gender.مرد;                
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case Gender.زن: return 0;
                case Gender.مرد: return 1;                
            }
            return null;
        }
    }
}
