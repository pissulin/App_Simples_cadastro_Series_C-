using System;
using System.Collections.Generic;

namespace APP_series
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario().ToUpper();
            string[] opcoes = new string[7] {"1", "2", "3", "4", "5", "C", "X"};
            
            while(opcaoUsuario!= "X")
            {
                foreach (var item in opcoes)
                {
                    if( opcaoUsuario == item){
                        switch(opcaoUsuario)
                {
                    case "1":
                        ListarSeries();
                    break;

                    case "2":
                        InserirSerie();
                    break;

                    case "3":
                        AtualizarSerie();
                    break;

                    case "4":
                        
                        ExcluirSerie();
                        
                    break;  

                    case "5":
                        VisualizarSerie();
                    break;  

                    case "C":
                        Console.Clear();
                    break;  

                    default:
                        throw new ArgumentOutOfRangeException();
                }
                    }
                }
                opcaoUsuario = ObterOpcaoUsuario();
            }
            

            Console.WriteLine("Obrigado por utilizar nossos serviços!!!");
            //Console.ReadLine();
        }
        
        private static void VisualizarSerie()
        {
            var lista = repositorio.Lista();
            if(lista.Count == 0)
                {
                    Console.WriteLine("Nenhuma série cadastrada!");
                    return; 
                }
            Console.Write("Digite o id da série: ");
            int indiceSerie = int.Parse(Console.ReadLine());

            var serie = repositorio.RetornaPorId(indiceSerie);
            
            Console.WriteLine(serie.ToString());
        }

        private static void ExcluirSerie()
        {
            var lista = repositorio.Lista();
            if(lista.Count == 0)
                {
                    Console.WriteLine("Nenhuma série cadastrada!");
                    return; 
                }
            Console.WriteLine("Confirmar a exclusão! [s] = sim e [n] = não");
            string confirmacaoExclusao = Console.ReadLine().ToUpper();
            if(confirmacaoExclusao == "S")
                {
                    Console.Write("Digite o id da série: ");
                    int indiceSerie = int.Parse(Console.ReadLine());
                    var seriePorId = repositorio.RetornaPorId(indiceSerie);
                    if(!seriePorId.retornaExcluido()){
                        repositorio.Exclui(indiceSerie);
                        return;
                    }
                    else{
                        Console.WriteLine("A série com ID {0} já foi exluida!", indiceSerie);
                    }

                }
        }

        private static void AtualizarSerie(){
            var lista = repositorio.Lista();
            if(lista.Count == 0)
                {
                    Console.WriteLine("Nenhuma série cadastrada!");
                    return; 
                }

            Console.Write("Digite o id da série: ");
            int indiceSerie = int.Parse(Console.ReadLine());

            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0} - {1}", i, Enum.GetName(typeof(Genero), i));
            }
            

            Console.Write("Digite o gênero entre as opções acima: ");
            int entradaGenero = int.Parse(Console.ReadLine());

            Console.Write("Digite o título da série: ");
            string entradaTitulo = Console.ReadLine();

            Console.Write("Digite o Ano de início da série: ");
            int entradaAno = int.Parse(Console.ReadLine());
            
            Console.Write("Digite a descrição da série: ");
            string entradaDescricao = Console.ReadLine();

            Series atualizaSerie = new Series(
                id: indiceSerie,
                genero: (Genero)entradaGenero,
                titulo: entradaTitulo,
                ano: entradaAno,
                descricao: entradaDescricao
            );

            repositorio.Atualiza(indiceSerie,atualizaSerie);
            
        }
        private static void ListarSeries()
            {
                Console.WriteLine("Listar séries");
                var lista = repositorio.Lista();
                if(lista.Count == 0)
                    {
                       Console.WriteLine("Nenhuma série cadastrada!");
                       return; 
                    }else {
                        foreach(var serie in lista)
                        {
                            var excluido = serie.retornaExcluido();
                            if(!excluido)
                            {
                                Console.WriteLine("#ID {0}: - {1}", serie.retornaId(), serie.retornaTitulo());
                            }
                        }
                        Console.WriteLine("Nenhuma série cadastrada!");

                    }
            
            }
        
        private static void InserirSerie()
        {
            Console.WriteLine("Inserir nova série");

            foreach(int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0} - {1}", i, Enum.GetName(typeof(Genero), i));
            }
            Console.WriteLine("Digite o gênero entre as opções acima: ");
            int entradaGenero = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o Título da série: ");
            string entradaTitulo = Console.ReadLine();

            Console.WriteLine("Digite o Ano de início da série: ");
            int entradaAno = int.Parse(Console.ReadLine());
            
            Console.WriteLine("Digite a descrição da série: ");
            string entradaDescricao = Console.ReadLine();

            Series novaSerie = new Series(
                id: repositorio.ProximoId(),
                genero: (Genero)entradaGenero,
                titulo: entradaTitulo,
                ano: entradaAno,
                descricao: entradaDescricao
            );

            repositorio.Insere(novaSerie);

        }

        private static string ObterOpcaoUsuario()
        {
            Console.WriteLine();
            Console.WriteLine("DIO Series a seu dispor!!!");
            Console.WriteLine("Informe a opção desejada:");

            Console.WriteLine("1- Listar séries");
            Console.WriteLine("2- Inserir nova série");
            Console.WriteLine("3- Atualizar série");
            Console.WriteLine("4- Excluir série");
            Console.WriteLine("5- Visualizar série");
            Console.WriteLine("C- Limpar tela");
            Console.WriteLine("X- Sair");
            Console.WriteLine();

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;
        }
        
            
    }
}
