using UnityEngine;
using System.Collections;
using System.Threading;

public class HiResScreenShots : MonoBehaviour
{
    [SerializeField]
    UI ui;

    [SerializeField]
    ClientConnection cn;

    public int resWidth = 1920;
    public int resHeight = 1080;

    public bool takeHiResShot;
    private new Camera camera;

    private void Start()
    {
        camera = GetComponent<Camera>();
        InvokeRepeating("repeater", 0f, 9f);
    }


    void repeater()
    {
        StartCoroutine(takeScreenShot());
    }

    public static string ScreenShotName(int width, int height)
    {
        return string.Format("{0}/screen.jpg",
                             Application.dataPath,
                             width, height,
                             System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }


    IEnumerator takeScreenShot()
    {
            RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
            camera.targetTexture = rt;
            Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
            yield return new WaitForSeconds(0.3f);
            camera.Render();
            RenderTexture.active = rt;
            screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
            camera.targetTexture = null;
            yield return new WaitForSeconds(0.3f);
            RenderTexture.active = null;
            Destroy(rt);
            byte[] bytes = screenShot.EncodeToJPG();
            string filename = ScreenShotName(resWidth, resHeight);

            //System.IO.File.WriteAllBytes(filename, bytes);
            Debug.Log("Took ScreenShoot");
            cn.dataToSend = bytes;
    }
}