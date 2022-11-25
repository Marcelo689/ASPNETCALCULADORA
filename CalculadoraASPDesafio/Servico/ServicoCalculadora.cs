using Servico.TO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Servico
{
    public class ServicoCalculadora : FuncaoCalculadora
    {
        public decimal Somar(decimal num1, decimal num2)
        {
            return num1 + num2;
        }
        public decimal Subtrair(decimal num1, decimal num2)
        {
            return num1 - num2;
        }
        public decimal Multiplicar(decimal num1, decimal num2)
        {
            return num1 * num2;
        }
        public decimal Dividir(decimal num1, decimal num2)
        {
            return num1 / num2;
        }
        public decimal Calcular(decimal num1, decimal num2, string operacao)
        {
            switch (operacao)
            {
                case "+":
                    return Somar(num1, num2);
                case "-":
                    return Subtrair(num1, num2);
                case "*":
                    return Multiplicar(num1, num2);
                case "/":
                    return Dividir(num1, num2);
            }

            return 0;
        }
        public int TrataCasoMenosUm(int indice, string entrada)
        {
            if (indice == -1)
                return entrada.Length;
            else
                return indice;
        }
        public int RetornaIndiceMenor(int indice1, int indice2, string entrada)
        {
            if (TrataCasoMenosUm(indice1, entrada) < TrataCasoMenosUm(indice2, entrada))
                return indice1;
            else
                return indice2;

        }
        public int RetornaIndicePrioridade(string entrada)
        {
            int naoEncontrado = -1;
            int indiceTempMult = entrada.IndexOf("*",1);
            int indiceTempDiv = entrada.IndexOf("/", 1);
            int indiceTempSoma = entrada.IndexOf("+", 1);
            int indiceTempSubt = entrada.IndexOf("-", 1);

            if (indiceTempMult != naoEncontrado || indiceTempDiv != naoEncontrado)
                return RetornaIndiceMenor(indiceTempMult, indiceTempDiv, entrada);
            else
            if (indiceTempSoma != naoEncontrado || indiceTempSubt != naoEncontrado)
                return RetornaIndiceMenor(indiceTempSoma, indiceTempSubt, entrada);
            else
                return -1;

        }
        public bool PossuiSinal(string entrada)
        {
            var sinais = new string[] { "*", "/", "+", "-" };

            foreach (var sinal in sinais)
            {
                if (entrada.Contains(sinal))
                    return true;
            }
            return false;
        }

        public bool IgnoraCarcactere(string carc,int indice, string entrada)
        {
            if (PossuiSinal(carc) && indice == entrada.Length - 1)
                return true;
            if ((carc == "*" || carc == "/") && indice == 0)
                return true;
            return false;

        }
        public string FormataStringParaCalculo(string entrada)
        {

            var sinais = new string[] { "*", "/", "+", "-" };
            var indice = 0;
            string saida = "";
            foreach (var c in entrada)
            {
                var carc = c.ToString();
                if(!IgnoraCarcactere(carc,indice,entrada))
                    saida += c;
                indice++;
            }

            return saida;
        }

        public bool CaractereValidoNumero(string teste,int indice )
        {
            if ("." == teste)
                return true;
            else if ("+" == teste && indice == 0)
                return true;
            else if ("-" == teste && indice == 0)
                return true;
            else
                return false;
        }
        public string StringParaCalcular(int indice, string entrada)
        {
            //entrada
            entrada = FormataStringParaCalculo(entrada);
            string sinal = entrada[indice].ToString();
            string esquerda = "";
            string direita = "";
            var apenasNumeros = new Regex(@"^\d+$");
            for(int i = indice-1; i >= 0; --i)
            {
                string carcactere = entrada[i].ToString();
                if (apenasNumeros.IsMatch(carcactere) || CaractereValidoNumero(carcactere,i))
                    esquerda += carcactere;
                else
                    break;
            }
            var esquerdaArray = esquerda.Reverse().ToArray();
            esquerda = new string(esquerdaArray);
            for (int i = indice+1; i <= entrada.Length -1; i++)
            {
                string carcactere = entrada[i].ToString();
                if (apenasNumeros.IsMatch(carcactere) || CaractereValidoNumero(carcactere,i))
                    direita += carcactere;
                else
                    break;
            }

            string stringCalculada = esquerda + sinal + direita;
            decimal numeroEsquerdo = decimal.Parse(esquerda,CultureInfo.InvariantCulture);
            decimal numeroDireito = decimal.Parse(direita, CultureInfo.InvariantCulture);

            var resultadoCalculo = Calcular(numeroEsquerdo, numeroDireito, sinal);
            string saida = entrada.Replace(stringCalculada, resultadoCalculo.ToString());

            var retorno = new CalculaParte()
            {
                numeroEsquerdo = numeroEsquerdo,
                numeroDireito = numeroDireito,
                stringCalculada = stringCalculada,
                antesCalculo = entrada,
                aposCalculo = saida
            };
            saida = saida.Replace(",", ".");

            return saida;
        }
        public string CalcularTudo(string entrada)
        {
            // 3+3*2
            int indice = -1;

            while (PossuiSinal(entrada))
            {
                indice = RetornaIndicePrioridade(entrada);
                entrada = StringParaCalcular(indice, entrada);
            }
            var resultadoDecimal = decimal.Parse(entrada,CultureInfo.InvariantCulture);
            var resultado = String.Format("{0:0.0#}", resultadoDecimal);

            resultado = resultado.Replace(",", ".");
            return resultado;
        }

    }
}
