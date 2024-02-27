using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Village : MonoBehaviour
{
    private static Village instance;
    public static Village Instance
    {
        get { return instance; }
    }

    [SerializeField] private int maxGodPower = 100;
    private int godPower = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        godPower = maxGodPower;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SubstractGodPower(int godPower)
    {
        AddGodPower(-godPower);
    }

    public void AddGodPower(int godPower)
    {
        this.godPower += godPower;

        if (this.godPower > maxGodPower)
        {
            this.godPower = maxGodPower;
        }
        else if (this.godPower < 0)
        {
            this.godPower = 0;
            Debug.Log("END TRIGGER");
        }
    }
}
