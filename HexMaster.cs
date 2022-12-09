using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMaster : MonoBehaviour {
    #region 変数
    public int[,] _hex;
    private int[] _card;
    private int _playerPointX;
    private int _playerPointY;
    private int _hexInputArray;
    private int _squareInputArray;
    private int _inputType;
    private int _order;
    private bool _isFin = false;
    [SerializeField] private GameObject _player;
    #endregion
    void Start() {
        _playerPointX = _player.GetComponent<CharacterStat>()._pointX;
        _playerPointY = _player.GetComponent<CharacterStat>()._pointY;
        _hex = new int[21, 11];//初期化
        for (int i = 0; i <= 20; i++) {
            for (int j = 0; j <= 10; j++) {
                _hex[i, j] = 0;//すべての配列に[空白(0)]を入れる
            }
        }
    }

    private void Update() {
        InputHexSystem();
        InputSquareSystem();
        if (Input.GetButtonDown("Submit")) {
            if (_hexInputArray != 0) {
                switch (_inputType) {
                    case 1:
                        Attack();//通常攻撃
                        _inputType = 0;
                        _order = 1;
                        break;
                    case 2:
                        Spell();//特技
                        _inputType = 0;
                        _order = 1;
                        break;
                    case 3:
                        Rest();//休憩
                        _inputType = 0;
                        _order = 1;
                        break;
                    case 4:
                        Move();//移動
                        _inputType = 0;
                        break;
                    default:
                        _inputType = _squareInputArray;
                        print(_inputType);
                        break;
                }
            }
        } else if (Input.GetButtonDown("Cancel")) {
            _inputType = 0;
        }
        if (_order > 0) {
            foreach (Transform children in this.transform) {
                children.GetComponent<CharacterStat>()._count++;
                if (children.GetComponent<CharacterStat>()._characterType == 2 
                && children.gameObject.activeSelf == true) {
                    _isFin = false;
                }
            }
            if (_isFin) {
                print("congratulations!!!");
            } else {
                _isFin = true;
            }
            _order--;
        }
    }
    private void InputHexSystem() {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) {
            float instantInput = Mathf.Atan2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * -180 / Mathf.PI;
            if (instantInput >= -60 && instantInput < 0) {
                _hexInputArray = 1;
            } else if (instantInput >= -120 && instantInput < -60) {
                _hexInputArray = 2;
            } else if (instantInput >= -180 && instantInput < -120) {
                _hexInputArray = 3;
            } else if (instantInput >= 120 && instantInput < 180) {
                _hexInputArray = 4;
            } else if (instantInput >= 60 && instantInput < 120) {
                _hexInputArray = 5;
            } else if (instantInput >= 0 && instantInput < 60) {
                _hexInputArray = 6;
            }
        } else {
            _hexInputArray = 0;
        }
    }
    private void InputSquareSystem() {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) {
            float instantInput = Mathf.Atan2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * -180 / Mathf.PI;
            if (Mathf.Abs(instantInput) < 45) {
                _squareInputArray = 1;
            } else if (instantInput >= -135 && instantInput < -45) {
                _squareInputArray = 2;
            } else if (Mathf.Abs(instantInput) >= 135) {
                _squareInputArray = 3;
            } else if (instantInput > 45 && instantInput <= 135) {
                _squareInputArray = 4;
            }
        }
    }
    public void Move() {
        switch (_hexInputArray) {
            case 1:
                //配列を超過する場合は処理をしない
                if (_playerPointX + 1 > 20 || _playerPointY + 1 > 10) {
                    break;
                } else if (_playerPointX + _playerPointY + 2 > 25) {
                    break;
                } else {
                    _hex[_playerPointX, _playerPointY] = 0;
                    _playerPointX += 1;
                    _playerPointY += 1;
                    _order = 1;
                }
                break;
            case 2:
                //配列を超過する場合は処理をしない
                if (_playerPointX + 2 > 20) {
                    break;
                } else if (_playerPointX + _playerPointY + 2 > 25 || _playerPointX - _playerPointY == 15) {
                    break;
                } else {
                    _hex[_playerPointX, _playerPointY] = 0;
                    _playerPointX += 2;
                    _order = 1;
                }
                break;
            case 3:
                //配列を超過する場合は処理をしない
                if (_playerPointX + 1 > 20 || _playerPointY - 1 < 0) {
                    break;
                } else if (_playerPointX - _playerPointY == 15) {
                    break;
                } else {
                    _hex[_playerPointX, _playerPointY] = 0;
                    _playerPointX += 1;
                    _playerPointY -= 1;
                    _order = 1;
                }
                break;
            case 4:
                //配列を超過する場合は処理をしない
                if (_playerPointX - 1 < 0 || _playerPointY - 1 < 0) {
                    break;
                } else if (_playerPointX + _playerPointY - 2 < 5) {
                    break;
                } else {
                    _hex[_playerPointX, _playerPointY] = 0;
                    _playerPointX -= 1;
                    _playerPointY -= 1;
                    _order = 1;
                }
                break;
            case 5:
                //配列を超過する場合は処理をしない
                if (_playerPointX - 2 < 0) {
                    break;
                } else if (_playerPointX + _playerPointY - 2 < 5 || _playerPointY - _playerPointX == 5) {
                    break;
                } else {
                    _hex[_playerPointX, _playerPointY] = 0;
                    _playerPointX -= 2;
                    _order = 1;
                }
                break;
            case 6:
                //配列を超過する場合は処理をしない
                if (_playerPointX - 1 < 0 || _playerPointY + 1 > 10) {
                    break;
                } else if (_playerPointY - _playerPointX == 5) {
                    break;
                } else {
                    _hex[_playerPointX, _playerPointY] = 0;
                    _playerPointX -= 1;
                    _playerPointY += 1;
                    _order = 1;
                }
                break;
            default:
                break;
        }
        _hex[_playerPointX, _playerPointY] = 5;
        _player.transform.position = new Vector2(1.28f * (_playerPointX - 10), 1.448f * (_playerPointY - 5));
    }
    public void Attack() {
        int reference;
        switch (_hexInputArray) {
            case 1:
                reference = _hex[_playerPointX + 1, _playerPointY + 1];
                if (reference == 2 || reference == 3) {
                    foreach (Transform children in this.transform) {
                        if (children.GetComponent<CharacterStat>()._pointX == _playerPointX + 1
                        || children.GetComponent<CharacterStat>()._pointY == _playerPointY + 1) {
                            children.GetComponent<CharacterStat>()._hp -= 10 + PlayerStatus._attack - children.GetComponent<CharacterStat>()._defense;
                        }
                    }
                }
                break;
            case 2:
                reference = _hex[_playerPointX + 2, _playerPointY];
                if (reference == 2 || reference == 3) {
                    foreach (Transform children in this.transform) {
                        if (children.GetComponent<CharacterStat>()._pointX == _playerPointX + 2
                        || children.GetComponent<CharacterStat>()._pointY == _playerPointY) {
                            children.GetComponent<CharacterStat>()._hp -= 10 + PlayerStatus._attack - children.GetComponent<CharacterStat>()._defense;
                        }
                    }
                }
                break;
            case 3:
                reference = _hex[_playerPointX + 1, _playerPointY - 1];
                if (reference == 2 || reference == 3) {
                    foreach (Transform children in this.transform) {
                        if (children.GetComponent<CharacterStat>()._pointX == _playerPointX + 1
                        || children.GetComponent<CharacterStat>()._pointY == _playerPointY - 1) {
                            children.GetComponent<CharacterStat>()._hp -= 10 + PlayerStatus._attack - children.GetComponent<CharacterStat>()._defense;
                        }
                    }
                }
                break;
            case 4:
                reference = _hex[_playerPointX - 1, _playerPointY - 1];
                if (reference == 2 || reference == 3) {
                    foreach (Transform children in this.transform) {
                        if (children.GetComponent<CharacterStat>()._pointX == _playerPointX - 1
                        || children.GetComponent<CharacterStat>()._pointY == _playerPointY - 1) {
                            children.GetComponent<CharacterStat>()._hp -= 10 + PlayerStatus._attack - children.GetComponent<CharacterStat>()._defense;
                        }
                    }
                }
                break;
            case 5:
                reference = _hex[_playerPointX - 2, _playerPointY];
                if (reference == 2 || reference == 3) {
                    foreach (Transform children in this.transform) {
                        if (children.GetComponent<CharacterStat>()._pointX == _playerPointX - 2
                        || children.GetComponent<CharacterStat>()._pointY == _playerPointY) {
                            children.GetComponent<CharacterStat>()._hp -= 10 + PlayerStatus._attack - children.GetComponent<CharacterStat>()._defense;
                        }
                    }
                }
                break;
            case 6:
                reference = _hex[_playerPointX - 1, _playerPointY + 1];
                if (reference == 2 || reference == 3) {
                    foreach (Transform children in this.transform) {
                        if (children.GetComponent<CharacterStat>()._pointX == _playerPointX - 1
                        || children.GetComponent<CharacterStat>()._pointY == _playerPointY + 1) {
                            children.GetComponent<CharacterStat>()._hp -= 10 + PlayerStatus._attack - children.GetComponent<CharacterStat>()._defense;
                        }
                    }
                }
                break;
            default:
                break;
        }
    }
    public void Spell() {

    }
    private void Rest() {
        _player.GetComponent<CharacterStat>()._hp += _player.GetComponent<CharacterStat>()._hpMax / 10;
        //リフレッシュ
    }
}