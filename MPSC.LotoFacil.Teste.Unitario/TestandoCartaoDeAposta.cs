using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPSC.LotoFacil.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MPSC.LotoFacil.Teste.Unitario
{
	[TestClass]
	public class TestandoCartaoDeAposta
	{
		private static readonly Int32[] numerosSorteados = new[] { 01, 02, 04, 07, 09, 10, 11, 12, 13, 14, 18, 20, 21, 24, 25 };

		[TestMethod]
		public void QuandoSolicitaGeracaoDe200JogosDe15NumerosSemParametrosEspeciais()
		{
			QuandoSolicitaGeracaoDeJogos(
				quantidadeDeJogos: 200,
				quantidadeDeNumerosPorJogo: 15,
				sorteiosPorLinha: new Int32[] { 0, 0, 0, 0, 0 },
				numerosEscolhidos: new Int32[] { }
			);
		}

		[TestMethod]
		public void QuandoSolicitaGeracaoDe200JogosDe15NumerosDefinindoQtdDeSorteiosPorLinha()
		{
			QuandoSolicitaGeracaoDeJogos(
				quantidadeDeJogos: 200,
				quantidadeDeNumerosPorJogo: 15,
				sorteiosPorLinha: new Int32[] { 3, 3, 4, 2, 3 },
				numerosEscolhidos: new Int32[] { }
			);
		}

		[TestMethod]
		public void QuandoSolicitaGeracaoDe200JogosDe15NumerosSemParametrosEspeciaisEscolhendo1NumeroFavorito()
		{
			QuandoSolicitaGeracaoDeJogos(
				quantidadeDeJogos: 200,
				quantidadeDeNumerosPorJogo: 15,
				sorteiosPorLinha: new Int32[] { 0, 0, 0, 0, 0 },
				numerosEscolhidos: new Int32[] { 24 }
			);
		}

		[TestMethod]
		public void QuandoSolicitaGeracaoDe200JogosDe15NumerosDefinindoQtdDeSorteiosPorLinhaEscolhendo1NumeroFavorito()
		{
			QuandoSolicitaGeracaoDeJogos(
				quantidadeDeJogos: 200,
				quantidadeDeNumerosPorJogo: 15,
				sorteiosPorLinha: new Int32[] { 3, 3, 4, 2, 2 },
				numerosEscolhidos: new Int32[] { 24 }
			);
		}


		[TestMethod]
		public void QuandoSolicitaGeracaoDe200JogosDe15NumerosSemParametrosEspeciaisEscolhendo2NumerosFavoritos()
		{
			QuandoSolicitaGeracaoDeJogos(
				quantidadeDeJogos: 200,
				quantidadeDeNumerosPorJogo: 15,
				sorteiosPorLinha: new Int32[] { 0, 0, 0, 0, 0 },
				numerosEscolhidos: new Int32[] { 24, 25 }
			);
		}

		[TestMethod]
		public void QuandoSolicitaGeracaoDe200JogosDe15NumerosDefinindoQtdDeSorteiosPorLinhaEscolhendo2NumerosFavoritos()
		{
			QuandoSolicitaGeracaoDeJogos(
				quantidadeDeJogos: 200,
				quantidadeDeNumerosPorJogo: 15,
				sorteiosPorLinha: new Int32[] { 3, 3, 4, 2, 1 },
				numerosEscolhidos: new Int32[] { 24, 25 }
			);
		}

		public void QuandoSolicitaGeracaoDeJogos(Int32 quantidadeDeJogos, Int32 quantidadeDeNumerosPorJogo, IEnumerable<Int32> sorteiosPorLinha, IEnumerable<Int32> numerosEscolhidos)
		{
			var cartaoDeAposta = new CartaoDeAposta(quantidadeDeJogos: quantidadeDeJogos, quantidadeDeNumerosPorJogo: quantidadeDeNumerosPorJogo);
			var jogos = cartaoDeAposta.GerarJogos(sorteiosPorLinha, numerosEscolhidos);

			Assert.AreEqual(quantidadeDeJogos, jogos.Count);
			Assert.IsTrue(jogos.All(j => j.QuantidadeDeNumeros == quantidadeDeNumerosPorJogo));

			var resultados = jogos.Select(j => j.Conferir(numerosSorteados)).ToArray();
			Console.WriteLine("{0} - {1} = {2} ({3} jogos / {4} acertos)\r\n",
				resultados.Sum(r => r.Premio).ToString("0.00"),
				resultados.Sum(r => r.Aposta).ToString("0.00"),
				resultados.Sum(r => r.Lucro).ToString("0.00"),
				resultados.Length,
				resultados.Count(r => r.Lucro > 0)
			);

			foreach (var resultado in resultados.Where(r => r.Acertos >= 11).OrderByDescending(r => r.Acertos))
			{
				Console.WriteLine("{0} -> {1} -> {2}",
					resultado.Acertos.ToString("00"),
					resultado.Jogo.ToString(),
					resultado.Premio.ToString("0.00")
				);
			}
		}
	}
}