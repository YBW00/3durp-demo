using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonTest : MonoBehaviour
{
    [SerializeField]
    private InputActionReference selectAction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(selectAction.action.ReadValue<float>());
    }
}
