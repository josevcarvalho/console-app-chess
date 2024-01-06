using tabuleiro;

namespace xadrez
{
    internal class Rei(Cor cor, Tabuleiro tab) : Peca(cor, tab)
    {
        public override string ToString() => "R";

        private bool PodeMover(Posicao pos)
        {
            Peca? peca = Tab.Peca(pos);
            return peca == null || peca.Cor != this.Cor;
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

            return movs;
        }
    }
}
