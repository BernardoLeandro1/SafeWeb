using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{

    public PhoneManager phoneManager;

    public GameObject controlos;
    public GameObject opcoes;
    public GameObject saida;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Aceitar()
    {
        phoneManager.Aceitar(transform.parent.gameObject);

    }

    public void Recusar()
    {
        phoneManager.Recusar(transform.parent.gameObject);
    }

    public void Sair()
    {
        Application.Quit();
    }

    public void Começar()
    {
        GameSaveManager.Instance.NewGame();
    }

    public void Continuar()
    {
        GameSaveManager.Instance.LoadExistingGame();
    }

    public void Controlos()
    {
        if(saida.activeSelf){
            saida.SetActive(false);
        }
        controlos.SetActive(!controlos.activeSelf);
    }

    public void Opcoes()
    {
        
        opcoes.SetActive(!opcoes.activeSelf);
    }
    
    public void ConfirmarSaida()
    {
        if(controlos.activeSelf){
            controlos.SetActive(false);
        }
        saida.SetActive(!saida.activeSelf);
    }
}
