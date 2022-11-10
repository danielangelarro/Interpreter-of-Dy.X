using System.Reflection.Emit;
namespace InterpreterDyZ;

public class Parser
{
    private Lexer Lexer;
    private Token CurrentToken;

    public Parser(Lexer lexer)
    {
        Lexer = lexer;
        CurrentToken = lexer.GetNextToken();
    }

    private void Error(string error="Caracter inválido")
    {
        throw new Exception(error);
    }

    private void Process(TokenTypes type)
    {
        Console.WriteLine($"{CurrentToken.Show()} {type}");

        if(CurrentToken.Type == type)

            CurrentToken = Lexer.GetNextToken();
        
        else

            Error();
    }

    private AST Factor()
    {
        AST node = new AST();
        Token token = new Token(CurrentToken);

        switch (token.Type)
        {
            case TokenTypes.ID:

                Process(TokenTypes.ID);
                
                node = new Var(token);

                break;

            case TokenTypes.PLUS:

                Process(TokenTypes.PLUS);
                
                node = new UnaryOperator(token, Factor());

                break;

            case TokenTypes.MINUS:

                Process(TokenTypes.MINUS);
                
                node = new UnaryOperator(token, Factor());
            
                break;

            case TokenTypes.INTEGER:

                Process(TokenTypes.INTEGER);
                
                node = new Num(token);
            
                break;

            case TokenTypes.FLOAT:

                Process(TokenTypes.FLOAT);
                
                node = new Num(token);
            
                break;
            
            case TokenTypes.BOOLEAN:

                Process(TokenTypes.BOOLEAN);
                
                node = new Bool(token);
            
                break;
            
            case TokenTypes.STRING:

                Process(TokenTypes.STRING);
                
                node = new Cadene(token);
            
                break;

            case TokenTypes.TRUE:

                Process(TokenTypes.TRUE);
                
                node = new Bool(new Token(TokenTypes.TRUE, true));
            
                break;

            case TokenTypes.FALSE:

                Process(TokenTypes.FALSE);
                
                node = new Bool(new Token(TokenTypes.FALSE, false));
            
                break;
            
            case TokenTypes.L_PARENT:

                Process(TokenTypes.L_PARENT);
                
                node = Compounds();
                
                Process(TokenTypes.R_PARENT);

                break;
        }

        return node;
    }

    private AST Termine()
    {
        AST node = Factor();
        Token token = new Token(CurrentToken);

        while (CurrentToken.Type == TokenTypes.MULT || CurrentToken.Type == TokenTypes.INTEGER_DIV
                || CurrentToken.Type == TokenTypes.FLOAT_DIV || CurrentToken.Type == TokenTypes.MOD)
        {
            token = new Token(CurrentToken);

            if (token.Type == TokenTypes.MULT)
                
                Process(TokenTypes.MULT);
            
            else if (token.Type == TokenTypes.INTEGER_DIV)
                
                Process(TokenTypes.INTEGER_DIV);
            
            else if (token.Type == TokenTypes.FLOAT_DIV)
                
                Process(TokenTypes.FLOAT_DIV);
            
            else if (token.Type == TokenTypes.MOD)
                
                Process(TokenTypes.MOD);

            node = new BinaryOperator(node, token, Factor());
        }


        return node;
    }

    private AST Expression()
    {
        AST node = Termine();
        Token token = new Token(CurrentToken);

        while (CurrentToken.Type == TokenTypes.PLUS || CurrentToken.Type == TokenTypes.MINUS)
        {
            token = new Token(CurrentToken);

            if (token.Type == TokenTypes.PLUS)
                
                Process(TokenTypes.PLUS);
            
            else if (token.Type == TokenTypes.MINUS)
                
                Process(TokenTypes.MINUS);
        
            node = new BinaryOperator(node, token, Termine());
        }


        return node;
    }

    private bool IsBooleanOperator()
    {
        Token type = CurrentToken;

        return type.Type == TokenTypes.SAME || type.Type == TokenTypes.DIFFERENT ||
                type.Type == TokenTypes.LESS || type.Type == TokenTypes.GREATER ||
                type.Type == TokenTypes.LESS_EQUAL || type.Type == TokenTypes.GREATER_EQUAL ||
                type.Type == TokenTypes.NOX;
    }

    private AST Comparer()
    {
        AST node = Expression();
        Token token = new Token(CurrentToken);

        while (IsBooleanOperator())
        {
            token = new Token(CurrentToken);

            if (token.Type == TokenTypes.SAME)
                
                Process(TokenTypes.SAME);
            
            else if (token.Type == TokenTypes.DIFFERENT)
                
                Process(TokenTypes.DIFFERENT);
            
            else if (token.Type == TokenTypes.LESS)
                
                Process(TokenTypes.LESS);
            
            else if (token.Type == TokenTypes.LESS_EQUAL)
                
                Process(TokenTypes.LESS_EQUAL);
            
            else if (token.Type == TokenTypes.GREATER)
                
                Process(TokenTypes.GREATER);
            
            else if (token.Type == TokenTypes.GREATER_EQUAL)
                
                Process(TokenTypes.GREATER_EQUAL);
            
            else if (token.Type == TokenTypes.NOX)
                
                Process(TokenTypes.NOX);
        
            node = new BinaryOperator(node, token, Expression());
        }


        return node;
    }

