using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPSC.LotoFacil.Domain;
using System;
using System.Linq;

namespace MPSC.LotoFacil.Teste.Unitario
{
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