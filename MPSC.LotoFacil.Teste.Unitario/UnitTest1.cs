using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPSC.LotoFacil.Domain;
using System;
using System.Linq;

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

	[TestClass]
	public class TestandoCartaoDeAposta
	{

		[TestMethod]
		public void QuandoSolicitaGeracaoDeJogos()
		{
			var quantidadeDeJogos = 400;
			var quantidadeDeNumerosPorJogo = 15;
			var sorteiosPorLinha = new Int32[] { 3, 2, 4, 0, 0 };
			var numerosEscolhidos = new Numero[]
			{
				new Numero(20),
				new Numero(25),
			};

			var cartaoDeAposta = new CartaoDeAposta(quantidadeDeJogos: quantidadeDeJogos, quantidadeDeNumerosPorJogo: quantidadeDeNumerosPorJogo);
			var jogos = cartaoDeAposta.GerarJogos(sorteiosPorLinha, numerosEscolhidos);

			Assert.AreEqual(quantidadeDeJogos, jogos.Count);
			Assert.IsTrue(jogos.All(j => j.QuantidadeDeNumeros == quantidadeDeNumerosPorJogo));

			//foreach (var jogo in jogos) Console.WriteLine(jogo.ToString());

			var resultado = new[] { 01, 02, 04, 07, 09, 10, 11, 12, 13, 14, 18, 20, 21, 24, 25 };

			var acertos = jogos.Where(j => j.Conferir(resultado) > 0);
			Console.WriteLine("{0}\r\n\r\n", acertos.Sum(j => j.Conferir(resultado)).ToString("0.00"));


			foreach (var jogo in acertos)
				Console.WriteLine("{0} -> {1}", jogo.Conferir(resultado).ToString("0.00"), jogo.ToString());
		}
	}
}