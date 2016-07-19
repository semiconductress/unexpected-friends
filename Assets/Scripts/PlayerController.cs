using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float jumpspeed;
	public Sprite sprforw;
	public Sprite sprback;
	public BoxCollider2D bodycollider;
	public PlayerboxesController colliderboxes;
	private BoxCollider2D topcollider;
	private BoxCollider2D bottomcollider;
	private BoxCollider2D leftcollider;
	private BoxCollider2D rightcollider;

	public bool displayactionbutton;
	public bool canspeak;

	public GameObject zbutton;
	private GameObject actionbutton;
	private GameObject actiontarget;
	public Text dialoguebox;
	private int dialogueindex;
	private int textdisplayindex;
	public Rigidbody2D body;
	public SpriteRenderer spriter;
	public ArrayList globalflags;
	private TextHandler texth;

	private bool activatezx;
	private bool activateas;
	private bool canjump;

	void Start () {
		Physics.IgnoreLayerCollision (0, 8, true);
		body = GetComponent<Rigidbody2D> ();
		spriter = GetComponent<SpriteRenderer> ();
		globalflags = new ArrayList ();
		texth = new TextHandler (globalflags, dialoguebox);
		colliderboxes = GetComponentInChildren<PlayerboxesController>();
		dialogueindex = 0;
		textdisplayindex = 0;
		activatezx = true;
		activateas = true;
		canjump = false;

		bottomcollider = colliderboxes.bottomcollider;
		topcollider = colliderboxes.topcollider;
		leftcollider = colliderboxes.leftcollider;
		rightcollider = colliderboxes.rightcollider;
	}

	void FixedUpdate () {
		float movex = Input.GetAxis ("Horizontal");
		float movey = Input.GetAxis ("Vertical");
		Vector2 newvelocity = new Vector2 (speed*movex, body.velocity.y);
		if (movex > 0) { spriter.sprite = sprforw;}
		if (movex < 0) { spriter.sprite = sprback;}

		if (canjump && Input.GetAxis ("Jump") > 0) {
			newvelocity.y = jumpspeed;
			canjump = false;
		}

		body.velocity = new Vector2 (newvelocity.x, newvelocity.y);
		if (displayactionbutton = true && actionbutton != null) {
			actionbutton.transform.position = new Vector3(actiontarget.GetComponent<SpriteRenderer>().bounds.center.x, 
				actiontarget.GetComponent<SpriteRenderer>().bounds.max.y + zbutton.GetComponent<SpriteRenderer>().bounds.size.y, 0);
		}

		if (canspeak && (Input.GetAxis ("Action1") > 0) && activatezx && actiontarget != null && texth.currentseg == null) {
			texth.StartConvo (actiontarget.name);
		} else if (canspeak && ((Input.GetAxis ("Action1") > 0 && activatezx) 
			|| (Input.GetAxis ("Action2") != 0) && activateas) && actiontarget != null) {
			texth.AdvanceConvo (Input.GetAxis ("Action2"));
		} else if ((actiontarget == null) && dialoguebox.enabled) {
			texth.ResetConvo();
		}

		if (activatezx && (Input.GetAxis ("Action1") != 0)) {
			activatezx = false;
		} else if (Input.GetAxis ("Action1") == 0) {
			activatezx = true;
		}

		if (activateas && (Input.GetAxis ("Action2") != 0)) {
			activateas = false;
		} else if (Input.GetAxis ("Action2") == 0) {
			activateas = true;
		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		if ((OnTopOf (other.gameObject.GetComponent<Collider2D>()) 
			&& !OnLeftOf (other.gameObject.GetComponent<Collider2D>()) 
			&& !OnRightOf (other.gameObject.GetComponent<Collider2D>()))
			&& !OnBottomOf (other.gameObject.GetComponent<Collider2D>())) {
			canjump = true;
		}
	}

	void OnCollisionStay2D(Collision2D other) {
		if (!OnBottomOf (other.gameObject.GetComponent<Collider2D>())) {
			canjump = true;
		}
	}

	void OnCollisionExit2D(Collision2D other) {
		canjump = false;
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.tag.Contains ("Speak")) {
			actiontarget = other.gameObject;
			if (OnTopOf (other.gameObject.GetComponent<Collider2D> ())) {
				if (displayactionbutton) {
				displayactionbutton = false;
				Destroy (actionbutton);
				}
				if (texth.currentseg == null) {
					texth.StartConvo (actiontarget.name, new Flag ("StandingOn", true));
					canspeak = false;
				}
			} else {
				if (!displayactionbutton) {
					texth.ResetConvo ();
					displayactionbutton = true;
					actionbutton = (GameObject)Instantiate 
						(zbutton, new Vector3 (actiontarget.GetComponent<SpriteRenderer> ().bounds.center.x, 
						actiontarget.GetComponent<SpriteRenderer> ().bounds.max.y + zbutton.GetComponent<SpriteRenderer> ().bounds.size.y, 0), 
						Quaternion.identity);
					canspeak = true;
				}
			}
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag.Contains ("Speak")) {
			if (dialoguebox.isActiveAndEnabled) {
				texth.ResetConvo();
				if (displayactionbutton) {
					canspeak = true;
				} else {
					canspeak = false;
				}
			}
			if (displayactionbutton) {
				displayactionbutton = false;
				Destroy (actionbutton);
				canspeak = false;
				actiontarget = null;
			}
		}
	}

	public bool OnTopOf(Collider2D other) {
		return (bottomcollider.bounds.Intersects(other.bounds) && !topcollider.bounds.Intersects(other.bounds));
	}

	public bool OnBottomOf(Collider2D other) {
		return (topcollider.bounds.Intersects(other.bounds) && !bottomcollider.bounds.Intersects(other.bounds));
	}

	public bool OnLeftOf(Collider2D other) {
		return (rightcollider.bounds.Intersects(other.bounds) && !leftcollider.bounds.Intersects(other.bounds));
	}

	public bool OnRightOf(Collider2D other) {
		return (leftcollider.bounds.Intersects(other.bounds) && !rightcollider.bounds.Intersects(other.bounds));
	}
}
