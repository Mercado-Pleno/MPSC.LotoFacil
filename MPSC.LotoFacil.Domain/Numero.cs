using System;
using System.Linq;

namespace MPSC.LotoFacil.Domain
{
	public class Numero
	{
		public readonly Int32 Valor;
		public Boolean Marcado { get; set; } = false;

		public Numero(Int32 valor)
		{
			Valor = valor;
		}

		public Numero Clone()
		{
			return new Numero(Valor);
		}

		public Boolean EhIgual(Numero numero)
		{
			return (numero.Valor == Valor);
		}
	}
}