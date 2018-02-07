using System;
using System.Collections.Generic;
using System.Linq;

namespace MPSC.LotoFacil.Domain
{
	public class CartaoDeAposta
	{
		public static readonly Decimal[] TabelaPremio = new[] { 0M, 0M, 0M, 0M, 0M, 0M, 0M, 0M, 0M, 0M, 0M, 4M, 8M, 20M, 1000M, 10000M };
		private readonly Linha[] Linhas;
		private readonly Int32 QuantidadeDeJogos;
		private readonly Int32 QuantidadeDeNumerosPorJogo;
		private Int32[] SorteiosPorLinha;
		private Numero[] NumerosEscolhidos;

		public CartaoDeAposta(Int32 quantidadeDeJogos, Int32 quantidadeDeNumerosPorJogo, Int32 linhas = 5, Int32 colunas = 5)
		{
			Linhas = Linha.GerarCartao(linhas, colunas);
			QuantidadeDeJogos = quantidadeDeJogos;
			QuantidadeDeNumerosPorJogo = quantidadeDeNumerosPorJogo;
		}

		public List<Jogo> GerarJogos(IEnumerable<Int32> sorteiosPorLinha, IEnumerable<Numero> numerosEscolhidos)
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

				var sort = rand.Next(1, 26);

				jogo.Adicionar(new Numero(sort));
			}

			return jogo;
		}
	}

	public class Linha
	{
		public readonly Int32 Index;
		public readonly Numero[] Numeros;

		public Linha(Int32 index, Int32 quantidadeDeNumerosPorLinha)
		{
			Index = index;
			var min = Index * quantidadeDeNumerosPorLinha;
			var max = (Index + 1) * quantidadeDeNumerosPorLinha;
			Numeros = Enumerable.Range(min, max).Select(i => new Numero(i)).ToArray();
		}

		public static Linha[] GerarCartao(Int32 linhas, Int32 colunas)
		{
			return Enumerable.Range(0, linhas).Select(i => new Linha(i, colunas)).ToArray();
		}
	}


	public class Jogo
	{
		private readonly List<Numero> _numeros;
		private IEnumerable<Numero> Numeros { get { return _numeros; } }
		public Int32 QuantidadeDeNumeros { get { return _numeros.Count; } }

		public Jogo(IEnumerable<Numero> numerosEscolhidos)
		{
			_numeros = new List<Numero>(numerosEscolhidos);
		}

		public Boolean Igual(Jogo novoJogo)
		{
			return _numeros.All(nA => novoJogo._numeros.Any(nA.EhIgual));
		}

		public void Adicionar(Numero numero)
		{
			if (!_numeros.Any(numero.EhIgual))
				_numeros.Add(numero);
		}

		public override String ToString()
		{
			return String.Join(" - ", _numeros.OrderBy(n => n.Valor).Select(n => n.Valor.ToString("00")));
		}

		public Decimal Conferir(IEnumerable<Int32> resultado)
		{
			var acertos = Conferir(resultado.Select(n => new Numero(n)));
			var premio = CartaoDeAposta.TabelaPremio[acertos];
			return premio;
		}

		public Int32 Conferir(IEnumerable<Numero> resultado)
		{
			return _numeros.Count(nA => resultado.Any(nA.EhIgual));
		}
	}
}
