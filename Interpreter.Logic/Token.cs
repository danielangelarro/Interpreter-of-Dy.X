namespace InterpreterDyZ;

public enum TokenTypes {
    #region Types of Datas
    INTEGER,
    #endregion

    #region Binarys Operators
    PLUS, MINUS, MULT, DIV,
    #endregion

    #region Symbols
    L_PARENT, R_PARENT, L_BRACKET, R_BRACKET,
    L_MARKS, R_MARKS,
    #endregion

    #region Auxiliars Operators
    EOF
    #endregion
}

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

    public string View()
    {
        return $"Token({Type}, {Value})";
    }
}