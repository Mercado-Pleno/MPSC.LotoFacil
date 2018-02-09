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
			var numerosSorteados = new[] { 01, 02, 04, 07, 09, 10, 11, 12, 13, 14, 18, 20, 21, 24, 25 };
			var quantidadeDeJogos = 500;
			var quantidadeDeNumerosPorJogo = 15;
			var sorteiosPorLinha = new Int32[] { 3, 2, 4, 0, 0 };
			var numerosEscolhidos = new Int32[] { 18, 20, 21, 24, 25 };

			var cartaoDeAposta = new CartaoDeAposta(quantidadeDeJogos: quantidadeDeJogos, quantidadeDeNumerosPorJogo: quantidadeDeNumerosPorJogo);
			var jogos = cartaoDeAposta.GerarJogos(sorteiosPorLinha, numerosEscolhidos);

			Assert.AreEqual(quantidadeDeJogos, jogos.Count);
			Assert.IsTrue(jogos.All(j => j.QuantidadeDeNumeros == quantidadeDeNumerosPorJogo));


			var resultados = jogos.Select(j => j.Conferir(numerosSorteados)).ToArray();
			Console.WriteLine("{0} - {1} = {2}\r\n",
				resultados.Sum(r => r.Premio).ToString("0.00"),
				resultados.Sum(r => r.Aposta).ToString("0.00"),
				resultados.Sum(r => r.Lucro).ToString("0.00")
			);

			foreach (var resultado in resultados.Where(r => r.Acertos > 10))
				Console.WriteLine("{0} -> {1}", resultado.Premio.ToString("0.00"), resultado.Jogo.ToString());
		}
	}
}