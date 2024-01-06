using tabuleiro;

namespace xadrez
{
    internal class Rei(Cor cor, Tabuleiro tab, PartidaXadrez partida) : Peca(cor, tab)
    {
        private PartidaXadrez _partida = partida;

        public override string ToString() => "R";

        private bool PodeMover(Posicao pos)
        {
            Peca? peca = Tab.Peca(pos);
            return peca == null || peca.Cor != Cor;
        }

        private bool TesteTorreParaRoque(Posicao pos)
        {
            Peca? p = Tab.Peca(pos);
            return p != null && p is Torre && p.Cor == Cor && p.QtdMovimentos == 0;
        }

        public override bool[,] MovimentosPossiveis()
        {

            bool[,] movs = new bool[Tab.Linhas, Tab.Colunas];

            if (Posicao == null) return movs;

            Posicao pos = new(0, 0);

            // Acima
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
                movs[pos.Linha, pos.Coluna] = true;

            // Nordeste
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
                movs[pos.Linha, pos.Coluna] = true;

            // Direita
            pos.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
                movs[pos.Linha, pos.Coluna] = true;

            // Sudeste
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
                movs[pos.Linha, pos.Coluna] = true;

            // Baixo
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
                movs[pos.Linha, pos.Coluna] = true;

            // Sudoeste
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
                movs[pos.Linha, pos.Coluna] = true;

            // Esquerda
            pos.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
                movs[pos.Linha, pos.Coluna] = true;

            // Noroeste
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
                movs[pos.Linha, pos.Coluna] = true;

            // #JogdaEspecial ROQUE
            if (QtdMovimentos == 0 && !_partida.Xeque)
            {
                // Roque pequeno
                Posicao posT1 = new(Posicao.Linha, Posicao.Coluna + 3);
                if(TesteTorreParaRoque(posT1))
                {
                    Posicao p1 = new(Posicao.Linha, Posicao.Coluna + 1);
                    Posicao p2 = new(Posicao.Linha, Posicao.Coluna + 2);
                    if (Tab.Peca(p1) == null && Tab.Peca(p2) == null)
                    {
                        movs[p2.Linha, p2.Coluna] = true;
                    }
                }

                // Roque grande
                Posicao posT2 = new(Posicao.Linha, Posicao.Coluna - 4);
                if (TesteTorreParaRoque(posT2))
                {
                    Posicao p1 = new(Posicao.Linha, Posicao.Coluna - 1);
                    Posicao p2 = new(Posicao.Linha, Posicao.Coluna - 2);
                    Posicao p3 = new(Posicao.Linha, Posicao.Coluna - 3);
                    if (Tab.Peca(p1) == null && Tab.Peca(p2) == null && Tab.Peca(p3) == null)
                    {
                        movs[p2.Linha, p2.Coluna] = true;
                    }
                }
            }

            return movs;
        }
    }
}
