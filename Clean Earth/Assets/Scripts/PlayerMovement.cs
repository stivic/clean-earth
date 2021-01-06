using System;
using System.Collections;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public enum PlayerState
{
	walk, 
	teleport
}
public class PlayerMovement : MonoBehaviourPunCallbacks, IPunObservable
{
	public static GameObject LocalPlayerInstance;
	public PlayerState currentState;
	public float speed;
	private Rigidbody2D myRigidbody;
	private Vector3 change;
	private Animator animator;
	public GameObject teleportationOrb;
	private TeleportationOrb projectile;

	// Cached property indexes
	private static readonly int MoveX = Animator.StringToHash("moveX");
	private static readonly int MoveY = Animator.StringToHash("moveY");
	private static readonly int Moving = Animator.StringToHash("moving");

	// Start is called before the first frame update
	private void Awake()
	{
		if (photonView.IsMine)
		{
			PlayerMovement.LocalPlayerInstance = this.gameObject;
		}
		// #Critical
		// we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
		DontDestroyOnLoad(this.gameObject);
	}

	private void Start()
    {
	    animator = GetComponent<Animator>();
	    myRigidbody = GetComponent<Rigidbody2D>();
	    currentState = PlayerState.walk;
	    animator.SetFloat(MoveX, 0);
	    animator.SetFloat(MoveY, -1);
	    CameraWork _cameraWork = this.gameObject.GetComponent<CameraWork>();
	    if (_cameraWork != null)
	    {
		    if (photonView.IsMine)
		    {
			    _cameraWork.OnStartFollowing();
		    }
	    }
	    else
	    {
		    Debug.LogError("<Color=Red><a>Missing</a></Color> CameraWork Component on playerPrefab.", this);
	    }
    }

    // Update is called once per frame

    private void Update()
    {
	    if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
	    {
		    return;
	    }
	    change = Vector3.zero;
	    change.x = Input.GetAxisRaw("Horizontal");
	    change.y = Input.GetAxisRaw("Vertical");
	    if (Input.GetButtonDown("Teleport") && currentState != PlayerState.teleport)
	    {
		    StartCoroutine(TeleportCo());
	    }
	    if (Input.GetButtonDown("Report"))
	    {
		    WorldInit.Instance.currentReportCount--;
		    if (this.GetComponent<PlayerInfo>().wasBadGuy)
		    {
			    this.GetComponent<PlayerInfo>().wasBadGuy = false;
			    GameObject badAI = WorldInit.Instance.badAIPlayers[WorldInit.Instance.badAIPlayers.Count - 1];
			    PhotonNetwork.Destroy(badAI);
			    WorldInit.Instance.badAIPlayers.Remove(badAI);
			    if (WorldInit.Instance.badAIPlayers.Count <= 0)
			    {
				    // new wave
				    WorldInit.Instance.WaveSetup();
			    }
		    }
		    else if (WorldInit.Instance.currentReportCount < WorldInit.Instance.badAIPlayers.Count)
		    {
			    WorldInit.Instance.gameOver = true;
			    WorldInit.Instance.IncreaseScore();
		    }
				
	    }

    }
    private void FixedUpdate()
    {
	    if (photonView.IsMine)
	    {
		    UpdateAnimationAndMove();
	    }
	    
    }

    private void UpdateAnimationAndMove()
    {
	    if (change != Vector3.zero)
	    {
		    MoveCharacter();
		    animator.SetFloat(MoveX, change.x);
		    animator.SetFloat(MoveY, change.y);
		    animator.SetBool(Moving, true);
	    }
	    else
	    {
		    animator.SetBool(Moving, false);
	    }
    }

    private void MoveCharacter()
    {
	    myRigidbody.MovePosition(
		    transform.position + change.normalized * (Time.deltaTime * speed)
		    );
    }

    private IEnumerator TeleportCo()
    {
	    currentState = PlayerState.teleport; 
	    yield return null;
	    MakeTeleportationOrb();
	    yield return new WaitForSeconds(projectile.duration);
	    currentState = PlayerState.walk;
	    
    }

    private void MakeTeleportationOrb()
    {
	    Vector2 temp = new Vector2(animator.GetFloat(MoveX), animator.GetFloat(MoveY)).normalized;
	    projectile = 
		    Instantiate(teleportationOrb, transform.position, Quaternion.identity).GetComponent<TeleportationOrb>();
	    projectile.Setup(temp);
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
	    if (stream.IsWriting)
	    {
		    // We own this player: send the others our data
		    stream.SendNext(currentState==PlayerState.teleport);
	    }
	    else
	    {
		    // Network player, receive data
		    if ((bool) stream.ReceiveNext())
		    {
			    this.currentState = PlayerState.teleport;
		    }
		    
	    }
    }
}

	
