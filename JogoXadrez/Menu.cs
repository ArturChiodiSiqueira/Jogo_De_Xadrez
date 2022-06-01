using System;
using System.Collections.Generic;
using Tabuleiro;
using Xadrez;

namespace JogoXadrez
{
    internal class Menu
    {
        public static int menuPrincipal()
        {
            int opcao;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  DENTRE AS OPÇÕES NO MENU, QUAL DESEJA EXECUTAR?\n");
            Console.ForegroundColor = ConsoleColor.White;

            do
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("\t|°°°°°°°° MENU PRINCIPAL °°°°°°°|");
                Console.WriteLine("\t|  opção 0 : sair               |");
                Console.WriteLine("\t|                               |");
                Console.WriteLine("\t|  opção 1 : iniciar novo jogo  |");
                Console.WriteLine("\t|_______________________________|");
                Console.ForegroundColor = ConsoleColor.White;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\n\tinforme a opção desejada no MENU: ");
                opcao = int.Parse(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.White;

                if (opcao < 0 || opcao > 1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\tOPÇÃO INVALIDA! Para voltar ao menu, pressione QUALQUER TECLA!");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.ReadKey();
                    Console.Clear();
                    menuPrincipal();
                }
                else
                {
                    switch (opcao)
                    {
                        case 0:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nvocê escolheu sair.");
                            Console.ForegroundColor = ConsoleColor.White;
                            opcao = 0;
                            break;

                        case 1:
                            Console.Clear();
                            try
                            {
                                PartidaDeXadrez partida = new PartidaDeXadrez();

                                while (!partida.Terminada)
                                {
                                    try
                                    {
                                        Console.Clear();
                                        Tela.imprimirPartida(partida);
                                        Console.Write("\nInfome a Origem: ");

                                        Posicao origem = Tela.lerPosicaoXadrez().toPosicao();
                                        partida.validarPosicaoDeOrigem(origem);

                                        bool[,] posicoesPossiveis = partida.tabuleiro.peca(origem).movimentosPossiveis();

                                        Console.Clear();
                                        Tela.imprimirTabuleiro(partida.tabuleiro, posicoesPossiveis);

                                        Console.Write("\nInforme o Destino: ");
                                        Posicao destino = Tela.lerPosicaoXadrez().toPosicao();
                                        partida.validarPosicaoDeDestino(origem, destino);

                                        partida.realizaJogada(origem, destino);
                                    }
                                    catch (TabuleiroException exception)
                                    {
                                        Console.WriteLine(exception.Message);
                                        Console.ReadLine();
                                    }
                                }
                                Console.Clear();
                                Tela.imprimirPartida(partida);
                            }
                            catch (TabuleiroException exception)
                            {
                                Console.WriteLine(exception.Message);
                            }
                            break;
                    }
                }
                return opcao;
            } while (opcao != 0);
        }
    }
}
