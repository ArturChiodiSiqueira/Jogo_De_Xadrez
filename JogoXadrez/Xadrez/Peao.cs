using Tabuleiro;

namespace Xadrez
{

    class Peao : Peca
    {

        private PartidaDeXadrez partida;

        public Peao(ClasseTabuleiro tabuleiro, Cor cor, PartidaDeXadrez partida) : base(tabuleiro, cor)
        {
            this.partida = partida;
        }

        public override string ToString()
        {
            return "P";
        }

        private bool existeInimigo(Posicao posicao)
        {
            Peca peca = tab.peca(posicao);
            return peca != null && peca.cor != cor;
        }

        private bool livre(Posicao posicao)
        {
            return tab.peca(posicao) == null;
        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] matriz = new bool[tab.linhas, tab.colunas];

            Posicao posicao = new Posicao(0, 0);

            if (cor == Cor.Branca)
            {
                posicao.definirValores(base.posicao.linha - 1, base.posicao.coluna);
                if (tab.posicaoValida(posicao) && livre(posicao))
                {
                    matriz[posicao.linha, posicao.coluna] = true;
                }
                posicao.definirValores(base.posicao.linha - 2, base.posicao.coluna);
                Posicao posicao2 = new Posicao(base.posicao.linha - 1, base.posicao.coluna);
                if (tab.posicaoValida(posicao2) && livre(posicao2) && tab.posicaoValida(posicao) && livre(posicao) && qteMovimentos == 0)
                {
                    matriz[posicao.linha, posicao.coluna] = true;
                }
                posicao.definirValores(base.posicao.linha - 1, base.posicao.coluna - 1);
                if (tab.posicaoValida(posicao) && existeInimigo(posicao))
                {
                    matriz[posicao.linha, posicao.coluna] = true;
                }
                posicao.definirValores(base.posicao.linha - 1, base.posicao.coluna + 1);
                if (tab.posicaoValida(posicao) && existeInimigo(posicao))
                {
                    matriz[posicao.linha, posicao.coluna] = true;
                }

                // #jogadaespecial en passant
                if (base.posicao.linha == 3)
                {
                    Posicao esquerda = new Posicao(base.posicao.linha, base.posicao.coluna - 1);
                    if (tab.posicaoValida(esquerda) && existeInimigo(esquerda) && tab.peca(esquerda) == partida.vulneravelEnPassant)
                    {
                        matriz[esquerda.linha - 1, esquerda.coluna] = true;
                    }
                    Posicao direita = new Posicao(base.posicao.linha, base.posicao.coluna + 1);
                    if (tab.posicaoValida(direita) && existeInimigo(direita) && tab.peca(direita) == partida.vulneravelEnPassant)
                    {
                        matriz[direita.linha - 1, direita.coluna] = true;
                    }
                }
            }
            else
            {
                posicao.definirValores(base.posicao.linha + 1, base.posicao.coluna);
                if (tab.posicaoValida(posicao) && livre(posicao))
                {
                    matriz[posicao.linha, posicao.coluna] = true;
                }
                posicao.definirValores(base.posicao.linha + 2, base.posicao.coluna);
                Posicao posicao2 = new Posicao(base.posicao.linha + 1, base.posicao.coluna);
                if (tab.posicaoValida(posicao2) && livre(posicao2) && tab.posicaoValida(posicao) && livre(posicao) && qteMovimentos == 0)
                {
                    matriz[posicao.linha, posicao.coluna] = true;
                }
                posicao.definirValores(base.posicao.linha + 1, base.posicao.coluna - 1);
                if (tab.posicaoValida(posicao) && existeInimigo(posicao))
                {
                    matriz[posicao.linha, posicao.coluna] = true;
                }
                posicao.definirValores(base.posicao.linha + 1, base.posicao.coluna + 1);
                if (tab.posicaoValida(posicao) && existeInimigo(posicao))
                {
                    matriz[posicao.linha, posicao.coluna] = true;
                }

                // #jogadaespecial en passant
                if (base.posicao.linha == 4)
                {
                    Posicao esquerda = new Posicao(base.posicao.linha, base.posicao.coluna - 1);
                    if (tab.posicaoValida(esquerda) && existeInimigo(esquerda) && tab.peca(esquerda) == partida.vulneravelEnPassant)
                    {
                        matriz[esquerda.linha + 1, esquerda.coluna] = true;
                    }
                    Posicao direita = new Posicao(base.posicao.linha, base.posicao.coluna + 1);
                    if (tab.posicaoValida(direita) && existeInimigo(direita) && tab.peca(direita) == partida.vulneravelEnPassant)
                    {
                        matriz[direita.linha + 1, direita.coluna] = true;
                    }
                }
            }

            return matriz;
        }
    }
}

