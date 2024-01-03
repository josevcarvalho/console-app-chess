using tabuleiro;

namespace xadrez
{
    internal class Torre(Tabuleiro tab, Cor cor) : Peca(cor, tab)
    {
        public override string ToString() => "T";

        private bool PodeMover(Posicao pos)
        {
            Peca peca = Tab.Peca(pos);
            return peca == null || peca.Cor != this.Cor;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] movs = new bool[Tab.Linhas, Tab.Colunas];

            Posicao pos = new(0, 0);

            // Acima
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            while (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                movs[pos.Linha, pos.Coluna] = true;
                if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != this.Cor)
                    break;

                pos.Linha--;
            }

            // Abaixo
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
            while (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                movs[pos.Linha, pos.Coluna] = true;
                if (Tab.Peca(pos) == null || Tab.Peca(pos).Cor == this.Cor)
                    pos.Linha++;
                else
                    break;
            }

            // Direita
            pos.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
            while (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                movs[pos.Linha, pos.Coluna] = true;
                if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != this.Cor)
                    break;

                pos.Coluna++;
            }

            // Esquerda
            pos.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
            while (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                movs[pos.Linha, pos.Coluna] = true;
                if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != this.Cor)
                    break;

                pos.Coluna--;
            }

            return movs;
        }
    }
}
