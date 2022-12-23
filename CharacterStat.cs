using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : MonoBehaviour 
{
    [SerializeField] private HexMaster _master;
    [SerializeField] private int _level;//レベル
    [SerializeField] public int _hpMax;//最大体力
    [SerializeField] public int _hp;//体力
    [SerializeField] private int _deckNumber;//デッキ番号
    [SerializeField] public int _attack;//攻撃力
    [SerializeField] public int _defense;//防御力
    [SerializeField] TextAsset _deckData;
    [SerializeField] private int _scanSubject = 5;//対象オブジェクトタイプ
    public int[,] _cardData;
    private string[] _allData;
    private string _deck;
    public int _characterType;//キャラクターのタイプ
    public int _pointX;//X座標
    public int _pointY;//Y座標
    public int _count;//行動回数ストック
    private int _rand;
    private GameObject _referenceObject;
    private void Start() {
        transform.position = new Vector2((_pointX - 10) * 1.28f, (_pointY - 5) * 1.448f);
        _allData = _deckData.text.Split('\n');
        for (int i = 0; i < _deckNumber; i++) {
            _deck = _allData[i];
        }
        _cardData = new int[4, 10];//変えたいなぁ
        for (int i = 0; _deck.Length > 1; i++) {
            for (int j = 0; _deck.Substring(0, 1) != "/"; j++) {
                _cardData[i, j] = int.Parse(_deck.Substring(0, _deck.IndexOf(",")));
                _deck = _deck.Substring(_deck.IndexOf(",") + 1);
            }
            _deck = _deck.Substring(1);
        }
        switch (_characterType) {
            case 2://敵
                _master.GetComponent<HexMaster>()._hex[_pointX, _pointY] = 2;
                break;
            case 3://味方
                _master.GetComponent<HexMaster>()._hex[_pointX, _pointY] = 3;
                break;
            case 5://プレイヤー
                _level = PlayerStatus._revel;
                _hpMax = PlayerStatus._hpMax;
                _hp = PlayerStatus._hp;
                _attack = PlayerStatus._attack;
                _defense = PlayerStatus._defense;
                _deckNumber = PlayerStatus._deckNo;
                _master._card = new int[4,10];
                for (int i = 0; i < 4; i++) {
                    for (int j = 0; j < 10; j++) {
                        _master._card[i, j] = _cardData[i, j];
                    }
                }
                _master._card = _cardData;
                GetComponentInParent<HexMaster>()._hex[_pointX, _pointY] = 5;
                break;
            default:
                break;
        }
    }
    private void Update() {
        if (_count > 0) {
            if (_hp <= 0) {
                gameObject.SetActive(false);//HPが0以下なら無効化
            }
            switch (_characterType) {
                case 2://敵
                    Action();
                    //ターンエンド処理
                    break;
                case 3://味方
                    Action();
                    //ターンエンド処理
                    break;
                case 5://プレイヤー
                    //ターンエンド処理
                    break;
                default:
                    break;
            }
            if (_hp > _hpMax) {
                _hp = _hpMax;//HPが最大値を上回っているなら最大HPに補正
            }
            _count -= 1;
        }
    }
    private void Action() {
        bool restflag = true;
        for (int i = 0; i < 4; i++) {
            if (_cardData[i,3] == 0) {
                restflag = false;
            }
        }
        if (restflag) {
            Rest();
            return;
        } else {
            restflag = true;
        }
        foreach (GameObject scanData in GameObject.FindGameObjectsWithTag("Character")) {
            if (scanData.GetComponent<CharacterStat>()._characterType == _scanSubject) {
                for (int i = 0; i < 4; i++) {
                    if (_cardData[i, 3] == 0) {
                        switch (_cardData[i, 0]) {
                            case 1:
                                Spell(i);
                                break;
                            case 2:
                                if (Mathf.Abs(_pointX - scanData.GetComponent<CharacterStat>()._pointX) == 1
                                && Mathf.Abs(_pointY - scanData.GetComponent<CharacterStat>()._pointY) == 1) {
                                    _referenceObject = scanData;
                                    Spell(i);
                                    return;
                                } else if (Mathf.Abs(_pointX - scanData.GetComponent<CharacterStat>()._pointX) == 2
                                && Mathf.Abs(_pointY - scanData.GetComponent<CharacterStat>()._pointY) == 0) {
                                    _referenceObject = scanData;
                                    Spell(i);
                                    return;
                                }
                                break;
                            case 3:
                                if (Mathf.Abs(_pointX - scanData.GetComponent<CharacterStat>()._pointX) <= _cardData[i, 1]
                                && Mathf.Abs(_pointY - scanData.GetComponent<CharacterStat>()._pointY) <= _cardData[i, 1]
                                && Mathf.Abs(_pointX - scanData.GetComponent<CharacterStat>()._pointX) == Mathf.Abs(_pointY - scanData.GetComponent<CharacterStat>()._pointY)) {
                                    _referenceObject = scanData;
                                    Spell(i);
                                    return;
                                } else if (Mathf.Abs(_pointX - scanData.GetComponent<CharacterStat>()._pointX) <= (_cardData[i, 1] * 2)
                                && Mathf.Abs(_pointY - scanData.GetComponent<CharacterStat>()._pointY) == 0) {
                                    _referenceObject = scanData;
                                    Spell(i);
                                    return;
                                }
                                break;
                            case 4:
                                if (Mathf.Abs(_pointX - scanData.GetComponent<CharacterStat>()._pointX) == _cardData[i, 1]
                                 && Mathf.Abs(_pointY - scanData.GetComponent<CharacterStat>()._pointY) == _cardData[i, 1]) {
                                    _referenceObject = scanData;
                                    Spell(i);
                                    return;
                                } else if (Mathf.Abs(_pointX - scanData.GetComponent<CharacterStat>()._pointX) == (_cardData[i, 1] * 2)
                                && Mathf.Abs(_pointY - scanData.GetComponent<CharacterStat>()._pointY) == 0) {
                                    _referenceObject = scanData;
                                    Spell(i);
                                    return;
                                }
                                break;
                        }
                    }
                }
                if (Mathf.Abs(_pointX - scanData.GetComponent<CharacterStat>()._pointX) == 1
                && Mathf.Abs(_pointY - scanData.GetComponent<CharacterStat>()._pointY) == 1) {
                    _referenceObject = scanData;
                    Attack();
                    return;
                } else if (Mathf.Abs(_pointX - scanData.GetComponent<CharacterStat>()._pointX) == 2 
                && Mathf.Abs(_pointY - scanData.GetComponent<CharacterStat>()._pointY) == 0) {
                    _referenceObject = scanData;
                    Attack();
                    return;
                }
            }
        }
        _rand = Random.Range(1,7);
        Move(_rand);
    }
    private void Attack() {
        _referenceObject.GetComponent<CharacterStat>()._hp -= 10 + _attack - _referenceObject.GetComponent<CharacterStat>()._defense;
    }
    private void Spell(int cast) {
        switch (_cardData[cast, 0]) {
            case 1:
                _hp += _cardData[cast, 2];
                break;
            case 2:
                _referenceObject.GetComponent<CharacterStat>()._hp += _cardData[cast, 2] + _attack - _referenceObject.GetComponent<CharacterStat>()._defense;
                break;
            case 3:
                _referenceObject.GetComponent<CharacterStat>()._hp += _cardData[cast, 2] + _attack - _referenceObject.GetComponent<CharacterStat>()._defense;
                break;
            case 4:
                _referenceObject.GetComponent<CharacterStat>()._hp += _cardData[cast, 2] + _attack - _referenceObject.GetComponent<CharacterStat>()._defense;
                break;
        }
        _cardData[cast, 3] = 1;
    }
    private void Rest() {
        _hp += _hpMax / 10;
        for (int i = 0; i < 4; i++) {
            _cardData[i, 3] = 0;
        }
    }
    private void Move(int random) {
        switch (random) {
            case 1:
                //配列を超過する場合は処理をしない
                if (_pointX + 1 > 20 || _pointY + 1 > 10) {
                    break;
                } else if (_pointX + _pointY + 2 > 25) {
                    break;
                } else if (GetComponentInParent<HexMaster>()._hex[_pointX, _pointY] == 0) {
                    GetComponentInParent<HexMaster>()._hex[_pointX, _pointY] = 0;
                    _pointX += 1;
                    _pointY += 1;
                    GetComponentInParent<HexMaster>()._hex[_pointX, _pointY] = _characterType;
                    transform.position = new Vector2(1.28f * (_pointX - 10), 1.448f * (_pointY - 5));
                }
                break;
            case 2:
                //配列を超過する場合は処理をしない
                if (_pointX + 2 > 20) {
                    break;
                } else if (_pointX + _pointY + 2 > 25 || _pointX - _pointY == 15) {
                    break;
                } else if (GetComponentInParent<HexMaster>()._hex[_pointX + 2, _pointY] == 0) {
                    GetComponentInParent<HexMaster>()._hex[_pointX, _pointY] = 0;
                    _pointX += 2;
                    GetComponentInParent<HexMaster>()._hex[_pointX, _pointY] = _characterType;
                    transform.position = new Vector2(1.28f * (_pointX - 10), 1.448f * (_pointY - 5));
                }
                break;
            case 3:
                //配列を超過する場合は処理をしない
                if (_pointX + 1 > 20 || _pointY - 1 < 0) {
                    break;
                } else if (_pointX - _pointY == 15) {
                    break;
                } else if (GetComponentInParent<HexMaster>()._hex[_pointX + 1, _pointY - 1] == 0) {
                    GetComponentInParent<HexMaster>()._hex[_pointX, _pointY] = 0;
                    _pointX += 1;
                    _pointY -= 1;
                    GetComponentInParent<HexMaster>()._hex[_pointX, _pointY] = _characterType;
                    transform.position = new Vector2(1.28f * (_pointX - 10), 1.448f * (_pointY - 5));
                }
                break;
            case 4:
                //配列を超過する場合は処理をしない
                if (_pointX - 1 < 0 || _pointY - 1 < 0) {
                    break;
                } else if (_pointX + _pointY - 2 < 5) {
                    break;
                } else if (GetComponentInParent<HexMaster>()._hex[_pointX - 1, _pointY - 1] == 0) {
                    GetComponentInParent<HexMaster>()._hex[_pointX, _pointY] = 0;
                    _pointX -= 1;
                    _pointY -= 1;
                    GetComponentInParent<HexMaster>()._hex[_pointX, _pointY] = _characterType;
                    transform.position = new Vector2(1.28f * (_pointX - 10), 1.448f * (_pointY - 5));
                }
                break;
            case 5:
                //配列を超過する場合は処理をしない
                if (_pointX - 2 < 0) {
                    break;
                } else if (_pointX + _pointY - 2 < 5 || _pointY - _pointX == 5) {
                    break;
                } else if (GetComponentInParent<HexMaster>()._hex[_pointX - 2, _pointY] == 0) {
                    GetComponentInParent<HexMaster>()._hex[_pointX, _pointY] = 0;
                    _pointX -= 2;
                    GetComponentInParent<HexMaster>()._hex[_pointX, _pointY] = _characterType;
                    transform.position = new Vector2(1.28f * (_pointX - 10), 1.448f * (_pointY - 5));
                }
                break;
            case 6:
                //配列を超過する場合は処理をしない
                if (_pointX - 1 < 0 || _pointY + 1 > 10) {
                    break;
                } else if (_pointY - _pointX == 5) {
                    break;
                } else if (GetComponentInParent<HexMaster>()._hex[_pointX - 1, _pointY + 1] == 0) {
                    GetComponentInParent<HexMaster>()._hex[_pointX, _pointY] = 0;
                    _pointX -= 1;
                    _pointY += 1;
                    GetComponentInParent<HexMaster>()._hex[_pointX, _pointY] = 5;
                    transform.position = new Vector2(1.28f * (_pointX - 10), 1.448f * (_pointY - 5));
                }
                break;
            default:
                break;
        }
    }
}