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

    //stan os�on w procentach (1=100%)
    float shieldCapacity = 1;
    
    
    // Start is called before the first frame update
    void Start()
    {
        levelManagerObject = GameObject.Find("LevelManager");
    }

    // Update is called once per frame
    void Update()
    {
        //dodaj do wsp�rz�dnych warto�� x=1, y=0, z=0 pomno�one przez czas
        //mierzony w sekundach od ostatniej klatki
        //transform.position += new Vector3(1, 0, 0) *Time.deltaTime;

        //prezentacja dzia�ania wyg�adzonego sterowania (emulacja joysticka)
        // Debug.Log(Input.GetAxis("Vertical"));

        //sterowanie pr�dko�ci�
        //stw�z nowy wektor przesuni�cia o warto�ci 1 do przodu
        Vector3 movement = transform.forward;
        //pomn� go przez czas od ostatniej klatki
        movement *= Time.deltaTime;
        //pomn� go przez "wychylenie joysticka"
        movement *= Input.GetAxis("Vertical");
        //pomn� przez pr�dko�� lotu
        movement *= flySpeed;
        //dodaj ruch do obiektu
        //zmiana na fizyke
        // --- transform.position += movement;
        
        //komponent fizyki wewn�trz gracza
        Rigidbody rb = GetComponent<Rigidbody>();
        //dodaj si�e - do przodu statku w trybie zmiany pr�dko�ci
        transform.GetComponent<Rigidbody>().AddForce(movement, ForceMode.VelocityChange);


        //obr�t
        //modyfikuj o� "Y" obiekty player
        Vector3 rotation = Vector3.up;
        //przemn� prze czas
        rotation *= Time.deltaTime;
        //przemn� przez klawiatur�
        rotation *= Input.GetAxis("Horizontal");
        //pomn� przez pr�dko�� obrotu
        rotation *= rotationSpeed;
        
        //dodaj obr�t do obiekt
        //nie mo�emy u�y� += poniewa� unity u�ywa Quaternion�w do zapisu rotacji
        transform.Rotate(rotation);
        UpdateUI();
    }

    private void UpdateUI()
    {
        //metoda wykonuje wszystko z aktualizacj� interfejsu u�ytkownika
        
        //wyci�gnij z menadzera poziomu pozycje wyj�cia
        Vector3 target = levelManagerObject.GetComponent<LevelManager>().exitPositon;
        //obroc znacznik w strone wyjscia
        transform.Find("NavUI").Find("TargetMarker").LookAt(target);
        //zmie� ilo�c procent�w widoczna w interfejsie
        //TODO; poprawidx
        TextMeshPro shieldText = 
            GameObject.Find("Canvas").transform.Find("ShieldCapacityText").GetComponent<TextMeshPro>();
        shieldText.text = " Shield: " + shieldCapacity.ToString() + "%";
    }
    private void OnCollisionEnter(Collision collision)
    {
        //uruchamia si� automatycznie je�li zetkniemy si� z innym coliderem

        //sprawdzamy czy dotkn�li�my asteroidy
        if (collision.collider.transform.CompareTag("Asteroid"))
        {
            //tranform asteroidy
            Transform asteroid = collision.collider.transform;
            
            //policz wektor wed�ug kt�rego odepchniemy asteroide
            Vector3 sheilForce = asteroid.position - transform.position;
            //popchnij asteroide
            asteroid.GetComponent<Rigidbody>().AddForce(sheilForce * 5, ForceMode.Impulse);
            shieldCapacity -= 0.25f;
        } 
    }
}
