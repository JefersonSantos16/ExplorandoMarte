using ExplorandoMarte.Domain.Interfaces;
using ExplorandoMarte.Domain.Entities;

namespace ExplorandoMarte.Domain.Commands
{
    public class RotateLeftCommand : ICommand
    {
        public void Execute(Rover rover, Plateau plateau)
        {
            rover.TurnLeft();
        }
    }
}
