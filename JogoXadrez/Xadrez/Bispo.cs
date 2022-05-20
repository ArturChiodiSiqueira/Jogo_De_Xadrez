﻿using Tabuleiro;

namespace Xadrez
{

    class Bispo : Peca
    {

        public Bispo(ClasseTabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
        {
        }

        public override string ToString()
        {
            return "B";
        }

        private bool podeMover(Posicao posicao)
        {
            Peca peca = tab.peca(posicao);
            return peca == null || peca.cor != cor;
        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] matriz = new bool[tab.linhas, tab.colunas];

            Posicao posicao = new Posicao(0, 0);

            // NO
            posicao.definirValores(base.posicao.linha - 1, base.posicao.coluna - 1);
            while (tab.posicaoValida(posicao) && podeMover(posicao))
            {
                matriz[posicao.linha, posicao.coluna] = true;
                if (tab.peca(posicao) != null && tab.peca(posicao).cor != cor)
                {
                    break;
                }
                posicao.definirValores(posicao.linha - 1, posicao.coluna - 1);
            }

            // NE
            posicao.definirValores(base.posicao.linha - 1, base.posicao.coluna + 1);
            while (tab.posicaoValida(posicao) && podeMover(posicao))
            {
                matriz[posicao.linha, posicao.coluna] = true;
                if (tab.peca(posicao) != null && tab.peca(posicao).cor != cor)
                {
                    break;
                }
                posicao.definirValores(posicao.linha - 1, posicao.coluna + 1);
            }

            // SE
            posicao.definirValores(base.posicao.linha + 1, base.posicao.coluna + 1);
            while (tab.posicaoValida(posicao) && podeMover(posicao))
            {
                matriz[posicao.linha, posicao.coluna] = true;
                if (tab.peca(posicao) != null && tab.peca(posicao).cor != cor)
                {
                    break;
                }
                posicao.definirValores(posicao.linha + 1, posicao.coluna + 1);
            }

            // SO
            posicao.definirValores(base.posicao.linha + 1, base.posicao.coluna - 1);
            while (tab.posicaoValida(posicao) && podeMover(posicao))
            {
                matriz[posicao.linha, posicao.coluna] = true;
                if (tab.peca(posicao) != null && tab.peca(posicao).cor != cor)
                {
                    break;
                }
                posicao.definirValores(posicao.linha + 1, posicao.coluna - 1);
            }

            return matriz;
        }
    }
}
