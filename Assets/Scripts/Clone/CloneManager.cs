using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using MightBMaybe.Cleone.Player;
using MightBMaybe.Cleone.Interactables;
using MightBMaybe.Cleone.UI;
namespace MightBMaybe.Cleone.Clones
{
    public class CloneManager : MonoBehaviour
    {   [HideInInspector]
        public Dictionary<string, CloneTypes> cloneTypes= new Dictionary<string, CloneTypes>();
        public static CloneManager cloneManager;
        
        [Header("Clone Variables")]
        [Tooltip("The distance a clone will spawn in front of the player.")]
        public float spawnOffset;
        [Tooltip("The distance a clone will be in front of the player when grabbed.")]
        public float grabOffset;
        [Tooltip("how quickly a clone follows the players movements when grabbed.")]
        public float grabSpeed;
        [Tooltip("self explanitory.")]
        public float maxGrabRange;
        private KeyCode[] cloneKeys = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3 };
        private GameObject grabbedObject;
        Transform player;
        Material cloneTextMaterial;
        private void Awake()
        {
            cloneManager = this;
            InstantiateCloneTypeList();
            player = GameObject.Find("PlayerBody").transform;

        }
        private void Start()
        {
             cloneTextMaterial = new Material(Shader.Find("TextMeshPro/DistanceFieldOverlay"));
        }

