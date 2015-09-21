using UnityEngine;
using System.Collections;

public class StarryNightCameraCtr : MonoBehaviour
{
    public Transform camLookAtPot;
    public Transform camera;
    public Camera camObj;
    public float distance = 3;
    public float initPosZ;

    public Vector3 dir;

    void Start()
    {
        dir = camera.forward; 
        initPosZ =  camera.transform.position.z;
    }

    void OnEnable()
    {
        FingerGestures.OnTap += FingerGestures_OnTap;
        FingerGestures.OnDragMove += FingerGestures_OnDrag;
        FingerGestures.OnPinchMove += FingerGestures_OnPinchMove;
    }

    void OnDisable()
    {
        FingerGestures.OnTap -= FingerGestures_OnTap;
        FingerGestures.OnDragMove -= FingerGestures_OnDrag;
        FingerGestures.OnPinchMove -= FingerGestures_OnPinchMove;
    }

    void FingerGestures_OnTap(Vector2 fingerPos, int tapCount)
    { 
        if (StarrynightUIManager.Instance.IsShow())
            return;

        Ray ray = camObj.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 60, 1 << LayerMask.NameToLayer("Click")))
        { 
            hit.collider.gameObject.SendMessage("OnClick", SendMessageOptions.DontRequireReceiver);
        } 
    }

    void FingerGestures_OnDrag(Vector2 fingerPos, Vector2 delta)
    {
        
    }


    void FingerGestures_OnPinchMove(Vector2 fingerPos1, Vector2 fingerPos2, float delta)
    { 
        if( StarrynightUIManager.Instance.IsShow())
        { 
            StarrynightUIManager.Instance.Hide();
            return;
        }

        Vector3 vec31 = camera.transform.position += dir * delta * 0.1f; ;
        camera.transform.position += dir  * delta * 0.1f; 
        Vector3 vec3 = camera.transform.position;
        float f1 = Mathf.Min(initPosZ, initPosZ + distance);
        float f2 = Mathf.Max(initPosZ, initPosZ + distance); 
        vec3.z = Mathf.Clamp(vec3.z, f1, f2);
        camera.transform.position = vec3;
        camera.transform.LookAt(camLookAtPot.position);

    }
}
