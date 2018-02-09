using System;
using System.Collections.Generic;
using System.Linq;

namespace MPSC.LotoFacil.Domain
{
	public class CartaoDeAposta
	{
		public static readonly Dictionary<Int32, Decimal> TabelaPrecos =
			new Dictionary<Int32, Decimal>() { { 15, 2M }, { 16, 32M }, { 17, 272M }, { 18, 1632M } };

		public static readonly Dictionary<Int32, Decimal> TabelaPremio =
			new Dictionary<Int32, Decimal>() { { 11, 4M }, { 12, 08M }, { 13, 020M }, { 14, 3000M }, { 15, 50000M } };

		private readonly Linha[] Linhas;
		private readonly Int32 QuantidadeDeJogos;
		private readonly Int32 QuantidadeDeNumerosPorJogo;
		private Int32[] SorteiosPorLinha;
		private Int32[] NumerosEscolhidos;

		public CartaoDeAposta(Int32 quantidadeDeJogos, Int32 quantidadeDeNumerosPorJogo, Int32 linhas = 5, Int32 colunas = 5)
		{
			Linhas = Linha.GerarCartao(linhas, colunas);
			QuantidadeDeJogos = quantidadeDeJogos;
			QuantidadeDeNumerosPorJogo = quantidadeDeNumerosPorJogo;
		}

		public List<Jogo> GerarJogos(IEnumerable<Int32> sorteiosPorLinha, IEnumerable<Int32> numerosEscolhidos)
		{
			var listaJogos = new List<Jogo>();
			SorteiosPorLinha = sorteiosPorLinha.ToArray();
			NumerosEscolhidos = numerosEscolhidos.ToArray();

			while (listaJogos.Count < QuantidadeDeJogos)
			{
				var novoJogo = GerarJogo();
				if (!listaJogos.Any(j => j.Igual(novoJogo)))
					listaJogos.Add(novoJogo);
			}

			return listaJogos.OrderBy(j => j.ToString()).ToList();
		}

		private Jogo GerarJogo()
		{
			var jogo = new Jogo(NumerosEscolhidos);
			var rand = new Random(Convert.ToInt32(DateTime.Now.Ticks % 10000000));
			while (jogo.QuantidadeDeNumeros < QuantidadeDeNumerosPorJogo)
			{
				//var linha = rand.Next(0, Linhas.Length);
				//var coluna = rand.Next(0, Linhas[linha].Numeros.Length);

				var numero = rand.Next(1, 26);

				jogo.Adicionar(numero);
			}
			return jogo;
		}
	}
}