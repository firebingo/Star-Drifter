using UnityEngine;
using System.Collections;

public class PlayerRespawn : MonoBehaviour {

    [SerializeField]
    private float respawnTime = 3;
    private float respawnTimer = 0;

    void Update()
    {
        Respawn();
    }

    void OnEnabled()
    {
        respawnTimer = 0;
    }

    public void Respawn()
    {
        if (respawnTimer >= respawnTime)
        {
            foreach (MonoBehaviour c in GetComponents<MonoBehaviour>())
                c.enabled = true;
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<PlayerRespawn>().enabled = false;
            respawnTimer = 0;
        }
        respawnTimer += Time.deltaTime;
    }
}
