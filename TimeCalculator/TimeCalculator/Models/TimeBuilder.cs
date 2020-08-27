namespace TimeCalculator
{
    using System.Text;

    public class TimeBuilder
    {
        private const int MAX_LENGTH = 16;
        private const int MAX_LENGTH_WITHOUT_DECIMAL_POINT = MAX_LENGTH - 1;

        private readonly StringBuilder _inputCharacters = new StringBuilder(MAX_LENGTH * 2);
        private bool _isUsedDecimalPoint;

        public TimeBuilder()
        {
            this._inputCharacters.Append('0');
        }

        public double ToBuild()
        {
            return double.Parse(this._inputCharacters.ToString());
        }

        public void Clear()
        {
            this._inputCharacters.Clear();
            this._inputCharacters.Append('0');
            this._isUsedDecimalPoint = false;
        }

        public void RemoveLastCharacter()
        {
            if (this._inputCharacters.Length == 0)
                return;

            if (this._inputCharacters[^1] == '.')
                this._isUsedDecimalPoint = false;

            this._inputCharacters.Length--;
        }

        public void AppendCharacter(char character)
        {
            if (CanAppend() == false && ValidateCharToAppend(character) == false)
                return;

            if (character == '.')
            {
                if (this._isUsedDecimalPoint)
                    return;

                this._isUsedDecimalPoint = true;
            }
            else
                RemoveFirstZero();

            this._inputCharacters.Append(character);
        }

        private bool CanAppend()
        {
            if (this._inputCharacters.Length == MAX_LENGTH)
                return false;

            if (this._isUsedDecimalPoint == false &&
                this._inputCharacters.Length == MAX_LENGTH_WITHOUT_DECIMAL_POINT)
                return false;

            return true;
        }

        private static bool ValidateCharToAppend(char input)
        {
            if ((input < '0' || input > '9') && input != '.')
                return true;
            else
                return false;
        }

        private void RemoveFirstZero()
        {
            if (this._inputCharacters.Length > 1)
                return;

            if (this._inputCharacters[0] == '0')
                this._inputCharacters.Remove(0, 1);
        }
    }
}
