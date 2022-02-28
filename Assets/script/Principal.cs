using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Principal : MonoBehaviour
{
    
    public GameObject jogador;
    public GameObject JogadorParado;
    public GameObject JogadorBate;

    float escalaJogadorHorizontal;
    // Start is called before the first frame update
    void Start()
    {
        escalaJogadorHorizontal = transform.localScale.x;

        JogadorBate.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")){

            if(Input.mousePosition.x > Screen.width/2){
                bateDireita();
            }else {
                bateEsquerda();
            }

        }
    }

    void bateDireita(){
        JogadorBate.SetActive(true);
        JogadorParado.SetActive(false);
        jogador.transform.position = new Vector2(-0.3f, jogador.transform.position.y);
        jogador.transform.localScale = new Vector2(escalaJogadorHorizontal,jogador.transform.localScale.y);
        Invoke("VoltaAnimacao", 0.25f);
    }
    void bateEsquerda(){
        JogadorBate.SetActive(true);
        JogadorParado.SetActive(false);
        jogador.transform.position = new Vector2(0.3f, jogador.transform.position.y);
        jogador.transform.localScale = new Vector2(-escalaJogadorHorizontal,jogador.transform.localScale.y);
        Invoke("VoltaAnimacao", 0.25f);
    }

    void VoltaAnimacao(){
        JogadorBate.SetActive(false);
        JogadorParado.SetActive(true);
    }
}
