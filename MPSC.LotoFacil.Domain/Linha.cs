using System;
using System.Linq;

namespace MPSC.LotoFacil.Domain
{
	public class Linha
	{
		public readonly Int32 Index;
		public readonly Int32[] Numeros;

		public Linha(Int32 index, Int32 quantidadeDeNumerosPorLinha)
		{
			Index = index;
			var min = Index * quantidadeDeNumerosPorLinha;
			var max = (Index + 1) * quantidadeDeNumerosPorLinha;
			Numeros = Enumerable.Range(min, max).ToArray();
		}

		public static Linha[] GerarCartao(Int32 linhas, Int32 colunas)
		{
			return Enumerable.Range(0, linhas).Select(i => new Linha(i, colunas)).ToArray();
		}
	}
}