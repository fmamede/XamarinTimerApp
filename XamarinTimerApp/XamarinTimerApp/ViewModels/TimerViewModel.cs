using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace XamarinTimerApp.ViewModels
{
    public class TimerViewModel : BaseViewModel
    {
        public TimerViewModel()
        {
            Title = "Timer";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
        }

        public ICommand OpenWebCommand { get; }
    }
}