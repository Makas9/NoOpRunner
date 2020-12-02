namespace NoOpRunner.Client.Logic.Interpreter
{
    public class SequenceExpression : AbstractExpression
    {
        public AbstractExpression LeftExpression { get; }
        public AbstractExpression RightExpression { get; }

        public SequenceExpression(AbstractExpression left, AbstractExpression right)
        {
            LeftExpression = left;
            RightExpression = right;
        }

        public override void Interpret(InterpreterContext context)
        {
            LeftExpression.Interpret(context);
            RightExpression.Interpret(context);
        }
    }
}
