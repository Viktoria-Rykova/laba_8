using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace компилятор_2
{
    public class Lexeme
    {
        public string Type { get; set; }
        public string Value { get; set; }
        public int Position { get; set; }

        public Lexeme(string type, string value, int position)
        {
            Type = type;
            Value = value;
            Position = position;
        }
    }

    public class Lexer
    {
        private string input;
        private int position;
        private List<Lexeme> lexemes;

        public Lexer(string input)
        {
            this.input = input;
            this.position = 0;
            lexemes = new List<Lexeme>();
        }

        public List<Lexeme> Analyze()
        {
            while (position < input.Length)
            {
                char current = input[position];

                if (char.IsWhiteSpace(current))
                {
                    position++;
                }
                else if (char.IsDigit(current))
                {
                    int start = position;
                    string number = "";

                    while (position < input.Length && char.IsDigit(input[position]))
                    {
                        number += input[position];
                        position++;
                    }

                    lexemes.Add(new Lexeme("ЧИСЛО", number, start));
                }
                else if ("+-*/".Contains(current))
                {
                    lexemes.Add(new Lexeme("ЗНАК", current.ToString(), position));
                    position++;
                }
                else if (current == '(')
                {
                    lexemes.Add(new Lexeme("ОТКР_СКОБКА", "(", position));
                    position++;
                }
                else if (current == ')')
                {
                    lexemes.Add(new Lexeme("ЗАКР_СКОБКА", ")", position));
                    position++;
                }
                else
                {
                    lexemes.Add(new Lexeme("ОШИБКА", current.ToString(), position));
                    position++;
                }
            }

            return lexemes;
        }
    }

}
