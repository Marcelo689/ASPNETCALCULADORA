using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servico
{
    public interface FuncaoCalculadora
    {
        decimal Somar(decimal num1, decimal num2);
        decimal Subtrair(decimal num1, decimal num2);
        decimal Multiplicar(decimal num1, decimal num2);
        decimal Dividir(decimal num1, decimal num2);
        decimal Calcular(decimal num1, decimal num2, string operacao);
        string CalcularTudo(string entrada);
    }
}
