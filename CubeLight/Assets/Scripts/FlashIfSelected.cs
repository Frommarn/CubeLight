using Assets.Scripts.Interfaces;
using Assets.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashIfSelected : MonoBehaviour {

    public float _FlashRate = 1.0f;
    private Color _OriginalColor;
    private ISelectionManager _SelectionManager;
    private bool _IsCoroutineRunning = false;

    // Use this for initialization
    void Start ()
	{
        _SelectionManager = Managers._PlayerSelectionManager.GetComponents<ISelectionManager>().ThrowIfMoreThanOne();
        _OriginalColor = GetComponent<Renderer>().material.color;
	}

    private void Update()
    {
        if (_SelectionManager.IsSelected(gameObject))
        {
            if (!_IsCoroutineRunning)
            {
                _IsCoroutineRunning = true;
                StartCoroutine("Flash");
            }
        }
        else
        {
            _IsCoroutineRunning = false;
            StopAllCoroutines();
            GetComponent<Renderer>().material.color = _OriginalColor;
        }
    }

    IEnumerator Flash ()
	{
        float t = 0;
        while (t < _FlashRate)
        {
            GetComponent<Renderer>().material.color = Color.Lerp(_OriginalColor, Color.white, t / _FlashRate);
            t += Time.deltaTime;
            yield return null;
        }

        GetComponent<Renderer>().material.color = Color.white;

        StartCoroutine("Return");
	}

    IEnumerator Return()
    {
        float t = 0;
        while (t < _FlashRate)
        {
            GetComponent<Renderer>().material.color = Color.Lerp(Color.white, _OriginalColor, t / _FlashRate);
            t += Time.deltaTime;
            yield return null;
        }

        GetComponent<Renderer>().material.color = _OriginalColor;
        StartCoroutine("Flash");
    }
}
