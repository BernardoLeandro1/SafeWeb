using TMPro;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.Networking;
using UnityEngine.Windows;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

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

    public GameObject friendRequestPrefab;

    public TMP_InputField user;

    public TMP_InputField pass;

    public GameObject registButton;

    public GameObject erro;


    private string username = "";

    private string password = "";

    private UIManager uIManager;

    private LogicManager logicManager;

    private MissionManager missionManager;

    List<Friend> friendNodes;
    int currentDay;

    List<GameObject> friendsList;
    List<GameObject> postsList;
    List<GameObject> messagesList;

    List<string> addedFriends;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // AddPost("Hello, world!");
        // AddPost("This is my second post.");
        // AddPost("Mobile scrolling is working!");
        // ShowFriendRequests("maria.png", "Maria");
        uIManager = GetComponent<UIManager>();
        logicManager = GetComponent<LogicManager>();
        missionManager = GetComponent<MissionManager>();
        StreamReader reader = new StreamReader(Path.Combine(Application.streamingAssetsPath, "friends.json"));
        var json = reader.ReadToEnd();
        friendNodes = JsonConvert.DeserializeObject<List<Friend>>(json);
        friendsList = new List<GameObject>();
        postsList = new List<GameObject>();
        messagesList = new List<GameObject>();
        addedFriends = new List<string>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Inicio()
    {
        screen.sprite = inicio;
        DisplayPosts();
    }

    public void Amigos()
    {
        screen.sprite = amigos;
        DisplayFriends();
    }

    public void Mensagens()
    {
        screen.sprite = mensagens;
        DisplayMessages();
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

        postsList.Add(newPost);
    }

    public void SetDay(int day)
    {
        currentDay = day;
        Debug.Log(day);
        foreach (Friend friend in friendNodes)
        {
            Debug.Log(friend.Day);
            if (friend.Day == day)
            {
                ShowFriendRequests(friend.Photo, friend.Name);
            }
        }
    }

    public void DisplayFriends()
    {
        foreach (GameObject friend in friendsList)
        {
            friend.SetActive(true);
        }
        foreach (GameObject post in postsList)
        {
            post.SetActive(false);
        }
        foreach (GameObject message in messagesList)
        {
            message.SetActive(false);
        }
    }
    public void DisplayPosts()
    {
        foreach (GameObject friend in friendsList)
        {
            friend.SetActive(false);
        }
        foreach (GameObject post in postsList)
        {
            post.SetActive(true);
        }
        foreach (GameObject message in messagesList)
        {
            message.SetActive(false);
        }
    }
    public void DisplayMessages()
    {
        foreach (GameObject friend in friendsList)
        {
            friend.SetActive(false);
        }
        foreach (GameObject post in postsList)
        {
            post.SetActive(false);
        }
        foreach (GameObject message in messagesList)
        {
            message.SetActive(true);
        }
    }

    public void ShowFriendRequests(string photoPath, string name)
    {
        Debug.Log("Friends will be added2");
        // Instantiate the prefab as a child of contentPanel
        GameObject newRequest = Instantiate(friendRequestPrefab, contentPanel);
        // Optionally, set the text or other UI data
        var textComp = newRequest.GetComponentInChildren<TMP_Text>();
        if (textComp != null)
        {
            Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaa");
            textComp.text = name;
        }
        var profPic = newRequest.GetComponentInChildren<Image>();

        string filePath = Path.Combine(Application.streamingAssetsPath, "/Images/" + photoPath);
        Debug.Log(filePath);
        //Sprite www = Resources.Load(Application.streamingAssetsPath+filePath) as Sprite;
        //Debug.Log(www);
        byte[] fileData = System.IO.File.ReadAllBytes(Application.streamingAssetsPath + filePath);
        Texture2D tex = new Texture2D(2, 2); // size doesn’t matter here, will be replaced
        if (tex.LoadImage(fileData))
        {
            tex.filterMode = FilterMode.Point; // Optional: Pixel Art
            tex.wrapMode = TextureWrapMode.Clamp;

            Sprite www = Sprite.Create(
                tex,
                new Rect(0, 0, tex.width, tex.height),
                new Vector2(0.5f, 0.5f), // Pivot
                100.0f                    // Pixels Per Unit — match to your project
            );
            profPic.sprite = www;
            //newRequest.SetActive(true);
            friendsList.Add(newRequest);
        }
        else
        {
            Debug.LogError("Could not load image data.");
        }


    }

    public void Registar()
    {
        if (pass.text.Length < 8)
        {
            erro.SetActive(true);
        }
        else
        {
            username = user.text;
            password = pass.text;
            Debug.Log(username + " " + password);
            user.gameObject.SetActive(false);
            pass.gameObject.SetActive(false);
            registButton.SetActive(false);
            uIManager.HidePhone();
            logicManager.DeactivatePhone();
            uIManager.DisplayToDoList("Volta a falar com eles. (E)");
            missionManager.isWaiting = false;
        }

    }

    public void Aceitar(GameObject gameObject)
    {
        friendsList.Remove(gameObject);
        addedFriends.Add(gameObject.GetComponentInChildren<TMP_Text>().text);
        Destroy(gameObject);
        if (friendsList.Count == 0)
        {
            missionManager.isWaiting = false;
            if (currentDay == 1)
            {
                uIManager.HidePhone();
                logicManager.DeactivatePhone();
                uIManager.DisplayToDoList("Volta a falar com eles. (E)");
                //missionManager.UpdateNodes();
                //missionManager.NextNodeMissions();
            }
        }
        
    }

    public void Recusar(GameObject gameObject)
    {
        friendsList.Remove(gameObject);
        Destroy(gameObject);
        if (friendsList.Count == 0)
        {
            missionManager.isWaiting = false;
            if (currentDay == 1)
            {
                uIManager.HidePhone();
                logicManager.DeactivatePhone();
                uIManager.DisplayToDoList("Volta a falar com eles. (E)");
                //missionManager.UpdateNodes();
                //missionManager.NextNodeMissions();
            }
            
        }
    }
    




}


public class Friend
{
    public string Name { get; private set; }

    public string Photo { get; private set; }

    public int Day { get; set; }

    public int Score { get; set; } = 0;

    public Friend(string name, string photo, int day)
    {
        this.Name = name;
        this.Photo = photo;
        this.Day = day;
    }
    

    

}