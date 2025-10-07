using TMPro;
using UnityEngine;
using UnityEngine.UI; 
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

    public GameObject messagePrefab;           // Assign your Message prefab here

    public TMP_InputField user;

    public TMP_InputField pass;

    public GameObject registButton;

    public GameObject erro;

    //public RectTransform choiceParent;      // Assign the Content GameObject here
    public GameObject choiceButtonPrefab;


    private string username = "";

    private string password = "";

    private UIManager uIManager;

    private LogicManager logicManager;

    private MissionManager missionManager;

    ScoreManager scoreManager;

    NodeManager nodeManager;

    List<Friend> friendNodes;

    List<PostMade> postNodes;

    List<MessageReceived> messageNodes;
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
        scoreManager = GetComponent<ScoreManager>();
        nodeManager = GetComponent<NodeManager>();
        StreamReader reader = new StreamReader(Path.Combine(Application.streamingAssetsPath, "friends.json"));
        var json = reader.ReadToEnd();
        friendNodes = JsonConvert.DeserializeObject<List<Friend>>(json);
        friendsList = new List<GameObject>();
        reader = new StreamReader(Path.Combine(Application.streamingAssetsPath, "posts.json"));
        json = reader.ReadToEnd();
        postNodes = JsonConvert.DeserializeObject<List<PostMade>>(json);
        reader = new StreamReader(Path.Combine(Application.streamingAssetsPath, "messages.json"));
        json = reader.ReadToEnd();
        messageNodes = JsonConvert.DeserializeObject<List<MessageReceived>>(json);
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

    public string GetPassword() {
        return password;
    }
        

    /// <summary>
    /// Adds a new post to the scrollable content.
    /// </summary>
    /// <param name="postData">Text or data for the post.</param>
    public void AddPost(string postText, string profile, string post, List<PhoneChoices> choices)
    {
        // Instantiate the prefab as a child of contentPanel
        GameObject newPost = Instantiate(postPrefab, contentPanel);
        // Optionally, set the text or other UI data
        var textComp = newPost.GetComponentInChildren<TMP_Text>();
        if (textComp != null)
            textComp.text = postText;

        var profileImage = newPost.transform.Find("ProfilePic").GetComponent<Image>();


        string filePath = Path.Combine(Application.streamingAssetsPath, "/Images/" + profile);

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
            profileImage.sprite = www;
            //newRequest.SetActive(true);
        }
        else
        {
            Debug.LogError("Could not load image data.");
        }
        if (post != null)
        {
            var postImage = newPost.transform.Find("PostImage").GetComponent<Image>();

            string filePath2 = Path.Combine(Application.streamingAssetsPath, "/Images/" + post);

            byte[] fileData2 = System.IO.File.ReadAllBytes(Application.streamingAssetsPath + filePath2);
            Texture2D tex2 = new Texture2D(2, 2); // size doesn’t matter here, will be replaced
            if (tex2.LoadImage(fileData2))
            {
                tex2.filterMode = FilterMode.Point; // Optional: Pixel Art
                tex2.wrapMode = TextureWrapMode.Clamp;

                Sprite www = Sprite.Create(
                    tex2,
                    new Rect(0, 0, tex2.width, tex2.height),
                    new Vector2(0.5f, 0.5f), // Pivot
                    100.0f                    // Pixels Per Unit — match to your project
                );
                postImage.sprite = www;
                //newRequest.SetActive(true);
            }
            else
            {
                Debug.LogError("Could not load image data.");
            }
        }

        var choiceParent = newPost.transform.Find("Choices").transform;
        if (choices != null)
        {
            foreach (var choice in choices)
            {
                GameObject btnObj = Instantiate(choiceButtonPrefab, choiceParent);
                Button btn = btnObj.GetComponent<Button>();
                TMP_Text btnText = btn.GetComponentInChildren<TMP_Text>();

                btnText.text = choice.Text;

                btn.onClick.AddListener(() =>
                {

                    postsList.Remove(newPost);
                    Destroy(newPost);

                    scoreManager.AddScore(choice.Reference, choice.Score);

                    if (postsList.Count == 0)
                    {
                        missionManager.isWaiting = false;
                        if (logicManager.GetDay() == 2)
                        {
                            uIManager.HidePhone();
                            logicManager.DeactivatePhone();
                            if (missionManager.GetCurrentMission().Id == 8)
                            {
                                uIManager.DisplayToDoList("Volta a falar com a Ana. (E)");
                            }
                        }
                        else if (logicManager.GetDay() == 4 && messagesList.Count == 0)
                        {
                            uIManager.HidePhone();
                            logicManager.DeactivatePhone();
                            nodeManager.NextNode();
                            logicManager.canGoToBed = true;
                            
                        }

                    }
                });
            }
        }


        postsList.Add(newPost);
    }

    public void AddMessage(string postText, string profile, List<PhoneChoices> choices)
    {
        // Instantiate the prefab as a child of contentPanel
        GameObject newPost = Instantiate(messagePrefab, contentPanel);
        // Optionally, set the text or other UI data
        var textComp = newPost.GetComponentInChildren<TMP_Text>();
        if (textComp != null)
            textComp.text = postText;

        var profileImage = newPost.transform.Find("ProfilePic").GetComponent<Image>();


        string filePath = Path.Combine(Application.streamingAssetsPath, "/Images/" + profile);

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
            profileImage.sprite = www;
            //newRequest.SetActive(true);
        }
        else
        {
            Debug.LogError("Could not load image data.");
        }

        var choiceParent = newPost.transform.Find("Choices").transform;
        if (choices != null)
        {
            foreach (var choice in choices)
            {
                GameObject btnObj = Instantiate(choiceButtonPrefab, choiceParent);
                Button btn = btnObj.GetComponent<Button>();
                TMP_Text btnText = btn.GetComponentInChildren<TMP_Text>();

                btnText.text = choice.Text;

                btn.onClick.AddListener(() =>
                {
                    if (choice.Text.Contains("Não clicas no link"))
                    {
                        scoreManager.AddScore("link", 0);
                    }
                    else if (choice.Text.Contains("Clicas no link"))
                    {
                        scoreManager.AddScore("link", 1);
                    }
                    else if (choice.Text.Contains("Bloqueias"))
                    {
                        scoreManager.AddScore("jaimelink", 0);
                    }
                    else if (choice.Text.Contains("Curioso"))
                    {
                        scoreManager.AddScore("jaimelink", 1);
                    }
                    scoreManager.AddScore(choice.Reference, choice.Score);
                    messagesList.Remove(newPost);
                    Destroy(newPost);
                    if (messagesList.Count == 0)
                    {
                        missionManager.isWaiting = false;
                        if (logicManager.GetDay() == 2)
                        {
                            uIManager.HidePhone();
                            logicManager.DeactivatePhone();
                            if (missionManager.GetCurrentMission().Id == 8)
                            {
                                uIManager.DisplayToDoList("Volta a falar com a Ana. (E)");
                            }
                        }
                        else if (logicManager.GetDay() == 4 && postsList.Count == 0)
                        {
                            uIManager.HidePhone();
                            logicManager.DeactivatePhone();
                            nodeManager.NextNode();
                            logicManager.canGoToBed = true;
                            
                        }
                        else if (logicManager.GetDay() == 5)
                        {
                            uIManager.HidePhone();
                            logicManager.DeactivatePhone();
                            if (missionManager.GetCurrentMission().Id == 25)
                            {
                                uIManager.DisplayToDoList("Volta a falar com eles. (E)");
                            }
                        }

                    }
                });
            }
        }

        newPost.SetActive(false);
        messagesList.Add(newPost);
    }

    public void ShowRequests()
    {
        foreach (Friend friend in friendNodes)
        {
            if (friend.Day == logicManager.GetDay())
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
        // Instantiate the prefab as a child of contentPanel
        GameObject newRequest = Instantiate(friendRequestPrefab, contentPanel);
        // Optionally, set the text or other UI data
        var textComp = newRequest.GetComponentInChildren<TMP_Text>();
        if (textComp != null)
        {
            textComp.text = name;
        }
        var profPic = newRequest.GetComponentInChildren<Image>();

        string filePath = Path.Combine(Application.streamingAssetsPath, "/Images/" + photoPath);

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

    public void ShowPosts()
    {
        foreach (PostMade post in postNodes)
        {
            if (post.Day == logicManager.GetDay())
            {
                if (post.Condition != null)
                {
                    if (scoreManager.GetScore(post.Condition) > 0)
                    {
                        AddPost(post.Name, post.Photo, post.Post, post.Choices);
                    }
                }
                else
                {
                    AddPost(post.Name, post.Photo, post.Post, post.Choices);
                }
                
            }
        }
        DisplayPosts();
    }

    public void ShowMessages()
    {
        foreach (MessageReceived message in messageNodes)
        {
            if (message.Day == logicManager.GetDay())
            {
                if (message.Condition != null)
                {
                    if (scoreManager.GetScore(message.Condition) > 0)
                    {
                        AddMessage(message.Name, message.Photo, message.Choices);
                    }
                }
                else
                {
                    AddMessage(message.Name, message.Photo, message.Choices);
                }
            }
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
            if (logicManager.GetDay() == 1)
            {
                uIManager.HidePhone();
                logicManager.DeactivatePhone();
                uIManager.DisplayToDoList("Volta a falar com eles. (E)");
                //missionManager.UpdateNodes();
                //missionManager.NextNodeMissions();
            }
            else if (logicManager.GetDay() == 4)
            {
                uIManager.HidePhone();
                logicManager.DeactivatePhone();
                nodeManager.NextNode();

                //missionManager.UpdateNodes();
                //missionManager.NextNodeMissions();
            }
            else if (logicManager.GetDay() == 5)
            {
                uIManager.HidePhone();
                logicManager.DeactivatePhone();
                nodeManager.NextNode();
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
            if (logicManager.GetDay() == 1)
            {
                uIManager.HidePhone();
                logicManager.DeactivatePhone();
                uIManager.DisplayToDoList("Volta a falar com eles. (E)");
                //missionManager.UpdateNodes();
                //missionManager.NextNodeMissions();
            }
            else if (logicManager.GetDay() == 4)
            {
                uIManager.HidePhone();
                logicManager.DeactivatePhone();
                nodeManager.NextNode();
                //missionManager.UpdateNodes();
                //missionManager.NextNodeMissions();
            }
            else if (logicManager.GetDay() == 5)
            {
                uIManager.HidePhone();
                logicManager.DeactivatePhone();
                nodeManager.NextNode();
                //missionManager.UpdateNodes();
                //missionManager.NextNodeMissions();
            }

        }
    }

    public List<string> GetFriends()
    {
        return addedFriends;
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


public class PostMade
{
    public string Name { get; private set; }

    public string Photo { get; private set; }

    public string Post { get; private set; }

    public int Day { get; set; }

    public List<PhoneChoices> Choices { get; set; }

    public string Condition { get; set; }

    public PostMade(string name, string photo, string post, int day, string condition = null)
    {
        this.Name = name;
        this.Photo = photo;
        this.Post = post;
        this.Day = day;
        this.Condition = condition;
    }
}

public class PhoneChoices
{
    public string Text { get; private set; }

    public int Score { get; private set; }

    public string Reference { get; private set; }




    public PhoneChoices(string text, int score, string reference)
    {
        this.Text = text;
        this.Score = score;
        this.Reference = reference;
    }
}

public class MessageReceived
{
    public string Name { get; private set; }

    public string Photo { get; private set; }

    public int Day { get; set; }
    public string Condition { get; set; }

    public List<PhoneChoices> Choices { get; set; }
    public MessageReceived(string name, string photo, int day, string condition = null)
    {
        this.Name = name;
        this.Photo = photo;
        this.Day = day;
        this.Condition = condition;
    }

}