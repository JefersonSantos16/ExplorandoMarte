using ExplorandoMarte.Domain.Entities;
using ExplorandoMarte.Domain.Interfaces;
using ExplorandoMarte.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExplorandoMarte.Domain.Exceptions;

namespace ExplorandoMarte.Domain.Entities
{
    public class Rover
    {
        public Position Position { get; private set; }
        public Direction Direction { get; private set; }


        public Rover(Position startPosition, Direction startDirection)
        {
            Position = startPosition;
            Direction = startDirection;
        }

        public void ExecuteCommand(ICommand command, Plateau plateau)
        {
            command.Execute(this, plateau);
        }

        public void TurnLeft()
        {
            switch (Direction)
            {
                case Direction.N:
                    Direction = Direction.W;
                    break;
                case Direction.W:
                    Direction = Direction.S;
                    break;
                case Direction.S:
                    Direction = Direction.E;
                    break;
                case Direction.E:
                    Direction = Direction.N;
                    break;
            }
        }

        public void TurnRight()
        {
            switch (Direction)
            {
                case Direction.N:
                    Direction = Direction.E;
                    break;
                case Direction.E:
                    Direction = Direction.S;
                    break;
                case Direction.S:
                    Direction = Direction.W;
                    break;
                case Direction.W:
                    Direction = Direction.N;
                    break;
            }
        }

        public void MoveForward(Plateau plateau)
        {
            var next = Position.Clone();

            switch (Direction)
            {
                case Direction.N:
                    next.Y += 1;
                    break;
                case Direction.E:
                    next.X += 1;
                    break;
                case Direction.S:
                    next.Y -= 1;
                    break;
                case Direction.W:
                    next.X -= 1;
                    break;
                default:
                    throw new InvalidOperationException($"Invalid direction: {Direction}");
            }

            if (!plateau.IsInside(next))
                throw new OutOfBoundsException($"Rover tried to move out of bounds to ({next.X}, {next.Y})");

            if (plateau.IsOccupied(next))
                throw new PositionConflictException($"Rover tried to move to an occupied position ({next.X}, {next.Y})");

            plateau.Free(Position);
            Position = next;
            plateau.MarkOccupied(Position);
        }

        public override string ToString()
{
    return $"{Position.X} {Position.Y} {Direction}";
}


}
} 
