using NPPNTTApp.Model;
using NPPNTTApp.View;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NPPNTTApp.ModelView
{
    public class MainViewModel : BaseViewModel
    {
        #region private fields

        private string _progressMessage;
        Progress<BaseProgressData> _progress;
        private List<BaseClass> _baseClassList;
        CancellationTokenSource tokenSource;
        private string _path;

        #endregion

        #region Bindible Properties

        public string Path
        {
            get { return _path; }
            set
            {
                _path = value;
                OnPropertyChanged(nameof(ProgressMessage));
                BeginLoadAsync()    ;
            }
        }

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


        public ICommand ClearTableCommand { get; set; }
        public ICommand CloseAppCommand { get; set; }
        public ICommand CancelLoadCommand { get; set; }
        public int Item { get; set; }
        public int Total { get; set; }

        #endregion

        public MainViewModel()
        {
            _progress = new Progress<BaseProgressData>
                (d =>
                {
                    ProgressMessage = $"{d.Item} rows out of {d.Total} is loaded";
                    Item = d.Item; Total = d.Total;
                });

            ClearTableCommand = new RelayCommand(o => BaseClassList = null);
            CloseAppCommand = new RelayCommand(o => OnClosingRequest());
            CancelLoadCommand = new RelayCommand(o => CancelLoad());
        }

        private async void BeginLoadAsync()
        {
            tokenSource = new CancellationTokenSource();
            BaseClassList = await ReadCSV.Get(Path, _progress, tokenSource.Token);

            if (BaseClassList.Count != 0)
                ProgressMessage = "Loading completed";

            await Task.Delay(1000);
            ProgressMessage = "Ready";
        }

        private void CancelLoad()
        {
            tokenSource.Cancel();
            ProgressMessage = "Loading was cancelled";
        }

        protected void OnClosingRequest()
        {
            ClosingRequest?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler ClosingRequest;
    }
}
