using tabuleiro;

namespace xadrez
{
    internal class Peao(Cor cor, Tabuleiro tab, PartidaXadrez partida) : Peca(cor, tab)
    {
        private PartidaXadrez _partida = partida;
        public override string ToString()
        {
            return "P";
        }

        private bool ExisteInimigo(Posicao pos)
        {
            Peca? p = Tab.Peca(pos);
            return p != null && p.Cor != Cor;
        }

        private bool Livre(Posicao pos)
        {
            return Tab.Peca(pos) == null;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] movs = new bool[Tab.Linhas, Tab.Colunas];

            if (Posicao == null) return movs;

            Posicao pos = new(0, 0);

            int direcao = (Cor == Cor.Branca) ? -1 : 1;

            pos.DefinirValores(Posicao.Linha + direcao, Posicao.Coluna);
            if (Tab.PosicaoValida(pos) && Livre(pos)) movs[pos.Linha, pos.Coluna] = true;

            pos.DefinirValores(Posicao.Linha + direcao * 2, Posicao.Coluna);
            if (Tab.PosicaoValida(pos) && Livre(pos) && QtdMovimentos == 0) movs[pos.Linha, pos.Coluna] = true;

            pos.DefinirValores(Posicao.Linha + direcao, Posicao.Coluna - 1);
            if (Tab.PosicaoValida(pos) && ExisteInimigo(pos)) movs[pos.Linha, pos.Coluna] = true;

            pos.DefinirValores(Posicao.Linha + direcao, Posicao.Coluna + 1);
            if (Tab.PosicaoValida(pos) && ExisteInimigo(pos)) movs[pos.Linha, pos.Coluna] = true;

            // #JogadaEspecial En Passant
            int linhaEnPassant = (Cor == Cor.Branca) ? 3 : 4;
            if (Posicao.Linha == linhaEnPassant)
            {
                Posicao esquerda = new(Posicao.Linha, Posicao.Coluna - 1);
                if (Tab.PosicaoValida(esquerda) && ExisteInimigo(esquerda) && Tab.Peca(esquerda) == _partida.VulneravelEnPassant)
                    movs[esquerda.Linha + direcao, esquerda.Coluna] = true;

                Posicao direita = new(Posicao.Linha, Posicao.Coluna + 1);
                if (Tab.PosicaoValida(direita) && ExisteInimigo(direita) && Tab.Peca(direita) == _partida.VulneravelEnPassant)
                    movs[direita.Linha + direcao, direita.Coluna] = true;
            }

            return movs;
        }
    }
}
