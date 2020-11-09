using System;
using System.Text.RegularExpressions;

namespace Calculator.Model
{
    static class MathCalculateExpression
    {

        public static int Calculate(string expression)
        {
            int result = 0;

            while (true)
            {
                if (int.TryParse(expression, out result))
                {
                    return result;
                }
                string subExpression = GetFirstExpression(expression);
                string subResult = CalculateSubExpression(subExpression);

                string replaced = Regex.Replace(subExpression, @"[()*/+-]", @"\$0");
                expression = new Regex(replaced).Replace(expression, subResult, 1);
            }
        }

        private static string CalculateSubExpression(string subExpression)
        {
            if (Regex.Match(subExpression, @"(\d+[\+|\-|\*|\/]\d+)").Value == "")
            {
                throw new System.ArgumentException("Incorrect expression", subExpression);
            }

            subExpression = Regex.Replace(subExpression, @"[()]", "");

            while (Regex.Match(subExpression, @"(\d+[\+|\-|\*|\/]\d+)").Value != "")
            {

                string firstSubExpression = GetFirstExpression(subExpression);

                string operatorExpression = Regex.Match(firstSubExpression, @"[\+|\-|\*|\/]").Value;
                int firstOperand = Convert.ToInt32(Regex.Matches(firstSubExpression, @"\d+")[0].Value);
                int secondOperand = Convert.ToInt32(Regex.Matches(firstSubExpression, @"\d+")[1].Value);
                int result = 0;

                if (operatorExpression != "")
                {
                    switch (operatorExpression)
                    {
                        case "+":
                            result = firstOperand + secondOperand;
                            break;
                        case "-":
                            result = firstOperand - secondOperand;
                            break;
                        case "*":
                            result = firstOperand * secondOperand;
                            break;
                        case "/":
                            if (secondOperand != 0) { result = firstOperand / secondOperand; break; }
                            else { result = 123456789; break; }
                    }
                }

                string replaced = Regex.Replace(firstSubExpression, @"[()*/+-]", @"\$0");
                subExpression = new Regex(replaced).Replace(subExpression, Convert.ToString(result), 1);
            }

            return subExpression;
        }

        private static string GetFirstExpression(string expression)
        {
            string subExpression;

            string patternBrackets = @"\([^(|)]+\)";
            subExpression = Regex.Match(expression, patternBrackets).Value;
            if (subExpression != "")
            {
                return subExpression;
            }

            string patternMultiply = @"\d+[\*|/]\d+";
            subExpression = Regex.Match(expression, patternMultiply).Value;
            if (subExpression != "")
            {
                return subExpression;
            }
            
            string patternAdd = @"\d+[\+|-]\d+";
            subExpression = Regex.Match(expression, patternAdd).Value;
            if (subExpression != "")
            {
                return subExpression;
            }

            return expression;
        }
    }
}
