namespace InterpreterDyZ;

public class Lexer
{
    string? Text;
    int Pos;
    char? CurrentChar;

    public Lexer(string text)
    {
        Text = text;
        Pos = 0;
        CurrentChar = text[Pos];
    }

    private void Error()
    {
        throw new Exception("Caracter inválido");
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
        while (CurrentChar != null && CurrentChar == ' ')

            Next();
    }

    private bool CurrentCharIsDigit()
    {
        int id = (int)(CurrentChar - '0');

        return 0 <= id && id <= 9;
    }

    private int Integer()
    {
        int value = 0;

        while (CurrentChar != null && CurrentCharIsDigit())
        {
            value = (value * 10) + (int)(CurrentChar - '0');
            Next();
        }

        return value;
    }

    public Token GetNextToken()
    {
        while (CurrentChar != null)
        {
            if (CurrentChar == ' ')
            {
                SkipSpace(); 
                continue;
            }

            if(CurrentCharIsDigit())

                return new Token(TokenTypes.INTEGER, Integer());

            switch (CurrentChar)
            {
                case '+':

                    Next();

                    return new Token(TokenTypes.PLUS, '+');

                case '-':

                    Next();

                    return new Token(TokenTypes.MINUS, '-');

                case '*':

                    Next();

                    return new Token(TokenTypes.MULT, '*');

                case '/':

                    Next();

                    return new Token(TokenTypes.DIV, '/');

                case '(':

                    Next();

                    return new Token(TokenTypes.L_PARENT, '(');

                case ')':

                    Next();

                    return new Token(TokenTypes.R_PARENT, ')');

                case '[':

                    Next();

                    return new Token(TokenTypes.L_BRACKET, '[');

                case ']':

                    Next();

                    return new Token(TokenTypes.R_BRACKET, ']');
            }

            Error();
        }

        return new Token(TokenTypes.EOF, null);
    }
}