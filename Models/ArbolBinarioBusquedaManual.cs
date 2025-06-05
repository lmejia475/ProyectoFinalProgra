using Microsoft.Extensions.Options;
using System.Data;

namespace ProyectoFinalProgra.Models
{
    public class ArbolBinarioBusquedaManual
    {
        public NodoABB? Raiz { get; set; }

        public NodoABB? Raiz2 { get; set; }

        public void Insertar(int valor)
        {
            Raiz = InsertarRecursivo(Raiz, valor);
        }

        public void Insertar2(int valor)
        {
            Raiz2 = InsertarRecursivo(Raiz2, valor);
        }

        private NodoABB InsertarRecursivo(NodoABB? nodo, int valor)
        {
            if (nodo == null)
                return new NodoABB(valor);

            if (valor < nodo.Valor)
                nodo.Izquierdo = InsertarRecursivo(nodo.Izquierdo, valor);
            else if (valor > nodo.Valor)
                nodo.Derecho = InsertarRecursivo(nodo.Derecho, valor);

            return nodo;
        }

        public bool Buscar(int valor)
        {
            return BuscarRecursivo(Raiz, valor);
        }

        private bool BuscarRecursivo(NodoABB? nodo, int valor)
        {
            if (nodo == null)
                return false;
            if (nodo.Valor == valor)
                return true;

            return valor < nodo.Valor
                ? BuscarRecursivo(nodo.Izquierdo, valor)
                : BuscarRecursivo(nodo.Derecho, valor);
        }

        private NodoABB? BuscarNodo(NodoABB? nodo, int valor)
        {
            if (nodo == null) return null;
            if (nodo.Valor == valor) return nodo;

            return valor < nodo.Valor
                ? BuscarNodo(nodo.Izquierdo, valor)
                : BuscarNodo(nodo.Derecho, valor);
        }

        private NodoABB? EliminarRecursivo(NodoABB? nodo, int valor, out bool eliminado)
        {
            eliminado = false;

            if (nodo == null)
                return null;

            if (valor < nodo.Valor)
            {
                nodo.Izquierdo = EliminarRecursivo(nodo.Izquierdo, valor, out eliminado);
            }
            else if (valor > nodo.Valor)
            {
                nodo.Derecho = EliminarRecursivo(nodo.Derecho, valor, out eliminado);
            }
            else
            {
                eliminado = true;

                if (nodo.Izquierdo == null)
                    return nodo.Derecho;

                if (nodo.Derecho == null)
                    return nodo.Izquierdo;

                NodoABB sucesor = EncontrarMinimo(nodo.Derecho);
                nodo.Valor = sucesor.Valor;

                bool eliminadoSubarbol;
                nodo.Derecho = EliminarRecursivo(nodo.Derecho, sucesor.Valor, out eliminadoSubarbol);
            }

            return nodo;
        }

        private NodoABB EncontrarMinimo(NodoABB nodo)
        {
            while (nodo.Izquierdo != null)
                nodo = nodo.Izquierdo;
            return nodo;
        }

        public bool EliminarConResultado(int valor)
        {
            bool eliminado;
            Raiz = EliminarRecursivo(Raiz, valor, out eliminado);
            return eliminado;
        }

        public void RecorridoPreOrden(NodoABB? nodo, RecorridoABB recorrido)
        {
            if (nodo != null)
            {
                recorrido.Agregar(nodo.Valor);
                RecorridoPreOrden(nodo.Izquierdo, recorrido);
                RecorridoPreOrden(nodo.Derecho, recorrido);
            }
        }

        public void RecorridoInOrden(NodoABB? nodo, RecorridoABB recorrido)
        {
            if (nodo != null)
            {
                RecorridoInOrden(nodo.Izquierdo, recorrido);
                recorrido.Agregar(nodo.Valor);
                RecorridoInOrden(nodo.Derecho, recorrido);
            }
        }

        public void RecorridoPostOrden(NodoABB? nodo, RecorridoABB recorrido)
        {
            if (nodo != null)
            {
                RecorridoPostOrden(nodo.Izquierdo, recorrido);
                RecorridoPostOrden(nodo.Derecho, recorrido);
                recorrido.Agregar(nodo.Valor);
            }
        }

