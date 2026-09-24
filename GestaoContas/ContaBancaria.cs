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
    }
}