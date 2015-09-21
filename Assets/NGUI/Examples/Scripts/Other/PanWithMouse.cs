using UnityEngine;

/// <summary>
/// Placing this script on the game object will make that game object pan with mouse movement.
/// </summary>

[AddComponentMenu("NGUI/Examples/Pan With Mouse")]
public class PanWithMouse : MonoBehaviour
{
	public Vector2 degrees = new Vector2(5f, 3f);
	public float range = 1f;

	Transform mTrans;
	Quaternion mStart;
	Vector2 mRot = Vector2.zero;


    public float xd = 0;
    public float yd = 0;
	void Start ()
	{
		mTrans = transform;
		mStart = mTrans.localRotation;
        FingerGestures.OnDragMove += FingerGestures_OnDrag;
	}

    void FingerGestures_OnDrag(Vector2 fingerPos, Vector2 delta)
    { 
        Rotate();
    }

    void Rotate()
	{
		float delta = RealTime.deltaTime;
		//Vector3 pos = Input.GetAxis();//Input.mousePosition;
        {
            float yr = Input.GetAxis("Mouse Y") * 0.05f;
            float xr = Input.GetAxis("Mouse X") * 0.05f;

            xd += xr;
            yd += yr;


            xd = Mathf.Clamp(xd, -1f, 1f);
            yd = Mathf.Clamp(yd, -1f, 1f);
            mRot = Vector2.Lerp(mRot, new Vector2(xd, yd), delta * 5f);

            mTrans.localRotation = mStart * Quaternion.Euler(-mRot.y * degrees.y, mRot.x * degrees.x, 0f);
        }
        
	}
}
