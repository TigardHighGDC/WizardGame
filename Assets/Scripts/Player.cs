using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float playerSpeed;
    public static GameObject Instance;

    [Header("Spells")]
    public GameObject lightningBolt;
    public GameObject fireBall;
    public GameObject waterWave;
    public GameObject earthquake;

    [Header("Auras")]
    public GameObject lightningAura;
    public GameObject fireAura;
    public GameObject waterAura;
    public GameObject earthAura;

    private Rigidbody2D rb;
    private Animator anim;

    void Start()
    {
        Instance = gameObject;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        MovementJoystick.Instance.Hide();
    }

    void FixedUpdate()
    {
        rb.velocity = MovementJoystick.Instance.GetVelocity(playerSpeed);
        anim.speed = rb.velocity.magnitude / playerSpeed;
        Vector3 direction = DirectionJoystick.Instance.GetVelocity(1.0f);
        if (direction.x != 0.0f || direction.y != 0.0f)
        {
            PlayerMovementAnimation(direction.x, direction.y);
        }
        else
        {
            PlayerMovementAnimation(rb.velocity.x, rb.velocity.y);
        }
    }

    private void PlayerMovementAnimation(float x, float y)
    {
        // Up, Down
        if (Mathf.Abs(y) > Mathf.Abs(x))
        {
            if (y > 0)
            {
                anim.Play("Forward");
            }
            else
            {
                anim.Play("Backward");
            }
        }
        // Left, Right
        else if (Mathf.Abs(y) < Mathf.Abs(x))
        {
            if (x > 0)
            {
                anim.Play("Right");
            }
            else
            {
                anim.Play("Left");
            }
        }
    }

    public void CastSpell(string spellName)
    {
        Vector3 direction = DirectionJoystick.Instance.GetVelocity(1.0f);

        switch (spellName)
        {
        case "lightning":
            GameObject lightning =
                Instantiate(lightningBolt, transform.position, Quaternion.FromToRotation(transform.up, direction));
            break;
        case "fire":
            GameObject fire =
                Instantiate(fireBall, transform.position, Quaternion.FromToRotation(transform.up, direction));
            break;
        case "water":
            GameObject water =
                Instantiate(waterWave, transform.position, Quaternion.FromToRotation(transform.up, direction));
            break;
        case "earth":
            GameObject earth =
                Instantiate(earthquake, transform.position, Quaternion.FromToRotation(transform.up, direction));
            break;
        case "shield":
            PlayerHealth.Instance.SetInvincability(1.5f);
            break;
        }
        DestroyAura();
    }

    public void CreateAura(string spellName)
    {
        if (spellName == "lightning")
        {
            lightningAura.SetActive(true);
            fireAura.SetActive(false);
            waterAura.SetActive(false);
            earthAura.SetActive(false);
        }
        else if (spellName == "fire")
        {
            lightningAura.SetActive(false);
            fireAura.SetActive(true);
            waterAura.SetActive(false);
            earthAura.SetActive(false);
        }
        else if (spellName == "water")
        {
            lightningAura.SetActive(false);
            fireAura.SetActive(false);
            waterAura.SetActive(true);
            earthAura.SetActive(false);
        }
        else if (spellName == "earth")
        {
            lightningAura.SetActive(false);
            fireAura.SetActive(false);
            waterAura.SetActive(false);
            earthAura.SetActive(true);
        }
    }

    public void DestroyAura()
    {
        lightningAura.SetActive(false);
        fireAura.SetActive(false);
        waterAura.SetActive(false);
        earthAura.SetActive(false);
    }
}