﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPfinal
{
    public class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new btn_Datosvehiculo());
        }
        public void ordenar_prioridad(List<cEnvio> listaLlenado)
        {
            for (int k = 0; k < listaLlenado.Count - 1; k++)
            {
                for (int i = 0; i < listaLlenado.Count - 1; i++)
                {
                    if (listaLlenado[i].estado > listaLlenado[i + 1].estado)
                    {
                        cEnvio aux;
                        aux = listaLlenado[i];
                        listaLlenado[i] = listaLlenado[i + 1];
                        listaLlenado[i + 1] = aux;
                    }
                }
            }
        }
        public Stack<cEnvio> recorrido(List<cEnvio> listaEnvios, List<Dictionary<string, float>> listaBarrios)
        {
            int pos = 0;
            Stack<cEnvio> aux = new Stack<cEnvio>();
            for (int i = 0; i < listaBarrios.Count; i++)
            {
                for (int x = 0; x < listaBarrios[i].Count - 1; x++)
                {
                    if (listaBarrios[i].Keys.ElementAt(x) == listaEnvios[i].barrio)
                    {
                        if (x == 0)
                            pos = x;

                        if (listaBarrios[i].Values.ElementAt(x) < listaBarrios[i].Values.ElementAt(x + 1))
                        {
                            pos = x;
                        }
                    }
                }
                for (int y = 0; y < listaBarrios.Count; y++)
                {
                    if (listaEnvios[y].barrio == listaBarrios[y].Keys.ElementAt(pos))
                    {
                        aux.Push(listaEnvios[y]);
                        listaEnvios[y].km = listaBarrios[y].Values.ElementAt(pos);
                    }
                }
            }
            return aux;

        }
        public void main2()
        {
            //Creo mis 3 vehiculos que se van a encargar de repartir
            cCamioneta camioneta = new cCamioneta(90,60);
            cFurgoneta furgoneta = new cFurgoneta();
            cFurgon furgon = new cFurgon();

            //Creo mis articulos 
            cArticulo art1 = new cArticulo(3, 3, 3);
            cArticulo art2 = new cArticulo(2, 3, 2);
            cArticulo art3 = new cArticulo(1, 1, 1);
            cArticulo art4 = new cArticulo(2, 1, 3);

            //Creo mis Envios
            cEnvio envio1 = new cEnvio("banana 1", art1, Estado.NOASIGNADO, false, "Liniers");
            cEnvio envio2 = new cEnvio("banana 2", art2, Estado.NOASIGNADO, false, "Villa_Luro");
            cEnvio envio3 = new cEnvio("banana 3", art3, Estado.NOASIGNADO, false, "Mataderos");
            cEnvio envio4 = new cEnvio("banana 4", art4, Estado.NOASIGNADO, false, "Villa_Real");

            //Creo mi lista de envios
            List<cEnvio> listaEnvios = new List<cEnvio>();
            listaEnvios.Add(envio1);
            listaEnvios.Add(envio2);
            listaEnvios.Add(envio3);
            listaEnvios.Add(envio4);

            //arranco funcionamiento programa
            Stack<cEnvio> pilaOrdenada = new Stack<cEnvio>();
            ordenar_prioridad(listaEnvios);
            //me creo un cBarrios para acceder a la lista total de barrios
            cBarrios Barrios = new cBarrios();
            pilaOrdenada = recorrido(listaEnvios,Barrios.listaBarrios);
            //pruebo en cada vehiculo 1 por 1 si entran los objetos de la lista.
            camioneta.guardar_vehiculos(pilaOrdenada);
            furgoneta.guardar_vehiculos(pilaOrdenada);
            furgon.guardar_vehiculos(pilaOrdenada);

            furgoneta.cargar_nafta_furgoneta();
            furgon.cargar_nafta_furgon();
            camioneta.cargar_nafta_camioneta();
         }
    }
}
