namespace TimeCalculator.ViewModels
{
    using System;
    using Saige.MVVM;

    public class ConvertedUnitViewModel : ViewModel
    {
        private static readonly Array _timeUnits = Enum.GetValues(typeof(ETimeUnit));

        private string _time;
        private ETimeUnit _selectedTimeUnit;

        #region Properties
        public string Time
        {
            get => this._time;
            private set => SetProperty(ref this._time, value);
        }

        public ETimeUnit SelectedTimeUnit
        {
            get => this._selectedTimeUnit;
            set => SetProperty(ref this._selectedTimeUnit, value);
        }

        public bool IsMainViewer { get; set; }
        #endregion

        public void UpdateTime(double newTime)
        {
            this.Time = newTime.ToString();
        }
    }
}
