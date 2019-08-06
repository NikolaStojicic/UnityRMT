using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBodyRandomizer : MonoBehaviour
{
    [SerializeField]
    private List<Material> materials;
    [SerializeField]
    private List<Mesh> meshes;
    [SerializeField]
    private List<Material> colors;

    // Start is called before the first frame update
    /// <summary>
    /// Makes sure that cars are random.
    /// </summary>
    void Start()
    {
        int matIndex = Random.Range(0, materials.Count);
        int meshIndex = Random.Range(0, meshes.Count);
        GetComponent<MeshRenderer>().material = materials[matIndex];
        GetComponent<MeshFilter>().mesh = meshes[meshIndex];

        if(colors.Count > 0)
        {
            Material[] mats = new Material[2];
            mats[0] = materials[matIndex];
            mats[1] = colors[Random.Range(0, colors.Count)];
            GetComponent<MeshRenderer>().materials = mats;
        }

    }

}
