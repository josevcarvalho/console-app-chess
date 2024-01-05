using tabuleiro;
using tela;
using xadrez;

try
{
    PartidaXadrez partida = new();

    while (!partida.Finalizada)
    {
        try
        {
            Console.Clear();
            Tela.ImprimirPartida(partida);

            Console.WriteLine();
            Console.Write("Origem: ");
            Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();
            partida.ValidarPosicaoDeOrigem(origem);

            #pragma warning disable CS8602 // Desreferência de uma referência possivelmente nula.
            bool[,] movimentosPossiveis = partida.Tab.Peca(origem).MovimentosPossiveis();
            #pragma warning restore CS8602 // Desreferência de uma referência possivelmente nula.

            Console.Clear();
            Tela.ImprimirTabuleiro(partida.Tab, movimentosPossiveis);

            Console.Write("Destino: ");
            Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();
            partida.ValidarPosicaoDeDestino(origem, destino);

            partida.RealizaJogada(origem, destino);
        }
        catch (TabuleiroException e )
        {
            Console.WriteLine(e.Message);
            Console.ReadLine();
        }
    }
    
    Console.ReadLine();
} catch (TabuleiroException e)
{
    Console.WriteLine(e.Message);
}