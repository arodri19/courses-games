using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlaInterface : MonoBehaviour
{
    private ControlaJogador scriptControlaJogador;
    public Slider SliderVidaJogador;
    public GameObject PainelDeGameOver;
    public Text textoTempoDeSobrevivencia;
    public Text TextoPontuacaoMaxima;
    private float tempoPontuacaoSalvo;

    // Start is called before the first frame update
    void Start()
    {
        scriptControlaJogador = GameObject.FindWithTag("Jogador").GetComponent<ControlaJogador>();

        SliderVidaJogador.maxValue = scriptControlaJogador.statusJogador.Vida;
        AtualizarSliderVidaJogador();
        Time.timeScale = 1;
        tempoPontuacaoSalvo = PlayerPrefs.GetFloat("PontuacaoMaxima");
    }

    public void AtualizarSliderVidaJogador()
    {
        SliderVidaJogador.value = scriptControlaJogador.statusJogador.Vida;
    }

    public void GameOver()
    {
        PainelDeGameOver.SetActive(true);
        Time.timeScale = 0;

        float valor = Time.timeSinceLevelLoad;

        int minutos = (int)(valor / 60);
        int segundos = (int)(valor % 60);

        textoTempoDeSobrevivencia.text = "Você sobreviveu por " + minutos + "min e " + segundos + "s";

        AjustarPontuacaoMaxima(minutos, segundos);
    }

    void AjustarPontuacaoMaxima(int min, int seg)
    {
        if(Time.timeSinceLevelLoad > tempoPontuacaoSalvo)
        {
            tempoPontuacaoSalvo = Time.timeSinceLevelLoad;
            TextoPontuacaoMaxima.text = string.Format("Seu melhor tempo é {0}min e {1}s", min, seg);

            PlayerPrefs.SetFloat("PontuacaoMaxima", tempoPontuacaoSalvo);
        }
        if(TextoPontuacaoMaxima.text == "")
        {
            float valor = tempoPontuacaoSalvo;

            min = (int)(valor / 60);
            seg = (int)(valor % 60);

            TextoPontuacaoMaxima.text = string.Format("Seu melhor tempo é {0}min e {1}s", min, seg);

        }

    }

    public void Reiniciar()
    {
        SceneManager.LoadScene("game");
    }
}
