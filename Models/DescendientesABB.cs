namespace ProyectoFinalProgra.Models
{
    public class NodoDescendiente
    {
        public int Valor { get; set; }
        public NodoDescendiente? Siguiente { get; set; }

        public NodoDescendiente(int valor)
        {
            Valor = valor;
            Siguiente = null;
        }
    }

    public class DescendientesABB
    {
        public NodoDescendiente? Primero { get; set; }
        private NodoDescendiente? Ultimo { get; set; }

        public void Agregar(int valor)
        {
            NodoDescendiente nuevo = new NodoDescendiente(valor);
            if (Primero == null)
            {
                Primero = nuevo;
                Ultimo = nuevo;
            }
            else
            {
                Ultimo!.Siguiente = nuevo;
                Ultimo = nuevo;
            }
        }

        public bool Contiene(int valor)
        {
            NodoDescendiente? actual = Primero;
            while (actual != null)
            {
                if (actual.Valor == valor) return true;
                actual = actual.Siguiente;
            }
            return false;
        }

        public string Mostrar()
        {
            string resultado = "";
            NodoDescendiente? actual = Primero;
            while (actual != null)
            {
                resultado += actual.Valor + (actual.Siguiente != null ? ", " : "");
                actual = actual.Siguiente;
            }
            return resultado;
        }
    }
}

