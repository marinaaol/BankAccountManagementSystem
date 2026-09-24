using System;
using System.Collections.Generic;

namespace GestaoContas
{
    internal class ContaBancaria
    {
        //------------ CAMPOS PRIVADOS ------------ 
        private List<string> _historico;
        private double _limiteDescoberto;

        //------------  PROPERTIES ------------ 
        public string Titular {get; set; }
        public string NumeroConta {get; private set; }
        public double Saldo { get; private set; }
        public double LimiteDescoberto
        {
            get => _limiteDescoberto;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException (nameof(value), "O limite de descoberto não pode ser negativo.");
                }
                _limiteDescoberto = value;
            }
        }

        public bool Ativa { get; private set; }
        public int NumeroOperacoes => _historico.Count;

        // --------------- CONSTRUTORES ---------------

        public ContaBancaria(string titular, string numeroConta)
        {
            Titular = titular;
            NumeroConta = numeroConta;
            Saldo = 0;
            LimiteDescoberto = 0;
            Ativa = true;
            _historico = new List<string>();
        }

        public ContaBancaria(string titular, string numeroConta, double saldoInicial, double limiteDescoberto)
        {
            Titular = titular;
            NumeroConta = numeroConta;
            Saldo = saldoInicial;
            LimiteDescoberto = limiteDescoberto;  //usa a property para validar se não é negativo
            Ativa = true;
            _historico = new List<string>();
        }

        //--------------- MÉTODOS ---------------

        public void Depositar(double valor)
        {
            if (!Ativa)
            {
                throw new InvalidOperationException("Conta encerrada");
            }
            if (valor <= 0)
            {
                throw new ArgumentException("Valor depositado deve ser positivo.", nameof(valor));
            }

            Saldo += valor;
            _historico.Add($"Depósito de {valor:F2} EUR");
            
        }

        public void Levantar(double valor)
        {
            if (!Ativa)
            {
                throw new InvalidOperationException("Conta encerrada");
            }
            if (valor <= 0)
            {
                throw new ArgumentException("O valor do levantamento deve ser positivo");
            }
            if ((Saldo - valor) < -_limiteDescoberto)
            {
                throw new InvalidOperationException("Saldo insuficiente.");
            }
            Saldo -= valor;
            _historico.Add($"Depósito de {valor:F2} EUR");
        }

        public void TransferirPara(ContaBancaria destino, double valor)
        {
            if (!Ativa)
            {
                throw new InvalidOperationException("Conta encerrada");
            }
            if (destino == null)
            {
                throw new ArgumentNullException(nameof(destino), "Conta de destino não pode ser nula.");
            }
            if(destino == this)
            {
                throw new InvalidOperationException("Não é possível transferir para a própria conta.");
            }

            //Reaproveita os métodos existentes
            this.Levantar(valor);
            destino.Depositar(valor);

            //Ajusta o último elemento do histórico para refletir que foi uma transferência 
            if (_historico.Count > 0)
            {
                _historico[_historico.Count - 1] = $"Transferência enviada de {valor:F2} EUR para conta {destino.NumeroConta}";
            }
        }

        public void Encerrar()
        {
            Ativa = false;
        }

        public void MostrarExtrato()
        {
            Console.WriteLine($"---- Extrato da conta: {NumeroConta} -----");
            if(_historico.Count == 0)
            {
                Console.WriteLine("Ainda não há operações registadas.");
            }
            else
            {
                foreach (var registo in _historico)
                {
                    Console.WriteLine($"- {registo}");
                }         
            }
            Console.WriteLine("-------------------------------------------");
        }

        public override string ToString()
        {
            string estado = Ativa ? "ativa" : "encerrada";
            return $"Titular: {Titular} | Conta: {NumeroConta} | Saldo: {Saldo:F2} EUR | Estado {estado}";
        }
    }
}