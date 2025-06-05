namespace ProyectoFinalProgra.Models
{
    public class NodoABB
    {
        public int Valor { get; set; }
        public NodoABB? Izquierdo { get; set; }
        public NodoABB? Derecho { get; set; }

        public NodoABB(int valor)
        {
            Valor = valor;
            Izquierdo = null;
            Derecho = null;
        }
    }
}
