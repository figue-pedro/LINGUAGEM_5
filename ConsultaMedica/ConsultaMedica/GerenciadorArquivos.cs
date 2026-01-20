using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace ConsultaMedica
{

    public class GerenciadorArquivos
        {
            private string caminhoPacientes = "pacientes.txt";
            private string caminhoMedicos = "medicos.txt";

            // Salvar pacientes em arquivo
            public void SalvarPacientes(List<Paciente> pacientes)
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter(caminhoPacientes))
                    {
                        foreach (Paciente paciente in pacientes)
                        {
                            writer.WriteLine(string.Format("{0};{1};{2};{3};{4};{5};{6}",
                                paciente.Nome,
                                paciente.Idade,
                                paciente.Morada,
                                paciente.Peso,
                                paciente.Temperatura,
                                string.Join(",", paciente.Sintomas),
                                paciente.EspecialidadeRecomendada));
                        }
                    }
                    Console.WriteLine("\nDados dos pacientes salvos com sucesso!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(string.Format("Erro ao salvar pacientes: {0}", ex.Message));
                }
            }

            // Salvar médicos em arquivo
            public void SalvarMedicos(List<Medico> medicos)
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter(caminhoMedicos))
                    {
                        foreach (Medico medico in medicos)
                        {
                            writer.WriteLine(string.Format("{0};{1};{2};{3};{4};{5}",
                                medico.Nome,
                                medico.Idade,
                                medico.Morada,
                                medico.Especialidade,
                                medico.NumeroRegistro,
                                medico.HorarioAtendimento));
                        }
                    }
                    Console.WriteLine("\nDados dos médicos salvos com sucesso!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(string.Format("Erro ao salvar médicos: {0}", ex.Message));
                }
            }

            // Carregar pacientes do arquivo
            public List<Paciente> CarregarPacientes()
            {
                List<Paciente> pacientes = new List<Paciente>();

                if (File.Exists(caminhoPacientes))
                {
                    try
                    {
                        using (StreamReader reader = new StreamReader(caminhoPacientes))
                        {
                            string linha;
                            while ((linha = reader.ReadLine()) != null)
                            {
                                string[] dados = linha.Split(';');
                                if (dados.Length >= 7)
                                {
                                    Paciente paciente = new Paciente(
                                        dados[0],
                                        int.Parse(dados[1]),
                                        dados[2],
                                        double.Parse(dados[3]),
                                        double.Parse(dados[4]));

                                    // Adicionar sintomas
                                    if (!string.IsNullOrEmpty(dados[5]))
                                    {
                                        string[] sintomas = dados[5].Split(',');
                                        foreach (string sintoma in sintomas)
                                        {
                                            paciente.AdicionarSintoma(sintoma);
                                        }
                                    }

                                    paciente.EspecialidadeRecomendada = dados[6];
                                    pacientes.Add(paciente);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(string.Format("Erro ao carregar pacientes: {0}", ex.Message));
                    }
                }

                return pacientes;
            }

            // Carregar médicos do arquivo
            public List<Medico> CarregarMedicos()
            {
                List<Medico> medicos = new List<Medico>();

                if (File.Exists(caminhoMedicos))
                {
                    try
                    {
                        using (StreamReader reader = new StreamReader(caminhoMedicos))
                        {
                            string linha;
                            while ((linha = reader.ReadLine()) != null)
                            {
                                string[] dados = linha.Split(';');
                                if (dados.Length >= 6)
                                {
                                    Medico medico = new Medico(
                                        dados[0],
                                        int.Parse(dados[1]),
                                        dados[2],
                                        dados[3],
                                        dados[4],
                                        dados[5]);

                                    medicos.Add(medico);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(string.Format("Erro ao carregar médicos: {0}", ex.Message));
                    }
                }

                return medicos;
            }
        }
    

}
