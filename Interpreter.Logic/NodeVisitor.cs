namespace InterpreterDyZ;

public abstract class NodeVisitor
{
    protected delegate int DelegateMethod(AST node);

    protected object Visit(AST node)
    {
        if (node is BinaryOperator)

            return VisitBinaryOperator((BinaryOperator)node);
        
        else if (node is UnaryOperator)

            return VisitUnaryOperator((UnaryOperator)node);
        
        else if (node is Num)

            return VisitNum((Num)node);
        
        else if (node is Cadene)

            return VisitCadene((Cadene)node);

        else if (node is Instructions)

            return VisitInstructions((Instructions)node);

        else if (node is Declarations)

            return VisitDeclarations((Declarations)node);

        else if (node is Assign)

            return VisitAssign((Assign)node);

        else if (node is Condition)

            return VisitCondition((Condition)node);

        else if (node is Cicle)

            return VisitCicle((Cicle)node);

        else if (node is Var)

            return VisitVar((Var)node);

        else if (node is VarDecl)

            return VisitVarDecl((VarDecl)node);

        else if (node is Bool)

            return VisitBool((Bool)node);

        else if (node is Type)

            return VisitType((Type)node);

        else if (node is Empty)

            return VisitEmpty((Empty)node);

        return null;
    }

    public abstract object VisitBinaryOperator(BinaryOperator node);
    public abstract object VisitUnaryOperator(UnaryOperator node);
    public abstract object VisitInstructions(Instructions node);
    public abstract object VisitDeclarations(Declarations node);
    public abstract object VisitAssign(Assign node);
    public abstract object VisitCondition(Condition node);
    public abstract object VisitCicle(Cicle node);
    public abstract object VisitVar(Var node);
    public abstract object VisitVarDecl(VarDecl node);
    public abstract object VisitNum(Num node);
    public abstract object VisitBool(Bool node);
    public abstract object VisitCadene(Cadene node);
    public abstract object VisitType(Type node);
    public abstract object VisitEmpty(Empty node);
}