using tabuleiro;

namespace tela
{
    internal class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tab)
        {
            for (int i = 0; i < tab.Linhas; i++)
            {
                for (int j = 0; j < tab.Colunas; j++)
                {
                    Console.Write(tab._pecas[i, j] != null ? $"{tab._pecas[i, j]} " : "- ");
                }
                Console.WriteLine();
            }
        }
    }
}