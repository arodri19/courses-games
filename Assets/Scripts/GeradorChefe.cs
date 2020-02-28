using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorChefe : MonoBehaviour
{
    private float tempoParaNovaGeracao = 0;
    public float TempoEntreGeracoes = 60;
    public GameObject ChefePrefab;

    private void Start()
    {
        tempoParaNovaGeracao = TempoEntreGeracoes;
    }

    private void Update()
    {
        if(Time.timeSinceLevelLoad > tempoParaNovaGeracao)
        {
            Instantiate(ChefePrefab, transform.position, Quaternion.identity);
            tempoParaNovaGeracao = Time.timeSinceLevelLoad + TempoEntreGeracoes;
        }
    }
}
