using UnityEngine;
using System.Collections;

public class PlayerLook : MonoBehaviour 
{
    public float sensiX = 10f;
    public float sensiY = 10f;
    public int touchIndex = -1;

    public enum Rotation
    {
        MouseX,
        MouseY
    };
    public Rotation rotation = Rotation.MouseX;
    float inputX, inputY;
    float ry = 0;
	
	// Update is called once per frame
	void Update () 
	{
       
        if (!(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer))
        {
            inputX = Input.GetAxis("Mouse X");
            inputY = Input.GetAxis("Mouse Y");
        }
        else
        {
            for (int i = 0; i < Input.touches.Length; i++)
            {
                Touch touch = Input.touches[i];

                if (touch.fingerId == touchIndex)
                    continue;
                else
                {
                    inputX = Mathf.Clamp(touch.deltaPosition.x, -1, 1);
                    inputY = Mathf.Clamp(touch.deltaPosition.y, -1, 1);
                }
                
            }
        }

        if (rotation == Rotation.MouseX)
        {
            transform.Rotate(0, inputX * sensiX, 0);
        }
        else if (rotation == Rotation.MouseY)
        {
          
            ry += inputY * sensiY;
            ry = Mathf.Clamp(ry, -80f, 80f);
            transform.localEulerAngles = new Vector3(ry, 0, 0);
            
         //   Debug.Log("InputX:" + transform.eulerAngles.x + "    " + "InputY:" + transform.eulerAngles.y);
         //   transform.Rotate(inputY * sensiY, 0, 0);
         //   transform.localEulerAngles = new Vector3(Mathf.Clamp(transform.localEulerAngles.x, -80f, 80f), 0, 0);
        }
	}
}
