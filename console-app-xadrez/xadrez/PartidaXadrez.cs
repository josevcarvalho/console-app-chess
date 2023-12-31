﻿using tabuleiro;

namespace xadrez
{
    internal class PartidaXadrez
    {
        public Tabuleiro Tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Finalizada { get; private set; }
        private readonly HashSet<Peca> _pecas;
        private readonly HashSet<Peca> _capturadas;
        public bool Xeque { get; private set; }
        public Peca? VulneravelEnPassant { get; private set; }

        public PartidaXadrez()
        {
            Tab = new(8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Finalizada = false;
            Xeque = false;
            _pecas = [];
            _capturadas = [];
            ColocarPecas();
        }

        public Peca? ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca? peca = Tab.RetirarPeca(origem);
            peca?.IncrementarQtdMovimentos();
            Peca? pecaCapturada = Tab.RetirarPeca(destino);

            if (peca != null)
                Tab.ColocarPeca(peca, destino);

            if (pecaCapturada != null)
                _capturadas.Add(pecaCapturada);

            // #JogadaEspecial ROQUE PEQUENO
            if (peca is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new(origem.Linha, origem.Coluna + 1);
                Peca T = Tab.RetirarPeca(origemT)!;
                T.IncrementarQtdMovimentos();
                Tab.ColocarPeca(T, destinoT);
            }

            // #JogadaEspecial ROQUE GRANDE
            if (peca is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new(origem.Linha, origem.Coluna - 1);
                Peca T = Tab.RetirarPeca(origemT)!;
                T.IncrementarQtdMovimentos();
                Tab.ColocarPeca(T, destinoT);
            }

            // JogadaEspecial En Passant
            if (peca is Peao && origem.Coluna != destino.Coluna && pecaCapturada == null)
            {
                Posicao posP = (peca.Cor == Cor.Branca) ? new(destino.Linha + 1, destino.Coluna) : new(destino.Linha - 1, destino.Coluna);

                pecaCapturada = Tab.RetirarPeca(posP)!;
                _capturadas.Add(pecaCapturada);
             
            }

            return pecaCapturada;

        }

        public void DesfazMovimento(Posicao origem, Posicao destino, Peca? pecaCapturada)
        {
            Peca? p = Tab.RetirarPeca(destino);
            p?.DecrementarQtdMovimentos();
            if (pecaCapturada != null)
            {
                Tab.ColocarPeca(pecaCapturada, destino);
                _capturadas.Remove(pecaCapturada);
            }

            if (p != null)
                Tab.ColocarPeca(p, origem);

            // #JogadaEspecial ROQUE PEQUENO
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new(origem.Linha, origem.Coluna + 1);
                Peca T = Tab.RetirarPeca(destinoT)!;
                T.DecrementarQtdMovimentos();
                Tab.ColocarPeca(T, origemT);
            }

            // #JogadaEspecial ROQUE GRANDE
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new(origem.Linha, origem.Coluna - 1);
                Peca T = Tab.RetirarPeca(destinoT)!;
                T.DecrementarQtdMovimentos();
                Tab.ColocarPeca(T, origemT);
            }

            // #JogadaEspecial En Passant
            if (p is Peao && origem.Coluna != destino.Coluna && pecaCapturada == VulneravelEnPassant)
            {
                Peca peao = Tab.RetirarPeca(destino)!;
                Posicao posP = (p.Cor == Cor.Branca) ? new(3, destino.Coluna) : new(4, destino.Coluna);

                Tab.ColocarPeca(peao, posP);
            }
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca? pecaCapturada = ExecutaMovimento(origem, destino);

