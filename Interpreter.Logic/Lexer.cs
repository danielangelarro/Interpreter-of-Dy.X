namespace InterpreterDyZ;

public class Lexer
{
    string? Text;
    int Pos;
    char? CurrentChar;
    ReservateKeywords? key;

    public Lexer(string text)
    {
        Text = text;
        Pos = 0;
        CurrentChar = text[Pos];
        key = new ReservateKeywords();
    }

    private void Error(string error = "Sintáxis inválido")
    {
        throw new Exception(error);
    }

    private char? SeekNextChar()
    {
        int pos = Pos + 1;

        if (pos == Text.Length)

            return null;
        
        return Text[pos];
    }

    private void Next()
    {
        Pos += 1;

        if (Pos == Text.Length)

            CurrentChar = null;
        
        else 

            CurrentChar = Text[Pos];
    }

    private void SkipSpace()
    {
        while (CurrentChar != null && Char.IsWhiteSpace((char)CurrentChar))

            Next();
    }

    private void SkipComment()
    {
        while (CurrentChar != null && !(CurrentChar == '>' && SeekNextChar() == ']'))

            Next();
        
        Next();
        Next();
    }

    private Token Cadene()
    {
        string value = "";

        Next();

        while (CurrentChar != null && CurrentChar != '\'')
        {
            value += CurrentChar;
            Next();
        }

        Next();

        return new Token(TokenTypes.STRING, value);
    }

    private Token Number()
    {
        string value = "";

        while (CurrentChar != null && char.IsDigit((char)CurrentChar))
        {
            value += CurrentChar;
            Next();
        }

        if (CurrentChar == '.')
        {
            value += CurrentChar;
            Next();

            while (CurrentChar != null && char.IsDigit((char)CurrentChar))
            {
                value += CurrentChar;
                Next();
            }

            return new Token(TokenTypes.FLOAT, float.Parse(value));
        }

        
        return new Token(TokenTypes.INTEGER, float.Parse(value));
    }

    public Token Variable()
    {
        string name = "";

        while (CurrentChar != null && char.IsLetterOrDigit((char)CurrentChar))
        {
            name += CurrentChar;
            Next();
        }

        if (key.Keyword.ContainsKey(name))

            return new Token(key.Keyword[name], key.Keyword[name]);

        // if (!key.Keyword.ContainsKey(name))

        //     Error();
        
        return new Token(TokenTypes.ID, name);
    }

    public Token GetNextToken()
    {
        while (CurrentChar != null)
        {
            if (char.IsWhiteSpace((char)CurrentChar))
            {
                SkipSpace(); 
                continue;
            }

            if (CurrentChar == '[' && SeekNextChar() == '<')
            {
                Next();
                Next();
                SkipComment();

                continue;
            }

            if (CurrentChar == '\'')
            {
                return Cadene();
            }

            if (char.IsLetter((char)CurrentChar))
            {
                return Variable();
            }

            if(char.IsDigit((char)CurrentChar))

                return Number();

            switch (CurrentChar)
            {
                case '+':

                    Next();

                    if (CurrentChar == '=')
                    {
                        Next();

                        return new Token(TokenTypes.ASSIGN_PLUS, "+=");
                    }

                    return new Token(TokenTypes.PLUS, '+');

                case '-':

                    Next();

                    if (CurrentChar == '=')
                    {
                        Next();

                        return new Token(TokenTypes.ASSIGN_MINUS, "-=");
                    }

                    return new Token(TokenTypes.MINUS, '-');

                case '*':

                    Next();

                    if (CurrentChar == '=')
                    {
                        Next();

                        return new Token(TokenTypes.ASSIGN_MUL, "*=");
                    }

                    return new Token(TokenTypes.MULT, '*');

                case '/':

                    Next();

                    if (CurrentChar == '=')
                    {
                        Next();

                        return new Token(TokenTypes.ASSIGN_DIV, "/=");
                    }

                    if (CurrentChar == '/')
                    {
                        Next();

                        return new Token(TokenTypes.INTEGER_DIV, "//");
                    }

                    return new Token(TokenTypes.FLOAT_DIV, '/');

                case '%':

                    Next();

                    if (CurrentChar == '=')
                    {
                        Next();

                        return new Token(TokenTypes.ASSIGN_MOD, "%=");
                    }

                    return new Token(TokenTypes.MOD, '%');

                case '(':

                    Next();

                    return new Token(TokenTypes.L_PARENT, '(');

                case ')':

                    Next();

                    return new Token(TokenTypes.R_PARENT, ')');

                case '{':

                    Next();

                    return new Token(TokenTypes.L_KEYS, '{');

                case '}':

                    Next();

                    return new Token(TokenTypes.R_KEYS, '}');
                
                case '=':
                    
                    Next();

                    if (CurrentChar == '=')
                    {
                        Next();

                        return new Token(TokenTypes.SAME, "==");
                    }

                    return new Token(TokenTypes.ASSIGN, "=");

                case '!':

                    Next();
                    
                    if (CurrentChar == '=')
                    {
                        Next();

                        return new Token(TokenTypes.DIFFERENT, "!=");
                    }

                    return new Token(TokenTypes.NOX, "!");
                
                case '<':

                    Next();
                    
                    if (CurrentChar == '=')
                    {
                        Next();

                        return new Token(TokenTypes.LESS_EQUAL, "<=");
                    }

                    return new Token(TokenTypes.LESS, '<');

                case '>':

                    Next();

                    if (CurrentChar == '=')
                    {
                        Next();

                        return new Token(TokenTypes.GREATER_EQUAL, ">=");
                    }

                    return new Token(TokenTypes.GREATER, '>');
                
                case '&':

                    if (SeekNextChar() == '&')
                    {
                        Next();
                        Next();

                        return new Token(TokenTypes.AND, "&&");
                    }

                    break;
                
                case '|':

                    if (SeekNextChar() == '|')
                    {
                        Next();
                        Next();

                        return new Token(TokenTypes.OR, "||");
                    }

                    break;
                
                case ',':

                    Next();

                    return new Token(TokenTypes.COMMA, ',');
                
                case ';':

                    Next();

                    return new Token(TokenTypes.SEMI, ';');
            }

            Error();
        }

        return new Token(TokenTypes.EOF, null);
    }
}