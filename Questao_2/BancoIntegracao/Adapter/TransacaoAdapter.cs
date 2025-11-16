using System;
using System.Collections.Generic;
using BancoIntegracao.Legado;
using BancoIntegracao.Moderno;

namespace BancoIntegracao.Adapter
{
    // Adapter bidirecional que converte moderno ⇄ legado
    public class TransacaoAdapter : IProcessadorTransacoes
    {
        private readonly SistemaBancarioLegado _legado;

        public TransacaoAdapter(SistemaBancarioLegado legado)
        {
            _legado = legado;
        }

        // --------------- Mod -> Leg ---------------
        public string Autorizar(string cartao, double valor, string moeda)
        {
            var parametros = ConverterModernoParaLegado(cartao, valor, moeda);

            var respostaLegado = _legado.ProcessarTransacao(parametros);

            return ConverterLegadoParaModerno(respostaLegado);
        }

        // Converte interface moderna → formato legado
        private Dictionary<string, object> ConverterModernoParaLegado(string cartao, double valor, string moeda)
        {
            // Campo obrigatório do legado que não existe no moderno:
            // "canal" → deve ser preenchido manualmente.
            var parametros = new Dictionary<string, object>
            {
                { "numeroCartao", cartao },
                { "valor", valor },
                { "moedaCodigo", CodigoMoeda.Converter(moeda) },
                { "canal", "APP_MOBILE" } // campo obrigatório inexistente no moderno
            };

            return parametros;
        }

        // --------------- Leg -> Mod ---------------
        private string ConverterLegadoParaModerno(Dictionary<string, object> resposta)
        {
            string status = resposta["status"].ToString();
            double valor = Convert.ToDouble(resposta["valorProcessado"]);

            return $"Legado→Moderno: Status={status}, Valor={valor}";
        }
    }
}
