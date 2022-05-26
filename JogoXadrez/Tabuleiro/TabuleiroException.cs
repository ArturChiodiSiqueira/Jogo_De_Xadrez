using System;

namespace Tabuleiro
{
    class TabuleiroException : Exception
    {

        public TabuleiroException(string mensagem) : base(mensagem)
        {
        }
    }
}