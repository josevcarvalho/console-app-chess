namespace tabuleiro
{
    internal class Peca(Posicao posicao, Cor cor, Tabuleiro tabuleiro)
    {
        public Posicao Posicao { get; set; } = posicao;
        public Cor Cor { get; protected set; } = cor;
        public int QtdMovimentos { get; protected set; } = 0;
        public Tabuleiro Tab { get; protected set; } = tabuleiro;
    }
}