            if (EstaEmXeque(JogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            Peca? p = Tab.Peca(destino);

            // #JogadaEspecial Promocao
            if (p is Peao && destino.Linha == ((p.Cor == Cor.Branca) ? 0 : 7))
            {
                p = Tab.RetirarPeca(destino);
                _pecas.Remove(p);
                Peca dama = new Dama(p.Cor, Tab);
                PosicaoXadrez pos = destino.ToPosicaoXadrez();
                ColocarNovaPeca(pos, dama);
            }

            if (EstaEmXeque(Adversaria(JogadorAtual))) Xeque = true;
            else Xeque = false;

            if (TesteXequemate(Adversaria(JogadorAtual))) Finalizada = true;
            else
            {
                Turno++;
                AlteraJogador();
            }

            // #JogadaEspecial En Passant
            if (p is Peao && (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2))
                VulneravelEnPassant = p;
            else VulneravelEnPassant = null;
        }

        public void ValidarPosicaoDeOrigem(Posicao pos)
        {
            Peca pecaEscolhida = Tab.Peca(pos) ??
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");

            if (JogadorAtual != pecaEscolhida.Cor)
                throw new TabuleiroException("A peça de origem escolhida não é sua");

            if (!pecaEscolhida.ExisteMovimentosPossiveis())
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem");
            
        }

        public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!((Tab.Peca(origem)?.PodeMoverPara(destino)) ?? true))
                throw new TabuleiroException("Posição de destino inválida");
        }

        private void AlteraJogador()
        {
            JogadorAtual = (JogadorAtual == Cor.Branca) ? Cor.Preta : Cor.Branca;
        }

        public HashSet<Peca> PecasCapturadas(Cor cor) =>
            new (_capturadas.Where(p => p.Cor == cor));

        public HashSet<Peca> PecasEmJogo(Cor cor) =>
            new (_pecas.Where(p => p.Cor == cor).Except(PecasCapturadas(cor)));

        private static Cor Adversaria(Cor cor)
        {
            return cor == Cor.Branca ? Cor.Preta : Cor.Branca;
        }

        private Peca? Rei(Cor cor)
        {
            return PecasEmJogo(cor).OfType<Rei>().FirstOrDefault();
        }

        public bool EstaEmXeque(Cor cor)
        {
            Peca rei = Rei(cor) ?? throw new TabuleiroException($"Não tem rei da cor {cor} no tabuleiro!");

            return PecasEmJogo(Adversaria(cor)).Any(p => p.MovimentosPossiveis()[rei.Posicao!.Linha, rei.Posicao!.Coluna]);

        }

        public bool TesteXequemate(Cor cor)
        {
            if (!EstaEmXeque(cor))
                return false;

            return PecasEmJogo(cor).Any(p => {    
                bool [,] movs = p.MovimentosPossiveis();
                for (int i = 0; i < Tab.Linhas; i++)
                {
                    for (int j = 0; j < Tab.Colunas; j++)
                    {
                        if (movs[i, j])
                        {
                            Posicao origem = p.Posicao!;
                            Posicao destino = new(i, j);
                            Peca? capturada = ExecutaMovimento(origem, destino);
                            bool testeXeque = EstaEmXeque(cor);
                            DesfazMovimento(origem, destino, capturada);
                            if (!testeXeque)
                                return false;
                        }
                    }
                }
                return true;
            });
        }

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            _pecas.Add(peca);
        }

        public void ColocarNovaPeca(PosicaoXadrez pos, Peca peca)
        {
            Tab.ColocarPeca(peca, new PosicaoXadrez(pos.Coluna, pos.Linha).ToPosicao());
            _pecas.Add(peca);
        }

        private void ColocarPecas()
        {
            ColocarNovaPeca('a', 1, new Torre(Cor.Branca, Tab));
            ColocarNovaPeca('b', 1, new Cavalo(Cor.Branca, Tab));
            ColocarNovaPeca('c', 1, new Bispo(Cor.Branca, Tab));
            ColocarNovaPeca('d', 1, new Dama(Cor.Branca, Tab));
            ColocarNovaPeca('e', 1, new Rei(Cor.Branca, Tab, this));
            ColocarNovaPeca('f', 1, new Bispo(Cor.Branca, Tab));
            ColocarNovaPeca('g', 1, new Cavalo(Cor.Branca, Tab));
            ColocarNovaPeca('h', 1, new Torre(Cor.Branca, Tab));
            ColocarNovaPeca('a', 2, new Peao(Cor.Branca, Tab, this));
            ColocarNovaPeca('b', 2, new Peao(Cor.Branca, Tab, this));
            ColocarNovaPeca('c', 2, new Peao(Cor.Branca, Tab, this));
            ColocarNovaPeca('d', 2, new Peao(Cor.Branca, Tab, this));
            ColocarNovaPeca('e', 2, new Peao(Cor.Branca, Tab, this));
            ColocarNovaPeca('f', 2, new Peao(Cor.Branca, Tab, this));
            ColocarNovaPeca('g', 2, new Peao(Cor.Branca, Tab, this));
            ColocarNovaPeca('h', 2, new Peao(Cor.Branca, Tab, this));

            ColocarNovaPeca('a', 8, new Torre(Cor.Preta, Tab));
            ColocarNovaPeca('b', 8, new Cavalo(Cor.Preta, Tab));
            ColocarNovaPeca('c', 8, new Bispo(Cor.Preta, Tab));
            ColocarNovaPeca('d', 8, new Dama(Cor.Preta, Tab));
            ColocarNovaPeca('e', 8, new Rei(Cor.Preta, Tab, this));
            ColocarNovaPeca('f', 8, new Bispo(Cor.Preta, Tab));
            ColocarNovaPeca('g', 8, new Cavalo(Cor.Preta, Tab));
            ColocarNovaPeca('h', 8, new Torre(Cor.Preta, Tab));
            ColocarNovaPeca('a', 7, new Peao(Cor.Preta, Tab, this));
            ColocarNovaPeca('b', 7, new Peao(Cor.Preta, Tab, this));
            ColocarNovaPeca('c', 7, new Peao(Cor.Preta, Tab, this));
            ColocarNovaPeca('d', 7, new Peao(Cor.Preta, Tab, this));
            ColocarNovaPeca('e', 7, new Peao(Cor.Preta, Tab, this));
            ColocarNovaPeca('f', 7, new Peao(Cor.Preta, Tab, this));
            ColocarNovaPeca('g', 7, new Peao(Cor.Preta, Tab, this));
            ColocarNovaPeca('h', 7, new Peao(Cor.Preta, Tab, this));
        }
    }
}