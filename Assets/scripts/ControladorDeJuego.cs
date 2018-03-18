using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorDeJuego : MonoBehaviour
{
    public static ControladorDeJuego Instance;
    public Instanciador instanciador;

    public bool yaGiro;
    public bool perdio = false;
    public bool gano = false;
    public bool puedeGirar = true;
    public int cubosenEscena = 0;
    public GameObject seleccionado1;
    public GameObject seleccionado2;
    public float tiempoLimite = 60f;

    public Text tiempoText;

    public int numerodenivel = 1;

    private void Update()
    {
        if (gano == false)
        {
            tiempoLimite -= Time.deltaTime;
        }

        if (tiempoLimite > 1)
        {
            tiempoText.text = "tiempo limite: " + tiempoLimite.ToString("f0");
        }

        if (gano == true)
        {
            tiempoText.text = "si llegaste hasta aqui felicidades ";
        }

        if (tiempoLimite <1)
        {
            tiempoText.text = "corre mas rapido!!! y suerte la proxima ";
            perdio = true;
        }
        
    }

    //este es un singleton que se encarga de proveer variables a todo script que las necesite acceder.

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        yaGiro = false;

        instanciador = GetComponent<Instanciador>();
        
    }

    public IEnumerator Voltear(GameObject cubo)
    {
        int contador = 0;

        //durante 15 ciclos el cubo rotará 12 grados en Y, total seran 180
        while (contador < 15)
        {
            if (cubo != null)
            {
                cubo.transform.Rotate(0, 12, 0);
                yield return new WaitForSeconds(0.0f);
                
            }
            contador++;
        }
    }

    // esta funcion toma los valores de las variables seleccionado1 y seleccionado2, (de las cuales se encargan otros scripts de asignarle el valor)
    public IEnumerator Comparar(GameObject seleccionado1, GameObject seleccionado2)
    {
        //mientras se comparen los cubos el jugador no podra seleccionar otros cubos
        puedeGirar = false;
        // este wait forseconds es para que el jugador pueda ver si los dos cubos que selecciono... son iguales o diferentes
        yield return new WaitForSeconds(1.0f);

        //si el jugador selecciona un cubo... y lo selecciona de nuevo... al ser del mismo tag, el juego lo aceptaria como una pareja
        // y desapareceria el cubo solo... este if es para evitar que eso suceda
        if (seleccionado1.transform.position != seleccionado2.transform.position)
        {

            if (seleccionado1.tag == seleccionado2.tag)
            {
                //si los dos cubos tienen el mismo tag, se destruirán. (los tiles tienen en su propio script una orden de aumentar en 1 esta variable cubosenescena. desde el momento que aparecen)
                Destroy(seleccionado1);
                Destroy(seleccionado2);
                cubosenEscena = cubosenEscena - 2;


                if (cubosenEscena < 1 /* (osea ya no hay mas cubitos) */ && numerodenivel < 4 /*(y no ha superado el nivel 3) */)
                {
                    
                    if (perdio == false)
                    {

                        numerodenivel++;
                        tiempoLimite += 90f;
                        instanciador.Empezarnivel();
                       
                           
                    }
                    else
                    {
                        numerodenivel = 1;
                        tiempoLimite = 60f;
                        perdio = false;
                        instanciador.Empezarnivel();
                    }
                }

                if(numerodenivel > 3)
                {

                    gano = true;
                    //numerodenivel = 1;
                    //tiempoLimite = 60f;
                    //perdio = true;
                    //instanciador.Empezarnivel();
                }                                
            }

            else
            //al no ser iguales los cubos, solo se voltearan nuevamente los cubos a su posicion original
            {
                StartCoroutine(Voltear(seleccionado1));
                StartCoroutine(Voltear(seleccionado2));
            }
        }
        

        //luego se debe de permitir al jugador seleccionar mas cubos
        puedeGirar = true;

      
       
        

    }

}

