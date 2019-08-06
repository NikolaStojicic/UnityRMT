using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField]
    private Material greenOff;
    [SerializeField]
    private Material greenOn;

    [SerializeField]
    private Material yellowOff;
    [SerializeField]
    private Material yellowOn;

    [SerializeField]
    private Material redOff;
    [SerializeField]
    private Material redOn;

    [SerializeField]
    public string trafficLightActive;

    private string currentLightActive;

    private Dictionary<string, GameObject> upLights;
    private Dictionary<string, GameObject> downLights;
    private Dictionary<string, Material> lightMaterialsOn;
    private Dictionary<string, Material> lightMaterialsOff;

    private Vector3 colPosition;

    // Start is called before the first frame update
    void Start()
    {
        /// Init lights and materals
        initLights();
        initMaterials();

        /// This collider is used on trafficlight to stop vehicles when red.
        colPosition = GetComponentInChildren<BoxCollider>().transform.position;
        GetComponentInChildren<BoxCollider>().transform.position = new Vector3(0, 0, 0);

        /// Initially close all trafficlights
        setTrafficLightStatus(trafficLightActive);
    }



    /// <summary>
    /// Is used to simulate traffic light opening and closing of traffic light with yellow ligth
    /// </summary>
    /// <param name="isOpen">If true, it is green on traffic light, otherwise it is red.</param>
    /// <param name="sec">Num of seconds to hold yellow light.</param>
    public void animateLights(bool isOpen, float sec)
    {
        if (isOpen && currentLightActive == "Red")
            StartCoroutine(animateLightsOn(sec));
        if (!isOpen && currentLightActive == "Green")
            StartCoroutine(animateLightsOff(sec));
    }
    IEnumerator animateLightsOn(float sec)
    {
        setTrafficLightStatus("Yellow");
        yield return new WaitForSeconds(sec);
        setTrafficLightStatus("Green");
    }
    IEnumerator animateLightsOff(float sec)
    {
        setTrafficLightStatus("Yellow");
        yield return new WaitForSeconds(sec);
        setTrafficLightStatus("Red");
    }

    /// <summary>
    /// Lights up light on traffic light that is provided, makes sure that only one light at the time is lit up.
    /// You may want to change that. setTrafficLightsOff(); does that.
    /// </summary>
    /// <param name="status">Represents light name [Green, Yellow, Red]</param>
    public void setTrafficLightStatus(string status)
    {
        // Checks
        if (status.Length < 3 || status == currentLightActive) return;
        status = status.ToLower();
        status = char.ToUpper(status[0]) + status.Substring(1);
        if (!upLights.ContainsKey(status)) return;

        // Manipulute lights
        setTrafficLightsOff();
        upLights[status].GetComponent<MeshRenderer>().material = lightMaterialsOn[status];
        downLights[status].GetComponent<MeshRenderer>().material = lightMaterialsOn[status];

        // Open light colliders
        if (status == "Red") GetComponentInChildren<BoxCollider>().transform.position = colPosition;
        else GetComponentInChildren<BoxCollider>().transform.position = new Vector3(0, 0, 0);

        // Remmember last status
        currentLightActive = status;
    }

    /// <summary>
    /// Kills all the lights.
    /// </summary>
    private void setTrafficLightsOff()
    {
        foreach (KeyValuePair<string, GameObject> trafficLight in upLights)
        {
            upLights[trafficLight.Key].GetComponent<MeshRenderer>().material = lightMaterialsOff[trafficLight.Key];
            downLights[trafficLight.Key].GetComponent<MeshRenderer>().material = lightMaterialsOff[trafficLight.Key];
        }
    }

    /// <summary>
    /// Simple init for easier manipulation.
    /// </summary>
    private void initMaterials()
    {
        lightMaterialsOff = new Dictionary<string, Material>();
        lightMaterialsOff.Add("Green", greenOff);
        lightMaterialsOff.Add("Yellow", yellowOff);
        lightMaterialsOff.Add("Red", redOff);

        lightMaterialsOn = new Dictionary<string, Material>();
        lightMaterialsOn.Add("Green", greenOn);
        lightMaterialsOn.Add("Yellow", yellowOn);
        lightMaterialsOn.Add("Red", redOn);
    }

    /// <summary>
    /// Simple init for easier manipulation.
    /// </summary>
    private void initLights()
    {
        upLights = findLightsByPositionOnPole("UpLight");
        downLights = findLightsByPositionOnPole("DownLight");
    }

    /// <summary>
    /// Finds all lights on trafficlight object, because of all nesting, this looks like this.
    /// </summary>
    /// <param name="name">Can take UpLight, DownLight, meaning this represents position on pole.</param>
    /// <returns>List that contains all 3 lights.</returns>
    private Dictionary<string, GameObject> findLightsByPositionOnPole(string name)
    {
        Dictionary<string, GameObject> lista = new Dictionary<string, GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject posLight = transform.GetChild(i).gameObject;
            if (posLight.name == name)
            {
                for (int j = 0; j < posLight.transform.childCount; j++)
                {
                    GameObject lights = posLight.transform.GetChild(j).gameObject;
                    if (lights.name == "Lights")
                    {
                        for (int k = 0; k < lights.transform.childCount; k++)
                        {
                            GameObject col = lights.transform.GetChild(k).gameObject;
                            lista.Add(col.name, col);
                        }
                    }
                }
            }
        }
        return lista;
    }
    // Update is called once per frame
    void Update()
    {
    }
}
