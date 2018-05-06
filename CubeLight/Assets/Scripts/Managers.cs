using UnityEngine;

public class Managers : MonoBehaviour {

    public static GameObject _PlayerSelectionManager;
    public static GameObject _PlayerResourceManager;
    public static GameObject _UIManager;

    protected Managers() { } // To ensure singleton behaviour.

    private void Awake()
    {
        _PlayerSelectionManager = GameObject.FindGameObjectWithTag("PlayerSelectionManager");
        _PlayerResourceManager = GameObject.FindGameObjectWithTag("PlayerResourceManager");
        _UIManager = GameObject.FindGameObjectWithTag("UIManager");
    }
    
    public static GameObject GetPlayerSelectionManager()
    {
        return _PlayerSelectionManager;
    }

    public static GameObject GetPlayerResourceManager()
    {
        return _PlayerResourceManager;
    }

    public static GameObject GetUIManager()
    {
        return _UIManager;
    }
}
