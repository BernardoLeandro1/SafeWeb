using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PhoneManager : MonoBehaviour
{

    public Image screen;

    public Sprite inicio;
    public Sprite amigos;
    public Sprite mensagens;

    public Button inicioButton;

    public Button amigosButton;
    public Button mensagensButton;

    public RectTransform contentPanel;      // Assign the Content GameObject here
    public GameObject postPrefab;           // Assign your Post prefab here

    

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AddPost("Hello, world!");
        AddPost("This is my second post.");
        AddPost("Mobile scrolling is working!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Inicio(){
        screen.sprite = inicio;
    }

    public void Amigos(){
        screen.sprite = amigos;
    }

    public void Mensagens(){
        screen.sprite = mensagens;
    }

    /// <summary>
    /// Adds a new post to the scrollable content.
    /// </summary>
    /// <param name="postData">Text or data for the post.</param>
    public void AddPost(string postData)
    {
        // Instantiate the prefab as a child of contentPanel
        GameObject newPost = Instantiate(postPrefab, contentPanel);
        // Optionally, set the text or other UI data
        var textComp = newPost.GetComponentInChildren<TMP_Text>();
        if (textComp != null)
            textComp.text = postData;
    }


}
