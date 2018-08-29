using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obst_Init : MonoBehaviour {

	private SpriteRenderer _sprite_rend;
	private Transform _transform;
	private BoxCollider2D _box_collider;
	
	private int layerMask;

	// Use this for initialization
	void Start () {
		layerMask = 1 << 15;
		_sprite_rend = GetComponent<SpriteRenderer>() as SpriteRenderer;
		_transform = GetComponent<Transform>() as Transform;
		_sprite_rend.sortingOrder = -(int)_transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {

	}
}

// public class Obst_Init : MonoBehaviour {

// 	private SpriteRenderer _sprite_rend;
// 	private Transform _transform;
// 	private BoxCollider2D _box_collider;
	
// 	private int layerMask;

// 	// Use this for initialization
// 	void Start () {
// 		layerMask = 1 << 8;
// 		_sprite_rend = GetComponent<SpriteRenderer>() as SpriteRenderer;
// 		_transform = GetComponent<Transform>() as Transform;
// 		_box_collider = GetComponent<BoxCollider2D>() as BoxCollider2D;
// 		_sprite_rend.sortingOrder = spriteAbove(_transform, _box_collider, layerMask);
// 	}
	
// 	// Update is called once per frame
// 	void Update () {
// 	}

// 	private int spriteAbove(Transform _tran, BoxCollider2D _box, int layer){
// 		Vector3 current_pos = _tran.position;
// 		Vector2 current_pos_2d = new Vector2(current_pos.x,current_pos.y);
// 		Vector2 collider_shift = _box.offset;
// 		float y = (float)(_box.size.y*0.5+0.1);
// 		Vector2 collider_size_shift = new Vector2(0,y);
// 		current_pos_2d = current_pos_2d+collider_shift+collider_size_shift;

// 		float detect_dst = 0.5f;
		
// 		if(Physics2D.Raycast(current_pos_2d, Vector2.up, detect_dst, layer)){
// 			Debug.Log("Something Above");	
// 			RaycastHit2D hit = Physics2D.Raycast(current_pos_2d,Vector2.up, detect_dst);
// 			Debug.Log(hit.point);
// 			Debug.Log(hit.collider);
// 			SpriteRenderer sprite = hit.collider.GetComponent<SpriteRenderer>();
// 			return sprite.sortingOrder+1;
// 		}else{	
// 			return 0;
// 		}
// 	}
// }
