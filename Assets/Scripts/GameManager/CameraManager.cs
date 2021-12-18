using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] GameObject P1;
    [SerializeField] GameObject P2;
    [SerializeField] [Range(0.1f, 10)] float camSpeed = 1;
    [SerializeField] [Range(0.1f, 10)] float camSizeSpeed = 0.5f;
    [SerializeField] [Range(5, 20)] float maxCamSize = 7;
    [SerializeField] [Range(5, 20)] float minCamSize = 5;
    [SerializeField] [Range(-20, 20)] float minCamY = -2;
    [SerializeField] [Range(-20, 20)] float maxCamY = 3;
    [SerializeField] float LayerInDistance = 14.5f;
    Camera camera;
    BoxCollider2D[] ArrayCol;

    float camSize = 5;
    public float CamSize
    {
        get { return camSize; }
        set 
        {
            if (value < minCamSize)
                camSize = minCamSize;
            else if (value > maxCamSize)
                camSize = maxCamSize;
            else camSize = value; 
        }
    }

    private void Awake()
    {
        if (camera == null) camera = this.GetComponent<Camera>();
        if (P1 == null) P1 = GameObject.Find("P1");
        if (P2 == null) P2 = GameObject.Find("P2");
        ArrayCol = this.GetComponents<BoxCollider2D>();
    }

    void Start()
    {
        camera.orthographicSize = camSize;
    }

    void LateUpdate()
    {
        camSizeSet();
        camMove();
    }

    void camMove()
    {
        float x = (P1.transform.position.x + P2.transform.position.x) / 2;
        float y = Utils.setWithMaxMin(minCamY, maxCamY, (P1.transform.position.y + P2.transform.position.y) / 2) + camera.orthographicSize - 5 ;
        Vector3 pos = new Vector3(x, y, -10);
        camera.transform.position = Vector3.Lerp(camera.transform.position, pos, Time.deltaTime * camSpeed);
    }

    void camSizeSet()
    {
        CamSize = Utils.Bigger(Utils.Distance(camera.transform.position.x, P1.transform.position.x),
            Utils.Distance(camera.transform.position.x, P2.transform.position.x)) / 1.5f;

        if (camera.orthographicSize != camSize)
        {
            if (camera.orthographicSize < camSize)
                camera.orthographicSize += Time.deltaTime * camSizeSpeed;
            else if (camera.orthographicSize > camSize)
                camera.orthographicSize -= Time.deltaTime * camSizeSpeed;
            if (Mathf.Abs(camera.orthographicSize - camSize) < 0.01f)
                camera.orthographicSize = camSize;
        }
        Vector2 offset = new Vector2(LayerInDistance, 0) * camera.orthographicSize / 5;
        ArrayCol[0].offset = offset;
        ArrayCol[1].offset = -offset;
    }

    void playerIsInLayer(GameObject InPlayer)
    {
        GameObject OutPlayer;
        if (InPlayer == P1) OutPlayer = P2;
        else OutPlayer = P1;

        P1.GetComponent<Player>().IsStop = true;
        P2.GetComponent<Player>().IsStop = true;

        float distance = Utils.Distance(InPlayer.transform.position.x, OutPlayer.transform.position.x) - 2;

        if (transform.position.x < InPlayer.transform.position.x)
            OutPlayer.transform.position = InPlayer.transform.position + new Vector3(distance, 0, 0);
        else
            OutPlayer.transform.position = InPlayer.transform.position + new Vector3(-distance, 0, 0);

        StartCoroutine(InLayerAfterCamera());
    }

    IEnumerator InLayerAfterCamera()
    {
        camSpeed *= 3;
        yield return new WaitForSeconds(2f);
        camSpeed /= 3;
        P1.GetComponent<Player>().IsStop = false;
        P2.GetComponent<Player>().IsStop = false;
        isPlayerIn = false;
    }

    bool isPlayerIn = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && !isPlayerIn)
        {
            isPlayerIn = true;
            playerIsInLayer(collision.gameObject);
        }
    }
}

public class Utils
{
    public static float Distance(float x, float y)
    {
        if (x < y) return y - x;
        else if (x > y) return x - y;
        else return 0;
    }

    public static float Bigger(float x, float y)
    {
        if (x < y) return y;
        else return x;
    }

    public static bool Bigger(float x, float y, bool isBool)
    {
        if (x > y) return true;
        else return false;
    }

    public static float setWithMaxMin(float min, float max, float value)
    {
        if (value < min) return min;
        else if (value > max) return max;
        else return value;
    }
}

// 17 : 8 = 10 : 5