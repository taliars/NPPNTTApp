using NPPNTTApp.Model;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace NPPNTTApp.ModelView
{
    public class MainViewModel : BaseViewModel
    {
        private string _progressMessage;

        public string ProgressMessage
        {
            get { return _progressMessage; }
            set
            {
                _progressMessage = value;
                OnPropertyChanged(nameof(ProgressMessage));
            }
        }

        Progress<BaseProgressData> _progress;

        private List<BaseClass> _baseClassList;

        public List<BaseClass> BaseClassList
        {
            get { return _baseClassList; }
            set
            {
                _baseClassList = value;
                OnPropertyChanged(nameof(BaseClassList));
            }
        }

        public ICommand PopulateTableCommand { get; set; }
        public ICommand ClearTableCommand { get; set; }
        public ICommand CloseAppCommand { get; set; }

        public MainViewModel()
        {
            PopulateTableCommand = new RelayCommand
                (async o => BaseClassList = await ReadCSV.Get(_progress));

            ClearTableCommand = new RelayCommand
                 (o => BaseClassList = null);

            CloseAppCommand = new RelayCommand(o => OnClosingRequest());

            _progress = new Progress<BaseProgressData>(d => ProgressMessage = $"{d.Item} from {d.Total}");
        }


        protected void OnClosingRequest()
        {
            ClosingRequest?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler ClosingRequest;
    }
}
