namespace InterpreterDyZ;

public class Token
{
    public TokenTypes? Type;
    public object Value;

    public Token(TokenTypes type, object value)
    {
        this.Type = type;
        this.Value = value;
    }

    public Token(Token other)
    {
        Type = other.Type;
        Value = other.Value;
    }

    public string Show()
    {
        return $"Token({Type}, {Value})";
    }
}

public class ReservateKeywords
{
    public Dictionary<string, TokenTypes> Keyword;
    public Dictionary<string, object> Variables;

    public ReservateKeywords()
    {
        Keyword = new Dictionary<string, TokenTypes>();
        Variables = new Dictionary<string, object>();

        Keyword.Add("Internal", TokenTypes.INTERNAL);
        Keyword.Add("States", TokenTypes.STATES);
        Keyword.Add("Models", TokenTypes.MODELS);
        Keyword.Add("@Internal", TokenTypes.INTERNAL);
        Keyword.Add("main", TokenTypes.MAIN);
        Keyword.Add("int", TokenTypes.INTEGER);
        Keyword.Add("float", TokenTypes.FLOAT);
        Keyword.Add("bool", TokenTypes.BOOLEAN);
        Keyword.Add("string", TokenTypes.STRING);
        Keyword.Add("if", TokenTypes.IF);
        Keyword.Add("True", TokenTypes.TRUE);
        Keyword.Add("False", TokenTypes.FALSE);
        Keyword.Add("while", TokenTypes.WHILE);
        Keyword.Add("ShowLine", TokenTypes.SHOWLINE);
        Keyword.Add("return", TokenTypes.RETURN);
    }
}