using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{

    public PhoneManager phoneManager;

    public GameObject controlos;
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
        SceneManager.LoadScene(1);
    }

    public void Controlos()
    {
        controlos.SetActive(!controlos.activeSelf);
    }
}
