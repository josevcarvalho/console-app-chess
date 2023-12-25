using tabuleiro;

namespace xadrez
{
    internal class Rei(Tabuleiro tab, Cor cor) : Peca(cor, tab)
    {
        public override string ToString()
        {
            return "R";
        }
    }
}
