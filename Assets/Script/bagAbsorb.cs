using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bagAbsorb : MonoBehaviour {

	
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

	public bool _isShow = false;

	//	///////////
    public GameObject popUpMenu;
	public GameObject absorbMenu;

    public GameObject[] color;
	public bag _bag;

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

    public bool isShow = false;
    public bool _isBlendable = false;
    private int _colorCount = 0;
    public int selectNumber = 0;
//  private int[] _mergeNumber;

    private SpriteRenderer[] _colorSpriteRenderer;
	private SpriteRenderer playerSpriteRenderer;
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
		playerSpriteRenderer = GetComponent<SpriteRenderer>();
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
		if(!_isShow){
			_isShow = true;
			_colorSpriteRenderer[0].sprite = mySprite[1];
		}
        
            if(Input.GetKeyDown(KeyCode.RightArrow)||Input.GetKeyDown(KeyCode.JoystickButton5)){
                _colorCount = (_colorCount >= 3) ? 3 : (_colorCount + 1);
                _colorSpriteRenderer[_colorCount-1].sprite = mySprite[1];
                if(_colorCount - 2 >= 0 && _colorSpriteRenderer[_colorCount-2].sprite == mySprite[1])
                    _colorSpriteRenderer[_colorCount-2].sprite = mySprite[0];
            }else if(Input.GetKeyDown(KeyCode.LeftArrow)||Input.GetKeyDown(KeyCode.JoystickButton4)){
                _colorCount = (_colorCount <= 1) ? 1 : (_colorCount - 1);
                _colorSpriteRenderer[_colorCount-1].sprite = mySprite[1];
				if(_colorSpriteRenderer[_colorCount].sprite == mySprite[1])
                	_colorSpriteRenderer[_colorCount].sprite = mySprite[0];
            }
            if(Input.GetKeyDown(KeyCode.Space)||Input.GetKeyDown(KeyCode.JoystickButton3)){

				Debug.Log(_colorCount);
                if(_colorCount - 1 >= 0 && _colorSpriteRenderer[_colorCount-1].sprite != mySprite[2]){
                //    Debug.Log("_colorCount selectNumber: " + _colorCount);
                    selectNumber = (selectNumber >= 1) ? 1 : (selectNumber + 1);
                    _colorSpriteRenderer[_colorCount-1].sprite = mySprite[2];
				//	Debug.Log("SelectNumber3 :" + selectNumber);
                }
                else if(_colorCount - 1 >= 0){
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
					_bag.enabled = false;
					this.enabled = false;
					Debug.Log(_bag.enabled);
                    ColorPropertyStore(_colorMerge[0], _colorSpriteRenderer[_colorCount - 1], _colorCount-1);
					_colorSpriteRenderer[_colorMerge[0]._mergeNumber].color = colorSlot[this.gameObject.GetComponent<PlayerStatus>().ReturnColor()];
					this.gameObject.GetComponent<PlayerStatus>().slot[_colorMerge[0]._mergeNumber] = this.gameObject.GetComponent<PlayerStatus>().ReturnColor();
					selectNumber = 0;
					_colorCount = 0;
					_colorSpriteRenderer[0].sprite = mySprite[0];
					_colorSpriteRenderer[1].sprite = mySprite[0];
					_colorSpriteRenderer[2].sprite = mySprite[0];
					_isShow = false;

//	///////////
//					absorbMenu.SetActive(false);
					popUpMenu.SetActive(false);
//					_bag.enabled = true;
            	}
                
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

   
	public void ChangePlayerStatus(PlayerStatus _playerStatus, ColorProperty[] _colorMerge, int colorIndex1, int colorIndex2){
		_playerStatus.slot[_colorMerge[0]._mergeNumber] = colorIndex1;
		_playerStatus.slot[_colorMerge[1]._mergeNumber] = colorIndex2;
	}

    public void SpriteRender(int color1, int color2){
        _colorSpriteRenderer[_colorMerge[0]._mergeNumber].color = new Color(colorSlot[color1].r/255, colorSlot[color1].g/255, colorSlot[color1].b/255);
        _colorSpriteRenderer[_colorMerge[1]._mergeNumber].color = new Color(colorSlot[color2].r/255, colorSlot[color2].g/255, colorSlot[color2].b/255);
    }

	public Color ReturnColor(){
		playerSpriteRenderer.color = _colorMerge[0]._colorValue255;
		return _colorMerge[0]._colorValue255;
	}

}
