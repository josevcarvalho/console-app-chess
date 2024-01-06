using xadrez;

namespace tabuleiro
{
    internal class Posicao(int linha, int coluna)
    {
        public int Linha { get; set; } = linha;
        public int Coluna { get; set; } = coluna;

        public void DefinirValores(int linha, int coluna)
        {
            Linha = linha;
            Coluna = coluna;
        }

        public override string ToString()
        {
            return $"{Linha}, {Coluna}";
        }

        public PosicaoXadrez ToPosicaoXadrez()
        {
            char colunaXadrez = (char)('a' + Coluna);
            int linhaXadrez = 8 - Linha;
            return new PosicaoXadrez(colunaXadrez, linhaXadrez);
        }
    }
}
