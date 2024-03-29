#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    //gracz (jego pozycja)
    Transform player;

    //prefab statycznej asteroidy
    public GameObject staticAsteriod;

    //czas od ostatnio wygenerowanej asteriody
    float timeSinceSpawn;

    // Start is called before the first frame update
    void Start()
    {
        //znajdz gracza i przypisz go do zmiennej 
        player = GameObject.FindWithTag("Player").transform;

        //zeruj czas
        timeSinceSpawn = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //dolicz czas od ostatniej klatki
        timeSinceSpawn += Time.deltaTime;
        //jezeli czas przekroczyl sekunde to spawnuj i zresteuj
        if (timeSinceSpawn > 0.1)
        {
            GameObject asteriod = SpawnAsteriod(staticAsteriod);
            timeSinceSpawn = 0;
        }
        AsteroidCountControll();
    }

    GameObject? SpawnAsteriod(GameObject prefab)
    {

        //stworz losow� pozycj� na okr�gu (x,y)
        Vector2 randomCirclePosition = Random.insideUnitCircle.normalized;

        //losowa pozycja w odleg�o�ci 10 jednostek od �rodka �wiata
        //mapujemy x->x, y->z, a y ustawiamy 0
        Vector3 randomPosition = new Vector3(randomCirclePosition.x, 0, randomCirclePosition.y) * 10;

        //na�� pozycj� gracza - teraz mamy pozycje 10 jednostek od gracza
        randomPosition += player.position;

        //sprawdz czy miejsce jest wolne
        //! oznacza "nie" czyli nie ma nic w promieniu 5 jednostek od miejsca randomPosition
        if (!Physics.CheckSphere(randomPosition, 5))
        {
            //stworz zmienn� asteroid, zespawnuj nowy asteroid korzystaj�c z prefaba
            // w losowym miejscu, z rotacj� domy�ln� (Quaternion.identity)
            GameObject asteroid = Instantiate(staticAsteriod, randomPosition, Quaternion.identity);

            //zwr�� asteroid� jako wynik dzia�ania
            return asteroid;
        }
        else
        {
            return null;
        }
    }
    void AsteroidCountControll()
    {
        //przygotuj tablic� wszystkich asteroid�w na scenie
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");

        //przejd� p�tl� przez wszystkie
        foreach (GameObject asteroid in asteroids)
        {
            //odleg�o�� od gracza 
            
            //wektor przesuni�cia mi�dzy graczem a asteroid�
            //(o ile musze przesun�� gracza, �eby znalaz� si� w miejscu asteroidy
            Vector3 delta = player.position - asteroid.transform.position;
            
            //magnitude to d�ugo�� wekotra od gracza
            float distanceToPlayer = delta.magnitude;

            if (distanceToPlayer > 30)
            {
                Destroy(asteroid);
            }
            
            
        }

    }

}

   

    

