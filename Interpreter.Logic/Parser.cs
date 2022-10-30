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

    private void Error()
    {
        throw new Exception("Caracter inválido");
    }

    private void Process(TokenTypes type)
    {
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

            case TokenTypes.L_PARENT:

                Process(TokenTypes.L_PARENT);
                
                node = Expression();
                
                Process(TokenTypes.R_PARENT);

                break;
        }

        return node;
    }

    private AST Termine()
    {
        AST node = Factor();
        Token token = new Token(CurrentToken);

        while (CurrentToken.Type == TokenTypes.MULT || CurrentToken.Type == TokenTypes.DIV)
        {
            token = new Token(CurrentToken);

            if (token.Type == TokenTypes.MULT)
                
                Process(TokenTypes.MULT);
            
            else if (token.Type == TokenTypes.DIV)
                
                Process(TokenTypes.DIV);
            
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

    public AST Parse()
    {
        AST node = Expression();

        if (CurrentToken.Type != TokenTypes.EOF)
            
            Error();

        return node;
    }
}