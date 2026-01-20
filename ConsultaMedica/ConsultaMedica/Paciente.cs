using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaMedica
{

    public class Paciente : Pessoa
        {
            // Propriedades específicas do Paciente
            private double peso;
            private double temperatura;
            private List<string> sintomas;
            private string especialidadeRecomendada;

            public double Peso
            {
                get { return peso; }
                set { peso = value; }
            }

            public double Temperatura
            {
                get { return temperatura; }
                set { temperatura = value; }
            }

            public List<string> Sintomas
            {
                get { return sintomas; }
                set { sintomas = value; }
            }

            public string EspecialidadeRecomendada
            {
                get { return especialidadeRecomendada; }
                set { especialidadeRecomendada = value; }
            }

            // Construtor
            public Paciente(string nome, int idade, string morada, double peso, double temperatura)
                : base(nome, idade, morada)
            {
                this.Peso = peso;
                this.Temperatura = temperatura;
                this.Sintomas = new List<string>();
                this.EspecialidadeRecomendada = "";
            }

            // Adicionar sintomas
            public void AdicionarSintoma(string sintoma)
            {
                Sintomas.Add(sintoma);
            }

            // Determinar especialidade baseada nos sintomas e idade
            public void DeterminarEspecialidade()
            {
                // Se for criança (até 12 anos) com sintomas comuns
                if (this.Idade <= 12)
                {
                    this.EspecialidadeRecomendada = "Pediatria";
                    return;
                }

                // Análise baseada nos sintomas
                foreach (string sintoma in this.Sintomas)
                {
                    string sintomaLower = sintoma.ToLower();

                    if (sintomaLower.Contains("dente") || sintomaLower.Contains("gengiva") || sintomaLower.Contains("boca"))
                    {
                        this.EspecialidadeRecomendada = "Dentista";
                        return;
                    }
                    else if (sintomaLower.Contains("olho") || sintomaLower.Contains("visão") || sintomaLower.Contains("ocular"))
                    {
                        this.EspecialidadeRecomendada = "Oftalmologista";
                        return;
                    }
                    else if (sintomaLower.Contains("coração") || sintomaLower.Contains("cardíaco") || sintomaLower.Contains("pressão"))
                    {
                        this.EspecialidadeRecomendada = "Cardiologista";
                        return;
                    }
                    else if (sintomaLower.Contains("osso") || sintomaLower.Contains("articulação") || sintomaLower.Contains("fratur"))
                    {
                        this.EspecialidadeRecomendada = "Ortopedista";
                        return;
                    }
                    else if (sintomaLower.Contains("urinário") || sintomaLower.Contains("rins") || sintomaLower.Contains("prostata"))
                    {
                        this.EspecialidadeRecomendada = "Urologista";
                        return;
                    }
                }

                // Se nenhum sintoma específico for encontrado
                this.EspecialidadeRecomendada = "Clínico Geral";
            }

            // Sobrescrevendo o método abstrato
            public override void ExibirInformacoes()
            {
                Console.WriteLine("\n=== INFORMAÇÕES DO PACIENTE ===");
                Console.WriteLine(string.Format("Nome: {0}", this.Nome));
                Console.WriteLine(string.Format("Idade: {0} anos", this.Idade));
                Console.WriteLine(string.Format("Morada: {0}", this.Morada));
                Console.WriteLine(string.Format("Peso: {0} kg", this.Peso));
                Console.WriteLine(string.Format("Temperatura: {0}°C", this.Temperatura));
                Console.Write("Sintomas: ");

                if (this.Sintomas.Count > 0)
                {
                    foreach (string sintoma in this.Sintomas)
                    {
                        Console.Write(sintoma + "; ");
                    }
                }
                else
                {
                    Console.Write("Nenhum sintoma registado");
                }

                Console.WriteLine(string.Format("\nEspecialidade Recomendada: {0}", this.EspecialidadeRecomendada));
            }
        }
    

}
