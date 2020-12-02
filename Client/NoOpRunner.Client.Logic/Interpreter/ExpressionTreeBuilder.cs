using System;

namespace NoOpRunner.Client.Logic.Interpreter
{
    static class ExpressionTreeBuilder
    {
        public static AbstractExpression Build(string query)
        {
            var nextIndex = query.IndexOf('&');
            if (nextIndex == -1)
            {
                return ParseQuery(query);
            }
            else
            {
                var left = query.Substring(0, nextIndex);
                var right = query.Substring(nextIndex + 1);
                return new SequenceExpression(ParseQuery(left), Build(right));
            }
        }

        private static AbstractExpression ParseQuery(string query)
        {
            var parts = query.Split(':');
            if (parts.Length == 1)
                return new LiteralExpression(parts[0]);
            else if (parts.Length == 2)
                return new KeyValueExpression(new LiteralExpression(parts[0]), new LiteralExpression(parts[1]));
            else throw new ArgumentException("Invalid key-value pair found in the provided expression");
        }
    }
}
