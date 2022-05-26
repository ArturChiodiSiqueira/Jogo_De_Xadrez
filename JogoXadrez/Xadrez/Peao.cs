using Tabuleiro;

namespace Xadrez
{
    class Peao : Peca
    {
        private PartidaDeXadrez Partida;

        public Peao(ClasseTabuleiro tabuleiro, Cor cor, PartidaDeXadrez partida) : base(tabuleiro, cor)
        {
            Partida = partida;
        }

        public override string ToString()
        {
            return "P";
        }

        private bool existeInimigo(Posicao posicao)
        {
            Peca peca = Tabuleiro.peca(posicao);
            return peca != null && peca.Cor != Cor;
        }

        private bool livre(Posicao posicao)
        {
            return Tabuleiro.peca(posicao) == null;
        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] matriz = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            Posicao posicao = new Posicao(0, 0);

            if (Cor == Cor.Branca)
            {
                posicao.definirValores(base.Posicao.Linha - 1, base.Posicao.Coluna);
                if (Tabuleiro.posicaoValida(posicao) && livre(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }
                posicao.definirValores(base.Posicao.Linha - 2, base.Posicao.Coluna);
                Posicao posicao2 = new Posicao(base.Posicao.Linha - 1, base.Posicao.Coluna);
                if (Tabuleiro.posicaoValida(posicao2) && livre(posicao2) && Tabuleiro.posicaoValida(posicao) && livre(posicao) && QuantidadeMovimentos == 0)
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }
                posicao.definirValores(base.Posicao.Linha - 1, base.Posicao.Coluna - 1);
                if (Tabuleiro.posicaoValida(posicao) && existeInimigo(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }
                posicao.definirValores(base.Posicao.Linha - 1, base.Posicao.Coluna + 1);
                if (Tabuleiro.posicaoValida(posicao) && existeInimigo(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                // #jogadaespecial en passant
                if (base.Posicao.Linha == 3)
                {
                    Posicao esquerda = new Posicao(base.Posicao.Linha, base.Posicao.Coluna - 1);
                    if (Tabuleiro.posicaoValida(esquerda) && existeInimigo(esquerda) && Tabuleiro.peca(esquerda) == Partida.VulneravelEnPassant)
                    {
                        matriz[esquerda.Linha - 1, esquerda.Coluna] = true;
                    }
                    Posicao direita = new Posicao(base.Posicao.Linha, base.Posicao.Coluna + 1);
                    if (Tabuleiro.posicaoValida(direita) && existeInimigo(direita) && Tabuleiro.peca(direita) == Partida.VulneravelEnPassant)
                    {
                        matriz[direita.Linha - 1, direita.Coluna] = true;
                    }
                }
            }
            else
            {
                posicao.definirValores(base.Posicao.Linha + 1, base.Posicao.Coluna);
                if (Tabuleiro.posicaoValida(posicao) && livre(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }
                posicao.definirValores(base.Posicao.Linha + 2, base.Posicao.Coluna);
                Posicao posicao2 = new Posicao(base.Posicao.Linha + 1, base.Posicao.Coluna);
                if (Tabuleiro.posicaoValida(posicao2) && livre(posicao2) && Tabuleiro.posicaoValida(posicao) && livre(posicao) && QuantidadeMovimentos == 0)
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }
                posicao.definirValores(base.Posicao.Linha + 1, base.Posicao.Coluna - 1);
                if (Tabuleiro.posicaoValida(posicao) && existeInimigo(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }
                posicao.definirValores(base.Posicao.Linha + 1, base.Posicao.Coluna + 1);
                if (Tabuleiro.posicaoValida(posicao) && existeInimigo(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                // #jogadaespecial en passant
                if (base.Posicao.Linha == 4)
                {
                    Posicao esquerda = new Posicao(base.Posicao.Linha, base.Posicao.Coluna - 1);
                    if (Tabuleiro.posicaoValida(esquerda) && existeInimigo(esquerda) && Tabuleiro.peca(esquerda) == Partida.VulneravelEnPassant)
                    {
                        matriz[esquerda.Linha + 1, esquerda.Coluna] = true;
                    }
                    Posicao direita = new Posicao(base.Posicao.Linha, base.Posicao.Coluna + 1);
                    if (Tabuleiro.posicaoValida(direita) && existeInimigo(direita) && Tabuleiro.peca(direita) == Partida.VulneravelEnPassant)
                    {
                        matriz[direita.Linha + 1, direita.Coluna] = true;
                    }
                }
            }
            return matriz;
        }
    }
}