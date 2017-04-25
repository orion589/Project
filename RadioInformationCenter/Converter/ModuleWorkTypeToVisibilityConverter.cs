using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using RadioInformationCenter.Tools;


namespace RadioInformationCenter.Converter
{
    public class ModuleWorkTypeToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                var worktype = value as Helper.ModuleWorkTypeEnum?;
                if (worktype == null) return Visibility.Visible;
                if (worktype == Helper.ModuleWorkTypeEnum.NormalWork) return Visibility.Collapsed;

                return Visibility.Visible;
            }
            catch (Exception ex)
            {

            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                var content = (Visibility)value;
                if (content == Visibility.Collapsed) return Helper.ModuleWorkTypeEnum.NormalWork;

                return Helper.ModuleWorkTypeEnum.WorkWithDB;
            }
            catch (Exception ex)
            {
                Helper.Logger.Error(ex, "Нет данных");
            }
            return false;
        }
    }
}
