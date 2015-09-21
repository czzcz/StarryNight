using UnityEngine;
using System.Collections;

public class CameraCtr : MonoBehaviour 
{
    public float speed =1;
    public float rotationSpeed = 1;
    public CharacterController character;
    public Camera cameraObj;

    public Vector3 tarPos;

    public bool canMove = false;

    public float posOffset = 0.5f;
    public Vector3 direction;
     

    public GameObject hitEffectPrefab;
    ParticleSystem hitEffectInstance;
    bool showHitEffect = false;

    public ImageCtr imageCtr;

    Vector3 localPos;

    public AudioSource  hitTerrainSound;

    public UIScrollBar imageBar;

    NavMeshAgent navMeshAgent = null;

	// Use this for initialization
	void Start () {

        navMeshAgent = GetComponent<NavMeshAgent>();

        tarPos = transform.position; 
        localPos = cameraObj.transform.localPosition; 

        hitEffectInstance = (GameObject.Instantiate(hitEffectPrefab) as GameObject).GetComponent<ParticleSystem>();
	}

    
    public void MoveTo()
    {

        if (!canMove)
        {
            return;
        } 

        direction = tarPos - transform.position;
   
        direction.y = 0;

        if (direction.magnitude > 1)
            direction = direction.normalized * speed * Time.deltaTime;
        else
        {
            direction = direction * speed * Time.deltaTime;
        }

        if (direction.magnitude < posOffset*Time.deltaTime)
        {
            StopMove();
            return;
        }

        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime * 5); 
        //Vector3 eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        // transform.eulerAngles = eulerAngles;    
        //character.Move(direction - Vector3.up*Time.deltaTime*10);
        cameraObj.transform.localRotation = Quaternion.Lerp(cameraObj.transform.localRotation, Quaternion.identity, Time.deltaTime *5); 

        //transform.forward = navMeshAgent.velocity;
        navMeshAgent.updateRotation = true; 
    }
     
	
	// Update is called once per frame
	void LateUpdate ()
    { 
        MoveTo();

        if(canMove)
        {
            if (showHitEffect)
            {
                hitEffectInstance.transform.position = tarPos + Vector3.up * 0.03f;
                hitEffectInstance.time = 0;
                hitEffectInstance.Play();
                showHitEffect = false;
            }
        } 

        if (cameraObj.transform.parent == transform)
        {
            if ((cameraObj.transform.localPosition - localPos).magnitude > 0.015f)
            {
                cameraObj.transform.localPosition += (localPos - cameraObj.transform.localPosition) * Time.deltaTime * 2;
                cameraObj.transform.localRotation = Quaternion.Lerp(cameraObj.transform.localRotation,Quaternion.identity,Time.deltaTime*3);
            }
            else
            {
                cameraObj.transform.localPosition = localPos;
            }
        }

	}

    void StopMove()
    {
        canMove = false;
        if (navMeshAgent.isActiveAndEnabled)
            navMeshAgent.Stop();
        navMeshAgent.enabled = false;
    }

    void Rotate()
    {
        float yr = Input.GetAxis("Mouse Y");
        float xr = Input.GetAxis("Mouse X");

        if (Mathf.Abs ( yr) > 0.001f || Mathf.Abs (xr) > 0.005f)
        {
            StopMove();
        }

        transform.Rotate(Vector3.up,xr);
        cameraObj.transform.Rotate(Vector3.right,-yr);
    }


    void OnEnable()
    {
        FingerGestures.OnTap += FingerGestures_OnTap;
        FingerGestures.OnDragMove+= FingerGestures_OnDrag;
        FingerGestures.OnPinchMove += FingerGestures_OnPinchMove;
        FingerGestures.OnRotationMove += OnRotationMove;
        FingerGestures.OnRotationEnd += OnRotationEnd;
        FingerGestures.OnDragMove += OnDragMove;
    }

    void OnDragMove( Vector2 fingerPos, Vector2 delta )
    {
        if (UIManager.Instance.currPage == pageType.GlobalUI2)
        {
            imageBar.value += delta.y * 0.003f;
        }
    }

    void OnRotationEnd( Vector2 fingerPos1, Vector2 fingerPos2, float totalRotationAngle )
    {
        if (UIManager.Instance.currPage == pageType.ImageUI2)
        {
            imageCtr.ExitRotate();
        }
    }
     
    void  OnRotationMove(Vector2 fingerPos1, Vector2 fingerPos2, float rotationAngleDelta )
    {
        if (UIManager.Instance.currPage == pageType.ImageUI2)
        {
            imageCtr.RotateImage(rotationAngleDelta);
        }
    }

    void FingerGestures_OnTap(Vector2 fingerPos, int tapCount)
    {

        if (UIManager.Instance.currPage == pageType.DaTi)
        {
            StopMove();
            return;
        }

        if (cameraObj.transform.parent != transform)
            return;

        if (Input.GetMouseButton(1))
        {
            return;
        }

        if (UIManager.Instance.IsShow)
            return;

        Ray ray = cameraObj.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 10, 1 << LayerMask.NameToLayer("Click")))
        {
            StopMove();
            hitTerrainSound.Play();
            hit.collider.gameObject.SendMessage("OnClick", cameraObj, SendMessageOptions.DontRequireReceiver);
            return;
        }
        if (Physics.Raycast(ray, out hit, 100, 1 << LayerMask.NameToLayer("Terrain")))
        {
            canMove = true;
            hitTerrainSound.Play();
            tarPos = hit.point;
            showHitEffect = true;

            navMeshAgent.enabled = true;
            navMeshAgent.SetDestination(tarPos);

        }
    }

    void FingerGestures_OnDrag(Vector2 fingerPos, Vector2 delta)
    {
        if (cameraObj.transform.parent != transform)
            return;

        if (!Application.isMobilePlatform)
        {
            if (Input.GetMouseButtonDown(2))
            {
                return;
            }
        }
        if (cameraObj.transform.parent != transform)
            return;

        Rotate();
    }


    void FingerGestures_OnPinchMove( Vector2 fingerPos1, Vector2 fingerPos2, float delta)
    {
        if (UIManager.Instance.currPage == pageType.DaTi || UIManager.Instance.currPage == pageType.GlobalUI2)
        { 
            return;
        }

        if(UIManager.Instance.currPage== pageType.ImageUI2)
        {
            imageCtr.ScaleImage(delta * 0.002f);
            return;
        }

        if (UIManager.Instance.IsShow)
        {
            UIManager.Instance.HideAll();
            cameraObj.transform.parent = transform;
            return;
        } 

       // Vector3 dir = transform.forward * delta*0.005f;
        //character.Move(dir - Vector3.up * Time.deltaTime);
    }

    public void ReturnToMainScene()
    {
        UIManager.Instance.HideAll();
        cameraObj.transform.parent = transform;
        UIManager.Instance.currPage = pageType.GlobalUI1;
    }

    void OnDisable()
    {
        FingerGestures.OnTap -= FingerGestures_OnTap;
        FingerGestures.OnDragMove-= FingerGestures_OnDrag;
        FingerGestures.OnPinchMove -= FingerGestures_OnPinchMove; 
        FingerGestures.OnRotationMove -= OnRotationMove; 
        FingerGestures.OnRotationEnd -= OnRotationEnd; 
        FingerGestures.OnDragMove -= OnDragMove;
    }

}
