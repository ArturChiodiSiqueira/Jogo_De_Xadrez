using Tabuleiro;

namespace Xadrez
{

    class Cavalo : Peca
    {

        public Cavalo(ClasseTabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
        {
        }

        public override string ToString()
        {
            return "C";
        }

        private bool podeMover(Posicao possicao)
        {
            Peca peca = tab.peca(possicao);
            return peca == null || peca.cor != cor;
        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] matriz = new bool[tab.linhas, tab.colunas];

            Posicao posicao = new Posicao(0, 0);

            posicao.definirValores(base.posicao.linha - 1, base.posicao.coluna - 2);
            if (tab.posicaoValida(posicao) && podeMover(posicao))
            {
                matriz[posicao.linha, posicao.coluna] = true;
            }
            posicao.definirValores(base.posicao.linha - 2, base.posicao.coluna - 1);
            if (tab.posicaoValida(posicao) && podeMover(posicao))
            {
                matriz[posicao.linha, posicao.coluna] = true;
            }
            posicao.definirValores(base.posicao.linha - 2, base.posicao.coluna + 1);
            if (tab.posicaoValida(posicao) && podeMover(posicao))
            {
                matriz[posicao.linha, posicao.coluna] = true;
            }
            posicao.definirValores(base.posicao.linha - 1, base.posicao.coluna + 2);
            if (tab.posicaoValida(posicao) && podeMover(posicao))
            {
                matriz[posicao.linha, posicao.coluna] = true;
            }
            posicao.definirValores(base.posicao.linha + 1, base.posicao.coluna + 2);
            if (tab.posicaoValida(posicao) && podeMover(posicao))
            {
                matriz[posicao.linha, posicao.coluna] = true;
            }
            posicao.definirValores(base.posicao.linha + 2, base.posicao.coluna + 1);
            if (tab.posicaoValida(posicao) && podeMover(posicao))
            {
                matriz[posicao.linha, posicao.coluna] = true;
            }
            posicao.definirValores(base.posicao.linha + 2, base.posicao.coluna - 1);
            if (tab.posicaoValida(posicao) && podeMover(posicao))
            {
                matriz[posicao.linha, posicao.coluna] = true;
            }
            posicao.definirValores(base.posicao.linha + 1, base.posicao.coluna - 2);
            if (tab.posicaoValida(posicao) && podeMover(posicao))
            {
                matriz[posicao.linha, posicao.coluna] = true;
            }

            return matriz;
        }
    }
}

