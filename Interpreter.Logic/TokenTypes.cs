namespace InterpreterDyZ;

public enum TokenTypes {
    #region Types of Datas

    INTEGER,
    FLOAT,
    BOOLEAN,
    STRING,

    #endregion

    #region Binarys Operators

    PLUS,
    MINUS,
    MULT,
    FLOAT_DIV,
    INTEGER_DIV,
    MOD,
    ASSIGN_PLUS,
    ASSIGN_MINUS,
    ASSIGN_MUL,
    ASSIGN_MOD,
    ASSIGN_DIV,

    #endregion

    #region Compare Operators

    SAME,
    DIFFERENT,
    LESS,
    GREATER,
    LESS_EQUAL,
    GREATER_EQUAL,
    NOX,
    AND,
    OR,

    #endregion

    #region Symbols

    L_PARENT,
    R_PARENT,
    L_BRACKET,
    R_BRACKET,
    L_KEYS,
    R_KEYS,
    L_QUOTE,
    R_QUOTE,

    #endregion

    #region Reserved Keywords

    STATES,
    MODELS,
    INTERNAL,
    MAIN,
    IF,
    TRUE,
    FALSE,
    WHILE,
    SHOWLINE,
    RETURN,

    #endregion

    #region Auxiliars Tokens

    ID,
    COMMA,
    FUNTIONS,
    SEMI,
    ASSIGN,
    L_COMMENT,
    R_COMMENT,
    EOF

    #endregion
}