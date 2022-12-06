using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 20f;
    public float panBorderThickness = 20f;
    public Vector2 panLimit = new Vector2(20f, 20f);
    private Vector2 panScrollLimit = new Vector2(1, 5);

    public float scrollSpeed = 1f;

    public AudioClip clip;


    void Start()
    {
        GameObject.FindGameObjectWithTag("MusicTag").GetComponent<AudioSource>().clip = clip;
        GameObject.FindGameObjectWithTag("MusicTag").GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        //camera controller
        if(Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos.y += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            pos.y -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }

        //scrolling
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        float size = GetComponent<Camera>().orthographicSize;
        size -= scroll * scrollSpeed * Time.deltaTime*100;

        pos.x = Mathf.Clamp(pos.x, 2F, panLimit.x);
        pos.y = Mathf.Clamp(pos.y, -panLimit.y, 0F);
        GetComponent<Camera>().orthographicSize = Mathf.Clamp(size, panScrollLimit.x, panScrollLimit.y);

        transform.position = pos;
    }
}
