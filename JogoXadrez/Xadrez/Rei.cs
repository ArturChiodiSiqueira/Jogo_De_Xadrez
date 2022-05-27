using Tabuleiro;

namespace Xadrez
{
    class Rei : Peca
    {
        private PartidaDeXadrez Partida;

        public Rei(ClasseTabuleiro tabuleiro, Cor cor, PartidaDeXadrez partida) : base(tabuleiro, cor)
        {
            Partida = partida;
        }

        public override string ToString()
        {
            return "R";
        }

        private bool podeMover(Posicao posicao)
        {
            Peca peca = Tabuleiro.peca(posicao);
            return peca == null || peca.Cor != Cor;
        }

        private bool testeTorreParaRoque(Posicao posicao)
        {
            Peca peca = Tabuleiro.peca(posicao);
            return peca != null && peca is Torre && peca.Cor == Cor && peca.QuantidadeMovimentos == 0;
        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] matriz = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            Posicao posicao = new Posicao(0, 0);

            // acima
            posicao.definirValores(base.Posicao.Linha - 1, base.Posicao.Coluna);
            if (Tabuleiro.posicaoValida(posicao) && podeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }
            // ne
            posicao.definirValores(base.Posicao.Linha - 1, base.Posicao.Coluna + 1);
            if (Tabuleiro.posicaoValida(posicao) && podeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }
            // direita
            posicao.definirValores(base.Posicao.Linha, base.Posicao.Coluna + 1);
            if (Tabuleiro.posicaoValida(posicao) && podeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }
            // se
            posicao.definirValores(base.Posicao.Linha + 1, base.Posicao.Coluna + 1);
            if (Tabuleiro.posicaoValida(posicao) && podeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }
            // abaixo
            posicao.definirValores(base.Posicao.Linha + 1, base.Posicao.Coluna);
            if (Tabuleiro.posicaoValida(posicao) && podeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }
            // so
            posicao.definirValores(base.Posicao.Linha + 1, base.Posicao.Coluna - 1);
            if (Tabuleiro.posicaoValida(posicao) && podeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }
            // esquerda
            posicao.definirValores(base.Posicao.Linha, base.Posicao.Coluna - 1);
            if (Tabuleiro.posicaoValida(posicao) && podeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }
            // no
            posicao.definirValores(base.Posicao.Linha - 1, base.Posicao.Coluna - 1);
            if (Tabuleiro.posicaoValida(posicao) && podeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // #jogadaespecial roque
            if (QuantidadeMovimentos == 0 && !Partida.Xeque)
            {
                // #jogadaespecial roque pequeno (T1)
                Posicao posicaoTorre1 = new Posicao(base.Posicao.Linha, base.Posicao.Coluna + 3);
                if (testeTorreParaRoque(posicaoTorre1))
                {
                    Posicao posicao1 = new Posicao(base.Posicao.Linha, base.Posicao.Coluna + 1);
                    Posicao posicao2 = new Posicao(base.Posicao.Linha, base.Posicao.Coluna + 2);
                    if (Tabuleiro.peca(posicao1) == null && Tabuleiro.peca(posicao2) == null)
                    {
                        matriz[base.Posicao.Linha, base.Posicao.Coluna + 2] = true;
                    }
                }
                // #jogadaespecial roque grande (T2)
                Posicao posicaoTorre2 = new Posicao(base.Posicao.Linha, base.Posicao.Coluna - 4);
                if (testeTorreParaRoque(posicaoTorre2))
                {
                    Posicao posicao1 = new Posicao(base.Posicao.Linha, base.Posicao.Coluna - 1);
                    Posicao posicao2 = new Posicao(base.Posicao.Linha, base.Posicao.Coluna - 2);
                    Posicao posicao3 = new Posicao(base.Posicao.Linha, base.Posicao.Coluna - 3);
                    if (Tabuleiro.peca(posicao1) == null && Tabuleiro.peca(posicao2) == null && Tabuleiro.peca(posicao3) == null)
                    {
                        matriz[base.Posicao.Linha, base.Posicao.Coluna - 2] = true;
                    }
                }
            }
            return matriz;
        }
    }
}