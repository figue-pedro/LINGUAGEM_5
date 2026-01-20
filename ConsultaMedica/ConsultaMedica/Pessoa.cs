using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaMedica
{

    public abstract class Pessoa
        {
            // Propriedades encapsuladas
            private string nome;
            private int idade;
            private string morada;

            public string Nome
            {
                get { return nome; }
                set { nome = value; }
            }

            public int Idade
            {
                get { return idade; }
                set { idade = value; }
            }

            public string Morada
            {
                get { return morada; }
                set { morada = value; }
            }

            // Construtor
            public Pessoa(string nome, int idade, string morada)
            {
                this.Nome = nome;
                this.Idade = idade;
                this.Morada = morada;
            }

            // Método abstrato para ser implementado nas classes derivadas
            public abstract void ExibirInformacoes();
        }
    

}
