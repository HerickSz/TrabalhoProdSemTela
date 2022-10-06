using System;
using System.Collections;
using System.Globalization;
using System.IO;

namespace TrabalhoXML
{
    class Program
    {
        //endereço do xml. Caso der erro: Remova todo o Conteúdo do xml blz  ( passei raiva com isso ) :)


        //Grupo: Herick, Ana Karolina, Geisa, Tiago, Maria Luiza

        // valdecio, tinha feito esse antes, queria q desse uma olhada, sei q ta bastante diferente mas deu trabalho pra fazer
        // e tinha q te enviar pra vc dar uma olhada.

        string Diretorio = Directory.GetCurrentDirectory();

        static string ficheiro = @"C:\Users\heric\source\repos\TrabalhoXML\TabelaProduto.XML";
        
        
        
        static StreamWriter file;
        static ArrayList ListaProduto = new ArrayList();
       
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Black; 
            CarregarArquivoProdutoXML(); 
            Escolher(); 
        }
       
        static void CarregarArquivoProdutoXML() 
        {
            //carregar diretório
            if (File.Exists(ficheiro))  
            {
                string[] linhas = File.ReadAllLines(ficheiro);
                foreach (string linha in linhas) 
                {
                    string[] partes = linha.Split('>');
                    ListaProdutos product = new ListaProdutos(); 
                    product.codigo = partes[0]; 
                    int c = 0;
                    for (int i = 0; i < 1; i++) 
                    {
                        ClassProdutos produto = new ClassProdutos(); 
                        produto.nome = partes[i + 1 + c]; 
                        produto.custo = partes[i + 2 + c]; 
                        produto.venda = double.Parse(partes[i + 2 + c]);
                        product.ListaProduto.Add(produto); 
                        c++; 
                    }
                    ListaProduto.Add(product);
                }
            }
        }
        //--------------------------------------------
        //  Menu
        //--------------------------------------------
        static void Escolher() 
        {
            char opcao = ' '; 
            do 
            {
                Console.Clear(); 
                Console.ForegroundColor = ConsoleColor.Red; 
                Console.WriteLine("|---------------------------------------------------------------------------------|");
                Console.WriteLine("|                                                                                 |");
                Console.WriteLine("|                                   MENU PRODUTOS                                 |");
                Console.WriteLine("|                                                                                 |");
                Console.WriteLine("|---------------------------------------------------------------------------------|");
                Console.ForegroundColor = ConsoleColor.White; 
                Console.WriteLine("|1- Adicionar Produtos                                                            |");
                Console.WriteLine("|---------------------------------------------------------------------------------|");
                Console.WriteLine("|2- Alterar ou Remover Produtos                                                   |");
                Console.WriteLine("|---------------------------------------------------------------------------------|");
                Console.WriteLine("|3- Consultar Produtos                                                            |");
                Console.WriteLine("|---------------------------------------------------------------------------------|");
                Console.WriteLine("|4- Listar Produtos                                                               |");
                Console.WriteLine("|---------------------------------------------------------------------------------|");
                Console.WriteLine("|0- Sair                                                                          |");
                Console.WriteLine("|---------------------------------------------------------------------------------|");

                Console.Write("|Insira a Opção:");
                opcao = char.Parse(Console.ReadLine()); 

                switch (opcao) 
                {
                    case '1': 
                        Inserir(); 
                        break; 
                    case '2': 
                        AlterarRemover(); 
                        break;
                    case '3':
                        Consultar();
                        break;
                    case '4':
                        Listar();
                        break;
                    default: 
                        if (opcao != '0') 
                        {
                            Console.WriteLine("Opção Inválida!!");
                        }
                        break; 
                }
            } while (opcao != '0'); 
        }
        //---------------------------------------
        //  Adcionar
        //---------------------------------------
        static void Inserir() 
        {
            do  
            {
                Console.Clear();
                bool encontrado = false; 

                Console.WriteLine("|---------------------------------------------------------------------------------|");
                Console.WriteLine("|                                                                                 |");
                Console.WriteLine("|                                CADASTRO DE PRODUTO                              |");
                Console.WriteLine("|                                                                                 |");
                Console.WriteLine("|---------------------------------------------------------------------------------|");

                ListaProdutos Product1 = new ListaProdutos(); 
                Console.WriteLine("|                                                                                 |");
                Console.Write("| Código: \t\t");
                Product1.codigo = Console.ReadLine(); 

                foreach (ListaProdutos produto in ListaProduto) 
                {
                    if (produto.codigo.ToLower().Contains(Product1.codigo.ToLower())) 
                    {
                        encontrado = true; 
                        Console.Clear(); 

                        Console.WriteLine("|---------------------------------------------------------------------------------|");
                        Console.WriteLine("|                                                                                 |");
                        Console.WriteLine("|                                CADASTRO DE PRODUTO                              |");
                        Console.WriteLine("|                                                                                 |");
                        Console.WriteLine("|---------------------------------------------------------------------------------|");
                        Console.WriteLine("|\n| O Produto {0} já foi adicionado.", produto.codigo);
                        Console.WriteLine("|\n|\n| Escolha a opção:");
                        Console.WriteLine("|\n| 1- Adicionar outro Produto\n| 2- Menu incial");
                        Console.Write("|\n|Insira a Opção:");
                        string opcao = Console.ReadLine(); 
                        if (opcao == "1") 
                        {
                            Inserir(); 
                        }
                        else if (opcao == "2") 
                        {
                            Escolher();
                        }
                        else 
                        {
                            Console.WriteLine("Opção inválida!"); 
                        }
                    }
                }
                if (!encontrado) 
                {
                    int contaproduto = 1;
                    string linha = "<XML>"+"\n"+
                          "      <Produtos>"+"\n"+
                        "         <Prod"+ contaproduto+">"+"\n" +
                    "             <Codigo>" + Product1.codigo + "</Codigo>" + "\n"; 
                    contaproduto++;
                    for (int i = 0; i < 1; i++) 
                    {
                        ClassProdutos produto = new ClassProdutos();
                        Console.Write("|\n| Nome do Produto: \t");
                        produto.nome = Console.ReadLine(); 
                        Console.Write("|\n| Valor do Produto: \t R$");
                        produto.custo = Console.ReadLine(); 
                        Console.Write("|\n| Insira a venda do Produto: \t R$");
                        produto.venda = double.Parse(Console.ReadLine());

                        //aonde vai pro txt

                        linha += produto.ToString();
                        Product1.ListaProduto.Add(produto); 
                    }
                    ListaProduto.Add(Product1); 
                    //SALVAR
                    if (File.Exists(ficheiro)) 
                    {
                        file = File.AppendText(ficheiro); 
                    }
                    else 
                    {
                        file = File.CreateText(ficheiro); 
                    }
                    file.WriteLine(linha); 
                    file.Close(); 
                    Console.Write("|\n| Deseja continuar inserindo Produtos? s p/ sim: \t"); 
                }
            } while (Console.ReadLine().ToLower() == "s"); 
            Console.Clear(); 
            Escolher();


    }
        //------------------------------------------
        //  ALTERAR OU REMOVER 
        //------------------------------------------
        static void AlterarRemover() 
        {
            Console.Clear(); 

            Console.WriteLine("|-------------------------------------------------------------------------------------|");
            Console.WriteLine("|                                                                                     |");
            Console.WriteLine("|                                ESCOLHER PRODUTO                                     |");
            Console.WriteLine("|                                                                                     |");
            Console.WriteLine("|-------------------------------------------------------------------------------------|");
            Console.WriteLine("|                                                                                     |");
            Console.WriteLine("| Escolha qual opção deseja:                                                          |");
            Console.WriteLine("|                                                                                     |");
            Console.WriteLine("| 1- Alterar                                                                          |");
            Console.WriteLine("| 2- Remover                                                                          |");
            Console.WriteLine("| 3- Ir ao Menu inicial                                                               |");
            Console.WriteLine("|                                                                                     |");
            Console.Write("| Insira a Opção:");

            string opcao = Console.ReadLine(); 
            if (opcao == "1") 
            {
                Console.Clear(); 

                Console.WriteLine("|---------------------------------------------------------------------------------|");
                Console.WriteLine("|                                                                                 |");
                Console.WriteLine("|                                ALTERAR PRODUTO                                  |");
                Console.WriteLine("|                                                                                 |");
                Console.WriteLine("|---------------------------------------------------------------------------------|");

                Console.Write("| Insira o código do Produto que quer alterar: "); 
                string nome = Console.ReadLine(); 
                bool encontrado = false; 
                int count = 0; 
                int countAlterar = 0;
                string linha; 
                ListaProdutos produtoOne = new ListaProdutos(); 
                foreach (ListaProdutos produto in ListaProduto) 
                {
                    if (produto.codigo.ToLower().Contains(nome.ToLower())) 
                    {                                                                                            
                        countAlterar = count; 
                        encontrado = true; 

                        Console.Clear(); 

                        Console.WriteLine("|---------------------------------------------------------------------------------|");
                        Console.WriteLine("|                                                                                 |");
                        Console.WriteLine("|                                ALTERAR PRODUTO                                  |");
                        Console.WriteLine("|                                                                                 |");
                        Console.WriteLine("|---------------------------------------------------------------------------------|");
                        Console.WriteLine("|\n| O Produto {0} foi encontrado.", produto.codigo); 
                        Console.WriteLine("|\n| Deseja alterar-lo? s p/ sim"); 
                        if (Console.ReadLine().ToLower() == "s") 
                        {
                            Console.Clear(); 

                            Console.WriteLine("|---------------------------------------------------------------------------------|");
                            Console.WriteLine("|                                                                                 |");
                            Console.WriteLine("|                                ALTERAR PRODUTO                                  |");
                            Console.WriteLine("|                                                                                 |");
                            Console.WriteLine("|---------------------------------------------------------------------------------|");

                            Console.Write("| Código: \t\t"); 
                            produtoOne.codigo = Console.ReadLine(); 
                            linha = produtoOne.codigo + "|";
                            for (int i = 0; i < 1; i++) 
                            {
                                ClassProdutos produtos = new ClassProdutos(); 
                                Console.Write("|\n| Nome do Produto: \t");
                                produtos.nome = Console.ReadLine(); 
                                Console.Write("|\n| Valor do Produto: \t");
                                produtos.custo = Console.ReadLine();  
                                Console.Write("|\n| Insira a venda do Produto: \t");
                                produtos.venda = double.Parse(Console.ReadLine());
                                linha += produtos.nome + "|" + produtos.custo + "|" + produtos.venda + "|"; 
                                                                                                         
                                produtoOne.ListaProduto.Add(produtos); 
                            }
                        }

                        else 
                        {
                            AlterarRemover(); 
                        }
                    }
                    count++; 
                }
                if (!encontrado) 
                {
                    Console.Clear(); 

                    Console.WriteLine("|---------------------------------------------------------------------------------|");
                    Console.WriteLine("|                                                                                 |");
                    Console.WriteLine("|                                ALTERAR PRODUTO                                  |");
                    Console.WriteLine("|                                                                                 |");
                    Console.WriteLine("|---------------------------------------------------------------------------------|");

                    Console.WriteLine("|\n|\n| Produto {0} não existe!", nome); 
                    Console.WriteLine("|\n|\n| Escolha a opção:");
                    Console.WriteLine("|\n| 1-Voltar a Alterar ou Remover \n| 2-Ir ao Menu incial"); 
                    Console.Write("|\n| Insira a Opção:");
                    opcao = Console.ReadLine(); 
                    if (opcao == "1") 
                    {
                        AlterarRemover(); 
                    }
                    else if (opcao == "2") 
                    {
                        Escolher(); 
                    }
                }
                else 
                {
                    ListaProduto[countAlterar] = produtoOne; 
                }
                if (File.Exists(ficheiro))
                {
                    file = new StreamWriter(ficheiro); 
                    foreach (ListaProdutos produto in ListaProduto) 
                    {
                        linha = "|Código do Produto: " + produto.codigo + "\n";
                        foreach (ClassProdutos produtos in produto.ListaProduto) 
                        {
                            linha += produtos.ToString(); 
                        }
                        file.WriteLine(linha); 
                    }
                    file.Close(); 
                }
                else 
                {
                    file = File.CreateText(ficheiro); 
                }
                Console.Clear(); 

                Console.WriteLine("|---------------------------------------------------------------------------------|");
                Console.WriteLine("|                                                                                 |");
                Console.WriteLine("|                                ALTERAR PRODUTO                                  |");
                Console.WriteLine("|                                                                                 |");
                Console.WriteLine("|---------------------------------------------------------------------------------|");
                Console.WriteLine("|\n| Produto alterado com sucesso!"); 
                Console.WriteLine("|\n|\n| Escolha a opção:");
                Console.WriteLine("|\n| 1-Voltar a Alterar ou Remover \n| 2-Ir ao Menu incial");
                Console.Write("|\n| Insira a Opção:");
                opcao = Console.ReadLine(); 
                if (opcao == "1") 
                {
                    AlterarRemover(); 
                }
                else if (opcao == "2") 
                {
                    Escolher(); 
                }
            }
            else if (opcao == "2") 
            {
                Console.Clear(); 

                Console.WriteLine("|---------------------------------------------------------------------------------|");
                Console.WriteLine("|                                                                                 |");
                Console.WriteLine("|                                REMOVER PRODUTO                                  |");
                Console.WriteLine("|                                                                                 |");
                Console.WriteLine("|---------------------------------------------------------------------------------|");

                Console.WriteLine("| Insira o código do Produto que quer deletar:                                     "); 
                string nome = Console.ReadLine(); 
                bool encontrado = false; 
                string linha; 
                foreach (ListaProdutos produto in ListaProduto) 
                {
                    if (produto.codigo.ToLower().Contains(nome.ToLower()))  
                                                                      
                    {
                        encontrado = true; 

                        Console.Clear(); 

                        Console.WriteLine("|---------------------------------------------------------------------------------|");
                        Console.WriteLine("|                                                                                 |");
                        Console.WriteLine("|                                REMOVER PRODUTO                                  |");
                        Console.WriteLine("|                                                                                 |");
                        Console.WriteLine("|---------------------------------------------------------------------------------|");

                        Console.WriteLine("| O Produto {0} existe e pode ser removido.", produto.codigo); 
                        Console.WriteLine("|\n| Deseja remover o Produto {0}? s p/ sim", produto.codigo);
                        if (Console.ReadLine().ToLower() == "s") 
                        {

                            ListaProduto.Remove(produto); 
                            break; 
                        }
                    }

                }
                if (!encontrado) 
                {
                    Console.Clear(); 

                    Console.WriteLine("|---------------------------------------------------------------------------------|");
                    Console.WriteLine("|                                                                                 |");
                    Console.WriteLine("|                                REMOVER PRODUTO                                  |");
                    Console.WriteLine("|                                                                                 |");
                    Console.WriteLine("|---------------------------------------------------------------------------------|");

                    Console.WriteLine("|\n|\n| Item {0} não existe!", nome); 
                    Console.WriteLine("|\n|\n| Escolha a opção:");
                    Console.WriteLine("|\n| 1-Voltar a Alterar ou Remover \n| 2-Ir ao Menu incial"); 
                    Console.Write("|\n| Insira a Opção:");
                    opcao = Console.ReadLine();
                    if (opcao == "1") 
                    {
                        AlterarRemover();
                    }
                    else if (opcao == "2") 
                    {
                        Escolher(); 
                    }
                }
                if (File.Exists(ficheiro)) 
                {
                    file = new StreamWriter(ficheiro);
                    foreach (ListaProdutos produto in ListaProduto) 
                    {
                        linha = "|Código do Produto: " + produto.codigo + "\n";
                        foreach (ClassProdutos produtos in produto.ListaProduto) 
                        {

                            linha += produtos.ToString();
                        }
                        file.WriteLine(linha); 
                    } 
                    file.Close();
                }
                else 
                {
                    file = File.CreateText(ficheiro); 
                }
                Console.Clear();

                Console.WriteLine("|---------------------------------------------------------------------------------|");
                Console.WriteLine("|                                                                                 |");
                Console.WriteLine("|                                REMOVER PRODUTO                                  |");
                Console.WriteLine("|                                                                                 |");
                Console.WriteLine("|---------------------------------------------------------------------------------|");

                Console.WriteLine("|\n|\n| Produto removido com sucesso!"); 
                Console.WriteLine("|\n|\n| Escolha a opção:");
                Console.WriteLine("|\n| 1-Voltar a Alterar ou Remover \n| 2-Ir ao Menu incial"); 
                Console.Write("|\n| Insira a Opção:");
                opcao = Console.ReadLine(); 
                if (opcao == "1") 
                {
                    AlterarRemover(); 
                }
                else if (opcao == "2") 
                {
                    Escolher(); 
                }
            }
            else if (opcao == "3") 
            {
                Escolher();  
            }

        }
        //---------------------------------------------
        //   CONSULTAR 
        //---------------------------------------------
        static void Consultar() 
        {
            Console.Clear(); 

            Console.WriteLine("|---------------------------------------------------------------------------------|");
            Console.WriteLine("|                                                                                 |");
            Console.WriteLine("|                             CONSULTAR PRODUTO                                   |");
            Console.WriteLine("|                                                                                 |");
            Console.WriteLine("|---------------------------------------------------------------------------------|\n");

            Console.WriteLine("| Insira o código do produto que deseja consultar:     |"); 
            string nome = Console.ReadLine(); 
            bool encontrado = false; 
            foreach (ListaProdutos produto in ListaProduto) 
            {
                if (produto.codigo.ToLower().Contains(nome.ToLower()))   
                                                                   
                {
                    encontrado = true; 

                    Console.Clear();

                    Console.WriteLine("|---------------------------------------------------------------------------------|");
                    Console.WriteLine("|                                                                                 |");
                    Console.WriteLine("|                             CONSULTAR PRODUTO                                   |");
                    Console.WriteLine("|                                                                                 |");                 
                    Console.WriteLine("  {0} foi encontrado no banco de dados.\n", produto.codigo); 
                    Console.WriteLine("|---------------------------------------------------------------------------------|");
                    Console.WriteLine("|Código:        Produto:                              Valor:        Venda:        |");
                    Console.WriteLine("|---------------------------------------------------------------------------------|");

                    foreach (ClassProdutos produtos in produto.ListaProduto) 
                    {
                        Console.WriteLine("   {0}           {1}  \t\t              {2}           {3}", produto.codigo, produtos.nome, produtos.custo, produtos.venda); 
                    }
                    Console.WriteLine("|---------------------------------------------------------------------------------|");
                    Console.WriteLine(" \n \n  Escolha a opção:");
                    Console.WriteLine(" \n  1-Voltar p/Consultar \n  2-Ir ao Menu incial"); 
                    Console.Write(" \n  Insira a Opção:");
                    string opcao = Console.ReadLine(); 
                    if (opcao == "1") 
                    {
                        Consultar(); 
                    }
                    else if (opcao == "2") 
                    {
                        Escolher();
                    }
                    Console.ReadKey(); 
                }
                foreach (ClassProdutos produtos in produto.ListaProduto) 
                {
                    if (produto.codigo.ToLower().Contains(nome.ToLower())) 
                                                                        
                    {
                        encontrado = true; 
                        Console.Clear();

                        Console.WriteLine("|---------------------------------------------------------------------------------|");
                        Console.WriteLine("|                                                                                 |");
                        Console.WriteLine("|                             CONSULTAR PRODUTO                                   |");
                        Console.WriteLine("|                                                                                 |");
                        Console.WriteLine("|---------------------------------------------------------------------------------|");
                        Console.WriteLine("|Código:      Produto:                              Valor:           Venda:       |");
                        Console.WriteLine("|---------------------------------------------------------------------------------|\n");
                        Console.WriteLine("| Produto Encontrado.");
                        Console.WriteLine("|\n| Código: {0,5}\n| Nome do Produto: {1,20} |\n| Valor do Produto: {2,10}\n| |\n| Venda: {3,10}\n|", produto.codigo, produtos.custo, produtos.venda); 
                        Console.WriteLine("|\n|\n| Escolha a opção:");
                        Console.WriteLine("|\n| 1-Voltar p/Consultar \n| 2-Ir ao Menu incial"); 
                        Console.Write("|\n| Insira a Opção:");
                        string opcao = Console.ReadLine(); 
                        if (opcao == "1") 
                        {
                            Consultar();
                        }
                        else if (opcao == "2") 
                        {
                            Escolher(); 
                        }
                    }
                }
            }
            if (!encontrado) 
            {
                Console.Clear(); 

                Console.WriteLine("|---------------------------------------------------------------------------------|");
                Console.WriteLine("|                                                                                 |");
                Console.WriteLine("|                             CONSULTAR PRODUTO                                   |");
                Console.WriteLine("|                                                                                 |");
                Console.WriteLine("|---------------------------------------------------------------------------------|");
                Console.WriteLine("|Código:      Produto:                              Valor:           Venda:       |");
                Console.WriteLine("|---------------------------------------------------------------------------------|\n");

                Console.WriteLine("|\n| {0} não existe no banco de dados.", nome); 
                Console.WriteLine("|\n|\n|\n| Escolha a opção:");
                Console.WriteLine("|\n| 1-Voltar p/Consultar \n| 2-Ir ao Menu incial"); 
                Console.WriteLine("|");
                Console.Write("| Insira a Opção:");
                string opcao = Console.ReadLine(); 
                if (opcao == "1") 
                {
                    Consultar(); 
                }
                else if (opcao == "2") 
                {
                    Escolher(); 
                }
            }
        }
        //-------------------------------------------------
        //  LISTAR
        //-------------------------------------------------
        static void Listar() 
        {
            Console.Clear(); 

            Console.WriteLine("|---------------------------------------------------------------------------------|");
            Console.WriteLine("|                                                                                 |");
            Console.WriteLine("|                             LISTAR PRODUTO                                      |");
            Console.WriteLine("|                                                                                 |");
            Console.WriteLine("|---------------------------------------------------------------------------------|");
            Console.WriteLine("|Código:      Produto:                              Valor:           Venda:       |");
            Console.WriteLine("|---------------------------------------------------------------------------------|");

            
            foreach (ListaProdutos products in ListaProduto) 
            {
                foreach (ClassProdutos produto in products.ListaProduto)  
                {
                    Console.WriteLine("   {0}           {1}  \t\t              {2}           {3}", products.codigo, produto.nome, produto.custo, produto.venda);
                }
            Console.WriteLine("                                                                                   ");
            Console.WriteLine("|-------------------------------------------------------------------------------- |");
            }
            Console.WriteLine("|                                                                                 |");
            Console.WriteLine("| Aperte qualquer tecla para voltar ao menu inicial.                              |"); 
            Console.WriteLine("|---------------------------------------------------------------------------------|");
            Console.ReadKey();
        }
    }
    //-------------------------------------------------
    //  VALDÉCIO, EU MANDEI ESSE PORQUÊ DEU UM TRABALHÃO PRA FAZER E QUERIA TE MOSTRAR E PASSAR VERGONHA UM POUCO kkkk
    //-------------------------------------------------
}