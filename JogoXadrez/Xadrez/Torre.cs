using Tabuleiro;

namespace Xadrez
{
    class Torre : Peca
    {

        public Torre(ClasseTabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
        {
        }

        public override string ToString()
        {
            return "T";
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

            // acima
            posicao.definirValores(base.posicao.linha - 1, base.posicao.coluna);
            while (tab.posicaoValida(posicao) && podeMover(posicao))
            {
                matriz[posicao.linha, posicao.coluna] = true;
                if (tab.peca(posicao) != null && tab.peca(posicao).cor != cor)
                {
                    break;
                }
                posicao.linha = posicao.linha - 1;
            }

            // abaixo
            posicao.definirValores(base.posicao.linha + 1, base.posicao.coluna);
            while (tab.posicaoValida(posicao) && podeMover(posicao))
            {
                matriz[posicao.linha, posicao.coluna] = true;
                if (tab.peca(posicao) != null && tab.peca(posicao).cor != cor)
                {
                    break;
                }
                posicao.linha = posicao.linha + 1;
            }

            // direita
            posicao.definirValores(base.posicao.linha, base.posicao.coluna + 1);
            while (tab.posicaoValida(posicao) && podeMover(posicao))
            {
                matriz[posicao.linha, posicao.coluna] = true;
                if (tab.peca(posicao) != null && tab.peca(posicao).cor != cor)
                {
                    break;
                }
                posicao.coluna = posicao.coluna + 1;
            }

            // esquerda
            posicao.definirValores(base.posicao.linha, base.posicao.coluna - 1);
            while (tab.posicaoValida(posicao) && podeMover(posicao))
            {
                matriz[posicao.linha, posicao.coluna] = true;
                if (tab.peca(posicao) != null && tab.peca(posicao).cor != cor)
                {
                    break;
                }
                posicao.coluna = posicao.coluna - 1;
            }

            return matriz;
        }
    }
}

