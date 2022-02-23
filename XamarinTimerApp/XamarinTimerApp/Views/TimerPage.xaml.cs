using System;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace XamarinTimerApp.Views
{
    public partial class TimerPage : ContentPage
    {
        private int Seconds { get; set; } = 0;
        private readonly Timer Timer = new Timer() { Enabled = false, Interval = 1000 };
        private TimerStatus CurrentTimerStatus = TimerStatus.Stopped;
        private bool IsTimerPaused = false;

        public TimerPage()
        {
            InitializeComponent();

            Timer.Elapsed += new ElapsedEventHandler(ElapsedTimerEvent);

            RestWorkButton.Clicked += OnRestWorkButtonClicked;
            ResetButton.Clicked += OnResetButtonClick;
            PauseButton.Clicked += OnPauseButtonClick;
        }

        private void OnRestWorkButtonClicked(object sender, EventArgs e)
        {
            if (CurrentTimerStatus == TimerStatus.Stopped) SwitchTimerToWork();
            else if (CurrentTimerStatus == TimerStatus.Work) SwitchTimerToRest();
            else if (CurrentTimerStatus == TimerStatus.Rest) SwitchTimerToWork();
        }

        private void OnResetButtonClick(object sender, EventArgs e)
        {
            SwitchTimerToReset();
        }

        private void OnPauseButtonClick(object sender, EventArgs e)
        {
            if (IsTimerPaused)
            {
                IsTimerPaused = false;
                Timer.Enabled = true;
                PauseButton.Text = "Pause";
            }
            else if (!IsTimerPaused)
            {
                IsTimerPaused = true;
                Timer.Enabled = false;
                PauseButton.Text = "Resume";
            }
        }

        private void AddOrRemoveASecondToTimer()
        {
            if (CurrentTimerStatus == TimerStatus.Work) Seconds++;
            if (CurrentTimerStatus == TimerStatus.Rest) Seconds--;

            if (Seconds <= 0 && CurrentTimerStatus == TimerStatus.Rest)
            {
                SwitchTimerToWork();
            }
        }

        private void AdjustAmountOfRestTime()
        {
            Seconds = (int)Math.Floor(Seconds * 0.25);
        }

        private void SwitchTimerToWork()
        {
            RestWorkButton.Text = "Rest";
            TimerStatusLabel.Text = "Work";

            if (Timer.Enabled == false) Timer.Enabled = true;
            if (CurrentTimerStatus == TimerStatus.Rest)
            {
                Seconds = 0;
                TimerStopwatchLabel.Text = "00:00:00";
            }
            CurrentTimerStatus = TimerStatus.Work;
        }

        private void SwitchTimerToRest()
        {
            TimerStatusLabel.Text = "Rest";
            RestWorkButton.Text = "Work";

            CurrentTimerStatus = TimerStatus.Rest;
            AdjustAmountOfRestTime();
        }

        private void SwitchTimerToReset()
        {
            CurrentTimerStatus = TimerStatus.Stopped;
            Seconds = 0;
            Timer.Enabled = false;
            IsTimerPaused = false;
            RestWorkButton.Text = "Work";
            TimerStatusLabel.Text = "Stopped";
            TimerStopwatchLabel.Text = "00:00:00";
        }

        private void UpdateStopwatchLabelText()
        {
            TimerStopwatchLabel.Text = $"{Math.Floor((float)Seconds / 3600):00}:{Math.Floor((float)Seconds / 60):00}:{Seconds % 60:00}";
        }

        private void ElapsedTimerEvent(object o, ElapsedEventArgs e)
        {
            if (CurrentTimerStatus == TimerStatus.Stopped) return;

            MainThread.BeginInvokeOnMainThread(() =>
            {
                AddOrRemoveASecondToTimer();
                UpdateStopwatchLabelText();
            });
        }

        enum TimerStatus
        {
            Work,
            Rest,
            Stopped
        }

    }
}