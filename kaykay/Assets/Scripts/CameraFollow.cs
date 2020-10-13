using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    [SerializeField] float smoothSpeed;
    [SerializeField] Vector3 offset;
    Vector3 desiredRotQ = new Vector3(7.22f, -16.803f, 1.249f);
    bool finishCam = false;
    bool aroundPlayer = false;
    // Update is called once per frame
    void LateUpdate()
    {
        if (aroundPlayer)
        {
            transform.RotateAround(target.position, new Vector3(0, .1f, 0), .5f);
            return;
        }
        if (finishCam)
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(desiredRotQ), Time.deltaTime * .6f);
        Vector3 desiredPos = target.position + offset; //istenilen pozisyon
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed); //Yumuşak şekilde geçiş için lerp kullandık
        transform.position = new Vector3(smoothedPos.x, smoothedPos.y, smoothedPos.z);
    }
    public void FinishCam()
    {
        finishCam = true;
        smoothSpeed = 4f;
      //  transform.eulerAngles = new Vector3(13.792f, -32.75f, 2.543f);
        offset = new Vector3(3, 4, -6);
    }
    public void AroundPlayerCam()
    {
        aroundPlayer = true;
    }
}
