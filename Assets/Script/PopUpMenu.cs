using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpMenu : MonoBehaviour {



	//COLOR INDEX
	/*	number - represent color - number in 255 value
		1 - red - 232, 55, 146
		2 - yellow - 255, 225, 41
		4 - cyan - 38, 197, 253

		3 - orange - 252, 107, 24
		5 - blue - 117, 110, 248
		6 - green - 176, 238, 49
		
		0 - white - 233, 233, 233
		7 - black - 17, 28, 56
	*/

	//COLOR FOMULA
	/*	1 + 2 = 3
		2 + 4 = 6
		1 + 4 = 5

		
	*/


	public GameObject popUpMenu;
	public GameObject[] colorPanel;

	static public Color[] colorSlot = {
		new Color(233, 233, 233),
		new Color(232, 55, 146),
		new Color(255, 225, 41),
		new Color(252, 107, 24),
		new Color(38, 197, 253),
		new Color(117, 110, 248),
		new Color(176, 238, 49),
		new Color(17, 28, 56),
	};
	

	public float _timeCount = 5.0f;
	public bool _isShow = false;
	private int _colorCount = 0;
	public int selectNumber = 0;
	private int[] _mergeNumber;
	private SpriteRenderer[] _panelSpriteRenderer;
	// Use this for initialization

	public class ColorProperty{
		public int _mergeNumber;
		public Color _colorValue255;
		public int _colorIndex;
	}

	private ColorProperty[] _colorMerge;

	void Start () {
		_panelSpriteRenderer = new SpriteRenderer[3];
		_colorMerge = new ColorProperty[2];
		for(int i = 0; i < 3; i++){
			_panelSpriteRenderer[i] = colorPanel[i].GetComponent<SpriteRenderer>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow)) && !_isShow){
			colorPanel[0].SetActive(true);
			popUpMenu.SetActive(true);
			
			_isShow = true;
			


		//	_isShow = true;

			/*
			while(_timeCount > 0.0f){
				if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow)){
					_timeCount = 5.0f;
				}else{
					_timeCount -= Time.deltaTime;
				}
			}
			
			popUpMenu.SetActive(false);
			_isShow = false;*/

		}
		if(_isShow){
			if(Input.GetKeyDown(KeyCode.RightArrow)){
				_colorCount = (_colorCount == 3) ? 3 : (_colorCount + 1);
				colorPanel[_colorCount-1].SetActive(true);
				if(_colorCount - 2 >= 0 && _panelSpriteRenderer[_colorCount-2].color == new Color(0,1,1,1))
					colorPanel[_colorCount-2].SetActive(false);
			}else if(Input.GetKeyDown(KeyCode.LeftArrow)){
				_colorCount = (_colorCount == 1) ? 1 : (_colorCount - 1);
				colorPanel[_colorCount-1].SetActive(true);
				if(_panelSpriteRenderer[_colorCount].color == new Color(0,1,1,1))
					colorPanel[_colorCount].SetActive(false);
			}
			if(Input.GetKeyDown(KeyCode.Space)){
				if(_panelSpriteRenderer[_colorCount-1].color == new Color(0,1,1,1)){
					selectNumber = (_colorCount == 2) ? 2 : (_colorCount + 1);
					_panelSpriteRenderer[_colorCount-1].color = new Color(1,0,1,1);
				}
				else{
					selectNumber = (_colorCount == 1) ? 1 : (_colorCount - 1);
					_panelSpriteRenderer[_colorCount-1].color = new Color(0,1,1,1);
				}
				
			}
			if(selectNumber == 1){	
					ColorPropertyStore(_colorMerge[0], _panelSpriteRenderer[_colorCount - 1], _colorCount);
					Debug.Log("1 :" + _colorCount);
			}
			if(selectNumber == 2){
				ColorPropertyStore(_colorMerge[1], _panelSpriteRenderer[_colorCount - 1], _colorCount);
				Debug.Log("2 :" + _colorCount);
			}

			
		}else{
			_colorCount = 0;
		}
	}


	public void ColorPropertyStore(ColorProperty _colorMerge, SpriteRenderer _panelSpriteRenderer, int _colorCount){
		_colorMerge._mergeNumber = _colorCount - 2;
		_colorMerge._colorValue255 = _panelSpriteRenderer.color;
		for(int i = 0; i < 8; i++){
			if(_colorMerge._colorValue255 == colorSlot[i]){
				_colorMerge._colorIndex = i;
				break;
			}
		}
	}
}
