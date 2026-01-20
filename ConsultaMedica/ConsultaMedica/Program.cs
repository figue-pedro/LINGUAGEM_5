using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaMedica
{
    class Program
        {
            static List<Paciente> pacientes = new List<Paciente>();
            static List<Medico> medicos = new List<Medico>();
            static GerenciadorArquivos gerenciadorArquivos = new GerenciadorArquivos();
            static string[] especialidades = { "Pediatria", "Dentista", "Oftalmologista", "Cardiologista", "Ortopedista", "Urologista", "Clínico Geral" };

            static void Main(string[] args)
            {
                Console.Title = "Sistema de Gerenciamento de Consultas Médicas";

                // Carregar dados salvos
                pacientes = gerenciadorArquivos.CarregarPacientes();
                medicos = gerenciadorArquivos.CarregarMedicos();

                // Se não houver médicos cadastrados, adicionar alguns exemplos
                if (medicos.Count == 0)
                {
                    CadastrarMedicosExemplo();
                }

                int opcao;
                do
                {
                    ExibirMenu();
                    Console.Write("\nEscolha uma opção: ");

                    if (int.TryParse(Console.ReadLine(), out opcao))
                    {
                        switch (opcao)
                        {
                            case 1:
                                CadastrarPaciente();
                                break;
                            case 2:
                                CadastrarMedico();
                                break;
                            case 3:
                                ListarPacientes();
                                break;
                            case 4:
                                ListarMedicos();
                                break;
                            case 5:
                                BuscarPacientePorNome();
                                break;
                            case 6:
                                BuscarMedicoPorEspecialidade();
                                break;
                            case 7:
                                RealizarTriagem();
                                break;
                            case 8:
                                SalvarDados();
                                break;
                            case 9:
                                Console.WriteLine("\nSaindo do sistema...");
                                break;
                            default:
                                Console.WriteLine("\nOpção inválida! Tente novamente.");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nOpção inválida! Digite um número.");
                    }

                    if (opcao != 9)
                    {
                        Console.WriteLine("\nPressione qualquer tecla para continuar...");
                        Console.ReadKey();
                    }

                } while (opcao != 9);
            }

            static void ExibirMenu()
            {
                Console.Clear();
                Console.WriteLine("==================================");
                Console.WriteLine(" SISTEMA DE CONSULTAS MÉDICAS");
                Console.WriteLine("==================================");
                Console.WriteLine("1. Cadastrar Paciente");
                Console.WriteLine("2. Cadastrar Médico");
                Console.WriteLine("3. Listar Pacientes");
                Console.WriteLine("4. Listar Médicos");
                Console.WriteLine("5. Buscar Paciente por Nome");
                Console.WriteLine("6. Buscar Médico por Especialidade");
                Console.WriteLine("7. Realizar Triagem");
                Console.WriteLine("9. Sair");
                Console.WriteLine("==================================");
            }

            static void CadastrarPaciente()
            {
                Console.Clear();
                Console.WriteLine("=== CADASTRO DE PACIENTE ===");

                Console.Write("Nome: ");
                string nome = Console.ReadLine();

                Console.Write("Idade: ");
                int idade;
                while (!int.TryParse(Console.ReadLine(), out idade) || idade <= 0)
                {
                    Console.Write("Idade inválida! Digite novamente: ");
                }

                Console.Write("Morada: ");
                string morada = Console.ReadLine();

                Console.Write("Peso (kg): ");
                double peso;
                while (!double.TryParse(Console.ReadLine(), out peso) || peso <= 0)
                {
                    Console.Write("Peso inválido! Digite novamente: ");
                }

                Console.Write("Temperatura (°C): ");
                double temperatura;
                while (!double.TryParse(Console.ReadLine(), out temperatura))
                {
                    Console.Write("Temperatura inválida! Digite novamente: ");
                }

                Paciente paciente = new Paciente(nome, idade, morada, peso, temperatura);

                // Adicionar sintomas
                Console.WriteLine("\nDigite os sintomas (digite 'fim' para terminar):");
                string sintoma;
                do
                {
                    Console.Write("Sintoma: ");
                    sintoma = Console.ReadLine();
                    if (sintoma.ToLower() != "fim" && !string.IsNullOrWhiteSpace(sintoma))
                    {
                        paciente.AdicionarSintoma(sintoma);
                    }
                } while (sintoma.ToLower() != "fim");

                // Determinar especialidade recomendada
                paciente.DeterminarEspecialidade();

                pacientes.Add(paciente);
                Console.WriteLine(string.Format("\nPaciente {0} cadastrado com sucesso!", nome));
                Console.WriteLine(string.Format("Especialidade recomendada: {0}", paciente.EspecialidadeRecomendada));
                gerenciadorArquivos.SalvarPacientes(pacientes);
            }

            static void CadastrarMedico()
            {
                Console.Clear();
                Console.WriteLine("=== CADASTRO DE MÉDICO ===");

                Console.Write("Nome: ");
                string nome = Console.ReadLine();

                Console.Write("Idade: ");
                int idade;
                while (!int.TryParse(Console.ReadLine(), out idade) || idade <= 0)
                {
                    Console.Write("Idade inválida! Digite novamente: ");
                }

                Console.Write("Morada: ");
                string morada = Console.ReadLine();

                Console.WriteLine("\nEspecialidades disponíveis:");
                for (int i = 0; i < especialidades.Length; i++)
                {
                    Console.WriteLine(string.Format("{0}. {1}", i + 1, especialidades[i]));
                }

                Console.Write("\nEscolha a especialidade (1-7): ");
                int escolha;
                while (!int.TryParse(Console.ReadLine(), out escolha) || escolha < 1 || escolha > 7)
                {
                    Console.Write("Escolha inválida! Digite novamente (1-7): ");
                }

                string especialidade = especialidades[escolha - 1];

                Console.Write("Número de Registro: ");
                string numeroRegistro = Console.ReadLine();

                Console.Write("Horário de Atendimento: ");
                string horarioAtendimento = Console.ReadLine();

                Medico medico = new Medico(nome, idade, morada, especialidade, numeroRegistro, horarioAtendimento);
                medicos.Add(medico);

                Console.WriteLine(string.Format("\nMédico {0} cadastrado com sucesso!", nome));
                gerenciadorArquivos.SalvarMedicos(medicos);
            }

            //static void CadastrarMedicosExemplo()
            //{
            //    // Cadastrar médicos exemplo para todas as especialidades
            //    medicos.Add(new Medico("Dr. João Silva", 45, "Rua das Flores, 123", "Pediatria", "CRM-12345", "08:00-12:00"));
            //    medicos.Add(new Medico("Dra. Maria Santos", 38, "Av. Central, 456", "Dentista", "CRM-23456", "14:00-18:00"));
            //    medicos.Add(new Medico("Dr. Carlos Oliveira", 52, "Rua dos Médicos, 789", "Oftalmologista", "CRM-34567", "09:00-17:00"));
            //    medicos.Add(new Medico("Dra. Ana Costa", 41, "Av. da Saúde, 101", "Cardiologista", "CRM-45678", "08:00-16:00"));
            //    medicos.Add(new Medico("Dr. Pedro Alves", 36, "Rua do Hospital, 202", "Ortopedista", "CRM-56789", "10:00-19:00"));
            //    medicos.Add(new Medico("Dra. Sofia Martins", 48, "Av. Principal, 303", "Urologista", "CRM-67890", "13:00-21:00"));
            //    medicos.Add(new Medico("Dr. Miguel Fernandes", 39, "Rua da Clínica, 404", "Clínico Geral", "CRM-78901", "07:00-15:00"));
            //}

            static void ListarPacientes()
            {
                Console.Clear();
                Console.WriteLine("=== LISTA DE PACIENTES ===");

                if (pacientes.Count == 0)
                {
                    Console.WriteLine("Nenhum paciente cadastrado.");
                }
                else
                {
                    for (int i = 0; i < pacientes.Count; i++)
                    {
                        Console.WriteLine(string.Format("\n--- Paciente {0} ---", i + 1));
                        pacientes[i].ExibirInformacoes();
                    }
                    Console.WriteLine(string.Format("\nTotal de pacientes: {0}", pacientes.Count));
                }
            }

            static void ListarMedicos()
            {
                Console.Clear();
                Console.WriteLine("=== LISTA DE MÉDICOS ===");

                if (medicos.Count == 0)
                {
                    Console.WriteLine("Nenhum médico cadastrado.");
                }
                else
                {
                    for (int i = 0; i < medicos.Count; i++)
                    {
                        Console.WriteLine(string.Format("\n--- Médico {0} ---", i + 1));
                        medicos[i].ExibirInformacoes();
                    }
                    Console.WriteLine(string.Format("\nTotal de médicos: {0}", medicos.Count));
                }
            }

            static void BuscarPacientePorNome()
            {
                Console.Clear();
                Console.WriteLine("=== BUSCAR PACIENTE POR NOME ===");
                Console.Write("Digite o nome (ou parte do nome): ");
                string busca = Console.ReadLine().ToLower();

                var pacientesEncontrados = pacientes.FindAll(p => p.Nome.ToLower().Contains(busca));

                if (pacientesEncontrados.Count == 0)
                {
                    Console.WriteLine("\nNenhum paciente encontrado com esse nome.");
                }
                else
                {
                    Console.WriteLine(string.Format("\nForam encontrados {0} paciente(s):", pacientesEncontrados.Count));
                    foreach (var paciente in pacientesEncontrados)
                    {
                        paciente.ExibirInformacoes();
                        Console.WriteLine();
                    }
                }
            }

            static void BuscarMedicoPorEspecialidade()
            {
                Console.Clear();
                Console.WriteLine("=== BUSCAR MÉDICO POR ESPECIALIDADE ===");
                Console.WriteLine("\nEspecialidades disponíveis:");
                for (int i = 0; i < especialidades.Length; i++)
                {
                    Console.WriteLine(string.Format("{0}. {1}", i + 1, especialidades[i]));
                }

                Console.Write("\nEscolha a especialidade (1-7): ");
                int escolha;
                while (!int.TryParse(Console.ReadLine(), out escolha) || escolha < 1 || escolha > 7)
                {
                    Console.Write("Escolha inválida! Digite novamente (1-7): ");
                }

                string especialidade = especialidades[escolha - 1];
                var medicosEncontrados = medicos.FindAll(m => m.Especialidade == especialidade);

                if (medicosEncontrados.Count == 0)
                {
                    Console.WriteLine(string.Format("\nNenhum médico encontrado na especialidade {0}.", especialidade));
                }
                else
                {
                    Console.WriteLine(string.Format("\nMédicos encontrados na especialidade {0}:", especialidade));
                    foreach (var medico in medicosEncontrados)
                    {
                        medico.ExibirInformacoes();
                        Console.WriteLine();
                    }
                }
            }

            static void RealizarTriagem()
            {
                Console.Clear();
                Console.WriteLine("=== REALIZAR TRIAGEM ===");

                if (pacientes.Count == 0)
                {
                    Console.WriteLine("Não há pacientes cadastrados para realizar triagem.");
                    return;
                }

                Console.WriteLine("Pacientes disponíveis:");
                for (int i = 0; i < pacientes.Count; i++)
                {
                    Console.WriteLine(string.Format("{0}. {1}", i + 1, pacientes[i].Nome));
                }

                Console.Write("\nEscolha o paciente (número): ");
                int escolha;
                while (!int.TryParse(Console.ReadLine(), out escolha) || escolha < 1 || escolha > pacientes.Count)
                {
                    Console.Write("Escolha inválida! Digite novamente: ");
                }

                Paciente paciente = pacientes[escolha - 1];

                Console.Clear();
                Console.WriteLine("=== TRIAGEM DO PACIENTE ===");
                paciente.ExibirInformacoes();

                // Mostrar médicos disponíveis para a especialidade recomendada
                Console.WriteLine("\n=== MÉDICOS DISPONÍVEIS ===");
                var medicosEspecialidade = medicos.FindAll(m => m.Especialidade == paciente.EspecialidadeRecomendada);

                if (medicosEspecialidade.Count == 0)
                {
                    Console.WriteLine(string.Format("Não há médicos disponíveis na especialidade {0}.", paciente.EspecialidadeRecomendada));
                }
                else
                {
                    Console.WriteLine(string.Format("Médicos disponíveis em {0}:", paciente.EspecialidadeRecomendada));
                    foreach (var medico in medicosEspecialidade)
                    {
                        Console.WriteLine(string.Format("- {0} (Horário: {1})", medico.Nome, medico.HorarioAtendimento));
                    }
                }
            }

            static void SalvarDados()
            {
                gerenciadorArquivos.SalvarPacientes(pacientes);
                gerenciadorArquivos.SalvarMedicos(medicos);
            }
        }

}
