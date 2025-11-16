using System;
using System.Collections.Generic;

namespace BancoIntegracao.Legado
{
    // Simula o sistema bancário antigo (dummy)
    public class SistemaBancarioLegado
    {
        public Dictionary<string, object> ProcessarTransacao(Dictionary<string, object> parametros)
        {
            // Mock de resposta do legado
            return new Dictionary<string, object>
            {
                { "status", "APROVADO" },
                { "codigo", 200 },
                { "valorProcessado", parametros["valor"] }
            };
        }
    }
}
