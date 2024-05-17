using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class ThrowNewObject : MonoBehaviour
{
    [SerializeField]
    private InputActionReference selectAction;

    [SerializeField]
    private float threashold = 0.5f;
    [SerializeField]
    private GameObject ThrowObject = null;
    [SerializeField]
    private GameObject ThrowedObjects = null;
    private bool isGrabbed = false;
    private GameObject GrabbedObject = null;
    private Vector3 Velocity;
    private Vector3 prev_position;
    // Start is called before the first frame update
    void Start()
    {
        prev_position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var position = transform.position;
            var rotation = transform.rotation;
        var dt = Time.deltaTime;
        if (dt> 0.001)
        {
            var diff = position - prev_position;
            Velocity = diff / dt;
        }else{
            Velocity.Set(0, 0, 0);
        }
        prev_position = position;

        if (selectAction.action.ReadValue<float>() < threashold)
        {
            if (isGrabbed)
            {
            GrabbedObject.GetComponent<Rigidbody>().velocity=Velocity;
                isGrabbed = false;
            }
        }
        else
        {

            if (!isGrabbed)
            {
                if (ThrowObject)
                {
                    GrabbedObject = Instantiate(ThrowObject, position, rotation, ThrowedObjects.transform);
                }
                isGrabbed = true;
            }
            GrabbedObject.transform.position = position;
            GrabbedObject.transform.rotation = rotation;

        }
    }
}
