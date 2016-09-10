using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Exercise.Command
{
    class CircleCommand : ICommand
    {

        public MainViewModel MainViewModel { get; set; }

        public CircleCommand(MainViewModel mainViewModel)
        {
            this.MainViewModel = mainViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this.MainViewModel.SetCircle();
        }
    }
}
