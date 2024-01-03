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

        Console.Write("Origem: ");
        Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();

        bool[,] movimentosPossiveis = partida.Tab.Peca(origem).MovimentosPossiveis();
            
        Console.Clear();
        Tela.ImprimirTabuleiro(partida.Tab, movimentosPossiveis);

        Console.Write("Destino: ");
        Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();

        partida.ExecutaMovimento(origem, destino);
    }
    
    Console.ReadLine();
} catch (TabuleiroException e)
{
    Console.WriteLine(e.Message);
}