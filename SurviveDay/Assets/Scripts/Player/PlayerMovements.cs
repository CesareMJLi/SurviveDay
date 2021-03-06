﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovements : MonoBehaviour {
	private float m_speed = 1.0f;
	public TextManager textManager;

	private Transform _transform;
	private BoxCollider2D _box_collider;

	public GameObject _player;
	private SpriteRenderer _sprite_rend;

	private float detect_dst = 1.5f;

	private int ObsLayerMask;
	private int NPCLayerMask;

	// Use this for initialization
	void Start () {
		ObsLayerMask = 1 << 15;
		NPCLayerMask = 1 << 10;
		_transform = GetComponent<Transform>() as Transform;
		_box_collider = GetComponent<BoxCollider2D>() as BoxCollider2D;
		_sprite_rend=_player.GetComponent<SpriteRenderer>();
		// textManager = GetComponent<TextManager>();
		// _sprite_rend = GetComponent<SpriteRenderer>() as SpriteRenderer;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 current_pos = _transform.position;
		Vector2 current_pos_2d = new Vector2(current_pos.x,current_pos.y);
		
		Vector2 collider_shift = _box_collider.offset;
		current_pos_2d = current_pos_2d+collider_shift;
		
		if(Input.GetKeyDown(KeyCode.W)){
			if(Physics2D.Raycast(current_pos_2d,Vector2.up, detect_dst,ObsLayerMask)){
				// Debug.Log("Cannot Go Up");
				// globalTextInfo="Cannot Go Up\n"+globalTextInfo;
				textManager.UpdateTextField("【Player】Cannot go up\n");
			}else if (Physics2D.Raycast(current_pos_2d,Vector2.up, detect_dst,NPCLayerMask)){
				// Debug.Log("NPC Detected");
				// globalTextInfo="Cannot Go Up\n"+globalTextInfo;
				textManager.UpdateTextField("【Player】NPC detected\n");
			}else{	
				current_pos.y = current_pos.y + m_speed;
				_sprite_rend.sortingOrder = -(int)_transform.position.y;
				Debug.Log("Moved");
			}
		}else if(Input.GetKeyDown(KeyCode.A)){
			if(Physics2D.Raycast(current_pos_2d, -Vector2.right, detect_dst,ObsLayerMask)){
				// Debug.Log("Cannot Go Left");
				textManager.UpdateTextField("【Player】Cannot go left\n");
			}else if (Physics2D.Raycast(current_pos_2d,-Vector2.right, detect_dst,NPCLayerMask)){
				// Debug.Log("NPC Detected");
				textManager.UpdateTextField("【Player】NPC detected\n");
			}else{
				current_pos.x = current_pos.x - m_speed;
				_sprite_rend.sortingOrder = -(int)_transform.position.y;
			}
		}else if(Input.GetKeyDown(KeyCode.S)){
			if(Physics2D.Raycast(current_pos_2d, -Vector2.up, detect_dst,ObsLayerMask)){
				// Debug.Log("Cannot Go Down");	
				textManager.UpdateTextField("【Player】Cannot go down\n");
			}else if (Physics2D.Raycast(current_pos_2d,-Vector2.up, detect_dst,NPCLayerMask)){
				// Debug.Log("NPC Detected");
				textManager.UpdateTextField("【Player】NPC detected\n");
			}else{
				current_pos.y = current_pos.y - m_speed;
				_sprite_rend.sortingOrder = -(int)_transform.position.y;
			}	
		}else if(Input.GetKeyDown(KeyCode.D)){
			if(Physics2D.Raycast(current_pos_2d, Vector2.right, detect_dst,ObsLayerMask)){
				// Debug.Log("Cannot Go Right");
				textManager.UpdateTextField("【Player】Cannot go right\n");
			}else if (Physics2D.Raycast(current_pos_2d,Vector2.right, detect_dst,NPCLayerMask)){
				// Debug.Log("NPC Detected");
				textManager.UpdateTextField("【Player】NPC detected\n");
			}else{
				current_pos.x = current_pos.x + m_speed;
				_sprite_rend.sortingOrder = -(int)_transform.position.y;
			}
		}
		_transform.position = current_pos;
	}
}
