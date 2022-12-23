using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMaster : MonoBehaviour {
    #region ïœêî
    public int[,] _hex;
    public int[,] _card;
    private int _playerPointX;
    private int _playerPointY;
    private int _hexInputArray;
    private int _squareInputArray;
    public int _inputType;
    private int _order;
    private int _cast;
    private bool _isFin = false;
    private int _reference;
    [SerializeField] private GameObject _player;
    #endregion
    void Start() {
        _playerPointX = _player.GetComponent<CharacterStat>()._pointX;
        _playerPointY = _player.GetComponent<CharacterStat>()._pointY;
        _hex = new int[21, 11];//èâä˙âª
        for (int i = 0; i <= 20; i++) {
            for (int j = 0; j <= 10; j++) {
                _hex[i, j] = 0;//Ç∑Ç◊ÇƒÇÃîzóÒÇ…[ãÛîí(0)]Çì¸ÇÍÇÈ
            }
        }
    }

    private void Update() {
        InputHexSystem();
        InputSquareSystem();
        if (Input.GetButtonDown("Submit")) {
            if (_hexInputArray != 0 || _inputType == 3) {
                switch (_inputType) {
                    case 1:
                        Attack();//í èÌçUåÇ
                        _inputType = 0;
                        _order = 1;
                        break;
                    case 2:
                        SpellSelect();//ì¡ãZ
                        if (_card[_cast, 3] == 1) {
                            _inputType = 2;
                            break;
                        }
                        _inputType = 5;
                        break;
                    case 3:
                        Rest();//ãxåe
                        _inputType = 0;
                        _order = 1;
                        break;
                    case 4:
                        Move();//à⁄ìÆ
                        _inputType = 0;
                        _order = 1;
                        break;
                    case 5:
                        SpellCast();
                        _inputType = 0;
                        _order = 1;
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
    private void Move() {
        switch (_hexInputArray) {
            case 1:
                //îzóÒÇí¥âﬂÇ∑ÇÈèÍçáÇÕèàóùÇÇµÇ»Ç¢
                if (_playerPointX + 1 > 20 || _playerPointY + 1 > 10) {
                    break;
                } else if (_playerPointX + _playerPointY + 2 > 25) {
                    break;
                } else if(_hex[_playerPointX + 1,_playerPointY + 1] == 0) {
                    _hex[_playerPointX, _playerPointY] = 0;
                    _playerPointX += 1;
                    _playerPointY += 1;
                    _player.GetComponent<CharacterStat>()._pointX += 1;
                    _player.GetComponent<CharacterStat>()._pointY += 1;
                    _hex[_playerPointX, _playerPointY] = 5;
                    _player.transform.position = new Vector2(1.28f * (_playerPointX - 10), 1.448f * (_playerPointY - 5));
                }
                break;
            case 2:
                //îzóÒÇí¥âﬂÇ∑ÇÈèÍçáÇÕèàóùÇÇµÇ»Ç¢
                if (_playerPointX + 2 > 20) {
                    break;
                } else if (_playerPointX + _playerPointY + 2 > 25 || _playerPointX - _playerPointY == 15) {
                    break;
                } else if (_hex[_playerPointX + 2, _playerPointY] == 0) {
                    _hex[_playerPointX, _playerPointY] = 0;
                    _playerPointX += 2;
                    _player.GetComponent<CharacterStat>()._pointX += 2;
                    _order = 1;
                    _hex[_playerPointX, _playerPointY] = 5;
                    _player.transform.position = new Vector2(1.28f * (_playerPointX - 10), 1.448f * (_playerPointY - 5));
                }
                break;
            case 3:
                //îzóÒÇí¥âﬂÇ∑ÇÈèÍçáÇÕèàóùÇÇµÇ»Ç¢
                if (_playerPointX + 1 > 20 || _playerPointY - 1 < 0) {
                    break;
                } else if (_playerPointX - _playerPointY == 15) {
                    break;
                } else if (_hex[_playerPointX + 1, _playerPointY - 1] == 0) {
                    _hex[_playerPointX, _playerPointY] = 0;
                    _playerPointX += 1;
                    _playerPointY -= 1;
                    _player.GetComponent<CharacterStat>()._pointX += 1;
                    _player.GetComponent<CharacterStat>()._pointY -= 1;
                    _order = 1;
                    _hex[_playerPointX, _playerPointY] = 5;
                    _player.transform.position = new Vector2(1.28f * (_playerPointX - 10), 1.448f * (_playerPointY - 5));
                }
                break;
            case 4:
                //îzóÒÇí¥âﬂÇ∑ÇÈèÍçáÇÕèàóùÇÇµÇ»Ç¢
                if (_playerPointX - 1 < 0 || _playerPointY - 1 < 0) {
                    break;
                } else if (_playerPointX + _playerPointY - 2 < 5) {
                    break;
                } else if (_hex[_playerPointX - 1, _playerPointY - 1] == 0) {
                    _hex[_playerPointX, _playerPointY] = 0;
                    _playerPointX -= 1;
                    _playerPointY -= 1;
                    _player.GetComponent<CharacterStat>()._pointX -= 1;
                    _player.GetComponent<CharacterStat>()._pointY -= 1;
                    _order = 1;
                    _hex[_playerPointX, _playerPointY] = 5;
                    _player.transform.position = new Vector2(1.28f * (_playerPointX - 10), 1.448f * (_playerPointY - 5));
                }
                break;
            case 5:
                //îzóÒÇí¥âﬂÇ∑ÇÈèÍçáÇÕèàóùÇÇµÇ»Ç¢
                if (_playerPointX - 2 < 0) {
                    break;
                } else if (_playerPointX + _playerPointY - 2 < 5 || _playerPointY - _playerPointX == 5) {
                    break;
                } else if (_hex[_playerPointX - 2, _playerPointY] == 0) {
                    _hex[_playerPointX, _playerPointY] = 0;
                    _playerPointX -= 2;
                    _player.GetComponent<CharacterStat>()._pointX -= 2;
                    _order = 1;
                    _hex[_playerPointX, _playerPointY] = 5;
                    _player.transform.position = new Vector2(1.28f * (_playerPointX - 10), 1.448f * (_playerPointY - 5));
                }
                break;
            case 6:
                //îzóÒÇí¥âﬂÇ∑ÇÈèÍçáÇÕèàóùÇÇµÇ»Ç¢
                if (_playerPointX - 1 < 0 || _playerPointY + 1 > 10) {
                    break;
                } else if (_playerPointY - _playerPointX == 5) {
                    break;
                } else if (_hex[_playerPointX - 1, _playerPointY + 1] == 0) {
                    _hex[_playerPointX, _playerPointY] = 0;
                    _playerPointX -= 1;
                    _playerPointY += 1;
                    _player.GetComponent<CharacterStat>()._pointX -= 1;
                    _player.GetComponent<CharacterStat>()._pointY += 1;
                    _order = 1;
                    _hex[_playerPointX, _playerPointY] = 5;
                    _player.transform.position = new Vector2(1.28f * (_playerPointX - 10), 1.448f * (_playerPointY - 5));
                }
                break;
            default:
                break;
        }
    }
    private void Attack() {
        switch (_hexInputArray) {
            case 1:
                if (_playerPointX + 1 > 20 || _playerPointY + 1 > 10) {
                    break;
                }
                _reference = _hex[_playerPointX + 1, _playerPointY + 1];
                if (_reference == 2 || _reference == 3) {
                    foreach (Transform children in this.transform) {
                        if (children.GetComponent<CharacterStat>()._pointX == _playerPointX + 1
                        || children.GetComponent<CharacterStat>()._pointY == _playerPointY + 1) {
                            children.GetComponent<CharacterStat>()._hp -= 10 + PlayerStatus._attack - children.GetComponent<CharacterStat>()._defense;
                        }
                    }
                }
                break;
            case 2:
                if (_playerPointX + 2 > 20) {
                    break;
                }
                _reference = _hex[_playerPointX + 2, _playerPointY];
                if (_reference == 2 || _reference == 3) {
                    foreach (Transform children in this.transform) {
                        if (children.GetComponent<CharacterStat>()._pointX == _playerPointX + 2
                        || children.GetComponent<CharacterStat>()._pointY == _playerPointY) {
                            children.GetComponent<CharacterStat>()._hp -= 10 + PlayerStatus._attack - children.GetComponent<CharacterStat>()._defense;
                        }
                    }
                }
                break;
            case 3:
                if (_playerPointX + 1 > 20 || _playerPointY - 1 < 0) {
                    break;
                }
                _reference = _hex[_playerPointX + 1, _playerPointY - 1];
                if (_reference == 2 || _reference == 3) {
                    foreach (Transform children in this.transform) {
                        if (children.GetComponent<CharacterStat>()._pointX == _playerPointX + 1
                        || children.GetComponent<CharacterStat>()._pointY == _playerPointY - 1) {
                            children.GetComponent<CharacterStat>()._hp -= 10 + PlayerStatus._attack - children.GetComponent<CharacterStat>()._defense;
                        }
                    }
                }
                break;
            case 4:
                if (_playerPointX - 1 > 0 || _playerPointY - 1 > 0) {
                    break;
                }
                _reference = _hex[_playerPointX - 1, _playerPointY - 1];
                if (_reference == 2 || _reference == 3) {
                    foreach (Transform children in this.transform) {
                        if (children.GetComponent<CharacterStat>()._pointX == _playerPointX - 1
                        || children.GetComponent<CharacterStat>()._pointY == _playerPointY - 1) {
                            children.GetComponent<CharacterStat>()._hp -= 10 + PlayerStatus._attack - children.GetComponent<CharacterStat>()._defense;
                        }
                    }
                }
                break;
            case 5:
                if (_playerPointX - 2 < 0) {
                    break;
                }
                _reference = _hex[_playerPointX - 2, _playerPointY];
                if (_reference == 2 || _reference == 3) {
                    foreach (Transform children in this.transform) {
                        if (children.GetComponent<CharacterStat>()._pointX == _playerPointX - 2
                        || children.GetComponent<CharacterStat>()._pointY == _playerPointY) {
                            children.GetComponent<CharacterStat>()._hp -= 10 + PlayerStatus._attack - children.GetComponent<CharacterStat>()._defense;
                        }
                    }
                }
                break;
            case 6:
                if (_playerPointX - 1 < 0 || _playerPointY + 1 > 10) {
                    break;
                }
                _reference = _hex[_playerPointX - 1, _playerPointY + 1];
                if (_reference == 2 || _reference == 3) {
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
    private void SpellSelect() {
        _cast = _squareInputArray - 1;
        if (_card[_cast, 3] == 1) {
            print("This spell's Already cast,Take a refresh.");
            _inputType = 2;
            return;
        }
        print("Cast!");
    }
    private void SpellCast() {
        switch (_hexInputArray) {
            case 1:
                if (_playerPointX + _card[_cast, 1] > 20 || _playerPointY + _card[_cast, 1] > 10) {
                    break;
                }
                _reference = _hex[_playerPointX + _card[_cast, 1], _playerPointY + _card[_cast, 1]];
                if (_reference == 2 || _reference == 3 || _reference == 5) {
                    foreach (Transform children in this.transform) {
                        if (children.GetComponent<CharacterStat>()._pointX == _playerPointX + _card[_cast, 1]
                        || children.GetComponent<CharacterStat>()._pointY == _playerPointY + _card[_cast, 1]) {
                            children.GetComponent<CharacterStat>()._hp += _card[_cast, 2] + PlayerStatus._attack - children.GetComponent<CharacterStat>()._defense;
                            _card[_cast, 3] = 1;
                        }
                    }
                }
                break;
            case 2:
                if (_playerPointX + (_card[_cast, 1] * 2) > 20) {
                    break;
                }
                _reference = _hex[_playerPointX + (_card[_cast, 1] * 2), _playerPointY];
                if (_reference == 2 || _reference == 3 || _reference == 5) {
                    foreach (Transform children in this.transform) {
                        if (children.GetComponent<CharacterStat>()._pointX == _playerPointX + (_card[_cast, 1] * 2)
                        || children.GetComponent<CharacterStat>()._pointY == _playerPointY) {
                            children.GetComponent<CharacterStat>()._hp += _card[_cast, 2] + PlayerStatus._attack - children.GetComponent<CharacterStat>()._defense;
                            _card[_cast, 3] = 1;
                        }
                    }
                }
                break;
            case 3:
                if (_playerPointX + _card[_cast, 1] > 20 || _playerPointY - _card[_cast, 1] < 0) {
                    break;
                }
                _reference = _hex[_playerPointX + _card[_cast, 1], _playerPointY - _card[_cast, 1]];
                if (_reference == 2 || _reference == 3 || _reference == 5) {
                    foreach (Transform children in this.transform) {
                        if (children.GetComponent<CharacterStat>()._pointX == _playerPointX + _card[_cast, 1]
                        || children.GetComponent<CharacterStat>()._pointY == _playerPointY - _card[_cast, 1]) {
                            children.GetComponent<CharacterStat>()._hp += _card[_cast, 2] + PlayerStatus._attack - children.GetComponent<CharacterStat>()._defense;
                            _card[_cast, 3] = 1;
                        }
                    }
                }
                break;
            case 4:
                if (_playerPointX - _card[_cast, 1] < 0 || _playerPointY - _card[_cast, 1] < 0) {
                    break;
                }
                _reference = _hex[_playerPointX - _card[_cast, 1], _playerPointY - _card[_cast, 1]];
                if (_reference == 2 || _reference == 3 || _reference == 5) {
                    foreach (Transform children in this.transform) {
                        if (children.GetComponent<CharacterStat>()._pointX == _playerPointX - _card[_cast, 1]
                        || children.GetComponent<CharacterStat>()._pointY == _playerPointY - _card[_cast, 1]) {
                            children.GetComponent<CharacterStat>()._hp += _card[_cast, 2] + PlayerStatus._attack - children.GetComponent<CharacterStat>()._defense;
                            _card[_cast, 3] = 1;
                        }
                    }
                }
                break;
            case 5:
                if (_playerPointX - (_card[_cast, 1] * 2) < 0) {
                    break;
                }
                _reference = _hex[_playerPointX - (_card[_cast, 1] * 2), _playerPointY];
                if (_reference == 2 || _reference == 3 || _reference == 5) {
                    foreach (Transform children in this.transform) {
                        if (children.GetComponent<CharacterStat>()._pointX == _playerPointX - (_card[_cast, 1] * 2)
                        || children.GetComponent<CharacterStat>()._pointY == _playerPointY) {
                            children.GetComponent<CharacterStat>()._hp += _card[_cast, 2] + PlayerStatus._attack - children.GetComponent<CharacterStat>()._defense;
                            _card[_cast, 3] = 1;
                        }
                    }
                }
                break;
            case 6:
                if (_playerPointX - _card[_cast, 1] < 0 || _playerPointY + _card[_cast, 1] > 10) {
                    break;
                }
                _reference = _hex[_playerPointX - _card[_cast, 1], _playerPointY + _card[_cast, 1]];
                if (_reference == 2 || _reference == 3 || _reference == 5) {
                    foreach (Transform children in this.transform) {
                        if (children.GetComponent<CharacterStat>()._pointX == _playerPointX - _card[_cast, 1]
                        || children.GetComponent<CharacterStat>()._pointY == _playerPointY + _card[_cast, 1]) {
                            children.GetComponent<CharacterStat>()._hp += _card[_cast, 2] + PlayerStatus._attack - children.GetComponent<CharacterStat>()._defense;
                            _card[_cast, 3] = 1;
                        }
                    }
                }
                break;
            default:
                break;
        }
    }
    private void Rest() {
        _player.GetComponent<CharacterStat>()._hp += _player.GetComponent<CharacterStat>()._hpMax / 10;
        for (int i = 0;i < 4;i++) {
            _card[i, 3] = 0;
        }
    }
}