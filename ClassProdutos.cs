using System;
using System.Globalization;

namespace TrabalhoXML
{
    public class ClassProdutos
    {
        public string nome { get; set; }
        public string codigo { get; set; }
        public string custo { get; set; }
        public double venda { get; set; }





        public override string ToString()
        {
            
            int contaproduto = 1;
            return
            "              <Nome>" + nome + "</Nome>" +"\n"+
            "              <Custo>" + custo +".00"+ "</Custo>" + "\n" +
            "              <Venda>" + venda.ToString("F2", CultureInfo.InvariantCulture) + "</Venda>" + "\n" +
           "          </Prod"+contaproduto+">" +"\n"+
           "       </Produtos>" + "\n"+
           "</XML>\n";
            contaproduto++;
        }

    }

}