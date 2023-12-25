using tabuleiro;

namespace xadrez
{
    internal class Torre(Tabuleiro tab, Cor cor) : Peca(cor, tab)
    {
        public override string ToString()
        {
            return "T";
        }
    }
}
