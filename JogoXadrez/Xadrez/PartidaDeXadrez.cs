using System.Collections.Generic;
using Tabuleiro;

namespace Xadrez
{
    class PartidaDeXadrez
    {

        public ClasseTabuleiro tabuleiro { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;
        public bool xeque { get; private set; }
        public Peca vulneravelEnPassant { get; private set; }

        public PartidaDeXadrez()
        {
            tabuleiro = new ClasseTabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            xeque = false;
            vulneravelEnPassant = null;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();
        }

        public Peca executaMovimento(Posicao origem, Posicao destino)
        {
            Peca peca = tabuleiro.retirarPeca(origem);
            peca.incrementarQteMovimentos();
            Peca pecaCapturada = tabuleiro.retirarPeca(destino);
            tabuleiro.colocarPeca(peca, destino);
            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }

            // #jogadaespecial roque pequeno
            if (peca is Rei && destino.coluna == origem.coluna + 2)
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna + 3);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna + 1);
                Peca T = tabuleiro.retirarPeca(origemT);
                T.incrementarQteMovimentos();
                tabuleiro.colocarPeca(T, destinoT);
            }

            // #jogadaespecial roque grande
            if (peca is Rei && destino.coluna == origem.coluna - 2)
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna - 4);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna - 1);
                Peca T = tabuleiro.retirarPeca(origemT);
                T.incrementarQteMovimentos();
                tabuleiro.colocarPeca(T, destinoT);
            }

            // #jogadaespecial en passant
            if (peca is Peao)
            {
                if (origem.coluna != destino.coluna && pecaCapturada == null)
                {
                    Posicao posP;
                    if (peca.cor == Cor.Branca)
                    {
                        posP = new Posicao(destino.linha + 1, destino.coluna);
                    }
                    else
                    {
                        posP = new Posicao(destino.linha - 1, destino.coluna);
                    }
                    pecaCapturada = tabuleiro.retirarPeca(posP);
                    capturadas.Add(pecaCapturada);
                }
            }
            return pecaCapturada;
        }

        public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca peca = tabuleiro.retirarPeca(destino);
            peca.decrementarQteMovimentos();
            if (pecaCapturada != null)
            {
                tabuleiro.colocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
            tabuleiro.colocarPeca(peca, origem);

            // #jogadaespecial roque pequeno
            if (peca is Rei && destino.coluna == origem.coluna + 2)
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna + 3);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna + 1);
                Peca T = tabuleiro.retirarPeca(destinoT);
                T.decrementarQteMovimentos();
                tabuleiro.colocarPeca(T, origemT);
            }

            // #jogadaespecial roque grande
            if (peca is Rei && destino.coluna == origem.coluna - 2)
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna - 4);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna - 1);
                Peca T = tabuleiro.retirarPeca(destinoT);
                T.decrementarQteMovimentos();
                tabuleiro.colocarPeca(T, origemT);
            }

            // #jogadaespecial en passant
            if (peca is Peao)
            {
                if (origem.coluna != destino.coluna && pecaCapturada == vulneravelEnPassant)
                {
                    Peca peao = tabuleiro.retirarPeca(destino);
                    Posicao posP;
                    if (peca.cor == Cor.Branca)
                    {
                        posP = new Posicao(3, destino.coluna);
                    }
                    else
                    {
                        posP = new Posicao(4, destino.coluna);
                    }
                    tabuleiro.colocarPeca(peao, posP);
                }
            }
        }

        public void realizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = executaMovimento(origem, destino);

            if (estaEmXeque(jogadorAtual))
            {
                desfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            Peca peca = tabuleiro.peca(destino);

            // #jogadaespecial promocao
            if (peca is Peao)
            {
                if ((peca.cor == Cor.Branca && destino.linha == 0) || (peca.cor == Cor.Preta && destino.linha == 7))
                {
                    peca = tabuleiro.retirarPeca(destino);
                    pecas.Remove(peca);
                    Peca dama = new Dama(tabuleiro, peca.cor);
                    tabuleiro.colocarPeca(dama, destino);
                    pecas.Add(dama);
                }
            }

            if (estaEmXeque(adversaria(jogadorAtual)))
            {
                xeque = true;
            }
            else
            {
                xeque = false;
            }

            if (testeXequemate(adversaria(jogadorAtual)))
            {
                terminada = true;
            }
            else
            {
                turno++;
                mudaJogador();
            }

            // #jogadaespecial en passant
            if (peca is Peao && (destino.linha == origem.linha - 2 || destino.linha == origem.linha + 2))
            {
                vulneravelEnPassant = peca;
            }
            else
            {
                vulneravelEnPassant = null;
            }

        }

        public void validarPosicaoDeOrigem(Posicao posicao)
        {
            if (tabuleiro.peca(posicao) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if (jogadorAtual != tabuleiro.peca(posicao).cor)
            {
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }
            if (!tabuleiro.peca(posicao).existeMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        public void validarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!tabuleiro.peca(origem).movimentoPossivel(destino))
            {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }

        private void mudaJogador()
        {
            if (jogadorAtual == Cor.Branca)
            {
                jogadorAtual = Cor.Preta;
            }
            else
            {
                jogadorAtual = Cor.Branca;
            }
        }

        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            HashSet<Peca> auxiliar = new HashSet<Peca>();
            foreach (Peca x in capturadas)
            {
                if (x.cor == cor)
                {
                    auxiliar.Add(x);
                }
            }
            return auxiliar;
        }

        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            HashSet<Peca> auxiliar = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {
                if (x.cor == cor)
                {
                    auxiliar.Add(x);
                }
            }
            auxiliar.ExceptWith(pecasCapturadas(cor));
            return auxiliar;
        }

        private Cor adversaria(Cor cor)
        {
            if (cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }

        private Peca rei(Cor cor)
        {
            foreach (Peca x in pecasEmJogo(cor))
            {
                if (x is Rei)
                {
                    return x;
                }
            }
            return null;
        }

        public bool estaEmXeque(Cor cor)
        {
            Peca R = rei(cor);
            if (R == null)
            {
                throw new TabuleiroException("Não tem rei da cor " + cor + " no tabuleiro!");
            }
            foreach (Peca x in pecasEmJogo(adversaria(cor)))
            {
                bool[,] matriz = x.movimentosPossiveis();
                if (matriz[R.posicao.linha, R.posicao.coluna])
                {
                    return true;
                }
            }
            return false;
        }

        public bool testeXequemate(Cor cor)
        {
            if (!estaEmXeque(cor))
            {
                return false;
            }
            foreach (Peca x in pecasEmJogo(cor))
            {
                bool[,] matriz = x.movimentosPossiveis();
                for (int i = 0; i < tabuleiro.linhas; i++)
                {
                    for (int j = 0; j < tabuleiro.colunas; j++)
                    {
                        if (matriz[i, j])
                        {
                            Posicao origem = x.posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = executaMovimento(origem, destino);
                            bool testeXeque = estaEmXeque(cor);
                            desfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tabuleiro.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }

        private void colocarPecas()
        {
            colocarNovaPeca('a', 1, new Torre(tabuleiro, Cor.Branca));
            colocarNovaPeca('b', 1, new Cavalo(tabuleiro, Cor.Branca));
            colocarNovaPeca('c', 1, new Bispo(tabuleiro, Cor.Branca));
            colocarNovaPeca('d', 1, new Dama(tabuleiro, Cor.Branca));
            colocarNovaPeca('e', 1, new Rei(tabuleiro, Cor.Branca, this));
            colocarNovaPeca('f', 1, new Bispo(tabuleiro, Cor.Branca));
            colocarNovaPeca('g', 1, new Cavalo(tabuleiro, Cor.Branca));
            colocarNovaPeca('h', 1, new Torre(tabuleiro, Cor.Branca));
            colocarNovaPeca('a', 2, new Peao(tabuleiro, Cor.Branca, this));
            colocarNovaPeca('b', 2, new Peao(tabuleiro, Cor.Branca, this));
            colocarNovaPeca('c', 2, new Peao(tabuleiro, Cor.Branca, this));
            colocarNovaPeca('d', 2, new Peao(tabuleiro, Cor.Branca, this));
            colocarNovaPeca('e', 2, new Peao(tabuleiro, Cor.Branca, this));
            colocarNovaPeca('f', 2, new Peao(tabuleiro, Cor.Branca, this));
            colocarNovaPeca('g', 2, new Peao(tabuleiro, Cor.Branca, this));
            colocarNovaPeca('h', 2, new Peao(tabuleiro, Cor.Branca, this));

            colocarNovaPeca('a', 8, new Torre(tabuleiro, Cor.Preta));
            colocarNovaPeca('b', 8, new Cavalo(tabuleiro, Cor.Preta));
            colocarNovaPeca('c', 8, new Bispo(tabuleiro, Cor.Preta));
            colocarNovaPeca('d', 8, new Dama(tabuleiro, Cor.Preta));
            colocarNovaPeca('e', 8, new Rei(tabuleiro, Cor.Preta, this));
            colocarNovaPeca('f', 8, new Bispo(tabuleiro, Cor.Preta));
            colocarNovaPeca('g', 8, new Cavalo(tabuleiro, Cor.Preta));
            colocarNovaPeca('h', 8, new Torre(tabuleiro, Cor.Preta));
            colocarNovaPeca('a', 7, new Peao(tabuleiro, Cor.Preta, this));
            colocarNovaPeca('b', 7, new Peao(tabuleiro, Cor.Preta, this));
            colocarNovaPeca('c', 7, new Peao(tabuleiro, Cor.Preta, this));
            colocarNovaPeca('d', 7, new Peao(tabuleiro, Cor.Preta, this));
            colocarNovaPeca('e', 7, new Peao(tabuleiro, Cor.Preta, this));
            colocarNovaPeca('f', 7, new Peao(tabuleiro, Cor.Preta, this));
            colocarNovaPeca('g', 7, new Peao(tabuleiro, Cor.Preta, this));
            colocarNovaPeca('h', 7, new Peao(tabuleiro, Cor.Preta, this));
        }
    }
}


