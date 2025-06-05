namespace ProyectoFinalProgra.Models
{
    public class Descendientes
    {
        public int? HijoIzquierdo { get; set; }
        public int? HijoDerecho { get; set; }

        public Descendientes()
        {
            HijoIzquierdo = null;
            HijoDerecho = null;
        }
    }
}
