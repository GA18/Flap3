using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveMorcego : MonoBehaviour
{

    public float velocidade = 1f;
    public float velocidadeHorizontal;
    public float velocidadeVertical;
    public float min;
    public float max;
    public float espera;

    private GameObject player;
    private bool pontuou = false;
    

    // Use this for initialization
    void Start()
    {
        float direcao = (Random.Range(0f, 1f) < 0.5) ? min : max;
        StartCoroutine(Move(direcao));
        player = GameObject.Find("bALÃO 4");
        pontuou = false;
    }

    private void Awake()
    {
        player = GameObject.Find("bALÃO 4");
    }


    void Update()
    {
        Vector3 velocidadeVetorial = Vector3.left * velocidadeHorizontal;
        transform.position = transform.position + velocidadeVetorial * Time.deltaTime;
        if (!pontuou && GameController.instancia.estado == Estado.Jogando)
        {
            if (transform.position.x < player.transform.position.x)
            {
                GameController.instancia.incrementarPontos(1);
                pontuou = true;
            }
        }
    }


    IEnumerator Move(float destino)
    {
        while(Mathf.Abs(destino - transform.position.y) > 0.3f) {
   
            Vector3 direcao2 = (destino == max) ? Vector3.up : Vector3.down;
           
            Vector3 velocidadeVetorial2 = direcao2 * velocidadeVertical;

            transform.position = transform.position + velocidadeVetorial2 * Time.deltaTime;

            yield return null;

        }

        yield return new WaitForSeconds(espera);

        destino = (destino == max) ? min : max;
        StartCoroutine(Move(destino));
    }
}