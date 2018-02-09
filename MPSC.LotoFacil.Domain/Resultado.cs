using System;
using System.Collections.Generic;

namespace MPSC.LotoFacil.Domain
{
	public class Resultado
	{
		public Resultado(Jogo jogo, IEnumerable<Int32> numerosSorteados)
		{
			Jogo = jogo;
			Acertos = jogo.ContarAcertos(numerosSorteados);
		}

		public Jogo Jogo { get; }
		public Decimal Aposta { get { return CartaoDeAposta.TabelaPrecos.TryGetValue(Jogo.QuantidadeDeNumeros, out var preco) ? preco : Decimal.Zero; } }
		public Int32 Acertos { get; }
		public Decimal Premio { get { return CartaoDeAposta.TabelaPremio.TryGetValue(Acertos, out var premio) ? premio : Decimal.Zero; } }
		public Decimal Lucro { get { return Premio - Aposta; } }
	}
}
