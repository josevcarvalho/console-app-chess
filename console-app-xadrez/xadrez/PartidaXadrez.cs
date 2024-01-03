using tabuleiro;

namespace xadrez
{
    internal class PartidaXadrez
    {
        public readonly Tabuleiro Tab;
        private int Turno;
        private Cor JogadorAtual;
        public bool Finalizada;

        public PartidaXadrez()
        {
            Tab = new(8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Finalizada = false;
            ColocarPecas();
        }

        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca? peca = Tab.RetirarPeca(origem);
            peca?.IncrementarQtdMovimentos();
            Peca? pecaCapturada = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(peca, destino);
        }

        private void ColocarPecas()
        {
            Tab.ColocarPeca(new Torre(Tab, Cor.Preta), new PosicaoXadrez('c', 1).ToPosicao());
            Tab.ColocarPeca(new Rei(Tab, Cor.Branca), new PosicaoXadrez('c', 2).ToPosicao());
        }
    }
}