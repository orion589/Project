using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using DevExpress.Utils.Drawing.Helpers;
using RadioInformationCenter.Model;
using RadioInformationCenter.Properties;
using RadioInformationCenter.Tools;
using Swsu.Lignis.Logger.Server.Psdb.Exceptions;
using Swsu.Lignis.Logger.Server.Psdb.Services;

namespace RadioInformationCenter.ViewModel.TabItems
{
    public class LoggingWorkRicViewModel : ViewModelBase
    {
        #region Fields

        private readonly Guid SoftwareId = Settings.Default.RICGuid;
        private Helper.ModuleWorkTypeEnum _moduleWorkType = Helper.ModuleWorkTypeEnum.NormalWork;

        #endregion

        #region Properties

        public ObservableCollection<RicEvent> RicEvents { get; } = new ObservableCollection<RicEvent>();

        public Helper.ModuleWorkTypeEnum ModuleWorkType
        {
            get { return _moduleWorkType; }
            set { SetProperty(ref _moduleWorkType, value, nameof(ModuleWorkType)); }
        }

        public DateTime Start { get; set; } = DateTime.Now.AddYears(-2);
        public DateTime End { get; set; } = DateTime.Now.AddDays(1);

        #endregion

        #region Commands

        public DelegateCommand LoadRicMessageCommand { get; set; }

        #endregion

        #region Constructor

        public LoggingWorkRicViewModel()
        {
            LoadRicMessageCommand = new DelegateCommand(LoadRicMessageAsync);

        }

        #endregion

        #region Methods

        private async void LoadRicMessageAsync()
        {
            try
            {
                ModuleWorkType = Helper.ModuleWorkTypeEnum.LoadFromDb;
                var events = await LoadRicEvents();
                RicEvents.Clear();
                events.ForEach(e => RicEvents.Add(e));
            }
            catch (PsdbException)
            {
                MessageBox.Show("Отсутствует соединение", "Внимание", MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
            }

            catch (PsdbServiceException)
            {
                MessageBox.Show("Отсутствует соединение", "Внимание", MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
            }
            catch (Exception e)
            {
                Helper.Logger.Error(e, "Не удалось загрузить сообщения РИЦ");
                Debug.WriteLine(e);
            }
            finally
            {
                ModuleWorkType = Helper.ModuleWorkTypeEnum.NormalWork;
            }
        }

        private Task<List<RicEvent>> LoadRicEvents()
        {
            var events = new List<RicEvent>();

            return Task.Run(() =>
            {
                using (var svc = new SoftwareService())
                {
                    var softIds = svc.GetSoftwares().Where(s => s.SoftwareKey == SoftwareId).Select(s => s.Key);

                    foreach (var sId in softIds)
                    {
                        var logs = svc.GetMessageLog((Guid) sId, Start, End);

                        foreach (var log in logs)
                        {
                            var ev = new RicEvent
                            {
                                Message = log.Message,
                                DateTime = log.CaptureDate
                            };
                            events.Add(ev);
                            
                        }
                    }
                }

                return events;
            });
        }


        #endregion


    }
}
