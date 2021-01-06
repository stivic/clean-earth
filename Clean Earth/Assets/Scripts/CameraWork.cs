// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CameraWork.cs" company="Exit Games GmbH">
//   Part of: Photon Unity Networking Demos
// </copyright>
// <summary>
//  Used in PUN Basics Tutorial to deal with the Camera work to follow the player
// </summary>
// <author>developer@exitgames.com</author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;


public class CameraWork : MonoBehaviour 
{
	#region Private Fields

	    [Tooltip("The distance in the local x-z plane to the target")]
	    [SerializeField]
	    private float distance = 0.0f;
	    
	    [Tooltip("The height we want the camera to be above the target")]
	    [SerializeField]
	    private float height = 5.0f;
	    
	    [Tooltip("Allow the camera to be offseted vertically from the target, for example giving more view of the sceneray and less ground.")]
	    [SerializeField]
	    private Vector3 centerOffset = Vector3.zero;

	    [Tooltip("Set this as false if a component of a prefab being instanciated by Photon Network, and manually call OnStartFollowing() when and if needed.")]
	    //[SerializeField]
	    private bool followOnStart = false;

	    [Tooltip("The Smoothing for the camera to follow the target")]
	    [SerializeField]
	    private float smoothSpeed = 10f;

        // cached transform of the target
        Transform cameraTransform;

		 public Camera camera;

		// maintain a flag internally to reconnect if target is lost or camera is switched
		bool isFollowing;
		
		// Cache for camera offset
		Vector3 cameraOffset = Vector3.zero;
		
		private const float minY = -101f;
		private const float minX = -104f; //manji broj
		private const float maxY = 99f; 
		private const float maxX = 90f;
		private const int cameraSize = 5;
		
		
        #endregion
	#region MonoBehaviour Callbacks

        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during initialization phase
        /// </summary>
        void Start()
		{
			// Start following the target if wanted.
			if (followOnStart)
			{
				OnStartFollowing();
			}
		}


		void LateUpdate()
		{
			// The transform target may not destroy on level load, 
			// so we need to cover corner cases where the Main Camera is different everytime we load a new scene, and reconnect when that happens
			if (cameraTransform == null && isFollowing)
			{
				OnStartFollowing();
			}

			// only follow is explicitly declared
			if (isFollowing) {
				Follow ();
			}
		}

		#endregion
	#region Public Methods

		/// <summary>
		/// Raises the start following event. 
		/// Use this when you don't know at the time of editing what to follow, typically instances managed by the photon network.
		/// </summary>
		public void OnStartFollowing()
		{	      
			cameraTransform = Camera.main.transform;
			isFollowing = true;
			
			Follow();
		}
		
		#endregion
		#region Private Methods

		/// <summary>
		/// Follow the target smoothly
		/// </summary>
		void Follow()
		{
			cameraOffset.z = -distance;
			cameraOffset.y = 0f;

			Vector3 targetPosition = new Vector3(this.transform.position.x, this.transform.position.y, cameraTransform.position.z);
			Debug.Log("Koord " + targetPosition);
			if ((targetPosition.x + cameraSize) > maxX | (targetPosition.x - cameraSize) < minX)
			{
				targetPosition.x = cameraTransform.position.x;
			}
			if ((targetPosition.y + cameraSize) > maxY | (targetPosition.y - cameraSize) < minY)
			{
				targetPosition.y = cameraTransform.position.y;
			}

			cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetPosition, 10f);

			
			//cameraTransform.LookAt(this.transform.position + centerOffset);
		    
		}

	   
		void Cut()
		{
			cameraOffset.z = -distance;
			cameraOffset.y = height;

			cameraTransform.position = this.transform.position + this.transform.TransformVector(cameraOffset);

			//cameraTransform.LookAt(this.transform.position + centerOffset);
		}
		#endregion
}
