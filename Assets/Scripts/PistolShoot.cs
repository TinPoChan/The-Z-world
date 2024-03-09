using UnityEngine;

public class PistolShoot : MonoBehaviour
{
    private CharacterStats characterStats;
    public GameObject bulletPrefab;
    public float shootingForce = 20f;
    public Transform shootingPoint;

    public int attackDamage = 25;

    public AudioClip pistolSound; // Assign in the Inspector
    private AudioSource audioSource;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // Make sure your player GameObject has the "Player" tag.
        if (player != null)
        {
            characterStats = player.GetComponent<CharacterStats>();
        }
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.timeScale > 0) // Left mouse button
        {
            ShootBullet();
            PlayPistolSound();
        }
    }

    void ShootBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shootingDirection = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y).normalized;
        bullet.GetComponent<Rigidbody2D>().velocity = shootingDirection * shootingForce; // Ensure fixed speed

        // Assign damage based on CharacterStats
        Bullet bulletComponent = bullet.GetComponent<Bullet>(); // Assuming your bullet prefab has a 'Bullet' script to handle damage
        if (characterStats != null && bulletComponent != null)
        {
            bulletComponent.damage = Mathf.RoundToInt(attackDamage * characterStats.baseAttackDamage);
        }
        else
        {
            if (characterStats == null) Debug.LogError("CharacterStats is null.");
            if (bulletComponent == null) Debug.LogError("Bullet component not found on bullet prefab.");
        }

        // Correct bullet rotation to face the moving direction
        float angle = Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void PlayPistolSound()
    {
        if (pistolSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(pistolSound);
        }
        else
        {
            Debug.LogWarning("AudioSource or pistolSound is missing", this);
        }
    }
}
