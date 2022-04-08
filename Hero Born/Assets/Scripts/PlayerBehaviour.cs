using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float MovSpeed = 10f;
    public float RotationSpeed = 75f;
    public float JumpVelocity = 5f;
    public float DistanceToGround = 0.1f;
    public LayerMask GroundLayer;
    public GameObject Bullet;
    public float BulletSpeed = 100f;

    private GameBehaviour _gameManager;
    private float _vinput;
    private float _hinput;
    private bool _isJumping;
    private bool _isShooting;
    private CapsuleCollider _col;
    private Rigidbody _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();

        _col = GetComponent<CapsuleCollider>();

        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        _vinput = Input.GetAxis("Vertical") * MovSpeed;
        _hinput = Input.GetAxis("Horizontal") * RotationSpeed;

        _isJumping |= Input.GetKeyDown(KeyCode.Space);
        /* this.transform.Translate(Vector3.forward * _vinput *Time.deltaTime);
         this.transform.Rotate(Vector3.up * _hinput * Time.deltaTime); */

        _isShooting |= Input.GetMouseButtonDown(0);
        
    }

    private void FixedUpdate()
    {
        Vector3 rotation = Vector3.up * _hinput;

        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);

        _rb.MovePosition(this.transform.position + this.transform.forward * _vinput * Time.fixedDeltaTime);
        _rb.MoveRotation(_rb.rotation * angleRot);

        /*if (_isJumping)
        {
            _rb.AddForce(Vector3.up * JumpVelocity, ForceMode.Impulse);
        }
        _isJumping = false*/;

        if (IsGrounded() && _isJumping)
        {
            _rb.AddForce(Vector3.up * JumpVelocity , ForceMode.Impulse);
        }
        _isJumping = false;

        if(_isShooting)
        {
            GameObject newBullet = Instantiate(Bullet, this.transform.position + new Vector3(0, 0, 1), this.transform.rotation);

            Rigidbody BulletRB = newBullet.GetComponent<Rigidbody>();

            BulletRB.velocity = this.transform.forward * BulletSpeed;
        }
        _isShooting = false;
    }

    private bool IsGrounded()
    {
        Vector3 capsuleBottom = new Vector3(_col.bounds.center.x, _col.bounds.min.y, _col.bounds.center.z);

        bool grounded = Physics.CheckCapsule(_col.bounds.center, capsuleBottom, DistanceToGround, GroundLayer, QueryTriggerInteraction.Ignore);
        return grounded;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Enemy")
        {
            _gameManager.Health -= 1;
        }
    }
}
