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

		private readonly Int32 Linhas;
		private readonly Int32 Colunas;
		private readonly Int32 QuantidadeDeJogos;
		private readonly Int32 QuantidadeDeNumerosPorJogo;

		public CartaoDeAposta(Int32 quantidadeDeJogos, Int32 quantidadeDeNumerosPorJogo, Int32 linhas = 5, Int32 colunas = 5)
		{
			Linhas = linhas;
			Colunas = colunas;
			QuantidadeDeJogos = quantidadeDeJogos;
			QuantidadeDeNumerosPorJogo = quantidadeDeNumerosPorJogo;
		}

		public List<Jogo> GerarJogos(IEnumerable<Int32> sorteiosPorLinha, IEnumerable<Int32> numerosEscolhidos)
		{
			var random = new Random(Convert.ToInt32(DateTime.Now.Ticks % 10000000));
			var listaJogos = new List<Jogo>();

			while (listaJogos.Count < QuantidadeDeJogos)
			{
				var novoJogo = GerarJogo(sorteiosPorLinha.ToArray(), numerosEscolhidos, random);
				if (!listaJogos.Any(j => j.Igual(novoJogo)))
					listaJogos.Add(novoJogo);
			}

			return listaJogos.OrderBy(j => j.ToString()).ToList();
		}

		private Jogo GerarJogo(Int32[] sorteiosPorLinha, IEnumerable<Int32> numerosEscolhidos, Random random)
		{
			var jogo = new Jogo(numerosEscolhidos);

			while (jogo.QuantidadeDeNumeros < QuantidadeDeNumerosPorJogo)
			{
				var linha = SortearLinha(random);

				if (LinhaAindaPodeSerSorteada(sorteiosPorLinha, linha))
				{
					ReduzirQuantidadeDisponivelParaSorteio(sorteiosPorLinha, linha);
					var numero = SortearNumero(random, linha);
					jogo.Adicionar(numero);
				}
			}

			return jogo;
		}

		private Int32 SortearLinha(Random random)
		{
			return random.Next(0, Linhas);
		}

		private Int32 SortearNumero(Random random, Int32 linha)
		{
			var coluna = random.Next(0, Colunas);
			return (linha * Linhas) + (coluna + 1);
		}

		private Boolean LinhaAindaPodeSerSorteada(Int32[] sorteiosPorLinha, Int32 linha)
		{
			return PossuiAlgumNumeroDisponivelParaSorteio(sorteiosPorLinha, linha) ||
			(
				ALinhaPermiteSerSorteada(sorteiosPorLinha, linha) &&
				AsLinhasNaoPossuemOutrosNumerosDisponiveisParaSorteio(sorteiosPorLinha)
			);
		}

		private Boolean PossuiAlgumNumeroDisponivelParaSorteio(Int32[] sorteiosPorLinha, Int32 linha)
		{
			return (sorteiosPorLinha[linha] > 0);
		}

		private Boolean ALinhaPermiteSerSorteada(Int32[] sorteiosPorLinha, Int32 linha)
		{
			return (sorteiosPorLinha[linha] >= 0);
		}

		private Boolean AsLinhasNaoPossuemOutrosNumerosDisponiveisParaSorteio(Int32[] sorteiosPorLinha)
		{
			return sorteiosPorLinha.All(q => q <= 0);
		}

		private void ReduzirQuantidadeDisponivelParaSorteio(Int32[] sorteiosPorLinha, Int32 linha)
		{
			if (PossuiAlgumNumeroDisponivelParaSorteio(sorteiosPorLinha, linha))
				sorteiosPorLinha[linha]--;
		}
	}
}