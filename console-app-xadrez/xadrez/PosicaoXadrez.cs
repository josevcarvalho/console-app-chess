using tabuleiro;

namespace xadrez
{
    internal class PosicaoXadrez(char coluna, int linha)
    {
        public char Coluna { get; set; } = coluna;
        public int Linha { get; set; } = linha;

        public override string ToString() => $"{Coluna}{Linha}";

        public Posicao ToPosicao() => new(8 - Linha, Coluna - 'a');

    }
}
