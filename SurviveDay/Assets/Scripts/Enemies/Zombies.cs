using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Zombies : MonoBehaviour {

	private bool detectPlayer;
	private float nextMovement;
	private int maxTime=5;
	private int minTime=2;
	private float m_speed=1.0f;

	private float chaseRate = 1.0f;
	private float nextChase;

	private float detect_dst = 2.5f;
	private float eye = 3.0f;
	private float cur_dst;

	private Transform _transform;
	private BoxCollider2D _box_collider;

	public GameObject player;

	private int ObsLayerMask;
	private int NPCLayerMask;

	// Use this for initialization
	void Start () {
		ObsLayerMask = 1 << 15;
		NPCLayerMask = 1 << 10;

		detectPlayer=false;
		nextMovement=0.0f;
		nextChase=0.0f;
		_transform = GetComponent<Transform>() as Transform;
		_box_collider = GetComponent<BoxCollider2D>() as BoxCollider2D;
	}
	
	// Update is called once per frame
	void Update () {
		cur_dst = Vector3.Distance(player.transform.position, _transform.position);
		if (cur_dst<eye){
			if (!detectPlayer){
				Debug.Log("IT DETECTS PLAYER");
				detectPlayer=true;
			}
		}else{
			if (detectPlayer){
				Debug.Log("IT NOT LONGER DETECTS PLAYER");
				detectPlayer=false;
			}
		}

		if (detectPlayer) {
			if (nextChase<Time.time){
				chasePlayer();
				nextChase=Time.time+chaseRate;
			}	
			// chasePlayer();	
		}else{
			idle();
		}
	}

	void idle() {
		if (nextMovement<Time.time){
			randomMove();
			Random rnd = new Random();
			nextMovement=Time.time+rnd.Next(minTime,maxTime);
		}
	}

	void randomMove(){
		Debug.Log("RANDOM MOVE");
		Vector3 current_pos = _transform.position;
		Vector2 current_pos_2d = new Vector2(current_pos.x,current_pos.y);
		
		Vector2 collider_shift = _box_collider.offset;
		current_pos_2d = current_pos_2d+collider_shift;

		Random rnd = new Random();
		int choice = rnd.Next(1,5);

		switch (choice){
			case 1:
				if(Physics2D.Raycast(current_pos_2d,Vector2.up, detect_dst,ObsLayerMask)){
					// Debug.Log("Cannot Go Up");
				}else if (Physics2D.Raycast(current_pos_2d,Vector2.up, detect_dst,NPCLayerMask)){
					// Debug.Log("NPC Detected");
				}else{	
					current_pos.y = current_pos.y + m_speed;
				}
				break;
			case 2:
				if(Physics2D.Raycast(current_pos_2d, -Vector2.right, detect_dst,ObsLayerMask)){
					// Debug.Log("Cannot Go Left");
				}else if (Physics2D.Raycast(current_pos_2d,-Vector2.right, detect_dst,NPCLayerMask)){
					// Debug.Log("NPC Detected");
				}else{
					current_pos.x = current_pos.x - m_speed;
				}
				break;
			case 3:
				if(Physics2D.Raycast(current_pos_2d, -Vector2.up, detect_dst,ObsLayerMask)){
					// Debug.Log("Cannot Go Down");	
				}else if (Physics2D.Raycast(current_pos_2d,-Vector2.up, detect_dst,NPCLayerMask)){
					// Debug.Log("NPC Detected");
				}else{
					current_pos.y = current_pos.y - m_speed;
				}
				break;
			default:
				if(Physics2D.Raycast(current_pos_2d, Vector2.right, detect_dst,ObsLayerMask)){
					// Debug.Log("Cannot Go Right");
				}else if (Physics2D.Raycast(current_pos_2d,Vector2.right, detect_dst,NPCLayerMask)){
					// Debug.Log("NPC Detected");
				}else{
					current_pos.x = current_pos.x + m_speed;
				}
				break;
			}
		// while (true) {
		// 	if (choice==1){
		// 		if(Physics2D.Raycast(current_pos_2d,Vector2.up, detect_dst,ObsLayerMask)){
		// 			// Debug.Log("Cannot Go Up");
		// 		}else if (Physics2D.Raycast(current_pos_2d,Vector2.up, detect_dst,NPCLayerMask)){
		// 			// Debug.Log("NPC Detected");
		// 		}else{	
		// 			current_pos.y = current_pos.y + m_speed;
		// 			break;
		// 		}
		// 	}else if (choice ==2){
		// 		if(Physics2D.Raycast(current_pos_2d, -Vector2.right, detect_dst,ObsLayerMask)){
		// 			// Debug.Log("Cannot Go Left");
		// 		}else if (Physics2D.Raycast(current_pos_2d,-Vector2.right, detect_dst,NPCLayerMask)){
		// 			// Debug.Log("NPC Detected");
		// 		}else{
		// 			current_pos.x = current_pos.x - m_speed;
		// 			break;
		// 		}
		// 	}else if (choice ==3){
		// 		if(Physics2D.Raycast(current_pos_2d, -Vector2.up, detect_dst,ObsLayerMask)){
		// 			// Debug.Log("Cannot Go Down");	
		// 		}else if (Physics2D.Raycast(current_pos_2d,-Vector2.up, detect_dst,NPCLayerMask)){
		// 			// Debug.Log("NPC Detected");
		// 		}else{
		// 			current_pos.y = current_pos.y - m_speed;
		// 			break;
		// 		}
		// 	}else{
		// 		if(Physics2D.Raycast(current_pos_2d, Vector2.right, detect_dst,ObsLayerMask)){
		// 			// Debug.Log("Cannot Go Right");
		// 		}else if (Physics2D.Raycast(current_pos_2d,Vector2.right, detect_dst,NPCLayerMask)){
		// 			// Debug.Log("NPC Detected");
		// 		}else{
		// 			current_pos.x = current_pos.x + m_speed;
		// 			break;
		// 		}
		// 	}
		// }
		_transform.position = current_pos;
	}

	void chasePlayer(){
		int x=1;
		Vector3 dif=player.transform.position-_transform.position;
		Vector3 current_pos = _transform.position;
		Vector2 current_pos_2d = new Vector2(current_pos.x,current_pos.y);
		
		Vector2 collider_shift = _box_collider.offset;
		current_pos_2d = current_pos_2d+collider_shift;

		if(dif.y>0){
			if(Physics2D.Raycast(current_pos_2d,Vector2.up, detect_dst,ObsLayerMask)){
				Debug.Log("Cannot Go Up");
			}else if (Physics2D.Raycast(current_pos_2d,Vector2.up, detect_dst,NPCLayerMask)){
				Debug.Log("NPC Detected");
			}else{	
				current_pos.y = current_pos.y + m_speed;
			}
		}else if(dif.x<0){
			if(Physics2D.Raycast(current_pos_2d, -Vector2.right, detect_dst,ObsLayerMask)){
				Debug.Log("Cannot Go Left");
			}else if (Physics2D.Raycast(current_pos_2d,-Vector2.right, detect_dst,NPCLayerMask)){
				Debug.Log("NPC Detected");
			}else{
				current_pos.x = current_pos.x - m_speed;
			}
		}else if(dif.y<0){
			if(Physics2D.Raycast(current_pos_2d, -Vector2.up, detect_dst,ObsLayerMask)){
				Debug.Log("Cannot Go Down");	
			}else if (Physics2D.Raycast(current_pos_2d,-Vector2.up, detect_dst,NPCLayerMask)){
				Debug.Log("NPC Detected");
			}else{
				current_pos.y = current_pos.y - m_speed;
			}	
		}else if(dif.x>0){
			if(Physics2D.Raycast(current_pos_2d, Vector2.right, detect_dst,ObsLayerMask)){
				Debug.Log("Cannot Go Right");
			}else if (Physics2D.Raycast(current_pos_2d,Vector2.right, detect_dst,NPCLayerMask)){
				Debug.Log("NPC Detected");
			}else{
				current_pos.x = current_pos.x + m_speed;
			}
		}
		_transform.position = current_pos;
	}
}

	