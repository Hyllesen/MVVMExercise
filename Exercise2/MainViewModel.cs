using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace Exercise
{
    public class MainViewModel : INotifyPropertyChanged
    {

        private double _x;

        public event PropertyChangedEventHandler PropertyChanged;

        public object Content { get; set; }
 
        public double X
        {
            get { return _x; }
            set
            {
                _x = value;
            }
        }
        
        public ICommand BtnSquareCommand { get; }

        public ICommand BtnCircleCommand { get; }

        public double Y { get; set; }

        public MainViewModel()
        {
            Content = new SquareViewModel();
            BtnSquareCommand = new RelayCommand(SetSquare);
            BtnCircleCommand = new RelayCommand(SetCircle);
        }

        public void SetSquare()
        {
            Content = new SquareViewModel();
            NotifyPropertyChanged("Content");
        }
        public void SetCircle()
        {
            Content = new CircleViewModel();
            NotifyPropertyChanged("Content");
        }

        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
