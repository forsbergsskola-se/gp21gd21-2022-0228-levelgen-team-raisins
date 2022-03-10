using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DisallowMultipleComponent]
public class ObjectPool : MonoBehaviour
{
   Dictionary<string, Queue<GameObject>> objectPool = new Dictionary<string, Queue<GameObject>>();

   public GameObject GetObject(GameObject insertedGameObject, Transform insertedTransform = null)
   {
      //Checks if the queue can be found
      if (objectPool.TryGetValue(insertedGameObject.name, out Queue<GameObject> objectList))
      {
         
         //If the queue does not contain the inserted game object, we create a new one.
         if (objectList.Count == 0)
         {
            return CreateNewObject(insertedGameObject, insertedTransform);
         }
         else
         {
            //If the queue does contain the inserted game object, dequeue it and set it active and then return it.
            GameObject _object = objectList.Dequeue();
            _object.SetActive(true);
            return _object;
         }
      }
      else
      {
         return CreateNewObject(insertedGameObject, insertedTransform);
      }
   }

   GameObject CreateNewObject(GameObject insertedGameObject, Transform insertedTransform = null)
   {
      GameObject newGameObject = Instantiate(insertedGameObject, insertedTransform);
      newGameObject.name = insertedGameObject.name;
      
      return newGameObject;
   }

   public void ReturnGameObject(GameObject insertedGameObject)
   {
      if (objectPool.TryGetValue(insertedGameObject.name, out Queue<GameObject> objectList))
      {
         objectList.Enqueue(insertedGameObject);
      }
      else
      {
         Queue<GameObject> newObjectQueue = new Queue<GameObject>();
         newObjectQueue.Enqueue(insertedGameObject);
         objectPool.Add(insertedGameObject.name,newObjectQueue);
      }
      insertedGameObject.SetActive(false);
   }
}
