using System;

namespace Calculator.Model
{
    class MathExpression
    {
        private string _Content;
        private int _Result;

        public string Content { get => _Content; }
        public int Result { get => _Result; }

        public MathExpression() { }

        public MathExpression(string expression)
        {
            _Content = expression;
        }

        public void AddChar(object obj)
        {
            char symbol = Convert.ToChar(obj);
            _Content += symbol;
        }

        public void CalculateResult()
        {
            _Result = MathCalculateExpression.Calculate(_Content);
        }

        public void Clear()
        {
            _Content = "";
            _Result = 0;
        }

        public void Delete()
        {
            if (_Content.Length > 0)
            {
                _Content = _Content.Remove(_Content.Length - 1);
            }
        }
    }
}
