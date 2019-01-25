using NPPNTTApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace NPPNTTApp.ModelView
{
    public class MainViewModel : INotifyPropertyChanged
    {
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

        public RelayCommand PopulateTableCommand { get; set; }
        public RelayCommand ClearTableCommand { get; set; }
        public RelayCommand CloseAppCommand { get; set; }

        public MainViewModel()
        {
            PopulateTableCommand = new RelayCommand
                (o => BaseClassList = ReadCSV.ReadFile());

            ClearTableCommand = new RelayCommand
                 (o => BaseClassList = null);

            CloseAppCommand = new RelayCommand(o => OnClosingRequest());
        }

        protected void OnClosingRequest()
        {
            ClosingRequest?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler ClosingRequest;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
