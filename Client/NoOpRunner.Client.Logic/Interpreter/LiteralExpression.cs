using System;
using System.Text.RegularExpressions;

namespace NoOpRunner.Client.Logic.Interpreter
{
    public class LiteralExpression : AbstractExpression
    {
        public string Literal { get; }
        public LiteralExpression(string literal)
        {
            if (!Regex.IsMatch(literal, @"^[a-zA-Z0-9\-]+$"))
            {
                throw new ArgumentException("Invalid literal provided for a literal expression");
            }

            Literal = literal;
        }

        public override void Interpret(InterpreterContext context)
        {
            switch(Literal)
            {
                case "host":
                    context.ViewModel.StartHostCommand.Execute(null); // TODO: What do I pass here? Checks?
                    break;
                case "connect":
                    context.ViewModel.ConnectToHostCommand.Execute(null);
                    break;
                case "settings":
                    context.ViewModel.OpenSettingsViewCommand.Execute(null);
                    break;
                case "send-message":
                    context.ViewModel.SendMessageCommand.Execute(null);
                    break;
                default:
                    throw new ArgumentException("User query contained a literal that cannot be interpreted");
            }
        }
    }
}
