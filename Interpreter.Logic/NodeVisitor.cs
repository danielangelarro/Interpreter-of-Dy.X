namespace InterpreterDyZ;

public abstract class NodeVisitor
{
    protected delegate int DelegateMethod(AST node);

    protected int Visit(AST node)
    {
        int result = 0;

        if (node is BinaryOperator)

            result = VisitBinaryOperator((BinaryOperator)node);
        
        else if (node is UnaryOperator)

            result = VisitUnaryOperator((UnaryOperator)node);
        
        else if (node is Num)

            result = VisitNum((Num)node);

        return result;
    }

    public abstract int VisitBinaryOperator(BinaryOperator node);
    public abstract int VisitNum(Num node);
    public abstract int VisitUnaryOperator(UnaryOperator node);
}