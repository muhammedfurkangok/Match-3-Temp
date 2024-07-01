using Runtime.Enums;
using Runtime.Managers;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    private bool isInputBlocked;
    
    void Update()
    {
        InputState();
        if(!isInputBlocked) GetInput();
    }

    private void InputState()
    {
        if (GameManager.Instance.GameStates != GameStates.Gameplay) isInputBlocked = true;
        else isInputBlocked = false;
    }

    private void GetInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit , Mathf.Infinity, layerMask))
            {
                hit.transform.GetComponent<Item>().OnSelected();
            }
        }
    }
}
