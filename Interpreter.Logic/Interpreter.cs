namespace InterpreterDyZ;

public class Interpreter : NodeVisitor
{
    private Parser? Parser;

    public Interpreter(Parser parser)
    {
        Parser = parser;
    }

    public override int VisitBinaryOperator(BinaryOperator node)
    {
        int result = 0;

        switch (node.Operator.Type)
        {
            case TokenTypes.PLUS:

                result = Visit(node.Left) + Visit(node.Right);
                
                break;

            case TokenTypes.MINUS:

                result = Visit(node.Left) - Visit(node.Right);
                
                break;
            
            case TokenTypes.MULT:

                result = Visit(node.Left) * Visit(node.Right);
                
                break;
            
            case TokenTypes.DIV:

                result = (int)(Visit(node.Left) / Visit(node.Right));
                
                break;
        }

        return result;
    }

    public override int VisitNum(Num node)
    {
        return (int)node.Value;
    }

    public override int VisitUnaryOperator(UnaryOperator node)
    {
        int result = 0;

        switch (node.Operator.Type)
        {
            case TokenTypes.PLUS:

                result = +Visit(node.Expression);

                break;
            
            case TokenTypes.MINUS:

                result = -Visit(node.Expression);

                break;
        }

        return result;
    }

    public int Interpret()
    {
        AST tree = Parser.Parse();

        if (tree == null)

            return -1;
        
        return Visit(tree);
    }
}