using NPPNTTApp.Model;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Input;

namespace NPPNTTApp.ModelView
{
    public class MainViewModel : BaseViewModel
    {
        private string _progressMessage;
        Progress<BaseProgressData> _progress;
        private List<BaseClass> _baseClassList;
        
        public string ProgressMessage
        {
            get { return _progressMessage; }
            set
            {
                if (_progressMessage != value)
                {
                    _progressMessage = value;
                    OnPropertyChanged(nameof(ProgressMessage));
                }
            }
        }

        public List<BaseClass> BaseClassList
        {
            get { return _baseClassList; }
            set
            {
                if (_baseClassList != value)
                {
                    _baseClassList = value;
                    OnPropertyChanged(nameof(BaseClassList));
                }
            }
        }

        public ICommand PopulateTableCommand { get; set; }
        public ICommand ClearTableCommand { get; set; }
        public ICommand CloseAppCommand { get; set; }
        public ICommand CancelLoadCommand { get; set; }

        public MainViewModel()
        {
            _progress = new Progress<BaseProgressData>
                (d => ProgressMessage = $"{d.Item} rows out of {d.Total} is loaded");

            PopulateTableCommand = new RelayCommand(o => BeginLoad());
            ClearTableCommand = new RelayCommand(o => BaseClassList = null);
            CloseAppCommand = new RelayCommand(o => OnClosingRequest());
            CancelLoadCommand = new RelayCommand(o => CancelLoad());
        }

        CancellationTokenSource m_source;

        private async void BeginLoadAsync(CancellationToken token)
        {
            BaseClassList = await ReadCSV.Get(_progress, token);
        }

        void BeginLoad()
        {
            m_source = new CancellationTokenSource();
            BeginLoadAsync(m_source.Token);
        }

        private void CancelLoad()
        {
            m_source.Cancel();
            ProgressMessage = "Loading is cancelled";
        }

        protected void OnClosingRequest()
        {
            ClosingRequest?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler ClosingRequest;
    }
}
