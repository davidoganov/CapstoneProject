using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour, IPlayerTriggerable
{
    [SerializeField] int sceneToLoad = -1;
    [SerializeField] DestinationIdentifier destinationPortal;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float speed;
    SpriteRenderer sr;

    PlayerController player;

    public void OnPlayerTriggered(PlayerController player)
    {
        this.player = player;
        StartCoroutine(SwitchScene());
    }

    IEnumerator SwitchScene()
    {
        DontDestroyOnLoad(gameObject);

        GameController.Instance.PauseGame(true);

        yield return TransitionManager.Instance.enteringHouse();

        yield return SceneManager.LoadSceneAsync(sceneToLoad);
        
        var destPortal = FindObjectsOfType<Portal>().First(x => x != this && x.destinationPortal == this.destinationPortal);
        player.transform.position = destPortal.spawnPoint.position;

        yield return TransitionManager.Instance.playerSpawning();

        GameController.Instance.PauseGame(false);

        Debug.Log("loading scene: " + sceneToLoad);
        if (sceneToLoad == 3)
        {
            MapController.Instance.hideMap();
            MapController.Instance.inside = true;
        }
        else
        {
            MapController.Instance.revealMap();
            MapController.Instance.inside = false;
        }

        Destroy(gameObject);
    }

    public Transform SpawnPoint => spawnPoint;
}

public enum DestinationIdentifier {  A, B, C, D, E, F }
