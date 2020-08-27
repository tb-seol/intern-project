namespace TimeCalculator.ViewModels
{
    using System;
    using System.Collections.Generic;
    using Saige.MVVM;
    using TimeCalculator.Models;

    public class TimeCalculatorViewModel : ViewModel
    {
        private readonly TimeBuilder _timeBuilder = new TimeBuilder();
        private readonly List<double> _times = new List<double>();
        private readonly int _timeUnitCount = Enum.GetNames(typeof(ETimeUnit)).Length;

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

            for (int i = 0; i < this._timeUnitCount; i++)
                this._times.Add(0);

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

        private void Update()
        {
            UpdateCurrentTime();
            UpdateViewer();
        }

        private void UpdateCurrentTime()
        {
            double srcTime = this._timeBuilder.ToBuild();
            ETimeUnit srcTimeUnit = this.TopViewer.SelectedTimeUnit;

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
