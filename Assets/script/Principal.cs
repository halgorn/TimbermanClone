using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
public class Principal : MonoBehaviour
{
    
    public GameObject jogador;
    public GameObject JogadorParado;
    public GameObject JogadorBate;

    public GameObject barril;
    public GameObject inimigoEsquerda;
    public GameObject inimigoDireita;
    float escalaJogadorHorizontal;
    // Start is called before the first frame update
    bool ladoPersonagem;
    private List<GameObject> listaBlocos;

    public Text pontuacao;

    int score;

    bool comecou;
    bool acabou;

    public GameObject barra;

    void Start()
    {
        listaBlocos = new List<GameObject>();
        escalaJogadorHorizontal = transform.localScale.x;

        JogadorBate.SetActive(false);

        GameObject barril = CriaNovoBarril(new Vector2(0,-2.1f));

        CriaBarrisInicio();

        pontuacao.transform.position =  new Vector2(Screen.width/2, Screen.height/2 + 50);
        pontuacao.text = "Toque Para Iniciar!";
        pontuacao.fontSize = 25;

        comecou = false;
        acabou = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!acabou){
            if(Input.GetButtonDown("Fire1")){

                if(!comecou){
                    comecou = true;
                    barra.SendMessage("Comecou");
                }

                if(Input.mousePosition.x > Screen.width/2){
                    bateDireita();
                }else {
                    bateEsquerda();
                }
                listaBlocos.RemoveAt(0);
                ReposicionaBlocos();
                ConfereJogada();
            }
        }
        
    }

    void bateEsquerda(){
        ladoPersonagem = true;
        JogadorBate.SetActive(true);
        JogadorParado.SetActive(false);
        jogador.transform.position = new Vector2(-0.3f, jogador.transform.position.y);
        jogador.transform.localScale = new Vector2(escalaJogadorHorizontal,jogador.transform.localScale.y);
        Invoke("VoltaAnimacao", 0.25f);
        listaBlocos[0].SendMessage("BateDireita");
    }
    void bateDireita(){
        ladoPersonagem = false;
        JogadorBate.SetActive(true);
        JogadorParado.SetActive(false);
        jogador.transform.position = new Vector2(0.3f, jogador.transform.position.y);
        jogador.transform.localScale = new Vector2(-escalaJogadorHorizontal,jogador.transform.localScale.y);
        Invoke("VoltaAnimacao", 0.25f);
        listaBlocos[0].SendMessage("BateEsquerda");
    }

    void VoltaAnimacao(){
        JogadorBate.SetActive(false);
        JogadorParado.SetActive(true);
    }

    GameObject CriaNovoBarril(Vector2 posicao){
        GameObject novoBarril;

        if(Random.value >0.5f || (listaBlocos.Count < 2)){
            novoBarril = Instantiate(barril);
        }else{

            if(Random.value  > 0.5f){
                novoBarril = Instantiate(inimigoEsquerda);
            }else{
                novoBarril = Instantiate(inimigoDireita);
            } 
        }
        novoBarril.transform.position = posicao;

        return novoBarril;
    }

    void CriaBarrisInicio(){
        for (int i = 0; i <=7; i++)
        {
            GameObject NovoBarril = CriaNovoBarril(new Vector2(0, -2.1f+(i*0.99f)));
            listaBlocos.Add(NovoBarril);
        }
    }

    void ReposicionaBlocos(){

        GameObject objBarril = CriaNovoBarril(new Vector2(0, -2.1f+(8*0.99f)));
        listaBlocos.Add(objBarril);
        for (int i = 0; i <=7; i++)
        {
            listaBlocos[i].transform.position = new Vector2(listaBlocos[i].transform.position.x, listaBlocos[i].transform.position.y-0.99f);
            
        }
    }
    void ConfereJogada(){
        if(listaBlocos[0].gameObject.CompareTag("Inimigo")){

            if((listaBlocos[0].name == "inimigoEsq(Clone)" &&  !ladoPersonagem)||(listaBlocos[0].name == "inimigoDir(Clone)" &&  ladoPersonagem)){
                FimDeJogo();
            }else{
                MarcaPonto();
            }

        }else{
            MarcaPonto();
        }
    }

    void MarcaPonto(){
        score++;
        pontuacao.text = score.ToString();
        pontuacao.fontSize = 100;
        pontuacao.color = new Color(0.95f,1.0f,0.35f);
        barra.SendMessage("AumentaBarra");
    }

    void FimDeJogo(){

        acabou = true;
        JogadorBate.GetComponent<SpriteRenderer>().color = new Color(1.0f,0.35f,0.35f);
        JogadorParado.GetComponent<SpriteRenderer>().color = new Color(1.0f,0.35f,0.35f);

        jogador.GetComponent<Rigidbody2D>().isKinematic = false;
        

        if(ladoPersonagem){
            jogador.GetComponent<Rigidbody2D>().AddTorque(100.0f);
            jogador.GetComponent<Rigidbody2D>().velocity = new Vector2(5.0f, 3.0f);
        }else{
            jogador.GetComponent<Rigidbody2D>().AddTorque(-100.0f);
            jogador.GetComponent<Rigidbody2D>().velocity = new Vector2(-5.0f, 3.0f);
        }
        Invoke("RecarregaCena", 2);
    }

    void RecarregaCena(){
        Application.LoadLevel("Game");
    }
}
