using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaMedica
{

    public class Medico : Pessoa
        {
            // Propriedades específicas do Médico
            private string especialidade;
            private string numeroRegistro;
            private string horarioAtendimento;

            public string Especialidade
            {
                get { return especialidade; }
                set { especialidade = value; }
            }

            public string NumeroRegistro
            {
                get { return numeroRegistro; }
                set { numeroRegistro = value; }
            }

            public string HorarioAtendimento
            {
                get { return horarioAtendimento; }
                set { horarioAtendimento = value; }
            }

            // Construtor
            public Medico(string nome, int idade, string morada, string especialidade, string numeroRegistro, string horarioAtendimento)
                : base(nome, idade, morada)
            {
                this.Especialidade = especialidade;
                this.NumeroRegistro = numeroRegistro;
                this.HorarioAtendimento = horarioAtendimento;
            }

            // Sobrescrevendo o método abstrato
            public override void ExibirInformacoes()
            {
                Console.WriteLine("\n=== INFORMAÇÕES DO MÉDICO ===");
                Console.WriteLine(string.Format("Nome: {0}", this.Nome));
                Console.WriteLine(string.Format("Idade: {0} anos", this.Idade));
                Console.WriteLine(string.Format("Morada: {0}", this.Morada));
                Console.WriteLine(string.Format("Especialidade: {0}", this.Especialidade));
                Console.WriteLine(string.Format("Número de Registro: {0}", this.NumeroRegistro));
                Console.WriteLine(string.Format("Horário de Atendimento: {0}", this.HorarioAtendimento));
            }
        }


}
