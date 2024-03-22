using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PositionFollower))]
public class ViewBobbing : MonoBehaviour
{
    public float effectIntensity;
    public float effectIntensityX;
    public float effectSpeed;

    private PositionFollower FollowerInstance;
    private Vector3 OriginalOffset;
    private float SinTime;
    // Start is called before the first frame update
    void Start()
    {
        FollowerInstance = GetComponent<PositionFollower>();   
        OriginalOffset = FollowerInstance.offset;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 inputVector = new Vector3(Input.GetAxis("Vertical"), 0f, Input.GetAxis("Horizontal"));
        if (inputVector.magnitude > 10f)
        {
            SinTime += Time.deltaTime * effectSpeed;
        }
        else
        {
            SinTime = 0f;
        }
        float sinAmountY = -Mathf.Abs(effectIntensity * Mathf.Sin(SinTime));
        Vector3 sinAmountX = FollowerInstance.transform.right * effectIntensity * Mathf.Cos(SinTime) * effectIntensityX;

        FollowerInstance.offset = new Vector3
        {
            x = OriginalOffset.x,
            y = OriginalOffset.y + sinAmountY,
            z = OriginalOffset.z
        };
        FollowerInstance.offset += sinAmountX;
    }
}
