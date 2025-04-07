using UnityEngine;

public class StartGame : MonoBehaviour
{
    public NodeManager nodeManager;
    public UIManager uIManager;

    public void GameStart()
    {
        uIManager.ChangeUI();
        nodeManager.NextNode();
    }
}
