namespace Tabuleiro
{
    abstract class Peca
    {

        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QuantidadeMovimentos { get; protected set; }
        public ClasseTabuleiro Tabuleiro { get; protected set; }

        public Peca(ClasseTabuleiro tabuleiro, Cor cor)
        {
            Posicao = null;
            Tabuleiro = tabuleiro;
            Cor = cor;
            QuantidadeMovimentos = 0;
        }

        public void incrementarQteMovimentos()
        {
            QuantidadeMovimentos++;
        }

        public void decrementarQteMovimentos()
        {
            QuantidadeMovimentos--;
        }

        public bool existeMovimentosPossiveis()
        {
            bool[,] matriz = movimentosPossiveis();
            for (int i = 0; i < Tabuleiro.Linhas; i++)
            {
                for (int j = 0; j < Tabuleiro.Colunas; j++)
                {
                    if (matriz[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool movimentoPossivel(Posicao posicao)
        {
            return movimentosPossiveis()[posicao.Linha, posicao.Coluna];
        }

        public abstract bool[,] movimentosPossiveis();
    }
}