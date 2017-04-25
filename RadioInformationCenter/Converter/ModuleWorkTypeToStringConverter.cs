using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using RadioInformationCenter.Tools;

namespace RadioInformationCenter.Converter
{
    public class ModuleWorkTypeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                var worktype = value as Helper.ModuleWorkTypeEnum?;
                switch (worktype)
                {
                    case (null):
                        return String.Empty;

                    case (Helper.ModuleWorkTypeEnum.NormalWork):
                        return String.Empty;

                    case (Helper.ModuleWorkTypeEnum.WorkWithDB):
                        return "Работа с базой данных";
                    //                        return Properties.Resources.MWTWorkWithDB;

                    case (Helper.ModuleWorkTypeEnum.LoadFromDb):
                        return "Загрузка из базы данных";
                    //                        return Properties.Resources.MWTLoadFromDb;

                    case (Helper.ModuleWorkTypeEnum.SaveToDB):
                        return "Запись в базу данных";
                        //                        return Properties.Resources.MWTSaveToDb;

                }
            }
            catch (Exception ex)
            {
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                var content = (string)value;
                if (content == string.Empty) return Helper.ModuleWorkTypeEnum.NormalWork;
                else return Helper.ModuleWorkTypeEnum.WorkWithDB;
            }
            catch (Exception ex)
            {
            }
            return false;
        }
    }
}
