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
        if(timeSinceSpawn > 1) 
        {
            GameObject asteriod = SpawnAsteriod(staticAsteriod);
            timeSinceSpawn = 0;
        }
        
    }

    GameObject SpawnAsteriod(GameObject prefab)
    {
        //generyczna funkcja sluzaca do wylosowania wspó³rzêdnych i umieszczenia
        // w tym miejscu asteriody z prefaba
        
        //losowa pozycja w odleg³oœci 10 jednostek od œrodka œwiata
        Vector3 randomPosition = Random.onUnitSphere * 10;

        //na³ó¿ pozycjê gracza - teraz mamy pozycje 10 jednostek od gracza
        randomPosition += player.position;

        //stwórz zmienn¹ asteriod, zespawnuj nowy asterioid korzystaj¹c z prefaba
        // w losowym miejscu, z rotacj¹ domyœln¹ (Quaternion.indentity)
        GameObject asteriod = Instantiate(staticAsteriod, randomPosition,Quaternion.identity);

        //zwróæ asteroidê jako wynik dzia³ania
        return asteriod;
    }
}
