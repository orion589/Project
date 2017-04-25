using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;

namespace RadioInformationCenter.ViewModel.TabItem
{
   public class LoggingInputRICViewModel : ViewModelBase
    {

        #region Fields

        #endregion

        #region Properties
        public DateTime Start { get; set; } = DateTime.Now.AddYears(-2);
        public DateTime End { get; set; } = DateTime.Now.AddDays(1);

        #endregion

        #region Commends

        #endregion

        #region Constructors
        public LoggingInputRICViewModel()
        {
        }
        #endregion

        #region Methods
        #endregion

    }
}
