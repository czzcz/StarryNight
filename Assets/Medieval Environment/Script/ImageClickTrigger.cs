using UnityEngine;
using System.Collections;

public class ImageClickTrigger : MonoBehaviour 
{
    public pageType type;

    public int ImageID = 1;
    public bool showTestBtn = true;

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
        if (isShow)
        {
            Vector3 vec3 = viewPointTrans.position - camera.transform.position;
            camera.transform.position += vec3 * Time.deltaTime * 3;
            camera.transform.rotation = Quaternion.Slerp(camera.transform.rotation, viewPointTrans.rotation, Time.deltaTime * 3);
            if (vec3.magnitude < 0.1f)
            {
                UIEventManager  uiem = FindObjectOfType(typeof(UIEventManager)) as UIEventManager;
                uiem.CurrClickImageID = ImageID;
                uiem.showTestBtn = showTestBtn;
                UIManager.Instance.Show(type);
                isShow = false;
            }
        }
    }
}
