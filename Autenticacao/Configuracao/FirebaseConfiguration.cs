using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

namespace RNovaTech.Configuracao
{
    // Refatorar a Configuração aqui nao esta funcionando 
    public static class FirebaseConfiguration
    {
        public static void FirebaseConnectConfig()
        {
            var optionsDesenvolvimento = new AppOptions
            {
                Credential = GoogleCredential.FromFile(""),
                ProjectId = "autenticacao-29915"
            };

            FirebaseApp.Create(optionsDesenvolvimento);
        }
    }
}
