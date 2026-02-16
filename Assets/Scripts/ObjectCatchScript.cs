using UnityEngine;

public class ObjectCatchScript : MonoBehaviour
{
    public float sizeIncrease = 0.5f;
    public float massIncrease = 1f;
    private Rigidbody2D rb;
    SFX_Script sfx;

    void Start()
    {
        sfx = FindFirstObjectByType<SFX_Script>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.IsChildOf(transform))
            return;

        if (collision.CompareTag("Good"))
        {

            if (sfx != null) sfx.PlaySFX(4);

            Destroy(collision.gameObject);

            transform.localScale += new Vector3(sizeIncrease, sizeIncrease, 0);
            rb.mass += massIncrease;
        }

        else if (collision.CompareTag("Bad"))
        {
            Debug.Log("Trāpīja sliktajam: " + collision.gameObject.name);

        }
        else
        {
            Debug.Log("Sadursme ar objektu bez taga: " + collision.gameObject.name);
        }
    }
}