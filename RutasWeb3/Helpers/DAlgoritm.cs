using RutasWeb3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RutasWeb3.Helpers
{
    public class DAlgoritm
    {
        private int cantNodos;
        private List<int> nodos;
        private List<int> nodosRestantes;
        private List<int> camino;
        private int[,] matrizNodos;
        private BestRoute bestRoute;

        public DAlgoritm(int[,] matriz)
        {
            matrizNodos = matriz;
            cantNodos = Convert.ToInt32(Math.Sqrt(matriz.Length));
            bestRoute = new BestRoute
            {
                selectedRoute = "",
                distance = 0
            };
        }

        public BestRoute Work()
        {
            // Cargo una lista de todos los nodos, menos el raíz.
            nodos = new List<int>();
            for (int k = 0; k < cantNodos; k++)
                nodos.Add(k);

            nodosRestantes = nodos.ToList();
            camino = new List<int>();

            int indiceSeleccionado = 0;
            int nodoSeleccionado;

            for (int i = 0; i < cantNodos; i++)
            {
                nodoSeleccionado = nodos[indiceSeleccionado];
                camino.Add(nodoSeleccionado);
                nodosRestantes.Remove(nodoSeleccionado);
                if (nodosRestantes.Count > 0)
                    indiceSeleccionado = EncontrarMasCercano(nodoSeleccionado);
            }

            // Agrego el primero de nuevo al final.
            camino.Add(0);

            bestRoute.distance += matrizNodos[indiceSeleccionado, 0];

            string msj = "";
            foreach (int nodo in camino)
            {
                msj += nodo + ", ";
            }

            bestRoute.selectedRoute = msj.Trim().TrimEnd(',');

            return bestRoute;
        }

        private int EncontrarMasCercano(int nodoSeleccionado)
        {
            // [nodo, distancia]
            int[] masProximo = { 0, Int16.MaxValue };
            int distancia;

            for (int k = 0; k < nodosRestantes.Count; k++)
            {
                distancia = matrizNodos[nodoSeleccionado, nodosRestantes[k]];
                if (distancia < masProximo[1])
                {
                    masProximo[0] = nodosRestantes[k];
                    masProximo[1] = distancia;
                }
            }

            bestRoute.distance += masProximo[1];

            return nodos.FindIndex(n => n == masProximo[0]);
        }
    }
}