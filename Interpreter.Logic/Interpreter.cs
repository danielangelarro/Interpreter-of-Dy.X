namespace InterpreterDyZ;

public class Interpreter : NodeVisitor
{
    private Parser? Parser;
    public Dictionary<string, object> Scope;

    public Interpreter(Parser parser)
    {
        Parser = parser;
        Scope = new Dictionary<string, object>();
    }

    private void Error(string error="Caracter inválido")
    {
        throw new Exception(error);
    }

    public override object VisitBinaryOperator(BinaryOperator node)
    {
        object result = 0;

        object left = Visit(node.Left);
        object right = Visit(node.Right);

        switch (node.Operator.Type)
        {
            case TokenTypes.PLUS:

                if (left is string)
                    result = (string)left + (string)right;
                else 
                    result = Convert.ToSingle(left) + Convert.ToSingle (right);
                
                break;

            case TokenTypes.MINUS:

                result = Convert.ToSingle(left) - Convert.ToSingle (right);
                
                break;
            
            case TokenTypes.MULT:

                result = Convert.ToSingle(left) * Convert.ToSingle (right);
                
                break;
            
            case TokenTypes.FLOAT_DIV:

                result = Convert.ToSingle(left) / Convert.ToSingle (right);
                
                break;

            case TokenTypes.INTEGER_DIV:

                result = (int)(Convert.ToSingle(left) / Convert.ToSingle (right));
                
                break;
            
            case TokenTypes.MOD:
            {
                result = Convert.ToSingle(left) % Convert.ToSingle (right);
                
                break;
            }

            
            case TokenTypes.SAME:
            
                if (left is string)
                    result = (string)left == (string)right;
                else 
                    result = Convert.ToSingle(left) == Convert.ToSingle (right);
                
                break;
            
            case TokenTypes.DIFFERENT:
            
                if (left is string)
                    result = (string)left != (string)right;
                else 
                    result = Convert.ToSingle(left) != Convert.ToSingle (right);
                
                break;
            
            case TokenTypes.LESS:

                result = Convert.ToSingle(left) < Convert.ToSingle (right);
                
                break;

            case TokenTypes.GREATER:

                result = Convert.ToSingle(left) > Convert.ToSingle (right);
                
                break;
            
            case TokenTypes.LESS_EQUAL:

                result = Convert.ToSingle(left) <= Convert.ToSingle (right);
                
                break;
            
            case TokenTypes.GREATER_EQUAL:

                result = Convert.ToSingle(left) >= Convert.ToSingle (right);
                
                break;
            
            case TokenTypes.AND:

                result = ((bool)Visit(node.Left) && (bool)Visit(node.Right));
                
                break;
            
            case TokenTypes.OR:

                result = ((bool)Visit(node.Left) || (bool)Visit(node.Right));
                
                break;
            
        }

        return result;
    }

    public override object VisitUnaryOperator(UnaryOperator node)
    {
        int result = 0;

        switch (node.Operator.Type)
        {
            case TokenTypes.PLUS:

                result = +(int)Visit(node.Expression);

                break;
            
            case TokenTypes.MINUS:

                result = -(int)Visit(node.Expression);

                break;
        }

        return result;
    }

    public override object VisitInstructions(Instructions node)
    {
        foreach (var item in node.Commands)
        {
            Visit(item);
        }

        return 0;
    }

    public override object VisitDeclarations(Declarations node)
    {
        foreach (var item in node.Commands)
        {
            Visit(item);
        }

        return 0;
    }
    
    public override object VisitAssign(Assign node)
    {
        string name = (string)node.Left.Value;

        Scope[name] = Visit(node.Right);

        return 0;
    }

    public override object VisitCondition(Condition node)
    {
        if ((bool)Visit(node.Compound))

            Visit(node.StatementList);

        return 0;
    }
    
    public override object VisitCicle(Cicle node)
    {
        
        while ((bool)Visit(node.Compound))
        {
            Visit(node.StatementList);
        }

        return 0;
    }
    
    public override object VisitVar(Var node)
    {
        string name = (string)node.Value;

        object value = Scope[name];

        if (value is null)

            Error($"Assignment error: {name} ");
        
        return value;
    }

    public override object VisitNum(Num node)
    {
        return node.Value;
    }
    
    public override object VisitBool(Bool node)
    {
        return (bool)node.Value;
    }

    public override object VisitCadene(Cadene node)
    {
        return (string)node.Value;
    }

    public override object VisitVarDecl(VarDecl node) 
    {
        if (node.Type == TokenTypes.INTEGER)

            Scope.Add((string)node.Node.Value, 0);
        
        else if (node.Type == TokenTypes.FLOAT)

            Scope.Add((string)node.Node.Value, 0.0);
        
        else if (node.Type == TokenTypes.BOOLEAN)

            Scope.Add((string)node.Node.Value, false);

        return 0; 
    }
    
    public override object VisitType(Type node) { return 0; }

    public override object VisitEmpty(Empty node) { return 0; }

    public object Interpret()
    {
        AST tree = Parser.Parse();

        if (tree == null)

            return -1;
        
        return Visit(tree);
    }
}