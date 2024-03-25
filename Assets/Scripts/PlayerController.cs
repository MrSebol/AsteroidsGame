using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float flySpeed = 5f;
    //odniesienie do menadzera poziomu
    GameObject levelManagerObject;

    //stan os³on w procentach (1=100%)
    float shieldCapacity = 1;
    
    
    // Start is called before the first frame update
    void Start()
    {
        levelManagerObject = GameObject.Find("LevelManager");
    }

    // Update is called once per frame
    void Update()
    {
        //dodaj do wspó³rzêdnych wartoœæ x=1, y=0, z=0 pomno¿one przez czas
        //mierzony w sekundach od ostatniej klatki
        //transform.position += new Vector3(1, 0, 0) *Time.deltaTime;

        //prezentacja dzia³ania wyg³adzonego sterowania (emulacja joysticka)
        // Debug.Log(Input.GetAxis("Vertical"));

        //sterowanie prêdkoœci¹
        //stwóz nowy wektor przesuniêcia o wartoœci 1 do przodu
        Vector3 movement = transform.forward;
        //pomnó¿ go przez czas od ostatniej klatki
        movement *= Time.deltaTime;
        //pomnó¿ go przez "wychylenie joysticka"
        movement *= Input.GetAxis("Vertical");
        //pomnó¿ przez prêdkoœæ lotu
        movement *= flySpeed;
        //dodaj ruch do obiektu
        //zmiana na fizyke
        // --- transform.position += movement;
        
        //komponent fizyki wewn¹trz gracza
        Rigidbody rb = GetComponent<Rigidbody>();
        //dodaj si³e - do przodu statku w trybie zmiany prêdkoœci
        transform.GetComponent<Rigidbody>().AddForce(movement, ForceMode.VelocityChange);


        //obrót
        //modyfikuj oœ "Y" obiekty player
        Vector3 rotation = Vector3.up;
        //przemnó¿ prze czas
        rotation *= Time.deltaTime;
        //przemnó¿ przez klawiaturê
        rotation *= Input.GetAxis("Horizontal");
        //pomnó¿ przez prêdkoœæ obrotu
        rotation *= rotationSpeed;
        
        //dodaj obrót do obiekt
        //nie mo¿emy u¿yæ += poniewa¿ unity u¿ywa Quaternionów do zapisu rotacji
        transform.Rotate(rotation);
        UpdateUI();
    }

    private void UpdateUI()
    {
        //metoda wykonuje wszystko z aktualizacj¹ interfejsu u¿ytkownika
        
        //wyci¹gnij z menadzera poziomu pozycje wyjœcia
        Vector3 target = levelManagerObject.GetComponent<LevelManager>().exitPositon;
        //obroc znacznik w strone wyjscia
        transform.Find("NavUI").Find("TargetMarker").LookAt(target);
        //zmieñ iloœc procentów widoczna w interfejsie
        //TODO; poprawidx
        TextMeshPro shieldText = 
            GameObject.Find("Canvas").transform.Find("ShieldCapacityText").GetComponent<TextMeshPro>();
        shieldText.text = " Shield: " + shieldCapacity.ToString() + "%";
    }
    private void OnCollisionEnter(Collision collision)
    {
        //uruchamia siê automatycznie jeœli zetkniemy siê z innym coliderem

        //sprawdzamy czy dotknêliœmy asteroidy
        if (collision.collider.transform.CompareTag("Asteroid"))
        {
            //tranform asteroidy
            Transform asteroid = collision.collider.transform;
            
            //policz wektor wed³ug którego odepchniemy asteroide
            Vector3 sheilForce = asteroid.position - transform.position;
            //popchnij asteroide
            asteroid.GetComponent<Rigidbody>().AddForce(sheilForce * 5, ForceMode.Impulse);
            shieldCapacity -= 0.25f;
        } 
    }
}
