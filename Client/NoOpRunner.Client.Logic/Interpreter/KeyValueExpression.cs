using System;

namespace NoOpRunner.Client.Logic.Interpreter
{
    public class KeyValueExpression : AbstractExpression
    {
        public LiteralExpression Key { get; }
        public LiteralExpression Value { get; }

        public KeyValueExpression(LiteralExpression key, LiteralExpression value)
        {
            Key = key;
            Value = value;
        }

        public override void Interpret(InterpreterContext context)
        {
            switch(Key.Literal)
            {
                case "volume-level":
                    var newVolume = int.Parse(Value.Literal);
                    context.ViewModel.SettingsViewModel.ChangeVolume(newVolume);
                    context.ViewModel.SettingsViewModel.ApplySettings();
                    break;
                case "resolution-index":
                    var newIndex = int.Parse(Value.Literal);
                    context.ViewModel.SettingsViewModel.ChangeResolution(newIndex);
                    context.ViewModel.SettingsViewModel.ApplySettings();
                    break;
                default:
                    throw new ArgumentException("Unkown key provided in the user query.");
            }
        }
    }
}
