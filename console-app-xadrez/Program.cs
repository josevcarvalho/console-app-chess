using tabuleiro;
using tela;
using xadrez;

try
{
    PartidaXadrez partida = new();

    while (!partida.Finalizada)
    {
        Console.Clear();
        Tela.ImprimirTabuleiro(partida.Tab);

        Console.WriteLine();
        Console.Write("Origem: ");
        Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();

        Console.Write("Destino: ");
        Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();

        partida.ExecutaMovimento(origem, destino);
    }
    
    Console.ReadLine();
} catch (TabuleiroException e)
{
    Console.WriteLine(e.Message);
}