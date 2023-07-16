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

        sr = GameObject.FindWithTag("Transition").GetComponent<SpriteRenderer>();
        //load first half trans
        float transitionProg = 0f;
        sr.material.SetFloat("_CutOff", transitionProg);
        while (sr.material.GetFloat("_CutOff") < 1.2f)
        {
            transitionProg += .01f;
            sr.material.SetFloat("_CutOff", transitionProg);
            yield return new WaitForSeconds(.05f / speed);
        }

        yield return SceneManager.LoadSceneAsync(sceneToLoad);
        
        var destPortal = FindObjectsOfType<Portal>().First(x => x != this && x.destinationPortal == this.destinationPortal);
        player.transform.position = destPortal.spawnPoint.position;

        //load second half trans
        while (sr.material.GetFloat("_CutOff") > 0f)
        {
            transitionProg -= .01f;
            sr.material.SetFloat("_CutOff", transitionProg);
            yield return new WaitForSeconds(.05f / speed);
        }

        GameController.Instance.PauseGame(false);

        Destroy(gameObject);
    }

    public Transform SpawnPoint => spawnPoint;
}

public enum DestinationIdentifier {  A, B, C, D, E, F }
