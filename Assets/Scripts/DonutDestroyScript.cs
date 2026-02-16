using TMPro;
using UnityEngine;

public class DonutDestroyScript : MonoBehaviour
{
    SFX_Script sfx;
    public TMP_Text counterText;
    private int destroyedDonuts = 0;

    void Start()
    {
        sfx = FindFirstObjectByType<SFX_Script>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        Destroy(collision.gameObject);

        destroyedDonuts++;

        if (sfx != null)
            sfx.PlaySFX(3);

        if (counterText != null)
            counterText.text = "Destroyed:\n" + destroyedDonuts;
    }
}