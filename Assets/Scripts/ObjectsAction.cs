using System.Collections.Generic;
using UnityEngine;

using SRandom = System.Random;


public class ObjectsActions : MonoBehaviour
{
    private List<GameObject> _gameObjects {get;set;} = new List<GameObject>(); 
    private GameObject[] _prefabs;
    private Vector3 _rotationSpeed = new(15, 30, 45);

    void Start()
    {
        _prefabs = Resources.LoadAll<GameObject>("Prefabs");
        Debug.Log($"[ObjectsActions] Загружено префабов: {_prefabs.Length}"); // ← Проверка префабов
        
        InitializeObjects();
        
        // Проверка созданных объектов
        Debug.Log($"[ObjectsActions] Создано объектов: {10}");
        foreach (var obj in _gameObjects)
        {
            Debug.Log($"[ObjectsActions] Объект: {(obj != null ? obj.name : "NULL")}");
        }
    }

    public List<GameObject> GetGameObjects() => _gameObjects;

    public void SetGameObjects(List<GameObject> gameObjects) => _gameObjects = gameObjects;

    private void InitializeObjects()
    {
        var randPrefab = new System.Random();
        float rotation = 0f;
        
        for (int i = 0; i < 10; i++)
        {
            var element = randPrefab.Next(_prefabs.Length);
            GameObject newObj = Instantiate(
                _prefabs[element],
                Vector3.zero,
                Quaternion.Euler(12f + rotation, 0f + rotation, -7f + rotation)
            );
            newObj.SetActive(false);
            _gameObjects.Add(newObj);
            rotation += 15f;
        }
    }


    public void SpawnObjects()
    {
        var random = new SRandom(); 

        foreach (var obj in _gameObjects)
        {
            if (obj == null) continue;

            float pX = (float)(random.NextDouble() * (10.0 - (-9.0)) + (-9.0));
            float pY = (float)(random.NextDouble() * (1.5 - (-1.4)) + (-1.4));
            float pZ = (float)(random.NextDouble() * (7.0 - (-3.5)) + (-3.5));

            obj.transform.localPosition = new Vector3(pX, pY, pZ);
            obj.SetActive(true);
        }
    } 
    
    public void SpinObjects()
    {
        foreach (var obj in _gameObjects)
            obj.transform.Rotate(_rotationSpeed*Time.deltaTime);
    }

}