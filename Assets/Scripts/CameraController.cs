using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //wspó³rzêdne gracza
    Transform player;
    //wysokoœæ kamery
    public float cameraHeight = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        //pod³¹cz pozycjê gracza do lokalnej zmiennej korzystaj¹c z jego taga
        //to nie jest zapisanie wartoœc jeden raz tylko referencja do obiektu
        //to znaczy, ¿e player zawszê bêdzie zawiera³ aktualn¹ pozycjê gracza
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //oblicz docelow¹ pozycje kamery
        Vector3 targetPosition = player.position + Vector3.up * cameraHeight;
        //p³ynnie przesuñ kamerê w kierunku gracza
        //funkcja Vector3.Lerp
        //p³ynnie przechodzi z pozycji pierwszego argumentu do pozycji drugiego w czasie trzeciegoo
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);
    }
}
