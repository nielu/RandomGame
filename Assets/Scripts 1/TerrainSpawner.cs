using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TerrainSpawner : MonoBehaviour
{

 public GameObject[] terrainToSpawn;
 public GameObject[] terrainFinishers;
 public GameObject[] terrainStarters;

 public GameObject[] ItemsToSpawn;
 public GameObject[] enemyToSpawn;

 public float startSpawnPosition = 11.2f;
 public float spawnYPos = 0;


 float lastPosition;
 // camera reference
 GameObject cam;
 // used to check if terrain can be generated depending on the camera position and lastposition
 bool canSpawn = true;

 private List<GameObject> mapElements = new List<GameObject>();

 private float garbageTimer = 0f;
 private LayerMask groundMask;
 private LayerMask objectMask;

 private bool lastExitSmooth;

 void Start()
 {
  // make the lastposition start at start spawn position
  lastPosition = startSpawnPosition;
  // pair camera to camera reference
  cam = GameObject.Find("MainCamera");
  spawnYPos = 0;

  groundMask = LayerMask.GetMask("Ground");
  objectMask = LayerMask.GetMask("GroundObject");

  lastExitSmooth = false;

 }

 void Update()
 {
  // if the camera is farther than the number last position minus 16 terrain is spawned
  // a lesser number may make the terrain 'pop' into the scene too early
  // showing the player the terrain spawning which would be unwanted
  if (cam.transform.position.x >= lastPosition - 16 && canSpawn == true)
  {
   // turn off spawning until ready to spawn again
   canSpawn = false;
   // we choose the random number that will determine what terrain is spawned
   var randomChoice = Random.Range(0, terrainToSpawn.Length);

   //randomChoice = 8;
   // SpawnTerrain is called and passed the randomchoice number
   SpawnTerrain(randomChoice);
  }

  garbageTimer += Time.deltaTime;

  if (garbageTimer > 5.0f)
  {
   CollectGarbage();
   garbageTimer = 0;
  }
 }

 private GameObject SpawnItem(GameObject terrain)
 {
  var item = ItemsToSpawn[Random.Range(0, ItemsToSpawn.Length)];
  item.GetComponent<SpriteRenderer>().sortingLayerID = -Random.Range(0, 1);
  var position = new Vector3(lastPosition - Random.Range(0.0f, 10.0f), spawnYPos + 10.0f);

  var ground = Physics2D.Raycast(position, -Vector3.up, 20.0f, groundMask);
  var otherItem = Physics2D.Raycast(position, -Vector3.up, 20.0f, objectMask);
  if (ground && !otherItem)
  {

   position.y = ground.point.y;
   var i = (GameObject)Instantiate(item, position, Quaternion.FromToRotation(Vector3.up, ground.normal));
   i.transform.parent = terrain.transform;

   return i;
  }
  else
   return SpawnItem(terrain);

 }

 void SpawnTerrain(int rand)
 {
  var t = terrainToSpawn[rand];
  var info = t.GetComponent<Terrain>();

  if (spawnYPos + info.ExitHeight < 0)
  {
   SpawnTerrain(Random.Range(0, terrainToSpawn.Length));
   return;
  }


  if (info.CleanEntry)
  {
   if (lastExitSmooth == false) //spawn smmoth exit
   {
    var e = terrainFinishers[Random.Range(0, terrainFinishers.Length)];
    var exit = (GameObject)Instantiate(e, new Vector3(lastPosition, spawnYPos), Quaternion.Euler(0, 0, 0));
    mapElements.Add(exit);
    lastPosition += e.GetComponent<Terrain>().Length;


    spawnYPos += Mathf.PerlinNoise(lastPosition, spawnYPos);
   }

   lastPosition += Random.Range(0.7f, 2.8f); //and introduce random gap
  }
  else
  {
   if (lastExitSmooth) //introduce smooth entry and random gap
   {
    lastPosition += Random.Range(0.7f, 2.8f);

    spawnYPos += Mathf.PerlinNoise(lastPosition, spawnYPos);

    var e = terrainStarters[Random.Range(0, terrainStarters.Length)];
    var entry = (GameObject)Instantiate(e, new Vector3(lastPosition, spawnYPos), Quaternion.Euler(0, 0, 0));
    mapElements.Add(entry);
    lastPosition += e.GetComponent<Terrain>().Length;
   }
   //else proceed normally
  }

  var o = (GameObject)Instantiate(t, new Vector3(lastPosition, spawnYPos), Quaternion.Euler(0, 0, 0));


  for (int i = 0; i < Random.Range(0, ItemsToSpawn.Length); i++)
   SpawnItem(o);

  for (int i = 0; i < Random.Range(0, enemyToSpawn.Length); i++)
   SpawnEnemy();

  lastPosition += info.Length;
  spawnYPos += info.ExitHeight;
  lastExitSmooth = info.CleanExit;

  mapElements.Add(o);

  canSpawn = true;
 }

 private void SpawnEnemy()
 {
  var e = enemyToSpawn[Random.Range(0, enemyToSpawn.Length)];

  var position = new Vector3(lastPosition - Random.Range(0.0f, 10.0f), spawnYPos + 10.0f);

  var ground = Physics2D.Raycast(position, -Vector3.up, 20.0f, groundMask);
  var otherItem = false; // Physics2D.Raycast(position, -Vector3.up, 20.0f, objectMask);
  if (ground && !otherItem)
  {

   position.y = ground.point.y;
   mapElements.Add((GameObject)Instantiate(e, position, Quaternion.FromToRotation(Vector3.up, ground.normal)));
  }
  else
   SpawnEnemy();
 }

 void CollectGarbage()
 {
  if (mapElements.Count > 20)
  {
   for (int i = mapElements.Count - 20; i >= 0; i--)
   {
    Destroy(mapElements[0], 1.0f);
    mapElements.RemoveAt(0);
   }
  }
 }

 public void GameOver()
 {
  foreach (var o in mapElements)
   Destroy(o);

  mapElements.Clear();

  Start();

 }


}

