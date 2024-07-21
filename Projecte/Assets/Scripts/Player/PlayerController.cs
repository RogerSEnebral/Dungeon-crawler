using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameManager gameManager;
    public const string WEAPON_PATH = "root/pelvis/spine_01/spine_02/spine_03/clavicle_r/upperarm_r/lowerarm_r/hand_r/weapon_r";
    public int linearSpeed = 7;
    public float angularSpeed = 7;
    public bool invencible {
        get;
        set;
    } = false;
    private int _health = 6;
    private PlayerState state;
    private HUDUpdate hUDUpdate;
    private TrailRenderer trail;
    private BoxCollider trailCollider;
    private Transform sword;
    private WeaponProperties weaponProperties;
    public GameObject collidedChest;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;

        PlayerState newState = new PlayerIdleState(this);
        ChangeState(newState);
        
        GameObject hUD = transform.Find("HUD").gameObject;
        hUDUpdate = hUD.GetComponent<HUDUpdate>();

        sword = transform.Find($"{WEAPON_PATH}/OHS03");

        GameObject trailGameObject = transform.Find($"{WEAPON_PATH}/Trail").gameObject;
        trail = trailGameObject.GetComponent<TrailRenderer>();
        trailCollider = trailGameObject.GetComponent<BoxCollider>();
        weaponProperties = trailGameObject.GetComponent<WeaponProperties>();
    }

    // Update is called once per frame
    void Update()
    {
        state.Update();
    }

    void FixedUpdate()
    {
        state.FixedUpdate(linearSpeed, angularSpeed);
    }

    void OnCollisionEnter(Collision collision)
    {
        state.OnCollisionEnter(collision);
    }

    void OnCollisionExit(Collision collision)
    {
        state.OnCollisionExit(collision);
    }

    public void ChangeState(PlayerState newState)
    {
        state = newState;
        state.Start();
    }

    public void TakeDamage(int damage)
    {
        state.TakeDamage(damage);
    }

    public void Attack(int inProgress)
    {
        state.Attack(inProgress);
    }

    public void Push()
    {
        state.Push();
    }

    public void Exit()
    {
        state.Exit();
    }

    public int health {
        get => _health;
        set {
            _health = value;

            hUDUpdate.UpdateHearts(health);

            float scale = ((Mathf.Ceil(health / 2f) - 1)/2f) + 2f;
            sword.localScale = Vector3.one * scale;
            trail.widthMultiplier = scale;

            float yPos = (2f - 0.75f) * (scale - 1) / 3f + 0.75f;
            Transform trailTransform = trail.gameObject.transform;
            trailTransform.localPosition = new Vector3(trailTransform.localPosition.x, yPos, trailTransform.localPosition.z);
            
            float ySize = (3.2f - 1.1f) * (scale - 1) / 3f + 1.1f;
            trailCollider.size = new Vector3(trailCollider.size.x, ySize, trailCollider.size.z);

            weaponProperties.damage = scale;
        }
    }

    public void SwitchLight(bool darkRoom)
    {
        transform.Find("Body05").GetComponent<Light>().enabled = darkRoom;
    }
}
