using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIScript : MonoBehaviour
{
    public GameObject player;
    RawImage m_RawImage;
    public Texture HP0, HP1, HP2, HP3, HP4, HP5;

    // Start is called before the first frame update
    void Start()
    {
        m_RawImage = GetComponent<RawImage>();
        m_RawImage.texture = HP1;
    }

    // Update is called once per frame
    void Update()
    {
       
        m_RawImage = GetComponent<RawImage>();
        
        if (player.GetComponent<PlayerHealth>().health == 5) { m_RawImage.texture = HP5; }
        if (player.GetComponent<PlayerHealth>().health == 4) { m_RawImage.texture = HP4; }
        if (player.GetComponent<PlayerHealth>().health == 3) { m_RawImage.texture = HP3; }
        if (player.GetComponent<PlayerHealth>().health == 2) { m_RawImage.texture = HP2; }
        if (player.GetComponent<PlayerHealth>().health == 1) { m_RawImage.texture = HP1; }
        if (player.GetComponent<PlayerHealth>().health == 0) { m_RawImage.texture = HP0; }
    }
}
