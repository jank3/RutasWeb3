using RutasWeb3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RutasWeb3.Helpers
{
    public class FBAlgoritm
    {
        private int repeats;
        private int cantNodos;
        private List<int> nodos;
        private BestRoute bestRoute;
        private int[,] matrizNodos;


        public FBAlgoritm(int[,] matriz)
        {
            matrizNodos = matriz;
            cantNodos = Convert.ToInt32(Math.Sqrt(matriz.Length));

            // Calculo la cantidad de veces que voy a repetir la búsqueda del camino más óptimo. 
            // En un grafo de "n" nodos, hay (n-1)!/2 caminos posibles. Se divide por 2 porque hay caminos inversos -duplicados-.
            repeats = Factorial(cantNodos - 1);

            bestRoute = new BestRoute
            {
                selectedRoute = "",
                distance = Int32.MaxValue
            };
        }

        public BestRoute Work()
        {
            // Cargo una lista de todos los nodos, menos el raíz.
            nodos = new List<int>();
            for (int k = 1; k < cantNodos; k++)
                nodos.Add(k);

            string[] nodosEnString = new string[cantNodos - 1];
            int contador = 0;
            foreach (int nodo in nodos)
            {
                nodosEnString[contador] = nodo.ToString();
                contador++;
            }

            ProcesarCaminos(nodosEnString, "", cantNodos - 1, cantNodos - 1);

            return bestRoute;
        }

        private void ProcesarCaminos(string[] nodos, string caminoAux, int n, int length)
        {
            if (n == 0)
                EvaluarCamino("0, " + caminoAux.Trim().TrimEnd(',') + ", 0");
            else
            {
                for (int i = 0; i < length; i++)
                {
                    if (!caminoAux.Contains(nodos[i]))
                    { // Controla que no haya repeticiones
                        ProcesarCaminos(nodos, caminoAux + nodos[i] + ", ", n - 1, length);
                    }
                }
            }
        }

        private void EvaluarCamino(string camino)
        {
            string[] nodos = camino.Split(',');
            int totalDistance = 0;
            for (int i = 0; i < nodos.Length - 1; i++)
                totalDistance += matrizNodos[Convert.ToInt16(nodos[i]), Convert.ToInt16(nodos[i + 1])];

            if (totalDistance < bestRoute.distance)
            {
                bestRoute.distance = totalDistance;
                bestRoute.selectedRoute = camino;
            }

        }

        private int Factorial(int number)
        {
            int factorial = 1;

            for (int counter = 1; counter <= number; counter++)
                factorial = factorial * counter;

            return factorial;
        }
    }
}