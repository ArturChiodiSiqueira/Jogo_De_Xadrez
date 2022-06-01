using System;
using System.Collections.Generic;
using Tabuleiro;
using Xadrez;

namespace JogoXadrez
{
    class Tela
    {
        public static void imprimirPartida(PartidaDeXadrez partida)
        {
            imprimirTabuleiro(partida.tabuleiro);
            Console.WriteLine();
            imprimirPecasCapturadas(partida);

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\nTurno: " + partida.Turno);
            Console.ForegroundColor = ConsoleColor.White;
            if (!partida.Terminada)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Aguardando jogada: " + partida.JogadorAtual);
                Console.ForegroundColor = ConsoleColor.White;
                if (partida.Xeque)
                {
                    Console.WriteLine("XEQUE!");
                }
            }
            else
            {
                Console.WriteLine("XEQUEMATE!");
                Console.WriteLine("Vencedor: " + partida.JogadorAtual);
            }
        }

        public static void imprimirPecasCapturadas(PartidaDeXadrez partida)
        {
            Console.WriteLine("Peças capturadas:");
            Console.Write("Brancas: ");
            imprimirConjunto(partida.pecasCapturadas(Cor.Branca));

            Console.WriteLine();

            ConsoleColor auxiliar = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Pretas: ");
            imprimirConjunto(partida.pecasCapturadas(Cor.Preta));
            Console.ForegroundColor = auxiliar;
            Console.WriteLine();
        }

        public static void imprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("[");
            foreach (Peca peca in conjunto)
            {
                Console.Write(peca + " ");
            }
            Console.Write("]");
        }

        public static void imprimirTabuleiro(ClasseTabuleiro tabuleiro)
        {

            for (int i = 0; i < tabuleiro.Linhas; i++)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write(8 - i + " ");
                Console.ForegroundColor = ConsoleColor.White;
                for (int j = 0; j < tabuleiro.Colunas; j++)
                {
                    imprimirPeca(tabuleiro.peca(i, j));
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("  a b c d e f g h");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void imprimirTabuleiro(ClasseTabuleiro tabuleiro, bool[,] posicoePossiveis)
        {

            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;

            for (int i = 0; i < tabuleiro.Linhas; i++)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write(8 - i + " ");
                Console.ForegroundColor = ConsoleColor.White;
                for (int j = 0; j < tabuleiro.Colunas; j++)
                {
                    if (posicoePossiveis[i, j])
                    {
                        Console.BackgroundColor = fundoAlterado;
                    }
                    else
                    {
                        Console.BackgroundColor = fundoOriginal;
                    }
                    imprimirPeca(tabuleiro.peca(i, j));
                    Console.BackgroundColor = fundoOriginal;
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("  a b c d e f g h");
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = fundoOriginal;
        }

        public static PosicaoXadrez lerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");
            return new PosicaoXadrez(coluna, linha);
        }

        public static void imprimirPeca(Peca peca)
        {

            if (peca == null)
            {
                Console.Write("* ");
            }
            else
            {
                if (peca.Cor == Cor.Branca)
                {
                    Console.Write(peca);
                }
                else
                {
                    ConsoleColor auxiliar = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(peca);
                    Console.ForegroundColor = auxiliar;
                }
                Console.Write(" ");
            }
        }
    }
}