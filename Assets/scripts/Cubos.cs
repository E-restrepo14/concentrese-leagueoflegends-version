using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cubos : MonoBehaviour
{
    public GameObject particula;

    void Start()
    {
        //este script esta dentro de todos los tiles prefabs, 
        ControladorDeJuego.Instance.cubosenEscena++;
    }


    void OnMouseDown()
    {
        //cada vez que detecta el onmouse down, consulta al gamemanager si el jugador puede girar o no
        if (ControladorDeJuego.Instance.puedeGirar == true)
        {

            //este if se ejecutara si el usuario no ha girado ningun cubo.
            if (ControladorDeJuego.Instance.yaGiro == false)
            {
                // la variable ya giro empieza siempre valiendo falso, 
                // pero cuando se ejecuta esta vale verdadero... para informar que ya hay un cubo seleccionado  
                ControladorDeJuego.Instance.yaGiro = true;
                // el cubo se guarda en esta variable seleccionado1... que se encuentra almacenada en el game manager
                ControladorDeJuego.Instance.seleccionado1 = gameObject;
                //se instancia una particula y se vuelve hija del cubo selecionado1
                Instantiate(particula, transform.position, Quaternion.identity);
                // y desde esta se le ordena que inicie la coroutine voltear.
                StartCoroutine(ControladorDeJuego.Instance.Voltear(ControladorDeJuego.Instance.seleccionado1));

            }

            //en caso de que ya se haya girado un cubo en la escena... se ejecutara este else
            else
            {
                // pero solo se ejecutara si tiene una posicion diferente al "seleccionado1"
                if (transform.position != ControladorDeJuego.Instance.seleccionado1.transform.position)
                {
                    ControladorDeJuego.Instance.yaGiro = false;
                    ControladorDeJuego.Instance.seleccionado2 = gameObject;
                    Instantiate(particula, transform.position, Quaternion.identity);
                    StartCoroutine(ControladorDeJuego.Instance.Voltear(ControladorDeJuego.Instance.seleccionado2));

                    //en el momento que ya se hayan seleccionado dos... se comparan los dos cubos
                    StartCoroutine(ControladorDeJuego.Instance.Comparar(ControladorDeJuego.Instance.seleccionado1, ControladorDeJuego.Instance.seleccionado2));
                }
            }
        }
    }
}