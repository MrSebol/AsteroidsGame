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
        //generyczna funkcja sluzaca do wylosowania wsp�rz�dnych i umieszczenia
        // w tym miejscu asteriody z prefaba
        
        //losowa pozycja w odleg�o�ci 10 jednostek od �rodka �wiata
        Vector3 randomPosition = Random.onUnitSphere * 10;

        //na�� pozycj� gracza - teraz mamy pozycje 10 jednostek od gracza
        randomPosition += player.position;

        //stw�rz zmienn� asteriod, zespawnuj nowy asterioid korzystaj�c z prefaba
        // w losowym miejscu, z rotacj� domy�ln� (Quaternion.indentity)
        GameObject asteriod = Instantiate(staticAsteriod, randomPosition,Quaternion.identity);

        //zwr�� asteroid� jako wynik dzia�ania
        return asteriod;
    }
}
