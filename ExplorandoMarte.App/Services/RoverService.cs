using ExplorandoMarte.Domain.Entities;
using ExplorandoMarte.Domain.Enums;
using ExplorandoMarte.Domain.Exceptions;
using ExplorandoMarte.Domain.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExplorandoMarte.App.Services
{
    public class RoverService
    {
        private readonly Plateau _plateau;
        private readonly List<Rover> _rovers = new List<Rover>();

        public RoverService(int maxX, int maxY)
        {
            _plateau = new Plateau(maxX, maxY);
        }

        public void AddRover(int x, int y, Direction direction, string commandSequence)
        {
            var position = new Position(x, y);

            if (!_plateau.IsInside(position))
                throw new OutOfBoundsException($"Posição inicial ({x}, {y}) está fora do planalto.");

            if (_plateau.IsOccupied(position))
                throw new PositionConflictException($"Já existe uma sonda na posição ({x}, {y}).");

            var rover = new Rover(position, direction);
            _plateau.MarkOccupied(position);
            _rovers.Add(rover);

            ExecuteCommands(rover, commandSequence);
        }

        private void ExecuteCommands(Rover rover, string commandSequence)
        {
            foreach (char c in commandSequence)
            {
                var command = CommandFactory.Create(c);
                command.Execute(rover, _plateau);
            }
        }

        public List<Rover> GetRovers()
        {
            return _rovers;
        }
    }
}
