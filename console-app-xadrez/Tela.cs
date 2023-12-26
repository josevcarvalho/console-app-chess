﻿using System.Linq;
using tabuleiro;

namespace tela
{
    internal class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tab)
        {
            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write($"{8 - i} ");
                for (int j = 0; j < tab.Colunas; j++)
                {
                    if (tab.Peca(i, j) == null)
                        Console.Write("- ");
                    else
                    {
                        ImprimirPeca(tab.Peca(i, j));
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }

            ImprimirColunas(tab.Colunas);
        }

        private static void ImprimirColunas(int colunas)
        {
            Console.Write($"  {string.Concat(Enumerable.Range('a', colunas).Select(c => $"{(char)c} "))}");
        }

        public static void ImprimirPeca(Peca peca)
        {
            if (peca.Cor == Cor.Branca)
                Console.Write(peca);
            else
            {
                ConsoleColor originalForegroundColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(peca);
                Console.ForegroundColor = originalForegroundColor;
            }
        } 

    }
}