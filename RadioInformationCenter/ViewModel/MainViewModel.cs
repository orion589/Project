using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using RadioInformationCenter.Properties;
using RadioInformationCenter.Tools;
using RadioInformationCenter.ViewModel.TabItem;
using RadioInformationCenter.ViewModel.TabItems;
using Swsu.Lignis.Logger.Server.Psdb;
using Swsu.Lignis.MiddleWare.Db;

namespace RadioInformationCenter.ViewModel
{
   public class MainViewModel:BindableBase
    {
        #region Fields
        public LoggingWorkRicViewModel LoggingWorkRicViewModel { get; }
        public LoggingInputRICViewModel LoggingInputRICViewModel { get; }
        public LoggingOutRICViewModel LoggingOutRICViewModel { get; }
        #endregion

        #region Properties

        #endregion

        #region Command

        #endregion

        #region Constructor
        public MainViewModel()
        {
            try
            {
                
                var connection = LoggerSystemDatabase.CheckDatabase(Settings.Default.ConnectionString);
                var docDb = new LoggerSystemDatabase(Settings.Default.ConnectionString);
                DatabaseFactory.SetDatabases(() => docDb);
                Helper.Logger.Info($"{docDb.ConnectionString}");
                LoggerSystemDatabase.InitializeFactories();

                LoggingWorkRicViewModel = new LoggingWorkRicViewModel();
                LoggingInputRICViewModel=new LoggingInputRICViewModel();
                LoggingOutRICViewModel=new LoggingOutRICViewModel();
            }
            catch (Exception e)
            {
                Helper.Logger.Error(e, "Не удалось иницивлизировать базу данных.");
                return;
            }
        }
        #endregion

        #region Methods

        #endregion
    }
}
