namespace AplicacaoWebApi.Features.Shared
{
    public class CustomResult
    {
        public bool Sucesso { get; private set; }
        public List<string>? Erros { get; private set; }
        public object? Dados { get; private set; }

        public CustomResult(bool sucesso, object? dados, List<string>? erros)
        {
            Sucesso = sucesso;
            Dados = dados;
            Erros = erros;
        }

        public CustomResult(bool sucesso, List<string> erros) : this(sucesso, null, erros) { }
        public CustomResult(bool sucesso, object dados) : this(sucesso, dados,null) { }
        public CustomResult(bool sucesso) : this(sucesso, null, null) { }
    }
}
