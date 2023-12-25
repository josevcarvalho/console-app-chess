namespace tabuleiro
{
    internal class Peca(Cor cor, Tabuleiro tabuleiro)
    {
        public Posicao? Posicao { get; set; } = null;
        public Cor Cor { get; protected set; } = cor;
        public int QtdMovimentos { get; protected set; } = 0;
        public Tabuleiro Tab { get; protected set; } = tabuleiro;
    }
}
