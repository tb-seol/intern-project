namespace TimeCalculator.ViewModels
{
    using System.Runtime.CompilerServices;
    using Saige.MVVM;
    using TimeCalculator.Models;

    public class TimeCalculatorViewModel : ViewModel
    {
        private readonly TimeBuilder _timeBuilder = new TimeBuilder();

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

        public RelayCommand<char> InputCommand { get; private set; }
        public RelayCommand ClearCommand { get; private set; }
        public RelayCommand DeleteCommand { get; private set; }
        #endregion

        public TimeCalculatorViewModel()
        {
            this.InputCommand = new RelayCommand<char>(AppendCharacter);
            this.ClearCommand = new RelayCommand(Clear);
            this.DeleteCommand = new RelayCommand(RemoveLastCharacter);

            Update();
        }

        public void AppendCharacter(char character)
        {
            this._timeBuilder.AppendCharacter(character);
            Update();
        }

        public void Clear()
        {
            this._timeBuilder.Clear();
            Update();
        }

        public void RemoveLastCharacter()
        {
            this._timeBuilder.RemoveLastCharacter();
            Update();
        }

        protected override void OnPropertyChanged(object oldValue, object newValue, [CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(oldValue, newValue, propertyName);
            switch (propertyName)
            {
                case nameof(this.SelectedTimeUnit):
                    ConvertTime((ETimeUnit)oldValue, (ETimeUnit)newValue);
                    Update();
                    break;
                default:
                    break;
            }
        }

        private void Update()
        {
            this.Time = this._timeBuilder.ToBuild();
        }

        private void ConvertTime(ETimeUnit oldUnit, ETimeUnit newUnit)
        {
            string newTime = TimeConverter.Convert(this._time, oldUnit, newUnit);
            this._timeBuilder.SetTime(newTime);
        }
    }
}
