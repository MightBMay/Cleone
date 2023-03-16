using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MightBMaybe.Cleone.Clones;
namespace MightBMaybe.Cleone.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public static PlayerMovement pMove;
        SpawnPointManager spawnMan;
        public bool canMove;
        PlayerStats pStats;
        Rigidbody rb;
        float xrot = 0;
        private void Awake()
        {
            pMove = this;
        }
        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            spawnMan = FindObjectOfType<SpawnPointManager>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R)) { RespawnPlayer(); }
        }
        void FixedUpdate()
        {

            MovePlayer(GetMovementInput());
            RotatePlayer();
        }
        private Vector3 GetMovementInput()
        {
            float xInp = Input.GetAxisRaw("Horizontal");
            float yInp = Input.GetAxisRaw("Vertical");
            return new Vector3(xInp, 0, yInp);
        }
        private Vector2 GetMouseInput()
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            return new Vector2(mouseX, mouseY).normalized;
        }
        private void MovePlayer(Vector3 inputDir)
        {
            float moveSpeed = PlayerStats.pStats.MStats.moveSpeed;
            float maxSpeed = PlayerStats.pStats.MStats.maxSpeed;
            float deceleration = PlayerStats.pStats.MStats.deceleration;
            float deadzone = PlayerStats.pStats.IStats.deadZone; 
            if (!canMove) { return; }
            inputDir = transform.TransformDirection(inputDir);

            //Vector3 tempVel = rb.velocity + inputDir * moveSpeed;
            Vector3 currentVel = rb.velocity;
            Vector3 inputVel = inputDir * moveSpeed;

            // calculate new velocity for each axis
            Vector3 tempVel = new Vector3(
                Mathf.Abs(inputDir.x) < deadzone ? Mathf.Lerp(currentVel.x, 0, Time.deltaTime * deceleration) : currentVel.x + inputVel.x,
                currentVel.y,
                Mathf.Abs(inputDir.z) < deadzone ? Mathf.Lerp(currentVel.z, 0, Time.deltaTime * deceleration) : currentVel.z + inputVel.z
            );

            Vector3 clampedVel = new Vector3(
                Mathf.Clamp(tempVel.x, -maxSpeed, maxSpeed),
                tempVel.y,
                Mathf.Clamp(tempVel.z, -maxSpeed, maxSpeed)
            );
            rb.velocity = clampedVel;
        }
        private void RotatePlayer()
        {
            Vector2 mouseInp = GetMouseInput();
            Vector3 playerRotation = transform.localRotation.eulerAngles;
            float sens = PlayerStats.pStats.IStats.lookSensitivity;
           
            playerRotation.y += mouseInp.x * sens * Time.deltaTime;

            
            xrot -= mouseInp.y * sens * Time.deltaTime;
            xrot = Mathf.Clamp(xrot, -90, 90);

            Camera.main.transform.localRotation = Quaternion.Euler(xrot,0,0);

            transform.localRotation = Quaternion.Euler(playerRotation);
            
        }
        public void RespawnPlayer()
        {
            transform.position = spawnMan.GetClosestSpawnPoint().position;
        }


      
    }
}
