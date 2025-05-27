using System;
using System.Linq;
using System.Windows.Forms;

public class RecursiveDescentParser
{
    private string input;
    private int position;
    private DataGridView grid;

    public RecursiveDescentParser(string input, DataGridView grid)
    {
        this.input = input.Replace(" ", "");
        this.position = 0;
        this.grid = grid;
    }

    public void Parse()
    {
        try
        {
            Formula();
            if (position < input.Length)
                Error("Лишние символы в конце");
            else
                grid.Rows.Add("<OK>", "Строка корректна", position);
        }
        catch (Exception ex)
        {
            grid.Rows.Add("Ошибка", ex.Message, position);
        }
    }

    private void Formula()
    {
        grid.Rows.Add("<ФОРМУЛА>", Peek(), position);
        Term();
        FormulaPrime();
    }

    private void FormulaPrime()
    {
        if (IsSign(Peek()))
        {
            grid.Rows.Add("<ФОРМУЛА'>", Peek(), position);
            Sign();
            Term();
            FormulaPrime();
        }
        else
        {
            grid.Rows.Add("<ФОРМУЛА'>", "ε", position);
        }
    }

    private void Term()
    {
        grid.Rows.Add("<СЛАГАЕМОЕ>", Peek(), position);
        if (Peek() == '(')
        {
            Consume('(');
            Formula();
            Consume(')');
        }
        else
        {
            Number();
        }
    }

    private void Number()
    {
        grid.Rows.Add("<ЧИСЛО>", Peek(), position);
        Digit();
        while (char.IsDigit(Peek()))
        {
            grid.Rows.Add("<ЧИСЛО'>", Peek(), position);
            Digit();
        }
        grid.Rows.Add("<ЧИСЛО'>", "ε", position);
    }

    private void Digit()
    {
        if (char.IsDigit(Peek()))
        {
            grid.Rows.Add("<ЦИФРА>", Peek(), position);
            position++;
        }
        else
        {
            Error("Ожидалась цифра");
        }
    }

    private void Sign()
    {
        if ("+-*/".Contains(Peek()))
        {
            grid.Rows.Add("<ЗНАК>", Peek(), position);
            position++;
        }
        else
        {
            Error("Ожидался знак");
        }
    }

    private char Peek()
    {
        return position < input.Length ? input[position] : '\0';
    }

    private void Consume(char expected)
    {
        if (Peek() == expected)
        {
            grid.Rows.Add($"'{expected}'", expected.ToString(), position);
            position++;
        }
        else
        {
            Error($"Ожидался символ '{expected}'");
        }
    }

    private void Error(string message)
    {
        throw new Exception(message);
    }

    private bool IsSign(char c)
    {
        return "+-*/".Contains(c);
    }
}

