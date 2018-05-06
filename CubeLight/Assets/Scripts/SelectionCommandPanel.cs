using Assets.Scripts;
using Assets.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.UI;

public class SelectionCommandPanel : MonoBehaviour {

    public GameObject _SelectionCommandPanel;
    public Button _Button1;
    public Button _Button2;
    public Button _Button3;
    public Button _Button4;

    // Use this for initialization
    void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{

    }
    
    public void PopulateCommandButtons(ICommandButtons commandButtons)
    {
        UpdateButton(_Button1, commandButtons.Button1);
        UpdateButton(_Button2, commandButtons.Button2);
        UpdateButton(_Button3, commandButtons.Button3);
        UpdateButton(_Button4, commandButtons.Button4);
    }

    private void UpdateButton(Button button, ButtonConfig buttonConfig)
    {
        button.onClick.RemoveAllListeners();
        if (buttonConfig == null)
        {
            button.gameObject.SetActive(false);
        }
        else
        {
            button.gameObject.SetActive(true);
            button.onClick.AddListener(new UnityEngine.Events.UnityAction(buttonConfig.ButtonCommand));
            Text textComponent = button.GetComponentInChildren<Text>();
            textComponent.text = buttonConfig.ButtonText;
        }
    }

    public void ShowPanel()
    {
        _SelectionCommandPanel.SetActive(true);
    }

    public void HidePanel()
    {
        _SelectionCommandPanel.SetActive(false);
    }
}
