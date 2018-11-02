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
	public GameObject[] color;

	public enum ColorEnum{
		White = 0,
		Red = 1,
		Yellow = 2,
		Orange = 3,
		Cyan = 4,
		Blue = 5,
		Green = 6,
		Black = 7,
	}
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
	public bool _isBlendable = false;
	private int _colorCount = 0;
	public int selectNumber = 0;
//	private int[] _mergeNumber;
	private SpriteRenderer[] _panelSpriteRenderer;
	private SpriteRenderer[] _colorSpriteRenderer;
	// Use this for initialization

	public class ColorProperty{
		public int _mergeNumber;
		public Color _colorValue255;
		public int _colorIndex;
		public ColorProperty(){}

		public ColorProperty(int _mergeNumber, Color _colorValue255, int _colorIndex){
			this._mergeNumber = _mergeNumber;
			this._colorValue255 = _colorValue255;
			this._colorIndex = _colorIndex;
		}
	}

	private ColorProperty[] _colorMerge;
//	private ColorProperty _colorMerge1;
//	private ColorProperty _colorMerge2;

	void Start () {
		_panelSpriteRenderer = new SpriteRenderer[3];
		_colorSpriteRenderer = new SpriteRenderer[3];
		_colorMerge = new ColorProperty[2];
		for(int i = 0; i < 2; i++)
			_colorMerge[i] = new ColorProperty(0,Color.red,0);
	//	_colorMerge1 = new ColorProperty(0,Color.red,0);
	//	_colorMerge2 = new ColorProperty(0,Color.red,0);
		for(int i = 0; i < 3; i++){
			_panelSpriteRenderer[i] = colorPanel[i].GetComponent<SpriteRenderer>();
		}
		for(int i = 0; i < 3; i++)
			_colorSpriteRenderer[i] = color[i].GetComponent<SpriteRenderer>();
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
			//		Debug.Log("_colorCount selectNumber" + _colorCount);
					selectNumber = (selectNumber >= 2) ? 2 : (selectNumber + 1);
					_panelSpriteRenderer[_colorCount-1].color = new Color(1,0,1,1);
				}
				else{
					selectNumber = (selectNumber <= 1) ? 1 : (selectNumber - 1);
			//		Debug.Log("_colorCount selectNumber" + _colorCount);
					_panelSpriteRenderer[_colorCount-1].color = new Color(0,1,1,1);
				}
				if(selectNumber == 1){
					ColorPropertyStore(_colorMerge[0], _colorSpriteRenderer[_colorCount - 1], _colorCount-1);
			//		Debug.Log("1 :" + _colorMerge[1]._colorIndex);
				}
			//	Debug.Log("SelectNumber :" + selectNumber);
				
			}
			
			if(selectNumber == 2){
				ColorPropertyStore(_colorMerge[1], _colorSpriteRenderer[_colorCount - 1], _colorCount-1);
			//	Debug.Log("2 :" + _colorMerge[0]._colorIndex);
				selectNumber = 0;
				colorPanel[_colorMerge[1]._mergeNumber].SetActive(false);
				BlendColor(_colorMerge[0]._colorIndex, _colorMerge[1]._colorIndex, _colorMerge);
			}

			
		}else{
			_colorCount = 0;
		}
	}


	public void ColorPropertyStore(ColorProperty _colorMerge, SpriteRenderer _colorSpriteRenderer, int _colorCount)
	{
		_colorMerge._mergeNumber = _colorCount;
		_colorMerge._colorValue255 = _colorSpriteRenderer.color;
	//	Debug.Log("_colorValue255.r: " + _colorMerge._colorValue255.r*255);
		int i = 0;
		for(i = 0; i < 8; i++){
			if(_colorMerge._colorValue255.r*255 <= colorSlot[i].r + 0.5f && _colorMerge._colorValue255.r*255 >= colorSlot[i].r - 0.5f ){
				_colorMerge._colorIndex = i;
			//	Debug.Log("_colorValue255.r: " + _colorMerge._colorValue255.r*255 + "       colorSlot.r: " + colorSlot[i].r);
				break;
			}
		}
	}

	public void BlendColor(int mergeIndex1, int mergeIndex2, ColorProperty[] _colorMerge){
		int mergePlus = mergeIndex1 + mergeIndex2;
	//	Debug.Log(mergePlus);
		if(mergePlus == 3 && isBasicColor(_colorMerge)){
			SpriteRender((int)ColorEnum.Orange, (int)ColorEnum.White);
		}else if(mergePlus == 5 && isBasicColor(_colorMerge)){
			SpriteRender((int)ColorEnum.Blue, (int)ColorEnum.White);
		//	_colorSpriteRenderer[_colorMerge[0]._mergeNumber].color = new Color(colorSlot[(int)ColorEnum.Blue].r/255, colorSlot[(int)ColorEnum.Blue].g/255, colorSlot[(int)ColorEnum.Blue].b/255);
			
		}else if(mergePlus == 6 && isBasicColor(_colorMerge)){
			SpriteRender((int)ColorEnum.Green, (int)ColorEnum.White);
		//	_colorSpriteRenderer[_colorMerge[0]._mergeNumber].color = new Color(colorSlot[(int)ColorEnum.Green].r/255, colorSlot[(int)ColorEnum.Green].g/255, colorSlot[(int)ColorEnum.Green].b/255);
		}else if(mergeIndex1 == 0 || mergeIndex2 == 0){
			if(mergeIndex1 == 0){
				SpriteRender(_colorMerge[0]._colorIndex, (int)ColorEnum.White);
			}else{
				SpriteRender((int)ColorEnum.White, _colorMerge[0]._colorIndex);
			}

		}else{
			SpriteRender((int)ColorEnum.Black, (int)ColorEnum.White);
		//	_colorSpriteRenderer[_colorMerge[0]._mergeNumber].color = new Color(colorSlot[(int)ColorEnum.Black].r/255, colorSlot[(int)ColorEnum.Black].g/255, colorSlot[(int)ColorEnum.Black].b/255);
		}
	//	_colorSpriteRenderer[_colorMerge[1]._mergeNumber].color = new Color(colorSlot[(int)ColorEnum.White].r/255, colorSlot[(int)ColorEnum.White].g/255, colorSlot[(int)ColorEnum.White].b/255);

	}

	public bool isBasicColor(ColorProperty[] _colorMerge){
		
		bool is1 = false, is2 = false;
		if(_colorMerge[0]._colorIndex == 1 || _colorMerge[0]._colorIndex == 2 || _colorMerge[0]._colorIndex == 4)
			is1 = true;
		if(_colorMerge[1]._colorIndex == 1 || _colorMerge[1]._colorIndex == 2 || _colorMerge[1]._colorIndex == 4)
			is2 = true;
		if(is1 && is2)
			return true;

		return false;
	}

	public void SpriteRender(int color1, int color2){
		_colorSpriteRenderer[_colorMerge[0]._mergeNumber].color = new Color(colorSlot[color1].r/255, colorSlot[color1].g/255, colorSlot[color1].b/255);
		_colorSpriteRenderer[_colorMerge[1]._mergeNumber].color = new Color(colorSlot[color2].r/255, colorSlot[color2].g/255, colorSlot[color2].b/255);
	}
}
