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

            int maxX;
            int maxY;
            if (plateauDimensions == null || plateauDimensions.Length != 2 ||
                !int.TryParse(plateauDimensions[0], out maxX) ||
                !int.TryParse(plateauDimensions[1], out maxY))
            {
                Console.WriteLine("Entrada inválida para o planalto.");
                return;
            }

            var service = new RoverService(maxX, maxY);

            if (!AdicionarSonda(service, "primeira"))
                return;

            if (!AdicionarSonda(service, "segunda"))
                return;

            Console.WriteLine("\nPosições finais das sondas:");
            foreach (var rover in service.GetRovers())
            {
                Console.WriteLine($"{rover.Position.X} {rover.Position.Y} {rover.Direction}");
            }

            Console.WriteLine("\nPressione ENTER para sair...");
            Console.ReadLine();
        }

        static bool AdicionarSonda(RoverService service, string identificador)
        {
            int x;
            int y;
            Direction direction;
            if (identificador == "primeira")
            {
                Console.WriteLine($"\nDigite a posição inicial da {identificador} sonda (ex: 1 2 N):");
                var input = Console.ReadLine()?.Split(' ');

                if (input == null || input.Length != 3)
                {
                    Console.WriteLine("Entrada inválida. Formato esperado: X Y D");
                    return false;
                }

                if (!int.TryParse(input[0], out x) || !int.TryParse(input[1], out y))
                {
                    Console.WriteLine("Coordenadas inválidas. Use números inteiros.");
                    return false;
                }

                if (!Enum.TryParse(input[2], true, out direction))
                {
                    Console.WriteLine("Direção inválida. Use N, S, E ou W.");
                    return false;
                }
                Console.WriteLine($"Digite a sequência de comandos da {identificador} sonda (ex: LMLMLMLMM):");
            }
            else
            {
                Console.WriteLine($"\nDigite a posição inicial da {identificador} sonda (ex: 3 3 E):");
                var input = Console.ReadLine()?.Split(' ');

                if (input == null || input.Length != 3)
                {
                    Console.WriteLine("Entrada inválida. Formato esperado: X Y D");
                    return false;
                }

                if (!int.TryParse(input[0], out x) || !int.TryParse(input[1], out y))
                {
                    Console.WriteLine("Coordenadas inválidas. Use números inteiros.");
                    return false;
                }

                if (!Enum.TryParse(input[2], true, out direction))
                {
                    Console.WriteLine("Direção inválida. Use N, S, E ou W.");
                    return false;
                }
                Console.WriteLine($"Digite a sequência de comandos da {identificador} sonda (ex: MMRMMRMRRM):");
            }
          
            var commandSequence = Console.ReadLine()?.ToUpper() ?? "";
            try
            {
                service.AddRover(x, y, direction, commandSequence);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar a sonda: {ex.Message}");
                return false;
            }
        }
    }
}