        public List<GameObject> clones = new List<GameObject>();

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                CreateClone();
                
            }
            if (Input.GetKeyDown(KeyCode.G)) {
                grabbedObject = TryGrabClone();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                ResetClonesAndInteractables(); 
            }

            TeleportInput();

            UpdateGrabOffset();
           
            
            
        }
        private void FixedUpdate()
        {
            Grab();
        }
        private void InstantiateCloneTypeList()
        {
            cloneTypes.Add("NonRigid", new NonRigidClone());
            cloneTypes.Add("Rigid", new RigidClone());
        }

        public void CreateClone()
        {
            
            if(PlayerStats.pStats.CStats.CurrentCloneType == null) { return; }
            while (clones.Count >= PlayerStats.pStats.CStats.maxCloneCount)
            {
                Destroy(clones[0]);
                clones.RemoveAt(0); 
            }
            
                GameObject clone = GameObject.CreatePrimitive(PrimitiveType.Cube);
           
                Vector3 spawnPosition = player.position + player.forward * spawnOffset; // Calculate the spawn position using the player position and forward direction
                clone.transform.position = spawnPosition;
                clone.transform.SetParent(transform);// set parent to clone holder
                clone.transform.position = spawnPosition; // Set the position of the clone directly to the calculated spawn position
                clone.transform.rotation = player.rotation;

                AssignCloneData(clone, PlayerStats.pStats.CStats.CurrentCloneType);
                clones.Add(clone);
                AddCanvasToWorldSpace(clone);

 
        }
        
        private void AssignCloneData(GameObject cloneObj,CloneTypes type)
        {
            // Instantiate a cube primitive
            // Add a CloneBehaviour script to the cube
            CloneBehaviour cloneBehaviour = cloneObj.AddComponent<CloneBehaviour>();
            Rigidbody cloneRB = cloneObj.AddComponent<Rigidbody>();
            cloneRB.constraints = RigidbodyConstraints.FreezeRotation;

            // Create a new instance of the clonetypes derived class and assign it to the clonetype variable
            CloneTypes cloneTypeInstance = (CloneTypes)Activator.CreateInstance(type.GetType());
            cloneBehaviour.CloneType = cloneTypeInstance;
            cloneObj.tag = cloneBehaviour.CloneType.GetTagName();
            //cloneObj.transform.position = PlayerMovement.pMove.transform.position + cloneBehaviour.CloneType.PositionOffset;
            cloneObj.transform.localScale = cloneBehaviour.CloneType.Scale;
            cloneObj.layer = cloneBehaviour.CloneType.GetLayer();
            cloneObj.GetComponent<MeshRenderer>().material.color = type.cloneColour;

        }
        private void Grab()
        {
            
            


            if (grabbedObject == null) return;
            if (Vector3.Distance(grabbedObject.transform.position, player.position)>= maxGrabRange+0.2f) { grabbedObject = null; return; }
            Collider collider = grabbedObject.GetComponent<Collider>();
            Rigidbody rigidBody = grabbedObject.GetComponent<Rigidbody>();
            if (rigidBody.useGravity) rigidBody.useGravity = false;
            //if(collider.enabled) collider.enabled = false;
            grabbedObject.layer = 8;
            grabbedObject.transform.position = Vector3.Lerp(grabbedObject.transform.position, PlayerMovement.pMove.transform.position + Camera.main.transform.forward*grabOffset, grabSpeed);


        }

        private void UpdateGrabOffset()
        {
            if (grabbedObject != null)
            {
                if (Input.mouseScrollDelta.magnitude >= 0)
                {

                    grabOffset = Mathf.Clamp(grabOffset + Input.mouseScrollDelta.y * 0.1f, 2, 4.8f);



                }
            }
        }

        private GameObject TryGrabClone()
        {

            if (grabbedObject != null) {
                Collider col = grabbedObject.GetComponent<Collider>();
                Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();               
                if (col != null) col.enabled = true;
                if (col != null) rb.useGravity = true;
                grabbedObject.layer = grabbedObject.GetComponent<CloneBehaviour>().CloneType.GetLayer();
                return null;
            }

            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward); // create a ray from camera center to mouse position
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, maxGrabRange)) // shoot the ray and check if it hits anything
            {
                CloneBehaviour clone = hit.collider.GetComponent<CloneBehaviour>(); // get the game object that the ray hits
                if (clone != null)
                {
                    if (clone.CloneType.TryGrab())
                    {
                        grabOffset = 2;
                        return clone.gameObject;
                    }
                }
                
            }
            return null;
        }

        public void AddCanvasToWorldSpace(GameObject targetObject)
        {
            // Create a new Canvas object and set its properties
            Canvas canvas = new GameObject("Canvas", typeof(Canvas), typeof(UnityEngine.UI.CanvasScaler), typeof(UnityEngine.UI.GraphicRaycaster)).GetComponent<Canvas>();
            canvas.renderMode = RenderMode.WorldSpace;
            canvas.transform.SetParent(targetObject.transform);
            canvas.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, 1, 0);
            canvas.transform.localScale = new Vector3(.01f, .005f, .01f);

            // Create a new TextMeshProUGUI object and set its properties
            TextMeshProUGUI text = new GameObject("Text", typeof(TextMeshProUGUI)).GetComponent<TextMeshProUGUI>();
            text.rectTransform.SetParent(canvas.transform, false);
            text.alignment = TextAlignmentOptions.Center;
            text.verticalAlignment = VerticalAlignmentOptions.Middle;
            //makes text rotate to player
            text.gameObject.AddComponent<RotateTowardPlayer>();
            
            Material existingMaterial = text.material;

            // Create a new material based on the existing material
            Material newMaterial = new Material(existingMaterial);

            // Set the shader on the new material to TextMeshPro/DistanceFieldOverlay
            newMaterial.shader = Shader.Find("TextMeshPro/DistanceFieldOverlay");

            // Set the new material on the TextMeshPro object
            text.material = newMaterial;
            UpdateCloneText();
            
        }

        public void UpdateCloneText()
        {
            foreach(GameObject g in clones)
            {
                g.GetComponentInChildren<TextMeshProUGUI>().text = clones.IndexOf(g) + 1 + "\n▼";
            }
        }


        private void TeleportInput()
        {
            // Iterate over the cloneKeys array
            for (int i = 0; i < cloneKeys.Length; i++)
            {
                // Check if the current key is pressed
                if (Input.GetKeyDown(cloneKeys[i]))
                {
                    // Check if the clones list contains a clone at the current index
                    if (i < clones.Count)
                    {
                        // Get the clone at the current index and do something with it
                        Transform clone = clones[i].transform;
                        Vector3 cameraPosition = Camera.main.transform.position;
                        Vector3 clonePosition = clone.position;
                        Vector3 direction = clonePosition - cameraPosition;
                        RaycastHit hit;
                        Physics.Raycast(cameraPosition, direction, out hit);
                        if (hit.collider.gameObject == clones[i])
                        {
                            // No obstacles found, set the player's position and rotation
                            Vector3 storedPos = player.transform.position;
                            Quaternion storedRot = player.transform.rotation;
                            player.transform.position = clonePosition;
                            player.transform.rotation = clone.rotation;
                            clone.position = storedPos;
                            clone.rotation = storedRot;
                        }
                        else
                        {
                            Debug.LogWarning("Obstacle detected between camera and clone: " + hit.collider.gameObject.name);
                        }


                    }
                    else
                    {
                        Debug.LogWarning("Clones list does not contain a clone at index " + i);
                    }
                }
            }
        }

        
        public void ClearClones()
        {
            while (clones.Count> 0){
            
                Destroy(clones[0]);
                clones.Remove(clones[0]);
            }
        }

        public void ResetPowerUps()
        {
            var powerCollecters = FindObjectsOfType<PowerUpCollecter>();
            foreach (PowerUpCollecter p in powerCollecters)
            {
                p.ResetCollect();
            }
        }

        public void ResetButtons()
        {
            var buttons = FindObjectsOfType<Button>();
            foreach (Button b in buttons)
            {
                b.et.trigger.isTriggered = false;
            }
        }
        
        public void ResetClonesAndInteractables()
        {
            ClearClones();
            ResetPowerUps();
            ResetButtons();
            PlayerStats.pStats.CStats.CurrentCloneType = null;
        }

    }
    public class CloneTypes
    {
        public string layerName;
        public string tagName;
        public Color32   cloneColour;
        public Vector3 positionOffset;
        public Vector3 Scale;

        public virtual bool TryGrab() { return false; }
        public virtual int GetLayer()
        {
            int layer = LayerMask.NameToLayer(layerName);
            if (layer != -1)
            {
                return layer;
            }
            else
            {
                Debug.LogError("Layer not found: " + layerName);
                return -1;
            }
        }
        public virtual string GetTagName()
        {
            return tagName;
        }
    }
    public class NonRigidClone : CloneTypes
    {
        public NonRigidClone()
        {
            layerName = "NonRigidClone";
            tagName = "Clone";
            cloneColour = new Color32(141, 201, 188,255);
            positionOffset = new Vector3(0, 0, 0);
            Scale = new Vector3(1, 2, 1);
        }
        public override bool TryGrab() { return true; }


    }
    public class RigidClone : CloneTypes
    {
        public RigidClone()
        {
            layerName = "RigidClone";
            tagName = "Clone";
            cloneColour = new Color32(74, 150, 158,255);
            positionOffset = new Vector3(0, 0, 0);
            Scale = new Vector3(1, 2, 1);
        }
        public override bool TryGrab() { return true; }


    }

}

