using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Management;
using Node = UnityEngine.XR.XRNode;

public class HeadScaler : MonoBehaviour
{
    public Vector3 maxHeadMovementScale;
    public Vector3 minHeadMovementScale;
    public float maxHeight;
    public Vector3 offset;

    private void Awake()
    {
        /*if (XRGeneralSettings.Instance.Manager.activeLoader != null)
        {
            XRGeneralSettings.Instance.Manager.StopSubsystems();
            XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        }

        XRGeneralSettings.Instance.Manager.InitializeLoaderSync();
        XRGeneralSettings.Instance.Manager.StartSubsystems();*/

    }
    private void Start()
    {
        transform.position = offset;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            transform.position = offset;
        }
    }

    private void FixedUpdate()
    {
        Vector3 centerEyePosition = Vector3.zero;
        if (OVRNodeStateProperties.GetNodeStatePropertyVector3(Node.CenterEye, NodeStatePropertyType.Velocity,
                       OVRPlugin.Node.EyeCenter, OVRPlugin.Step.Render, out centerEyePosition))
            transform.Translate(Vector3.Scale(centerEyePosition, GetHeightFactor()));
    }

    private Vector3 GetHeightFactor()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity))
        {
            float height = transform.position.y - hit.point.y;

            return Vector3.Lerp(maxHeadMovementScale, minHeadMovementScale, height/maxHeight);
        }

        return maxHeadMovementScale;
    }

    private void OnApplicationQuit()

    {

        if (XRGeneralSettings.Instance.Manager.activeLoader != null)
        {
            XRGeneralSettings.Instance.Manager.StopSubsystems();
            XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        }

    }
}
