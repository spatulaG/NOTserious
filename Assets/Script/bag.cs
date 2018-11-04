using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bag : MonoBehaviour {



    //COLOR INDEX
    /*  number - represent color - number in 255 value
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
    /*  1 + 2 = 3
        2 + 4 = 6
        1 + 4 = 5
    */

    public GameObject popUpMenu;
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
    
	public Sprite[] mySprite;

    public bool _isShow = false;
    public bool _isBlendable = false;
    private int _colorCount = 0;
    public int selectNumber = 0;
//  private int[] _mergeNumber;

    private SpriteRenderer[] _colorSpriteRenderer;
	public SpriteRenderer playerSpriteRenderer;
	private PlayerStatus _playerStatus;
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

    void Start () {
        _colorSpriteRenderer = new SpriteRenderer[3];
        _colorMerge = new ColorProperty[2];
		_playerStatus = GetComponent<PlayerStatus>();
        for(int i = 0; i < 2; i++)
            _colorMerge[i] = new ColorProperty(0,Color.red,0);
        for(int i = 0; i < 3; i++)
            _colorSpriteRenderer[i] = color[i].GetComponent<SpriteRenderer>();
    }
    
    // Update is called once per frame
    void Update () {
		for(int i = 0; i < 3; i++){
			_colorSpriteRenderer[i].color = new Color(colorSlot[_playerStatus.slot[i]].r/255, colorSlot[_playerStatus.slot[i]].g/255, colorSlot[_playerStatus.slot[i]].b/255);
		}
		/*
		if(Input.GetKeyDown(KeyCode.Escape))
			_isShow = false;
        */
        if(((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.JoystickButton4))|| (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.JoystickButton5))) && !_isShow){
            popUpMenu.SetActive(true);
			_colorSpriteRenderer[0].sprite = mySprite[1];
           
            _isShow = true;

        }
        if(_isShow){
			if(Input.GetKeyDown(KeyCode.Escape)||Input.GetKeyDown(KeyCode.JoystickButton0))
				_isShow = false;
            if(Input.GetKeyDown(KeyCode.RightArrow)||Input.GetKeyDown(KeyCode.JoystickButton5)){
                _colorCount = (_colorCount >= 3) ? 3 : (_colorCount + 1);
				if( _colorSpriteRenderer[_colorCount-1].sprite != mySprite[2])
                	_colorSpriteRenderer[_colorCount-1].sprite = mySprite[1];
                if(_colorCount - 2 >= 0 && _colorSpriteRenderer[_colorCount-2].sprite == mySprite[1])
                    _colorSpriteRenderer[_colorCount-2].sprite = mySprite[0];
            }else if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.JoystickButton4)){
                _colorCount = (_colorCount <= 1) ? 1 : (_colorCount - 1);
				
				if(_colorSpriteRenderer[_colorCount-1].sprite != mySprite[2])
                	_colorSpriteRenderer[_colorCount-1].sprite = mySprite[1];
				if(_colorSpriteRenderer[_colorCount].sprite == mySprite[1])
                	_colorSpriteRenderer[_colorCount].sprite = mySprite[0];
            }
            if(Input.GetKeyDown(KeyCode.Space)||Input.GetKeyDown(KeyCode.JoystickButton1)){
                if(_colorSpriteRenderer[_colorCount-1].sprite != mySprite[2]){
                //    Debug.Log("_colorCount selectNumber: " + _colorCount);
                    selectNumber = (selectNumber >= 2) ? 2 : (selectNumber + 1);
                    _colorSpriteRenderer[_colorCount-1].sprite = mySprite[2];
				//	Debug.Log("SelectNumber3 :" + selectNumber);
                }
                else{
					if(_colorMerge[1]._mergeNumber == _colorMerge[0]._mergeNumber){
                //    	Debug.Log("equal");

						_colorSpriteRenderer[_colorCount-1].sprite = mySprite[1];
                    	selectNumber = 0;
                	}
					else{
						selectNumber = (selectNumber <= 0) ? 0 : (selectNumber - 1);
                //    Debug.Log("_colorCount selectNumber: " + _colorCount);
                 	   _colorSpriteRenderer[_colorCount-1].sprite = mySprite[1];
				//		Debug.Log("SelectNumber4 :" + selectNumber);
                    
					}
                }
				if(selectNumber == 1){
                    ColorPropertyStore(_colorMerge[0], _colorSpriteRenderer[_colorCount - 1], _colorCount-1);
					Debug.Log("_colorMerge[0]._colorIndex" + _colorMerge[0]._colorIndex);
					Debug.Log("_colorMerge[1]._colorIndex" + _colorMerge[1]._colorIndex);
					playerSpriteRenderer.color = _colorMerge[0]._colorValue255;
					ReturnColor();
				//	Debug.Log("SelectNumber5 :" + selectNumber);
            //      Debug.Log("1 :" + _colorMerge[1]._colorIndex);
				//	Debug.Log("return color" + ReturnColor());
            	}
				
                
            //  	Debug.Log("SelectNumber1 :" + selectNumber);
                
            }
			

            if(selectNumber == 2){
                ColorPropertyStore(_colorMerge[1], _colorSpriteRenderer[_colorCount - 1], _colorCount-1);
				_colorSpriteRenderer[_colorMerge[1]._mergeNumber].sprite = mySprite[0];
				_colorSpriteRenderer[_colorMerge[0]._mergeNumber].sprite = mySprite[1];
			//	Debug.Log("SelectNumber2 :" + selectNumber);
		
             
                    selectNumber = 0;
                    _colorCount = _colorMerge[0]._mergeNumber + 1;
            //      Debug.Log("_colorCount selectNumber2 " + _colorCount);
                    BlendColor(_colorMerge[0]._colorIndex, _colorMerge[1]._colorIndex, _colorMerge);
					if(selectNumber == 0){
					_colorSpriteRenderer[_colorMerge[0]._mergeNumber].sprite = mySprite[1];
					for(int i = 0; i < 3; i++){
						if(i != _colorMerge[0]._mergeNumber)
							_colorSpriteRenderer[i].sprite = mySprite[0];
					}
				}
            }
					//Debug.Log("SelectNumber " + selectNumber);

            
        }else{
            _colorCount = 0;
			selectNumber = 0;
			_isShow = false;
			_colorSpriteRenderer[0].sprite = mySprite[0];
			_colorSpriteRenderer[1].sprite = mySprite[0];
			_colorSpriteRenderer[2].sprite = mySprite[0];
			popUpMenu.SetActive(false);
			//Debug.Log("SetFalse");
		//	Destroy(this);
        }
    }


    public void ColorPropertyStore(ColorProperty _colorMerge, SpriteRenderer _colorSpriteRenderer, int _colorCount)
    {
        _colorMerge._mergeNumber = _colorCount;
        _colorMerge._colorValue255 = _colorSpriteRenderer.color;
    //  Debug.Log("_colorValue255.r: " + _colorMerge._colorValue255.r*255);
        int i = 0;
        for(i = 0; i < 8; i++){
            if(_colorMerge._colorValue255.r*255 <= colorSlot[i].r + 0.5f && _colorMerge._colorValue255.r*255 >= colorSlot[i].r - 0.5f ){
                _colorMerge._colorIndex = i;
            //  Debug.Log("_colorValue255.r: " + _colorMerge._colorValue255.r*255 + "       colorSlot.r: " + colorSlot[i].r);
                break;
            }
        }
    }

    public void BlendColor(int mergeIndex1, int mergeIndex2, ColorProperty[] _colorMerge){
        int mergePlus = mergeIndex1 + mergeIndex2;
    //  Debug.Log(mergePlus);
        if(mergePlus == 3 && isBasicColor(_colorMerge)){
            SpriteRender((int)ColorEnum.Orange, (int)ColorEnum.White);
			ChangePlayerStatus(_playerStatus, _colorMerge, (int)ColorEnum.Orange, (int)ColorEnum.White);
        }else if(mergePlus == 5 && isBasicColor(_colorMerge)){
            SpriteRender((int)ColorEnum.Blue, (int)ColorEnum.White);
			ChangePlayerStatus(_playerStatus, _colorMerge, (int)ColorEnum.Blue, (int)ColorEnum.White);
        }else if(mergePlus == 6 && isBasicColor(_colorMerge)){
            SpriteRender((int)ColorEnum.Green, (int)ColorEnum.White);
			ChangePlayerStatus(_playerStatus, _colorMerge, (int)ColorEnum.Green, (int)ColorEnum.White);

        }else if(isBlackColor(_colorMerge)){
            SpriteRender((int)ColorEnum.Black, (int)ColorEnum.White);
			ChangePlayerStatus(_playerStatus, _colorMerge, (int)ColorEnum.Black, (int)ColorEnum.White);
        }else if(isWhiteColor(_colorMerge) && _colorMerge[0]._mergeNumber != _colorMerge[1]._mergeNumber){
            if(mergeIndex1 == 0){
            //    Debug.Log("_colorMerge[0]._colorIndex " + _colorMerge[0]._colorIndex);
                SpriteRender(_colorMerge[1]._colorIndex, (int)ColorEnum.White);
				ChangePlayerStatus(_playerStatus, _colorMerge, _colorMerge[1]._colorIndex, (int)ColorEnum.White);
            }else{
            //    SpriteRender((int)ColorEnum.White, _colorMerge[0]._colorIndex);
            }

        }else if(_colorMerge[0]._colorIndex == _colorMerge[1]._colorIndex){
		//	Debug.Log("same");
			SpriteRender(_colorMerge[0]._colorIndex,(int)ColorEnum.White);
			ChangePlayerStatus(_playerStatus, _colorMerge, _colorMerge[0]._colorIndex, (int)ColorEnum.White);
		}else if(_colorMerge[0]._mergeNumber != _colorMerge[1]._mergeNumber){
			SpriteRender((int)ColorEnum.Black, (int)ColorEnum.White);
			ChangePlayerStatus(_playerStatus, _colorMerge, (int)ColorEnum.Black, (int)ColorEnum.White);
		}
	//	Debug.Log("_colorMerge[0]._mergeNumber " + _colorMerge[0]._mergeNumber);
	//	Debug.Log("_colorMerge[1]._mergeNumber " + _colorMerge[1]._mergeNumber);
		
	//	Debug.Log("_colorMerge[0]._colorIndex" + _colorMerge[0]._colorIndex);
    }
	public void ChangePlayerStatus(PlayerStatus _playerStatus, ColorProperty[] _colorMerge, int colorIndex1, int colorIndex2){
		_playerStatus.slot[_colorMerge[0]._mergeNumber] = colorIndex1;
		_playerStatus.slot[_colorMerge[1]._mergeNumber] = colorIndex2;
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
    public bool isWhiteColor(ColorProperty[] _colorMerge){
        if(_colorMerge[0]._colorIndex == 0 || _colorMerge[1]._colorIndex == 0)
            return true;
        return false;
    }

    public bool isBlackColor(ColorProperty[] _colorMerge){
        if(_colorMerge[0]._colorIndex == 7 || _colorMerge[1]._colorIndex == 7)
            return true;
        return false;
    }

    public void SpriteRender(int color1, int color2){
        _colorSpriteRenderer[_colorMerge[0]._mergeNumber].color = new Color(colorSlot[color1].r/255, colorSlot[color1].g/255, colorSlot[color1].b/255);
        _colorSpriteRenderer[_colorMerge[1]._mergeNumber].color = new Color(colorSlot[color2].r/255, colorSlot[color2].g/255, colorSlot[color2].b/255);
    }

	public Color ReturnColor(){
		playerSpriteRenderer.color = _colorMerge[0]._colorValue255;
		return _colorMerge[0]._colorValue255;
	}
/*
	void OnEnable() {
		_colorSpriteRenderer = new SpriteRenderer[3];
        for(int i = 0; i < 3; i++)
            _colorSpriteRenderer[i] = color[i].GetComponent<SpriteRenderer>();
		_colorCount = 0;
		selectNumber = 0;
		_isShow = false;
		_colorSpriteRenderer[0].sprite = mySprite[0];
		_colorSpriteRenderer[1].sprite = mySprite[0];
		_colorSpriteRenderer[2].sprite = mySprite[0];
		Debug.Log("SetFalse");
	}
*/	

	
}