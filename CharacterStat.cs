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
    public int[,] _cardData;
    private string[] _allData;
    private string _deck;
    public int _characterType;//キャラクターのタイプ
    public int _pointX;//X座標
    public int _pointY;//Y座標
    public int _count;//行動回数ストック
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
                GetComponentInParent<HexMaster>()._hex[_pointX, _pointY] = 2;
                break;
            case 3://味方
                GetComponentInParent<HexMaster>()._hex[_pointX, _pointY] = 3;
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
        //_card = _player.GetComponent<CharacterStat>()._cardData;
    }
    private void Update() {
        if (_count > 0) {
            if (_hp <= 0) {
                gameObject.SetActive(false);//HPが0以下なら無効化
            } else if (_hp > _hpMax) {
                _hp = _hpMax;//HPが最大値を上回っているなら最大HPに補正
            }
            switch (_characterType) {
                case 2://敵
                    //行動
                    //ターンエンド処理
                    break;
                case 3://味方
                    //行動
                    //ターンエンド処理
                    break;
                case 5://プレイヤー
                    //ターンエンド処理
                    break;
                default:
                    break;
            }
            _count -= 1;
        }
    }
}