using tabuleiro;
using tela;
using xadrez;

try
{
    Tabuleiro tab = new(8);
    tab.ColocarPeca(new Torre(tab, Cor.Preta), new Posicao(0, 0));
    tab.ColocarPeca(new Torre(tab, Cor.Preta), new Posicao(0, 7));
    tab.ColocarPeca(new Rei(tab, Cor.Preta), new Posicao(2, 4));

    Tela.ImprimirTabuleiro(tab);
    Console.ReadLine();
} catch (TabuleiroException e)
{
    Console.WriteLine(e.Message);
}