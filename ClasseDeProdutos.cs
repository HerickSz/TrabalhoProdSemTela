using System;
using System.Globalization;
using System.Collections;

namespace TrabalhoXML
{
    public class ListaProdutos
    {
        public string codigo { get; set; }
        public ArrayList ListaProduto { get; set; }

        public ListaProdutos()
        {
            ListaProduto = new ArrayList();
        }
    }
}