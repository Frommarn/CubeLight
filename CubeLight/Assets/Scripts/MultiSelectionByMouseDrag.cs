using Assets.Scripts.Interfaces;
using Assets.Scripts.Utils;
using UnityEngine;

public class MultiSelectionByMouseDrag : MonoBehaviour {

    public GameObject _MultiSelectorPrefab;

    private GameObject _MultiSelectorInstance;
    private Vector3 _FirstCorner;
    private bool _IsDragSelecting = false;
    private IPreSelectionManager _PreSelectionManager;

    // Use this for initialization
    void Start ()
	{
        _PreSelectionManager = Managers.GetPlayerSelectionManager().GetComponents<IPreSelectionManager>().ThrowIfMoreThanOne();
    }
	
	// Update is called once per frame
	void Update ()
	{
        if (Input.GetMouseButtonDown(0))
        {
            _IsDragSelecting = true;
            StartDragSelection();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _IsDragSelecting = false;
            CompleteDragSelection();
        }
        else if (_IsDragSelecting)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _IsDragSelecting = false;
                StopDragSelection();
            }
            else
            {
                UpdateDragSelection();
            }
        }
    }

    private void StartDragSelection()
    {
        // Raycast to determine position in worldspace
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit info;
        Physics.Raycast(ray, out info, Mathf.Infinity, LayerMask.GetMask("Tile"));
        _FirstCorner = info.point;

        // Instantiate our selector with proper position
        _MultiSelectorInstance = Instantiate(_MultiSelectorPrefab, _FirstCorner, _MultiSelectorPrefab.transform.rotation);
    }

    private void UpdateDragSelection()
    {
        // Raycast to determine position in worldspace
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit info;
        Physics.Raycast(ray, out info, Mathf.Infinity, LayerMask.GetMask("Tile"));

        // Resize our selector with new position
        Vector3 resizeVector = info.point - _FirstCorner;
        Vector3 newScale = _MultiSelectorInstance.transform.localScale;
        newScale.x = resizeVector.x;
        newScale.z = -resizeVector.z;
        _MultiSelectorInstance.transform.localScale = newScale;
    }

    private void StopDragSelection()
    {
        _PreSelectionManager.ClearPreSelectedGameObjects();

        // Destroy selector instance
        Destroy(_MultiSelectorInstance);
    }

    private void CompleteDragSelection()
    {
        _PreSelectionManager.MovePreSelectionToSelectedGameObjects();

        // Destroy selector instance
        Destroy(_MultiSelectorInstance);
    }
}
