using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorChefe : MonoBehaviour
{
    private float tempoParaNovaGeracao = 0;
    public float TempoEntreGeracoes = 60;
    public GameObject ChefePrefab;
    private ControlaInterface scriptControlaInterface;
    public Transform[] PosicoesPossiveisDeGeracao;
    private Transform jogador;

    private void Start()
    {
        tempoParaNovaGeracao = TempoEntreGeracoes;
        scriptControlaInterface = GameObject.FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;
        jogador = GameObject.FindWithTag("Jogador").transform;
    }

    private void Update()
    {
        if(Time.timeSinceLevelLoad > tempoParaNovaGeracao)
        {
            Vector3 posicaoDeCriacao = CalcularPosicaoMaisDistanteDoJogador();
            Instantiate(ChefePrefab, posicaoDeCriacao, Quaternion.identity);
            scriptControlaInterface.AparecerTextoChefeCriado();
            tempoParaNovaGeracao = Time.timeSinceLevelLoad + TempoEntreGeracoes;
        }
    }

    Vector3 CalcularPosicaoMaisDistanteDoJogador()
    {
        Vector3 posicaoDeMaiorDistancia = Vector3.zero;

        float maiorDistancia = 0;

        foreach (Transform posicao in PosicoesPossiveisDeGeracao)
        {
            float distanciaEntreJogador = Vector3.Distance(posicao.position, jogador.position);
            if(distanciaEntreJogador > maiorDistancia)
            {
                maiorDistancia = distanciaEntreJogador;
                posicaoDeMaiorDistancia = posicao.position;
            }
        }

        return posicaoDeMaiorDistancia;
    }
}
