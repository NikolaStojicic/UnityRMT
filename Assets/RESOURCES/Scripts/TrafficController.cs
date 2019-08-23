using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class TrafficController : MonoBehaviour
{
    /// <summary>
    /// Space that is used between cars when spawning.
    /// </summary>
    [SerializeField]
    private float carDistance;

    [SerializeField]
    private List<GameObject> carPrefabList;

    List<string> directions;
    private List<string> __wayHelper;


    private Dictionary<string, GameObject> ways;
    // Start is called before the first frame update
    void Start()
    {
        initAllWays();
        moreInits();
      
        carDistance = 9;
        

        spawnCarFromLightRandomDirections("Way3", 0);
        spawnCarFromLightRandomDirections("Way1", 0);
        spawnCarFromLightRandomDirections("Way4", 0);
        spawnCarFromLightRandomDirections("Way2", 0);

        //Dodavanje random automobila
        InvokeRepeating("randomCarSpwaner", 0.5f, 3f);
    }
    private void moreInits()
    {
        directions = new List<string>();
        directions.Add("Left");
        directions.Add("Right");
        directions.Add("Straight");

        __wayHelper = new List<string>();
        for (int i = 1; i <= 4; i++)
            __wayHelper.Add("Way" + i);
    }

    public void randomCarSpwaner()
    {
        addCar(__wayHelper[Random.Range(0,4)], directions[Random.Range(0, 3)]);
    }


    /// <summary>
    /// Spawns from light to back with random routing. Nummber of cars is determined by numOfCars.
    /// </summary>
    /// <param name="track">Track ["Way1","Way2","Way3","Way4"]</param>
    /// <param name="numOfCars">Number of cars that is going to be spawned.</param>
    public void spawnCarFromLightRandomDirections(string track, int numOfCars)
    {

        GameObject way = ways[track];
        GameObject spawner = getSpawnerForWay(way);

        Vector3 wt = way.transform.localPosition;
        Vector3 currentPosition = new Vector3(wt.x, wt.y, wt.z);

        for (int i = 0; i < numOfCars; i++)
        {
            GameObject carPrefab = carPrefabList[Random.Range(0, carPrefabList.Count)];
            carPrefab.transform.rotation = way.transform.rotation;

            WaypointCircuit path = getPathFromWayPoint(way, directions[Random.Range(0,3)]);
            carPrefab.GetComponent<WaypointProgressTracker>().circuit = path;
            if (track == "Way1")
            {
                carPrefab.transform.position = currentPosition + spawner.transform.localPosition;
                currentPosition += new Vector3(0, 0, -carDistance);
            }

            if (track == "Way3")
            {
                carPrefab.transform.position = currentPosition - spawner.transform.localPosition;
                currentPosition += new Vector3(0, 0, carDistance);
            }
            if (track == "Way2")
            {
                Vector3 compensateForRotation = Quaternion.Euler(0, 90, 0) * spawner.transform.localPosition;
                carPrefab.transform.position = currentPosition + compensateForRotation;
                currentPosition += new Vector3(-carDistance, 0, 0.25f);
            }
            if (track == "Way4")
            {
                Vector3 compensateForRotation = Quaternion.Euler(0, -90, 0) * spawner.transform.localPosition;
                carPrefab.transform.position = currentPosition + compensateForRotation;
                currentPosition += new Vector3(carDistance, 0, 0);
            }
            Instantiate(carPrefab, transform);
        }
    }

    /// <summary>
    /// Spawns from light to back. Nummber of cars is determined by directions.Count.
    /// </summary>
    /// <param name="track">Track ["Way1","Way2","Way3","Way4"]</param>
    /// <param name="directions">["Left","Right","Straight"]</param>
    public void spawnCarFromLight(string track, List<string> directions)
    {
        GameObject way = ways[track];
        GameObject spawner = getSpawnerForWay(way);

        Vector3 wt = way.transform.localPosition;
        Vector3 currentPosition = new Vector3(wt.x, wt.y, wt.z);

        for (int i = 0; i < directions.Count; i++)
        {
            GameObject carPrefab = carPrefabList[Random.Range(0, carPrefabList.Count)];
            carPrefab.transform.rotation = way.transform.rotation;

            WaypointCircuit path = getPathFromWayPoint(way, directions[i]);
            carPrefab.GetComponent<WaypointProgressTracker>().circuit = path;
            if (track == "Way1")
            {
                carPrefab.transform.position = currentPosition + spawner.transform.localPosition;
                currentPosition += new Vector3(0, 0, -carDistance);
            }

            if (track == "Way3")
            {
                carPrefab.transform.position = currentPosition - spawner.transform.localPosition;
                currentPosition += new Vector3(0, 0, carDistance);
            }
            if (track == "Way2")
            {
                Vector3 compensateForRotation = Quaternion.Euler(0, 90, 0) * spawner.transform.localPosition;
                carPrefab.transform.position = currentPosition + compensateForRotation;
                currentPosition += new Vector3(-carDistance, 0, 0.25f);
            }
            if (track == "Way4")
            {
                Vector3 compensateForRotation = Quaternion.Euler(0, -90, 0) * spawner.transform.localPosition;
                carPrefab.transform.position = currentPosition + compensateForRotation;
                currentPosition += new Vector3(carDistance, 0, 0);
            }   
            Instantiate(carPrefab, transform);
        }
    }

    /// <summary>
    /// Adds car on the beggining of the way.
    /// </summary>
    /// <param name="track">Track ["Way1","Way2","Way3","Way4"]</param>
    /// <param name="direction">["Left","Right","Straight"]</param>
    public void addCar(string track, string direction)
    {
        GameObject carPrefab = carPrefabList[Random.Range(0, carPrefabList.Count)];
        GameObject way = ways[track];

        WaypointCircuit path = getPathFromWayPoint(way, direction);
        carPrefab.GetComponent<WaypointProgressTracker>().circuit = path;

        carPrefab.transform.position = way.transform.localPosition;
        carPrefab.transform.rotation = way.transform.rotation;
        Instantiate(carPrefab, transform);
    }

    /// <summary>
    /// Gets spawn point, so it can be used to spawn from light to back of the way.
    /// </summary>
    /// <param name="way">["Way1","Way2","Way3","Way4"]</param>
    /// <returns>Spawner object</returns>
    private GameObject getSpawnerForWay(GameObject way)
    {
        for (int i = 0; i < way.transform.childCount; i++)
            if (way.transform.GetChild(i).gameObject.name == "Spawner")
                return way.transform.GetChild(i).gameObject;
        return null;
    }

    /// <summary>
    /// Gets the path for a provided way and direction.
    /// </summary>
    /// <param name="way">["Way1","Way2","Way3","Way4"]</param>
    /// <param name="direction">["Left","Right","Straight"]</param>
    /// <returns>Path object</returns>
    private WaypointCircuit getPathFromWayPoint(GameObject way, string direction)
    {
        GameObject paths = null;

        for (int i = 0; i < way.transform.childCount; i++)
        {
            GameObject w = way.transform.GetChild(i).gameObject;
            if (w.name == "Paths")
            {
                paths = w;
                break;
            }
        }

        if (paths == null)
            throw new System.Exception("PATHS");

        for (int i = 0; i < paths.transform.childCount; i++)
        {
            GameObject p = paths.transform.GetChild(i).gameObject;
            if (p.name == direction)
                return p.GetComponent<WaypointCircuit>();
        }

        return null;
    }


    private void initAllWays()
    {
        ways = new Dictionary<string, GameObject>();
        for (int i = 0; i < transform.childCount; i++)
            ways.Add(transform.GetChild(i).name, transform.GetChild(i).gameObject);
    }


    // Update is called once per frame
    void Update()
    {

    }
}
