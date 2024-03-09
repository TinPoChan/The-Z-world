using UnityEngine;

public class ShotgunShoot : MonoBehaviour
{
    private CharacterStats characterStats;
    public GameObject bulletPrefab;
    public float shootingForce = 20f;
    public Transform shootingPoint;
    public int bulletsPerShot = 3;
    public float spreadAngle = 5f; // Degrees
    public int attackDamage = 15;

    public AudioClip shotGunSound; // Assign in the Inspector
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
            ShootBullets();
            PlayShotgunSound();
        }
    }

    void ShootBullets()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        for (int i = 0; i < bulletsPerShot; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
            Vector2 shootingDirection = (new Vector2(mousePosition.x, mousePosition.y) - new Vector2(transform.position.x, transform.position.y)).normalized;

            // Calculate spread
            float angleOffset = spreadAngle * (i - (bulletsPerShot - 1) / 2.0f);
            Vector2 spreadDirection = Quaternion.Euler(0, 0, angleOffset) * shootingDirection;

            bullet.GetComponent<Rigidbody2D>().velocity = spreadDirection * shootingForce;

            // Adjust bullet rotation
            float angle = Mathf.Atan2(spreadDirection.y, spreadDirection.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(0, 0, angle);

            // Incorporate base attack damage into bullet damage
            Bullet bulletComponent = bullet.GetComponent<Bullet>(); // Assuming your bullet prefab has a 'Bullet' script
            if (bulletComponent != null)
            {
                bulletComponent.damage = Mathf.RoundToInt(attackDamage * characterStats.baseAttackDamage); // Or any calculation based on CharacterStats
            }
        }
    }

    void PlayShotgunSound()
    {
        if (shotGunSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shotGunSound);
        }
        else
        {
            Debug.LogWarning("AudioSource or shotGunSound is missing", this);
        }
    }

}
