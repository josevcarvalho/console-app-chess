using tabuleiro;

namespace xadrez
{
    internal class PartidaXadrez
    {
        public Tabuleiro Tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Finalizada { get; private set; }

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
            _ = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(peca, destino);
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            ExecutaMovimento(origem, destino);
            Turno++;
            AlteraJogador();
        }

        public void ValidarPosicaoDeOrigem(Posicao pos)
        {
            Peca pecaEscolhida = Tab.Peca(pos) ??
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");

            if (JogadorAtual != pecaEscolhida.Cor)
                throw new TabuleiroException("A peça de origem escolhida não é sua");

            if (!pecaEscolhida.ExisteMovimentosPossiveis())
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem");
            
        }

        public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            #pragma warning disable CS8602 // Desreferência de uma referência possivelmente nula.
            if (!Tab.Peca(origem).PodeMoverPara(destino))
            {
                throw new TabuleiroException("Posica de destino invalida");
            }
            #pragma warning restore CS8602 // Desreferência de uma referência possivelmente nula.
        }

        private void AlteraJogador()
        {
            JogadorAtual = (JogadorAtual == Cor.Branca) ? Cor.Preta : Cor.Branca;
        }


        private void ColocarPecas()
        {
            Tab.ColocarPeca(new Torre(Tab, Cor.Preta), new PosicaoXadrez('c', 1).ToPosicao());
            Tab.ColocarPeca(new Rei(Tab, Cor.Branca), new PosicaoXadrez('c', 2).ToPosicao());
        }
    }
}