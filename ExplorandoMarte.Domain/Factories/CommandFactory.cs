using ExplorandoMarte.Domain.Commands;
using ExplorandoMarte.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExplorandoMarte.Domain.Factories
{
    public static class CommandFactory
    {
        public static ICommand Create(char commandChar)
        {
            switch (commandChar)
            {
                case 'L':
                    return new RotateLeftCommand();
                case 'R':
                    return new RotateRightCommand();
                case 'M':
                    return new MoveCommand();
                default:
                    throw new ArgumentException($"Comando inválido: {commandChar}");
            }
        }
    }
}
