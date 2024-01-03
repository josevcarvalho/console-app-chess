namespace tabuleiro
{
    internal class Tabuleiro
    {
        public int Linhas { get; set; }
        public int Colunas { get; set; }
        public Peca?[,] _pecas { get; private set; }

        public Tabuleiro(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;
            _pecas = new Peca[linhas, colunas];
        }

        public Tabuleiro(int tamanho) {
            Linhas = tamanho;
            Colunas = tamanho;
            _pecas = new Peca[Linhas, Colunas];
        }

        public void ColocarPeca(Peca peca, Posicao pos)
        {
            if (ExistePeca(pos))
                throw new TabuleiroException("Ja existe uma peça nessa posição");
            _pecas[pos.Linha, pos.Coluna] = peca;
            peca.Posicao = pos;
        }

        public Peca? RetirarPeca(Posicao pos)
        {
            if (Peca(pos) == null)
                return null;

            Peca? aux = Peca(pos);
            #pragma warning disable CS8602 // Desreferência de uma referência possivelmente nula.
            aux.Posicao = null;
            #pragma warning restore CS8602 // Desreferência de uma referência possivelmente nula.
            _pecas[pos.Linha, pos.Coluna] = null;
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
            return _pecas[linha, coluna];
        }

        public Peca? Peca(Posicao pos) =>
            _pecas[pos.Linha, pos.Coluna];

    }
}
