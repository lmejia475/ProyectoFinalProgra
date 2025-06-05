namespace ProyectoFinalProgra.Models
{
    public class RecorridoABB
    {
        public NodoRecorrido? Primero { get; set; }
        private NodoRecorrido? Ultimo { get; set; }

        public void Agregar(int valor)
        {
            NodoRecorrido nuevo = new NodoRecorrido(valor);
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

        public string Mostrar()
        {
            string resultado = "";
            NodoRecorrido? actual = Primero;
            while (actual != null)
            {
                resultado += actual.Valor + (actual.Siguiente != null ? ", " : "");
                actual = actual.Siguiente;
            }
            return resultado;
        }




        public bool Contiene(int valor)
        {
            NodoRecorrido? actual = Primero;
            while (actual != null)
            {
                if (actual.Valor == valor)
                    return true;
                actual = actual.Siguiente;
            }
            return false;
        }

        public void Vaciar()
        {
            Primero = null;
            Ultimo = null;
        }
    }
}

