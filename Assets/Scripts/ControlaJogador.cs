using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaJogador : MonoBehaviour, IMatavel
{

    private Vector3 direcao;
    public LayerMask MascaraChao;
    public GameObject TextoGameOver;
    public ControlaInterface scriptControlaInterface;
    public AudioClip SomDeDano;
    private MovimentoJogador meuMovimentoJogador;
    private AnimacaoPersonagem animacaojogador;
    public Status statusJogador;

    private void Start()
    {
        meuMovimentoJogador = GetComponent<MovimentoJogador>();
        animacaojogador = GetComponent<AnimacaoPersonagem>();
        statusJogador = GetComponent<Status>();

    }

    // Update is called once per frame
    void Update()
    {

        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);

        animacaojogador.Movimentar(direcao.magnitude);
    }

    void FixedUpdate()
    {
        meuMovimentoJogador.Movimentar(direcao, statusJogador.Velocidade);

        meuMovimentoJogador.RotacaoJogador(MascaraChao);
    }

    public void TomarDano(int dano)
    {
        statusJogador.Vida -= dano;
        scriptControlaInterface.AtualizarSliderVidaJogador();
        ControlaAudio.instancia.PlayOneShot(SomDeDano);
        if (statusJogador.Vida <= 0)
        {
            Morrer();
        }
        
    }

    public void Morrer()
    {
        //Time.timeScale = 0;
        //TextoGameOver.SetActive(true);
        scriptControlaInterface.GameOver();
    }
}
