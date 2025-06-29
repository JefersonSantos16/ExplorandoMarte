using ExplorandoMarte.Domain.Entities;
using ExplorandoMarte.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExplorandoMarte.Domain.Commands
{
    public class MoveCommand : ICommand
    {
        public void Execute(Rover rover, Plateau plateau)
        {
            rover.MoveForward(plateau);
        }
    }
}
