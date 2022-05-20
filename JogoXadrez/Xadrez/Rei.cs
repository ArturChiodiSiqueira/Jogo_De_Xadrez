using Tabuleiro;

namespace Xadrez
{
    class Rei : Peca
    {

        private PartidaDeXadrez partida;

        public Rei(ClasseTabuleiro tabuleiro, Cor cor, PartidaDeXadrez partida) : base(tabuleiro, cor)
        {
            this.partida = partida;
        }

        public override string ToString()
        {
            return "R";
        }

        private bool podeMover(Posicao posicao)
        {
            Peca peca = tab.peca(posicao);
            return peca == null || peca.cor != cor;
        }

        private bool testeTorreParaRoque(Posicao posicao)
        {
            Peca peca = tab.peca(posicao);
            return peca != null && peca is Torre && peca.cor == cor && peca.qteMovimentos == 0;
        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] matriz = new bool[tab.linhas, tab.colunas];

            Posicao posicao = new Posicao(0, 0);

            // acima
            posicao.definirValores(base.posicao.linha - 1, base.posicao.coluna);
            if (tab.posicaoValida(posicao) && podeMover(posicao))
            {
                matriz[posicao.linha, posicao.coluna] = true;
            }
            // ne
            posicao.definirValores(base.posicao.linha - 1, base.posicao.coluna + 1);
            if (tab.posicaoValida(posicao) && podeMover(posicao))
            {
                matriz[posicao.linha, posicao.coluna] = true;
            }
            // direita
            posicao.definirValores(base.posicao.linha, base.posicao.coluna + 1);
            if (tab.posicaoValida(posicao) && podeMover(posicao))
            {
                matriz[posicao.linha, posicao.coluna] = true;
            }
            // se
            posicao.definirValores(base.posicao.linha + 1, base.posicao.coluna + 1);
            if (tab.posicaoValida(posicao) && podeMover(posicao))
            {
                matriz[posicao.linha, posicao.coluna] = true;
            }
            // abaixo
            posicao.definirValores(base.posicao.linha + 1, base.posicao.coluna);
            if (tab.posicaoValida(posicao) && podeMover(posicao))
            {
                matriz[posicao.linha, posicao.coluna] = true;
            }
            // so
            posicao.definirValores(base.posicao.linha + 1, base.posicao.coluna - 1);
            if (tab.posicaoValida(posicao) && podeMover(posicao))
            {
                matriz[posicao.linha, posicao.coluna] = true;
            }
            // esquerda
            posicao.definirValores(base.posicao.linha, base.posicao.coluna - 1);
            if (tab.posicaoValida(posicao) && podeMover(posicao))
            {
                matriz[posicao.linha, posicao.coluna] = true;
            }
            // no
            posicao.definirValores(base.posicao.linha - 1, base.posicao.coluna - 1);
            if (tab.posicaoValida(posicao) && podeMover(posicao))
            {
                matriz[posicao.linha, posicao.coluna] = true;
            }

            // #jogadaespecial roque
            if (qteMovimentos == 0 && !partida.xeque)
            {
                // #jogadaespecial roque pequeno
                Posicao posT1 = new Posicao(base.posicao.linha, base.posicao.coluna + 3);
                if (testeTorreParaRoque(posT1))
                {
                    Posicao posicao1 = new Posicao(base.posicao.linha, base.posicao.coluna + 1);
                    Posicao posicao2 = new Posicao(base.posicao.linha, base.posicao.coluna + 2);
                    if (tab.peca(posicao1) == null && tab.peca(posicao2) == null)
                    {
                        matriz[base.posicao.linha, base.posicao.coluna + 2] = true;
                    }
                }
                // #jogadaespecial roque grande
                Posicao posT2 = new Posicao(base.posicao.linha, base.posicao.coluna - 4);
                if (testeTorreParaRoque(posT2))
                {
                    Posicao posicao1 = new Posicao(base.posicao.linha, base.posicao.coluna - 1);
                    Posicao posicao2 = new Posicao(base.posicao.linha, base.posicao.coluna - 2);
                    Posicao posicao3 = new Posicao(base.posicao.linha, base.posicao.coluna - 3);
                    if (tab.peca(posicao1) == null && tab.peca(posicao2) == null && tab.peca(posicao3) == null)
                    {
                        matriz[base.posicao.linha, base.posicao.coluna - 2] = true;
                    }
                }
            }


            return matriz;
        }
    }
}

