using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instanciador : MonoBehaviour
{
    private int equis = 0;
    public int columnas;
    public int filas;
    private Vector3 posicion;
    public GameObject empty;
    public List<Transform> posicionesCubos;
    public List<GameObject> cubosprefabs;

    private void Start()
    {
        Empezarnivel (ControladorDeJuego.Instance.numerodenivel);
        
    }

    public void Empezarnivel()
    {
        Empezarnivel(ControladorDeJuego.Instance.numerodenivel);
    }

    void Empezarnivel ( int numerodenivel)
    {
    
        int nivelnum = numerodenivel;

        if (nivelnum > 0 && nivelnum < 2)
        {
            transform.position = new Vector3(1.5f,1.5f,-5f);
            columnas = 4;
            filas = 4;
        }
        if (nivelnum > 1 && nivelnum < 3)
        {
            transform.position = new Vector3(1.5f, 2.5f, -6.5f);
            columnas = 4;
            filas = 6;
        }
        if (nivelnum > 2 && nivelnum < 4)
        {
            transform.position = new Vector3(2.5f, 2.5f, -6.5f);
            columnas = 6;
            filas = 6;
        }


        for (int x = 0; x < columnas; x++)
        {
            for (int y = 0; y < filas; y++)
            {
                posicion = new Vector3(x, y, 0);

                posicionesCubos.Add(Instantiate(empty, posicion, Quaternion.identity).transform);
            }
        }
        Mezclar(filas,columnas);
    }

    void Mezclar(int fil, int colum)
    {

        //este for se ejecutara el numero de cubos en escena, dividido por dos
        for (int i = 0; i < ((fil * colum )/ 2); i++)
        {
       // un int j tomara un espacio en el tablero de cubos al azar
            int j = Random.Range(0, posicionesCubos.Count);

           // un int x tomara una clase de cubo unico misterioso
            equis = (equis + 1) % cubosprefabs.Count;
            // e instanciara el cubo unico en la posicion j del tablero
            Instantiate(cubosprefabs[equis], (posicionesCubos[j].position), Quaternion.identity);
            // y borrara esa posicion de la lista de disponibles
            posicionesCubos.Remove(posicionesCubos[j]);

            //se le da un nuevo valor a j porque se le ha destruido
            j = Random.Range(0, posicionesCubos.Count);
            // e instancia el mismo cubo misterioso en otra posicion diferente y luego lo borra de las posiciones disponibles
            Instantiate(cubosprefabs[equis], (posicionesCubos[j].position), Quaternion.identity);
            posicionesCubos.Remove(posicionesCubos[j]);


        }
    }
}
