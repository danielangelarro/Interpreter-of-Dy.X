using InterpreterDyZ;


while (true)
{
    string text="";

    Console.Write(">>> ");

    try
    {
        text = Console.ReadLine();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
        
        continue;
    }

    Lexer lexer = new Lexer(text);

    Parser parser = new Parser(lexer);
    Interpreter interpreter = new Interpreter(parser);

    int result = interpreter.Interpret();

    Console.WriteLine(result);
}