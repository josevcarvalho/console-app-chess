using tabuleiro;

namespace xadrez
{
    internal class PartidaXadrez
    {
        public Tabuleiro Tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Finalizada { get; private set; }
        private HashSet<Peca> _pecas;
        private HashSet<Peca> _capturadas;
        public bool Xeque { get; private set; }

        public PartidaXadrez()
        {
            Tab = new(8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Finalizada = false;
            Xeque = false;
            _pecas = [];
            _capturadas = [];
            ColocarPecas();
        }

        public Peca? ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca? peca = Tab.RetirarPeca(origem);
            peca?.IncrementarQtdMovimentos();
            Peca? pecaCapturada = Tab.RetirarPeca(destino);
            #pragma warning disable CS8604 // Possível argumento de referência nula.
            Tab.ColocarPeca(peca, destino);
            #pragma warning restore CS8604 // Possível argumento de referência nula.

            if (pecaCapturada != null)
                _capturadas.Add(pecaCapturada);

            return pecaCapturada;
        }

        public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca? p = Tab.RetirarPeca(destino);
            p.DecrementarQtdMovimentos();
            if (pecaCapturada != null)
            {
                Tab.ColocarPeca(pecaCapturada, destino);
                _capturadas.Remove(pecaCapturada);
            }
            Tab.ColocarPeca(p, origem);
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca? pecaCapturada = ExecutaMovimento(origem, destino);

            if (EstaEmXeque(JogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            if (EstaEmXeque(Adversaria(JogadorAtual)))
                Xeque = true;
            else
                Xeque = false;


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

        public HashSet<Peca> PecasCapturadas(Cor cor) =>
            new (_capturadas.Where(p => p.Cor == cor));

        public HashSet<Peca> PecasEmJogo(Cor cor) =>
            new (_pecas.Where(p => p.Cor == cor).Except(PecasCapturadas(cor)));

        private static Cor Adversaria(Cor cor)
        {
            return cor == Cor.Branca ? Cor.Preta : Cor.Branca;
        }

        private Peca? Rei(Cor cor)
        {
            return PecasEmJogo(cor).OfType<Rei>().FirstOrDefault();
        }

        public bool EstaEmXeque(Cor cor)
        {
            Peca rei = Rei(cor) ?? throw new TabuleiroException($"Não tem rei da cor {cor} no tabuleiro!");

            #pragma warning disable CS8602 // Desreferência de uma referência possivelmente nula.
            return PecasEmJogo(Adversaria(cor)).Any(p => p.MovimentosPossiveis()[rei.Posicao.Linha, rei.Posicao.Coluna]);
            #pragma warning restore CS8602 // Desreferência de uma referência possivelmente nula.
        }


        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            _pecas.Add(peca);
        }
        
        private void ColocarPecas()
        {
            ColocarNovaPeca('c', 1, new Torre(Tab, Cor.Branca));
            ColocarNovaPeca('c', 2, new Torre(Tab, Cor.Branca));
            ColocarNovaPeca('d', 2, new Torre(Tab, Cor.Branca));
            ColocarNovaPeca('e', 2, new Torre(Tab, Cor.Branca));
            ColocarNovaPeca('e', 1, new Torre(Tab, Cor.Branca));
            ColocarNovaPeca('d', 1, new Rei(Tab, Cor.Branca));

            ColocarNovaPeca('c', 7, new Torre(Tab, Cor.Preta));
            ColocarNovaPeca('c', 8, new Torre(Tab, Cor.Preta));
            ColocarNovaPeca('d', 7, new Torre(Tab, Cor.Preta));
            ColocarNovaPeca('e', 7, new Torre(Tab, Cor.Preta));
            ColocarNovaPeca('e', 8, new Torre(Tab, Cor.Preta));
            ColocarNovaPeca('d', 8, new Rei(Tab, Cor.Preta));

        }
    }
}