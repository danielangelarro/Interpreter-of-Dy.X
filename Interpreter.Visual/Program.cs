using System;
using InterpreterDyZ;


string text = @"
main{
    int a, b, c;
    a = 105;
    b = 25;

    string s;
    s = 'fire';

    while (a % b != 0)
    {
        c = a % b;
        a = b;
        b = c;
    };

    if (s == 'fire')
    {
        s = 'water';
    }
}
";

Lexer lexer = new Lexer(text);

Parser parser = new Parser(lexer);
Interpreter interpreter = new Interpreter(parser);

Console.WriteLine(interpreter.Interpret());

foreach (var item in interpreter.Scope)
{
    Console.ForegroundColor = ConsoleColor.DarkYellow;
    Console.Write('[');

    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write(item.Key);

    Console.ForegroundColor = ConsoleColor.DarkYellow;
    Console.Write("]: ");

    Console.ForegroundColor = ConsoleColor.DarkBlue;
    Console.WriteLine(item.Value);
}