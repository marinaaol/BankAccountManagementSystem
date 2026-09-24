using System;
using System.Collections.Generic;

namespace GestaoContas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== SISTEMA DE GESTÃO DE CONTAS BANCÁRIAS ===\n");

            // 1. Criar pelo menos duas contas - uma com cada construtor
            ContaBancaria conta1 = new ContaBancaria("Marina Silva", "PT501234789");
            ContaBancaria conta2 = new ContaBancaria("Cauê Salgado", "PT505678123", 500.0, 100.0);

            Console.WriteLine("--- Contas Criadas ---");
            Console.WriteLine(conta1);
            Console.WriteLine(conta2);

            // 2. Fazer alguns depósitos e levantamentos válidos
            Console.WriteLine("\n--- Operações Válidas ---");
            conta1.Depositar(250.0);
            conta1.Levantar(50.0);
            conta1.MostrarExtrato();
            Console.WriteLine(conta1);

            // 3. Fazer uma transferência da conta 2 para a conta 1
            Console.WriteLine("\n--- Transferência ---");
            conta2.TransferirPara(conta1, 150.0);
            Console.WriteLine("Após transferir 150.00 EUR da Conta 2 para a Conta 1:");
            Console.WriteLine(conta1);
            Console.WriteLine(conta2);

            // 4. Try/Catch: Tentar levantamento acima do saldo + limite de descoberto
            Console.WriteLine("\n--- Teste 1: Levantamento Acima do Limite ---");
            try
            {
                conta1.Levantar(10000.0);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"[Exceção Apanhada]: {ex.Message}");
            }

            // 5. Try/Catch: Criar uma conta temporária, encerra-la e tentar depositar
            Console.WriteLine("\n--- Teste 2: Depósito em Conta Encerrada ---");
            try
            {
                ContaBancaria contaTemporaria = new ContaBancaria("Maria Teste", "PT500000000");
                contaTemporaria.Encerrar();
                contaTemporaria.Depositar(100.0);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"[Exceção Apanhada]: {ex.Message}");
            }

            // 6. Try/Catch: Tentar criar uma conta com limite de descoberto negativo
            Console.WriteLine("\n--- Teste 3: Limite de Descoberto Negativo ---");
            try
            {
                ContaBancaria contaInvalida = new ContaBancaria("Mário", "PT509999888", 0.0, -50.0);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"[Exceção Apanhada]: {ex.Message}");
            }

            // Desafio Extra: Resumo Geral do Banco (conta1 e conta2 continuam ativas)
            Console.WriteLine("\n--- Desafio Extra: Resumo Geral do Banco ---");
            List<ContaBancaria> banco = new List<ContaBancaria> { conta1, conta2 };
            double saldoTotalAtivas = 0;

            foreach (var conta in banco)
            {
                Console.WriteLine(conta.ToString());
                if (conta.Ativa)
                {
                    saldoTotalAtivas += conta.Saldo;
                }
            }

            Console.WriteLine($"\nTotal de contas no sistema: {banco.Count}");
            Console.WriteLine($"Saldo total das contas ativas: {saldoTotalAtivas:F2} EUR");
        }
    }
}