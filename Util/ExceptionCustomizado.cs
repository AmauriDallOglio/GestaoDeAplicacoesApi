namespace GestaoDeAplicacoesApi.Util
{

    public class ExceptionCustomizado : Exception
    {
        public string Codigo { get; }

        public ExceptionCustomizado(string message, string codigo) : base(message)
        {
            Codigo = codigo;
        }

        public ExceptionCustomizado(string message, string codigo, Exception inner) : base(message, inner)
        {
            Codigo = codigo;
        }
    }
}
