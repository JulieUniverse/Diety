using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {

    public AudioClip killPigAudio;
    private AudioSource source;
    public static GameMaster gm;
    public Transform player;
    public Transform spawnPoint;
    private Health health;
    public GameObject gameOverScreen;
    public bool death;
    private Poop poopScript;
    CharacterController2D controller;
    void Start()
    {
        source = GetComponent<AudioSource>();
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }
    }

    void Awake()
    {
        health = FindObjectOfType<Health>();
        poopScript = FindObjectOfType<Poop>();
        controller = FindObjectOfType<CharacterController2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            KillPlayer();
            //poopScript.SelfDestruct();
            StartCoroutine(RespawnPlayer(player));
            //RespawnPlayer();
            GameOverOn();
            //Restart();
            //Invoke("Restart",1f);
        }
        //death = false;
    }

    void KillPlayer()
    {
        death = true;
        source.PlayOneShot(killPigAudio);
        health.lives -= 1;
        Debug.Log(health.lives);
    }
    IEnumerator RespawnPlayer (Transform player) {
        yield return new WaitForSeconds(1f);
        player.position = new Vector2(spawnPoint.position.x, spawnPoint.position.y);
        controller.currentPoops = 3;
        death = false;
        yield return false;
    }

    void Restart()
    {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOverOn()
    {
        if (health.lives <= 0)
        {
            GameObject.FindGameObjectWithTag("Player").SetActive(false);
            gameOverScreen.SetActive(true);
        }

    }
}
