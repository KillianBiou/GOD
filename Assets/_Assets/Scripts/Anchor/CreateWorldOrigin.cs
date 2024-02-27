using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreateWorldOrigin : MonoBehaviour
{

    public static GameObject worldOrigin { get; private set; }

    private Transform orientation;
    private Transform originPosition;
    [SerializeField]
    private GameObject anchor;

    private bool isCalled = false;

    void Start()
    {
        worldOrigin = anchor;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void setAnchor(Transform anchor, string val)
    {
        if (val == "Anchor1")
        {
            originPosition = anchor;
            WorldOrigin.LeftAnchor = anchor;

        }
        else if (val == "Anchor2")
        {
            orientation = anchor;
            WorldOrigin.RightAnchor = anchor;
        }

        if (originPosition != null && orientation != null)
        {
            //isDone
            SceneManager.LoadScene("GameplayScene", LoadSceneMode.Additive);
        }
    }

    public void needAnchor()
    {
        if(isCalled) { 
            return;
        }
        isCalled = true;
        SceneManager.LoadScene("AnchorCreator", LoadSceneMode.Additive);
    }
}
