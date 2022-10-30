using System.Linq.Expressions;
namespace InterpreterDyZ;

public class AST
{

}

public class BinaryOperator : AST
{
    public AST Left, Right;
    public Token Operator;

    public BinaryOperator(AST left, Token op, AST right)
    {
        Left = left;
        Operator = op;
        Right = right;
    }
}

public class UnaryOperator : AST
{
    public Token Operator;
    public AST Expression;

    public UnaryOperator(Token op, AST expr)
    {
        Operator = op;
        Expression = expr;
    }
}

public class Num : AST
{
    public Token Token;
    public object Value;


    public Num(Token token)
    {
        Token = token;
        Value = token.Value;
    }
}