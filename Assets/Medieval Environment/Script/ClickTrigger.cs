using UnityEngine;
using System.Collections;

public class ClickTrigger : MonoBehaviour {

    public string showText;

    public pageType type;
 
    public Transform viewPointTrans;

    Camera camera;
    bool isShow = false;

    void OnClick(Camera cam)
    { 
        isShow = true;
        camera = cam;
        camera.transform.parent = null; 
    }

    void Update()
    {
        if(isShow)
        {
            Vector3 vec3 = viewPointTrans.position - camera.transform.position;
            camera.transform.position += vec3 * Time.deltaTime*3;
            camera.transform.rotation = Quaternion.Slerp(camera.transform.rotation, viewPointTrans.rotation,Time.deltaTime*3);
            if(vec3.magnitude<0.1f)
            {
                UIManager.Instance.Show(type);
                isShow = false;
            }
        }
    }
}
