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
    private int quantidadeMaximaDeZumbisVivos = 5;
    private int quantidadeDeZumbisVivos = 0;
    private float tempoProximoAumentoDeDificuldade = 5;
    private float contadorDeAumentarDificuldade;

    private void Start()
    {
        jogador = GameObject.FindWithTag("Jogador");
        contadorDeAumentarDificuldade = tempoProximoAumentoDeDificuldade;
        for (int i = 0; i < quantidadeMaximaDeZumbisVivos; i++)
        {
            StartCoroutine(GerarNovoZumbi());
        }

    }

    // Update is called once per frame
    void Update()
    {

        //GameObject[] zumbis = GameObject.FindGameObjectsWithTag("Inimigo");

        bool possoGerarZumbisPelaDistancia = Vector3.Distance(transform.position, jogador.transform.position) > distanciaDoJogadorParaGeracao;

        if (possoGerarZumbisPelaDistancia && quantidadeDeZumbisVivos < quantidadeMaximaDeZumbisVivos)
        {
            contadorTempo += Time.deltaTime;

            if (contadorTempo > TempoGerarZumbi)
            {
                StartCoroutine(GerarNovoZumbi());
                contadorTempo = 0;
            }
        }
        
        if(Time.timeSinceLevelLoad > contadorDeAumentarDificuldade)
        {
            quantidadeMaximaDeZumbisVivos++;
            contadorDeAumentarDificuldade = Time.timeSinceLevelLoad + tempoProximoAumentoDeDificuldade;
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
        ControlaInimigo zumbi = Instantiate(Zumbi, posicaoCriacao, transform.rotation).GetComponent<ControlaInimigo>();
        zumbi.meuGerador = this;
        quantidadeDeZumbisVivos++;
    }

    Vector3 AleatorizarPosicao()
    {
        Vector3 posicao = Random.insideUnitSphere * 3;
        posicao += transform.position;
        posicao.y = 0;

        return posicao;
    }

    public void DiminuirQuantidadeDeZumbisVivos()
    {
        quantidadeDeZumbisVivos--;
    }
}
