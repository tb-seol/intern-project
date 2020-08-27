namespace TimeCalculator.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using Saige.MVVM;
    using TimeCalculator.Models;

    public class TimeCalculatorViewModel : ViewModel
    {
        private readonly TimeBuilder _timeBuilder = new TimeBuilder();
        private readonly List<double> _times = new List<double>();
        private readonly int _timeUnitCount = Enum.GetNames(typeof(ETimeUnit)).Length;

        /*
        private string _mainTime;
        private string _subTime;
        private ETimeUnit _mainTimeUnit;
        private ETimeUnit _subTimeUnit;

        #region Properties
        public string MainTime
        {
            get => this._mainTime;
            private set => SetProperty(ref this._mainTime, value);
        }
        public string SubTime
        {
            get => this._subTime;
            private set => SetProperty(ref this._subTime, value);
        }
        public ETimeUnit SelectedMainTimeUnit
        {
            get => this._mainTimeUnit;
            set => SetProperty(ref this._mainTimeUnit, value);
        }
        public ETimeUnit SelectedSubTimeUnit
        {
            get => this._subTimeUnit;
            set => SetProperty(ref this._subTimeUnit, value);
        }
        */
        #region Properties
        public ConvertedUnitViewModel TopViewer { get; } = new ConvertedUnitViewModel();
        public ConvertedUnitViewModel BottomViewer { get; } = new ConvertedUnitViewModel();

        public RelayCommand<char> InputCommand { get; private set; }
        public RelayCommand ClearCommand { get; private set; }
        public RelayCommand DeleteCommand { get; private set; }
        #endregion

        public TimeCalculatorViewModel()
        {
            this.InputCommand = new RelayCommand<char>(AppendCharacter);
            this.ClearCommand = new RelayCommand(Clear);
            this.DeleteCommand = new RelayCommand(RemoveLastCharacter);

            this.TopViewer.IsMainViewer = true;

            for (int i = 0; i < this._timeUnitCount; i++)
                this._times.Add(0);

            UpdateCurrentTime();
            UpdateViewer();
        }

        protected override void OnPropertyChanged(object oldValue, object newValue, [CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(oldValue, newValue, propertyName);
            switch (propertyName)
            {
                case nameof(ConvertedUnitViewModel.SelectedTimeUnit):
                    UpdateViewer();
                    break;
                default:
                    break;
            }
        }

        public void AppendCharacter(char character)
        {
            this._timeBuilder.AppendCharacter(character);
            UpdateCurrentTime();
        }

        public void Clear()
        {
            this._timeBuilder.Clear();
            UpdateCurrentTime();
        }

        public void RemoveLastCharacter()
        {
            this._timeBuilder.RemoveLastCharacter();
            UpdateCurrentTime();
        }

        private void UpdateCurrentTime()
        {
            double srcTime = this._timeBuilder.ToBuild();
            ETimeUnit srcTimeUnit = this.TopViewer.IsMainViewer
                ? this.TopViewer.SelectedTimeUnit
                : this.BottomViewer.SelectedTimeUnit;

            for (int unitIndex = 0; unitIndex < this._timeUnitCount; unitIndex++)
            {
                var destTimeUnit = (ETimeUnit)unitIndex;
                this._times[unitIndex] = TimeConverter.Convert(srcTime, srcTimeUnit, destTimeUnit);
            }
        }

        private void UpdateViewer()
        {
            this.TopViewer.UpdateTime(this._times[(int)this.TopViewer.SelectedTimeUnit]);
            this.BottomViewer.UpdateTime(this._times[(int)this.BottomViewer.SelectedTimeUnit]);
        }
    }
}
