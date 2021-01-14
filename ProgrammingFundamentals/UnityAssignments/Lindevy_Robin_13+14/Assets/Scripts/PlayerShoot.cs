using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerShoot : MonoBehaviour
{
    public Text bulletText;
    public TextMeshProUGUI bulletTMPText;
    public GameObject bullet;
    public GameObject gun;

    private int numberOfBullets = 0;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject newBullet = Instantiate(bullet, gun.transform.position, gun.transform.rotation);
            numberOfBullets++;
            
            bulletText.text = "Bullets Fired: " + numberOfBullets;
            bulletTMPText.text = "Bullets Fired: " + numberOfBullets;
            
            newBullet.GetComponent<Rigidbody2D>().velocity = gun.transform.right * 10;
        }
    }
}
