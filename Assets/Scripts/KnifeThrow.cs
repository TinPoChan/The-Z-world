using UnityEngine;

public class KnifeThrow : MonoBehaviour
{
    private CharacterStats characterStats;
    public GameObject knifePrefab; // Assign in the inspector
    public float fixedSpeed = 10f; // Fixed speed of the knife
    
    public int attackDamage = 25; // The damage each knife will deal

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // Make sure your player GameObject has the "Player" tag.
        if (player != null)
        {
            characterStats = player.GetComponent<CharacterStats>();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.timeScale > 0) // Left click
        {
            ThrowKnife();
        }
    }

    void ThrowKnife()
    {
        GameObject knife = Instantiate(knifePrefab, transform.position, Quaternion.identity);
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 throwDirection = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y).normalized;
        knife.GetComponent<Rigidbody2D>().velocity = throwDirection * fixedSpeed;

        // Multiply the base damage by the character's base attack damage multiplier
        int finalDamage = Mathf.RoundToInt(attackDamage * characterStats.baseAttackDamage);
        knife.GetComponent<Knife>().damage = finalDamage;
        //Debug.Log("Knife thrown with damage: " + finalDamage);

        // Correct knife rotation to face the moving direction
        float angle = Mathf.Atan2(throwDirection.y, throwDirection.x) * Mathf.Rad2Deg;
        knife.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

}
