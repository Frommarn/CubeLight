using Assets.Scripts;
using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CommandPanel : MonoBehaviour, ICommandPanel {

    // Controlled from the Unity Editor
    public GameObject _SelectionCommandPanel;
    public List<Button> _Buttons;   // 4 Buttons are hardwired at the moment...

    private List<IButtonCommands> _ButtonCommandsList;
    private bool _IsVisible = false;

    // Use this for initialization
    void Start ()
	{
        _ButtonCommandsList = new List<IButtonCommands>();
	}
	
	// Update is called once per frame
	void Update ()
	{

    }

    void ICommandPanel.ShowPanel()
    {
        if (_IsVisible)
        {
            return;
        }
        _IsVisible = true;
        _SelectionCommandPanel.SetActive(true);
    }

    void ICommandPanel.HidePanel()
    {
        if (!_IsVisible)
        {
            return;
        }
        _IsVisible = false;
        _SelectionCommandPanel.SetActive(false);
    }

    void ICommandPanel.ClearCommandButtons()
    {
        _ButtonCommandsList.Clear();
        ClearAllButtons();
    }

    void ICommandPanel.AddButtonCommands(IButtonCommands buttonCommands)
    {
        _ButtonCommandsList.Add(buttonCommands);
        UpdateCommandPanelButtons();
    }

    void ICommandPanel.RemoveButtonCommands(IButtonCommands buttonCommands)
    {
        if (_ButtonCommandsList.Contains(buttonCommands))
        {
            _ButtonCommandsList.Remove(buttonCommands);
            UpdateCommandPanelButtons();
        }
    }

    private void ClearAllButtons()
    {
        foreach (var button in _Buttons)
        {
            button.onClick.RemoveAllListeners();
            button.gameObject.SetActive(false);
        }
    }

    private void UpdateCommandPanelButtons()
    {
        for (int i = 0; i < _Buttons.Count; i++)
        {
            List<ButtonConfig> buttonConfigs = new List<ButtonConfig>();
            foreach (var item in _ButtonCommandsList)
            {
                ButtonConfig buttonConfig = item.Buttons.FirstOrDefault(bc => bc.ButtonIndex == i);
                if (buttonConfig != null)
                {
                    buttonConfigs.Add(buttonConfig);
                }
            }
            UpdateButton(_Buttons[i], buttonConfigs);
        }
    }

    private void UpdateButton(Button button, List<ButtonConfig> buttonConfigs)
    {
        button.onClick.RemoveAllListeners();

        if (buttonConfigs.Count == 0)
        {
            button.gameObject.SetActive(false);
            return;
        }
        else if (buttonConfigs.Count == 1)
        {
            UpdateButton(button, buttonConfigs[0]);
        }
        else
        {
            for (int j = 1; j < buttonConfigs.Count; j++)
            {
                if (buttonConfigs[j - 1].ButtonText != buttonConfigs[j].ButtonText)
                {
                    // If contradicting commands, disable button.
                    button.gameObject.SetActive(false);
                    return;
                }
            }

            // All commands are the same, update button with info from first config
            UpdateButton(button, buttonConfigs[0]);

            // Subscribe all other config commands to button
            for (int i = 1; i < buttonConfigs.Count; i++)
            {
                button.onClick.AddListener(new UnityEngine.Events.UnityAction(buttonConfigs[i].ButtonCommand));
            }
        }
    }

    private void UpdateButton(Button button, ButtonConfig buttonConfig)
    {
        button.gameObject.SetActive(true);
        button.onClick.AddListener(new UnityEngine.Events.UnityAction(buttonConfig.ButtonCommand));
        Text textComponent = button.GetComponentInChildren<Text>();
        textComponent.text = buttonConfig.ButtonText;
    }
}
