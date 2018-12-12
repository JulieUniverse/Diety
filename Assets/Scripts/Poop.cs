using UnityEngine;

public class Poop : MonoBehaviour {

    public CircleCollider2D stinkRadius;
    public BoxCollider2D poopCollider;
    public GameMaster gm;
    private GameObject[] allPoops;


    void Awake()
    {
        gm = FindObjectOfType<GameMaster>();
        allPoops = GameObject.FindGameObjectsWithTag("Poop");
    }
    void FixedUpdate()
    {
        SelfDestruct();
    }
    public void SelfDestruct()
    {
        if (gm.death == true)
        {
            for (int i = 0; i < allPoops.Length; i++)
                GameObject.Destroy(allPoops[i]);
                return;
        }
        //gm.death = false;
    }
}