    private AST Compounds()
    {
        AST node = Comparer();
        Token token = new Token(CurrentToken);

        while (CurrentToken.Type == TokenTypes.AND || CurrentToken.Type == TokenTypes.OR)
        {
            token = new Token(CurrentToken);

            if (token.Type == TokenTypes.AND)
                
                Process(TokenTypes.AND);
            
            else if (token.Type == TokenTypes.OR)
                
                Process(TokenTypes.OR);
            
            node = new BinaryOperator(node, token, Comparer());
        }


        return node;
    }

    private AST Empty()
    {
        return new Empty();
    }

    private AST Variable()
    {
        AST node = new Var(CurrentToken);
        Process(TokenTypes.ID);

        return node;
    }

    private AST Assignment()
    {

        AST node = Variable();
        Token token = new Token(CurrentToken);

        Process(TokenTypes.ASSIGN);

        return new Assign((Var)node, token, Compounds());
    }

    private AST Conditional()
    {
        Process(TokenTypes.IF);
        Process(TokenTypes.L_PARENT);

        AST node = Compounds();

        Process(TokenTypes.R_PARENT);
        Process(TokenTypes.L_KEYS);
        
        node = new Condition(node, StatementList());
        
        Process(TokenTypes.R_KEYS);

        return node;
    }

    private AST Cicle()
    {
        Process(TokenTypes.WHILE);
        Process(TokenTypes.L_PARENT);

        AST node = Compounds();

        Process(TokenTypes.R_PARENT);
        Process(TokenTypes.L_KEYS);
        
        node = new Cicle(node, StatementList());
        
        Process(TokenTypes.R_KEYS);

        return node;
    }

    // TODO: hacer por otra via
    private AST Declaration()
    {
        Declarations declarations = new Declarations();

        TokenTypes type = (TokenTypes)(TypeData());

        declarations.Commands.Add(new VarDecl(new Var(CurrentToken), type));
        Process(TokenTypes.ID);

        while (CurrentToken.Type == TokenTypes.COMMA)
        {
            Process(TokenTypes.COMMA);

            declarations.Commands.Add(new VarDecl(new Var(CurrentToken), type));

            Process(TokenTypes.ID);
        }

        return declarations;
    }

    private bool IsDataType()
    {
        return CurrentToken.Type == TokenTypes.INTEGER || CurrentToken.Type == TokenTypes.FLOAT 
                || CurrentToken.Type == TokenTypes.BOOLEAN || CurrentToken.Type == TokenTypes.STRING;
    }

    private AST Statement()
    {
        AST node = Empty();

        if (CurrentToken.Type == TokenTypes.L_PARENT)
        {
            Process(TokenTypes.L_PARENT);

            Statement();

            Process(TokenTypes.R_PARENT);
        }

        if ( IsDataType() )

            node = Declaration();

        else if (CurrentToken.Type == TokenTypes.ID)

            node = Assignment();
        
        else if (CurrentToken.Type == TokenTypes.IF)

            node = Conditional();
        
        else if (CurrentToken.Type == TokenTypes.WHILE)

            node = Cicle();

        return node;
    }

    private AST StatementList()
    {
        Instructions instructions = new Instructions();
        instructions.Commands.Add(Statement());

        while (CurrentToken.Type == TokenTypes.SEMI)
        {
            Process(TokenTypes.SEMI);
            instructions.Commands.Add(Statement());
        }

        return instructions;
    }

    private TokenTypes TypeData()
    {
        TokenTypes token = TokenTypes.INTEGER;

        if (CurrentToken.Type == TokenTypes.INTEGER)
        {
            Process(TokenTypes.INTEGER);
            token = TokenTypes.INTEGER;
        }

        else if (CurrentToken.Type == TokenTypes.FLOAT)
        {
            Process(TokenTypes.FLOAT);
            token = TokenTypes.FLOAT;
        }

        else if (CurrentToken.Type == TokenTypes.BOOLEAN)
        {
            Process(TokenTypes.BOOLEAN);
            token = TokenTypes.BOOLEAN;
        }

        else if (CurrentToken.Type == TokenTypes.STRING)
        {
            Process(TokenTypes.STRING);
            token = TokenTypes.STRING;
        }

        return token;
    }

    private AST Block()
    {
        Process(TokenTypes.MAIN);
        Process(TokenTypes.L_KEYS);
        
        AST node = StatementList();

        Process(TokenTypes.R_KEYS);

        return node;
    }

    // TODO: *import* y *export*

    public AST Parse()
    {
        AST node = Block();

        if (CurrentToken.Type != TokenTypes.EOF)
            
            Error();

        return node;
    }
}