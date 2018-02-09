using System;
using System.Collections.Generic;
using System.Linq;

namespace MPSC.LotoFacil.Domain
{
	public class Jogo
	{
		private readonly List<Int32> _numeros;
		private IEnumerable<Int32> Numeros { get { return _numeros; } }
		public Int32 QuantidadeDeNumeros { get { return _numeros.Count; } }

		public Jogo(IEnumerable<Int32> numerosEscolhidos)
		{
			_numeros = new List<Int32>(numerosEscolhidos);
		}

		public Boolean Igual(Jogo outroJogo)
		{
			return _numeros.All(nA => outroJogo._numeros.Any(nA.Equals));
		}

		public void Adicionar(Int32 numero)
		{
			if (!_numeros.Any(numero.Equals))
				_numeros.Add(numero);
		}

		public override String ToString()
		{
			return String.Join(" - ", _numeros.OrderBy(n => n).Select(n => n.ToString("00")));
		}

		public Resultado Conferir(IEnumerable<Int32> numerosSorteados)
		{
			return new Resultado(this, numerosSorteados);
		}

		public Int32 ContarAcertos(IEnumerable<Int32> numerosSorteados)
		{
			return _numeros.Count(nE => numerosSorteados.Any(nE.Equals));
		}
	}
}
