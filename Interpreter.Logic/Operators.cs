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

public class Bool : AST
{
    public Token Token;
    public bool Value;

    public Bool(Token token)
    {
        Token = token;
        Value = (bool)token.Value;
    }
}

public class Cadene : AST
{
    public Token Token;
    public string Value;

    public Cadene(Token token)
    {
        Token = token;
        Value = (string)token.Value;
    }
}

public class Type : AST
{
    public Token Token;
    public object Value;

    public Type(Token token)
    {
        Token = token;
        Value = token.Value;
    }
}

public class Instructions : AST
{
    public List<AST>? Commands;

    public Instructions()
    {
        Commands = new List<AST>();
    }
}

public class Declarations : AST
{
    public List<AST>? Commands;

    public Declarations()
    {
        Commands = new List<AST>();
    }
}

public class Assign : AST
{
    public Var Left;
    public Token Operator;
    public AST Right;

    public Assign(Var left, Token op, AST right)
    {
        Left = left;
        Operator = op;
        Right = right;
    }
}

public class Var : AST
{
    public Token Token;
    public object Value;

    public Var(Token token)
    {
        Token = token;
        Value = token.Value;
    }
}

public class VarDecl : AST
{
    public Var Node;
    public TokenTypes Type;

    public VarDecl(Var node, TokenTypes type)
    {
        Node = node;
        Type = type;
    }
}

public class Empty : AST
{
    
}

public class Condition : AST
{
    public AST Compound;
    public AST StatementList;

    public Condition(AST compound, AST statements)
    {
        Compound = compound;
        StatementList = statements;
    }
}

public class Cicle : AST
{
    public AST Compound;
    public AST StatementList;

    public Cicle(AST compound, AST statements)
    {
        Compound = compound;
        StatementList = statements;
    }
}
