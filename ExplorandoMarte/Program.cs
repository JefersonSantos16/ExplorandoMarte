using ExplorandoMarte.App.Services;
using ExplorandoMarte.Domain.Enums;
using System;

namespace ExplorandoMarte
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Digite as dimensões do planalto (ex: 5 5):");
            var plateauDimensions = Console.ReadLine()?.Split(' ');
            if (plateauDimensions == null || plateauDimensions.Length != 2)
            {
                Console.WriteLine("Entrada inválida para o planalto.");
                return;
            }

            int maxX = int.Parse(plateauDimensions[0]);
            int maxY = int.Parse(plateauDimensions[1]);

            var service = new RoverService(maxX, maxY);

            while (true)
            {
                Console.WriteLine("Digite a posição inicial da sonda (ex: 1 2 N) ou pressione ENTER para finalizar:");
                var positionInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(positionInput))
                    break;

                var positionParts = positionInput.Split(' ');
                if (positionParts.Length != 3)
                {
                    Console.WriteLine("Entrada inválida para posição.");
                    continue;
                }

                int x, y;
                if (!int.TryParse(positionParts[0], out x) || !int.TryParse(positionParts[1], out y))
                {
                    Console.WriteLine("Coordenadas inválidas. Use números inteiros.");
                    continue;
                }

                // Valida e converte a direção (N, S, E, W)
                Direction direction;
                try
                {
                    direction = (Direction)Enum.Parse(typeof(Direction), positionParts[2], true);
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Direção inválida. Use N, S, E ou W.");
                    continue;
                }

                Console.WriteLine("Digite a sequência de comandos (ex: LMLMLMLMM):");
                var commandSequence = Console.ReadLine()?.ToUpper() ?? "";

                try
                {
                    service.AddRover(x, y, direction, commandSequence);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao adicionar sonda: {ex.Message}");
                }
            }

            Console.WriteLine("\n Posições finais das sondas:");
            foreach (var rover in service.GetRovers())
            {
                Console.WriteLine($"{rover.Position.X} {rover.Position.Y} {rover.Direction}");
            }
            Console.WriteLine("\n Pressione ENTER para sair...");
            Console.ReadLine();
        }
    }
}