        public Descendientes ObtenerDescendientes(int valor)
        {
            NodoABB? nodo = BuscarNodo(Raiz, valor);
            Descendientes d = new Descendientes();

            if (nodo != null)
            {
                if (nodo.Izquierdo != null)
                    d.HijoIzquierdo = nodo.Izquierdo.Valor;

                if (nodo.Derecho != null)
                    d.HijoDerecho = nodo.Derecho.Valor;
            }

            return d;
        }

        public DescendientesABB ObtenerDescendientesTodos(int valor)
        {
            NodoABB? nodo = BuscarNodo(Raiz, valor);
            DescendientesABB descendientes = new();
            if (nodo != null)
            {
                ObtenerDescendientesRecursivo(nodo.Izquierdo, descendientes);
                ObtenerDescendientesRecursivo(nodo.Derecho, descendientes);
            }
            return descendientes;
        }

        private void ObtenerDescendientesRecursivo(NodoABB? nodo, DescendientesABB lista)
        {
            if (nodo == null)
                return;

            lista.Agregar(nodo.Valor);
            ObtenerDescendientesRecursivo(nodo.Izquierdo, lista);
            ObtenerDescendientesRecursivo(nodo.Derecho, lista);
        }

        public int? ObtenerPadre(int valor)
        {
            return ObtenerPadreRecursivo(Raiz, valor, null);
        }

        private int? ObtenerPadreRecursivo(NodoABB? nodo, int valor, NodoABB? padre)
        {
            if (nodo == null) return null;

            if (nodo.Valor == valor)
                return padre?.Valor;

            if (valor < nodo.Valor)
                return ObtenerPadreRecursivo(nodo.Izquierdo, valor, nodo);
            else
                return ObtenerPadreRecursivo(nodo.Derecho, valor, nodo);
        }

        public RecorridoABB ObtenerNodosHoja()
        {
            RecorridoABB hojas = new();
            ObtenerHojasRecursivo(Raiz, hojas);
            return hojas;
        }

        public string EliminarNodosH() { 
            EliminarNodosHoja(Raiz);
            return "Nodos hoja eliminados correctamente.";
        }

        private void EliminarNodosHoja(NodoABB nodo)
        {
            if (nodo == null)
                return;

            if (nodo.Izquierdo != null)
            {
                if (nodo.Izquierdo.Izquierdo == null && nodo.Izquierdo.Derecho == null)
                    nodo.Izquierdo = null; 
                else
                    EliminarNodosHoja(nodo.Izquierdo);
            }

            if (nodo.Derecho != null)
            {
                if (nodo.Derecho.Izquierdo == null && nodo.Derecho.Derecho == null)
                    nodo.Derecho = null;
                else
                    EliminarNodosHoja(nodo.Derecho); 
            }
        }

        public bool verSimilar()
        {
            return SonSimilares(Raiz, Raiz2);
        }

        public static bool SonSimilares(NodoABB? a, NodoABB? b)
        {
            if (a == null && b == null) return true;
            if (a == null || b == null) return false;


            return SonSimilares(a.Izquierdo, b.Izquierdo) &&
                   SonSimilares(a.Derecho, b.Derecho);
        }

        public bool verEquivalente()
        {
            return SonEquivalentes(Raiz, Raiz2);
        }

        public static bool SonEquivalentes(NodoABB? a, NodoABB? b)
        {
            if (a == null && b == null) return true;
            if (a == null || b == null) return false;
            if (a.Valor != b.Valor) return false;      

            return SonEquivalentes(a.Izquierdo, b.Izquierdo) &&
                   SonEquivalentes(a.Derecho, b.Derecho);
        }

        public bool verDistintos() {
            return SonDistintos(Raiz, Raiz2);
        }

        public static bool SonDistintos(NodoABB? a, NodoABB? b)
        {
            return !SonEquivalentes(a, b);
        }

        public void IntercambiarSubArboles()
        {
            Intercambiar(Raiz);
        }

