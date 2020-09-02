namespace TimeCalculator
{
    using System.Text;

    public class TimeBuilder
    {
        private const int MAX_NUMBER_COUNT = 15;

        private readonly StringBuilder _inputCharacters = new StringBuilder(MAX_NUMBER_COUNT * 2);
        private bool _isUsedDecimalPoint;
        private int NumberCount
        {
            get
            {
                return this._isUsedDecimalPoint
                    ? this._inputCharacters.Length - 1
                    : this._inputCharacters.Length;
            }
        }

        public TimeBuilder()
        {
            this._inputCharacters.Append('0');
        }

        public void SetTime(string time)
        {
            if (ValidateStringForTime(time) == false)
                return;

            this._inputCharacters.Clear();
            this._inputCharacters.Append(time);

            if (time.Contains('.'))
                this._isUsedDecimalPoint = true;
        }

        public string ToBuild()
        {
            return this._inputCharacters.ToString();
        }

        public void Clear()
        {
            this._inputCharacters.Clear();
            this._inputCharacters.Append('0');
            this._isUsedDecimalPoint = false;
        }

        public void RemoveLastCharacter()
        {
            if (this._inputCharacters.Length == 1)
                this._inputCharacters[0] = '0';
            else
            {
                if (this._inputCharacters[^1] == '.')
                    this._isUsedDecimalPoint = false;

                this._inputCharacters.Length--;
            }
        }

        public void AppendCharacter(char character)
        {
            if (CanAppend(character) == false || ValidateCharToAppend(character) == false)
                return;

            if (character == '.')
                this._isUsedDecimalPoint = true;
            else
                TryRemoveFirstCharacterIfZero();

            this._inputCharacters.Append(character);
        }

        private bool CanAppend(char input)
        {
            if (input == '.')
                return (this._isUsedDecimalPoint == false);
            else
                return this.NumberCount < MAX_NUMBER_COUNT;
        }

        private bool TryRemoveFirstCharacterIfZero()
        {
            if (this._inputCharacters.Length == 1 && this._inputCharacters[0] == '0')
            {
                this._inputCharacters.Remove(0, 1);
                return true;
            }
            else
                return false;
        }

        private static bool ValidateCharToAppend(char input)
        {
            if ((input < '0' || input > '9') && input != '.')
                return false;
            else
                return true;
        }

        private static bool ValidateStringForTime(string input)
        {
            if (double.TryParse(input, out double output))
                if (output >= 0)
                    return true;

            return false;
        }
    }
}
