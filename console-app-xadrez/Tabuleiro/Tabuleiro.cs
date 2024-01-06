using System.Reflection.Metadata.Ecma335;

namespace tabuleiro
{
    internal class Tabuleiro
    {
        public int Linhas { get; set; }
        public int Colunas { get; set; }
        public Peca?[,] Pecas { get; private set; }

        public Tabuleiro(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;
            Pecas = new Peca[linhas, colunas];
        }

        public Tabuleiro(int tamanho) {
            Linhas = tamanho;
            Colunas = tamanho;
            Pecas = new Peca[Linhas, Colunas];
        }

        public void ColocarPeca(Peca peca, Posicao pos)
        {
            if (ExistePeca(pos))
                throw new TabuleiroException($"Ja existe uma peça nessa posição: {pos}");
            Pecas[pos.Linha, pos.Coluna] = peca;


            peca.Posicao = pos;
        }

        public Peca? RetirarPeca(Posicao pos)
        {
            Peca? aux = Peca(pos);
            if (aux == null) return null;
            aux.Posicao = null;
            Pecas[pos.Linha, pos.Coluna] = null;
            return aux;

        }

        public bool PosicaoValida(Posicao pos)
        {
            return pos.Linha >= 0 && pos.Linha < Linhas && pos.Coluna >= 0 && pos.Coluna < Colunas;
        }

        public void ValidarPosicao(Posicao pos)
        {
            if (!PosicaoValida(pos))
                throw new TabuleiroException("Posicao invalida");
        }

        public bool ExistePeca(Posicao pos) {
            ValidarPosicao(pos);
            return Peca(pos) != null;
        }

        public Peca? Peca(int linha, int coluna)
        {
            return Pecas[linha, coluna];
        }

        public Peca? Peca(Posicao pos)
        {
            return Pecas[pos.Linha, pos.Coluna];
        }
    }
}
