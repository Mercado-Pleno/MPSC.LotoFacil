using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPSC.LotoFacil.Domain;

namespace MPSC.LotoFacil.Teste.Unitario
{
	[TestClass]
	public class TestandoNumero
	{
		[TestMethod]
		public void QuandoSolicitaUmClone_DeveSerInstanciasDiferentes_ValoresIguais()
		{
			var numeroOriginal = new Numero(3);
			var numeroClonado = numeroOriginal.Clone();

			Assert.AreNotSame(numeroOriginal, numeroClonado, "Instâncias não são diferentes");
			Assert.AreEqual(numeroOriginal.Valor, numeroClonado.Valor, "Valores são diferentes");
		}

		[TestMethod]
		public void QuandoTestaNumerosIguais_DeveRetornarVerdadeiro()
		{
			var numero3A = new Numero(3);
			var numero3B = new Numero(3);
			var clone_3A = numero3A.Clone();
			var clone_3B = numero3B.Clone();

			Assert.IsTrue(numero3A.EhIgual(numero3A), "numero3A.EhIgual(numero3A)");
			Assert.IsTrue(numero3A.EhIgual(numero3B), "numero3A.EhIgual(numero3B)");
			Assert.IsTrue(numero3A.EhIgual(clone_3A), "numero3A.EhIgual(clone_3A)");
			Assert.IsTrue(numero3A.EhIgual(clone_3B), "numero3A.EhIgual(clone_3B)");

			Assert.IsTrue(numero3B.EhIgual(numero3A), "numero3B.EhIgual(numero3A)");
			Assert.IsTrue(numero3B.EhIgual(numero3B), "numero3B.EhIgual(numero3B)");
			Assert.IsTrue(numero3B.EhIgual(clone_3A), "numero3B.EhIgual(clone_3A)");
			Assert.IsTrue(numero3B.EhIgual(clone_3B), "numero3B.EhIgual(clone_3B)");

			Assert.IsTrue(clone_3A.EhIgual(numero3A), "clone_3A.EhIgual(numero3A)");
			Assert.IsTrue(clone_3A.EhIgual(numero3B), "clone_3A.EhIgual(numero3B)");
			Assert.IsTrue(clone_3A.EhIgual(clone_3A), "clone_3A.EhIgual(clone_3A)");
			Assert.IsTrue(clone_3A.EhIgual(clone_3B), "clone_3A.EhIgual(clone_3B)");

			Assert.IsTrue(clone_3B.EhIgual(numero3A), "clone_3B.EhIgual(numero3A)");
			Assert.IsTrue(clone_3B.EhIgual(numero3B), "clone_3B.EhIgual(numero3B)");
			Assert.IsTrue(clone_3B.EhIgual(clone_3A), "clone_3B.EhIgual(clone_3A)");
			Assert.IsTrue(clone_3B.EhIgual(clone_3B), "clone_3B.EhIgual(clone_3B)");
		}

		[TestMethod]
		public void QuandoTestaNumerosDiferentes_DeveRetornarFalso()
		{
			var numero3A = new Numero(3);
			var numero3B = new Numero(5);
			var clone_3A = numero3A.Clone();
			var clone_3B = numero3B.Clone();

			Assert.IsFalse(numero3A.EhIgual(numero3B), "numero3A.EhIgual(numero3B)");
			Assert.IsFalse(numero3A.EhIgual(clone_3B), "numero3A.EhIgual(clone_3B)");

			Assert.IsFalse(numero3B.EhIgual(numero3A), "numero3B.EhIgual(numero3A)");
			Assert.IsFalse(numero3B.EhIgual(clone_3A), "numero3B.EhIgual(clone_3A)");
		}
	}
}