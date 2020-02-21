using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorZumbis : MonoBehaviour
{
    public GameObject Zumbi;
    private float contadorTempo = 0;
    public float TempoGerarZumbi = 1;
    public LayerMask LayerZumbi;
    private float distanciaDeGeracao = 3;
    private float distanciaDoJogadorParaGeracao = 20;
    private GameObject jogador;

    private void Start()
    {
        jogador = GameObject.FindWithTag("Jogador");
    }

    // Update is called once per frame
    void Update()
    {

        if(Vector3.Distance(transform.position, jogador.transform.position) > distanciaDeGeracao)
        {
            contadorTempo += Time.deltaTime;

            if (contadorTempo > TempoGerarZumbi)
            {
                StartCoroutine(GerarNovoZumbi());
                contadorTempo = 0;
            }
        }        

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanciaDeGeracao);
    }

    IEnumerator GerarNovoZumbi()
    {
        Vector3 posicaoCriacao = AleatorizarPosicao();
        Collider[] colisores = Physics.OverlapSphere(posicaoCriacao, 1, LayerZumbi);

        while (colisores.Length > 0)
        {
            posicaoCriacao = AleatorizarPosicao();
            colisores = Physics.OverlapSphere(posicaoCriacao, 1, LayerZumbi);
            yield return null;
        }

        /*if(colisores.Length >= 0)
        {
            //GerarNovoZumbi();
        }*/
        Instantiate(Zumbi, posicaoCriacao, transform.rotation);
    }

    Vector3 AleatorizarPosicao()
    {
        Vector3 posicao = Random.insideUnitSphere * 3;
        posicao += transform.position;
        posicao.y = 0;

        return posicao;
    }
}
