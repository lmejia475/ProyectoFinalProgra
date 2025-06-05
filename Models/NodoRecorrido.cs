namespace ProyectoFinalProgra.Models
{
    public class NodoRecorrido
    {
        public int Valor { get; set; }
        public NodoRecorrido? Siguiente { get; set; }

        public NodoRecorrido(int valor)
        {
            Valor = valor;
            Siguiente = null;
        }
    }
}
