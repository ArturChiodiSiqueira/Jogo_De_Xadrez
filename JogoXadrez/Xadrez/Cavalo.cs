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
            Peca peca = Tabuleiro.peca(possicao);
            return peca == null || peca.Cor != Cor;
        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] matriz = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            Posicao posicao = new Posicao(0, 0);

            posicao.definirValores(base.Posicao.Linha - 1, base.Posicao.Coluna - 2);
            if (Tabuleiro.posicaoValida(posicao) && podeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }
            posicao.definirValores(base.Posicao.Linha - 2, base.Posicao.Coluna - 1);
            if (Tabuleiro.posicaoValida(posicao) && podeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }
            posicao.definirValores(base.Posicao.Linha - 2, base.Posicao.Coluna + 1);
            if (Tabuleiro.posicaoValida(posicao) && podeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }
            posicao.definirValores(base.Posicao.Linha - 1, base.Posicao.Coluna + 2);
            if (Tabuleiro.posicaoValida(posicao) && podeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }
            posicao.definirValores(base.Posicao.Linha + 1, base.Posicao.Coluna + 2);
            if (Tabuleiro.posicaoValida(posicao) && podeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }
            posicao.definirValores(base.Posicao.Linha + 2, base.Posicao.Coluna + 1);
            if (Tabuleiro.posicaoValida(posicao) && podeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }
            posicao.definirValores(base.Posicao.Linha + 2, base.Posicao.Coluna - 1);
            if (Tabuleiro.posicaoValida(posicao) && podeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }
            posicao.definirValores(base.Posicao.Linha + 1, base.Posicao.Coluna - 2);
            if (Tabuleiro.posicaoValida(posicao) && podeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            return matriz;
        }
    }
}