        private void Intercambiar(NodoABB? nodo)
        {
            if (nodo == null) return;

            NodoABB? tmp = nodo.Izquierdo;
            nodo.Izquierdo = nodo.Derecho;
            nodo.Derecho = tmp;

            Intercambiar(nodo.Izquierdo);
            Intercambiar(nodo.Derecho);
        }

        public int CalcularLongitudCaminoExterno()
        {
            return LCE(Raiz, 0);   
        }

        private int LCE(NodoABB? nodo, int nivel)
        {

            if (nodo == null)
                return nivel;


            return LCE(nodo.Izquierdo, nivel + 1) +
                   LCE(nodo.Derecho, nivel + 1);
        }






        private void ObtenerHojasRecursivo(NodoABB? nodo, RecorridoABB lista)
        {
            if (nodo == null)
                return;

            if (nodo.Izquierdo == null && nodo.Derecho == null)
            {
                lista.Agregar(nodo.Valor);
            }
            else
            {
                ObtenerHojasRecursivo(nodo.Izquierdo, lista);
                ObtenerHojasRecursivo(nodo.Derecho, lista);
            }
        }

        public RecorridoABB ObtenerNodosInteriores()
        {
            RecorridoABB interiores = new();
            ObtenerInterioresRecursivo(Raiz, interiores, esRaiz: true);
            return interiores;
        }

        private void ObtenerInterioresRecursivo(NodoABB? nodo, RecorridoABB lista, bool esRaiz)
        {
            if (nodo == null)
                return;

            bool tieneHijo = nodo.Izquierdo != null || nodo.Derecho != null;

            if (!esRaiz && tieneHijo)
                lista.Agregar(nodo.Valor);

            ObtenerInterioresRecursivo(nodo.Izquierdo, lista, false);
            ObtenerInterioresRecursivo(nodo.Derecho, lista, false);
        }


        int contador = 0;
        public string CalcularLongitudCaminoInterno()
        {

            double valor = LongitudCaminoInterno(Raiz, 1);
            if (contador != 0)
            {
                return $"LCI=  {valor} LCIM = {valor / contador}";
            }
            else {
                return "Error";
            }
            
        }

        /*implementar

        public string CalcularLongitudCaminoExterno()
        {
            return LongitudCaminoExterno(Raiz, 1);
        }
        */

        private double LongitudCaminoInterno(NodoABB nodo, int nivel)
        {
            if (nivel == 1) {
                contador = 0;
            }
            
            if (nodo == null)
                return 0;

            contador++;
            return nivel
                 + LongitudCaminoInterno(nodo.Izquierdo, nivel + 1)
                 + LongitudCaminoInterno(nodo.Derecho, nivel + 1);
        }

        /*
        public double LongitudCaminoExterno(NodoABB nodo, int nivel)
        {
            if (nodo == null)
                return 0;

            if (nodo.Izquierdo == null && nodo.Derecho == null)
            {
                return nivel;
            }
            else
            {
                return LongitudCaminoExterno(nodo.Izquierdo, nivel + 1)
                     + LongitudCaminoExterno(nodo.Derecho, nivel + 1);
            }
        }

        */




        public int? ObtenerGrado(int valor)
        {
            NodoABB? nodo = BuscarNodo(Raiz, valor);
            if (nodo == null)
                return null;

            int grado = 0;
            if (nodo.Izquierdo != null) grado++;
            if (nodo.Derecho != null) grado++;
            return grado;
        }
        public int? ObtenerNivel(int valor)
        {
            return ObtenerNivelRecursivo(Raiz, valor, 0);
        }

        private int? ObtenerNivelRecursivo(NodoABB? nodo, int valor, int nivelActual)
        {
            if (nodo == null)
                return null;

            if (nodo.Valor == valor)
                return nivelActual;

            if (valor < nodo.Valor)
                return ObtenerNivelRecursivo(nodo.Izquierdo, valor, nivelActual + 1);
            else
                return ObtenerNivelRecursivo(nodo.Derecho, valor, nivelActual + 1);
        }

    }
}
