namespace TimeCalculator
{
    using System.Text;

    public class TimeBuilder
    {
        private const int MAX_NUMBER_COUNT = 15;

        private readonly StringBuilder _inputCharacters = new StringBuilder(MAX_NUMBER_COUNT * 2);
        private uint _numberCount = 1;
        private bool _isUsedDecimalPoint;

        public TimeBuilder()
        {
            this._inputCharacters.Append('0');
        }

        public void SetTime(string time)
        {
            this._inputCharacters.Clear();
            this._inputCharacters.Append(time);

            if (time.Contains('.'))
                this._isUsedDecimalPoint = true;

            this._numberCount = (uint)time.Length;

            if (this._isUsedDecimalPoint)
                this._numberCount--;
        }

        public string ToBuild()
        {
            return this._inputCharacters.ToString();
        }

        public void Clear()
        {
            this._inputCharacters.Clear();
            this._inputCharacters.Append('0');
            this._numberCount = 1;
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
                else
                    this._numberCount--;
                this._inputCharacters.Length--;
            }
        }

        public void AppendCharacter(char character)
        {
            if (CanAppend(character) == false && ValidateCharToAppend(character) == false)
                return;

            if (character == '.')
                this._isUsedDecimalPoint = true;
            else if (TryRemoveFirstCharacterIfZero() == false)
                this._numberCount++;

            this._inputCharacters.Append(character);
        }

        private bool CanAppend(char input)
        {
            if (input == '.')
                return (this._isUsedDecimalPoint == false);
            else
                return this._numberCount < MAX_NUMBER_COUNT;
        }

        private static bool ValidateCharToAppend(char input)
        {
            if ((input < '0' || input > '9') && input != '.')
                return true;
            else
                return false;
        }

        private bool TryRemoveFirstCharacterIfZero()
        {
            if (this._inputCharacters.Length > 1)
                return false;

            if (this._inputCharacters[0] == '0')
            {
                this._inputCharacters.Remove(0, 1);
                return true;
            }

            return false;
        }
    }
}
