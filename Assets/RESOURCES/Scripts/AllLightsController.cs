using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllLightsController : MonoBehaviour
{
    [SerializeField]
    public bool light1;
    [SerializeField]
    public bool light2;
    [SerializeField]
    public bool light3;
    [SerializeField]
    public bool light4;

    [SerializeField]
    private float yellowTime;

    private List<LightController> lightControllers;
    // Start is called before the first frame update
    void Start()
    {
        initControllers();
    }
    // Inits all refs to individual trafficlight controllers into list for easier manipulation.
    private void initControllers()
    {
        lightControllers = new List<LightController>();
        for (int i = 0; i < transform.childCount; i++)
            lightControllers.Add(transform.GetChild(i).gameObject.GetComponent<LightController>());
    }
    // Update is called once per frame
    void Update()
    {
       // Updates traffic light every frame tied to checkboxes light1, light2..
        int i = 1;
        foreach (var lc in lightControllers)
            lc.animateLights((bool)this.GetType().GetField("light" + i++).GetValue(this), yellowTime);
    }
}
