namespace tabuleiro
{
    internal class Tabuleiro
    {
        public int Linhas { get; set; }
        public int Colunas { get; set; }
        public readonly Peca[,] _pecas;

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
    }
}
