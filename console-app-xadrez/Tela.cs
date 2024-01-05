using System.Net.NetworkInformation;
using tabuleiro;
using xadrez;

namespace tela
{
    internal class Tela
    {
        public static void ImprimirPartida(PartidaXadrez partida)
        {
            ImprimirTabuleiro(partida.Tab);
            ImprimirPecasCapturadas(partida);
            Console.WriteLine($"Turno: {partida.Turno}");
            Console.WriteLine($"Aguardando jogada: {partida.JogadorAtual}");
            if (partida.Xeque)
            {
                Console.WriteLine("XEQUE!");
            }
        }

        public static void ImprimirPecasCapturadas(PartidaXadrez partida)
        {
            Console.WriteLine("Peças capturadas:");
            Console.Write("Brancas: ");
            ImprimirConjunto(partida.PecasCapturadas(Cor.Branca));
            ConsoleColor originalConsoleColor = Console.ForegroundColor;
            Console.Write("Pretas: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            ImprimirConjunto(partida.PecasCapturadas(Cor.Preta));
            Console.ForegroundColor = originalConsoleColor;
            Console.WriteLine();
        }

        public static void ImprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.WriteLine($"[{string.Join(" ", conjunto)}]");
        }


        public static void ImprimirTabuleiro(Tabuleiro tab, bool[,]? posicoesPossiveis = null)
        {
            posicoesPossiveis ??= new bool[tab.Linhas, tab.Colunas];

            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;

            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write($"{8 - i} ");
                for (int j = 0; j < tab.Colunas; j++)
                {
                    if (posicoesPossiveis[i, j])
                        Console.BackgroundColor = fundoAlterado;
                    else
                        Console.BackgroundColor = fundoOriginal;

                    ImprimirPeca(tab.Peca(i, j));
                }
                Console.WriteLine();
            }
            Console.BackgroundColor = fundoOriginal;
            ImprimirColunas(tab.Colunas);
            Console.WriteLine();
        }

        private static void ImprimirColunas(int colunas)
        {
            Console.WriteLine($"  {string.Concat(Enumerable.Range('a', colunas).Select(c => $"{(char)c} "))}");
        }

        public static void ImprimirPeca(Peca? peca)
        {
            if (peca == null)
            {
                Console.Write("- ");
                return;
            }

            if (peca.Cor == Cor.Branca)
                Console.Write(peca);
            else
            {
                ConsoleColor originalForegroundColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(peca);
                Console.ForegroundColor = originalForegroundColor;
            }

            Console.Write(" ");
        }

        public static PosicaoXadrez LerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse($"{s[1]}");
            return new PosicaoXadrez(coluna, linha);
        }

    }
}