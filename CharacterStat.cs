using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : MonoBehaviour 
{
    [SerializeField] private HexMaster _master;
    [SerializeField] private int _level;//���x��
    [SerializeField] public int _hpMax;//�ő�̗�
    [SerializeField] public int _hp;//�̗�
    [SerializeField] private int _deckNumber;//�f�b�L�ԍ�
    [SerializeField] public int _attack;//�U����
    [SerializeField] public int _defense;//�h���
    [SerializeField] TextAsset _deckData;
    public int[,] _cardData;
    private string[] _allData;
    private string _deck;
    public int _characterType;//�L�����N�^�[�̃^�C�v
    public int _pointX;//X���W
    public int _pointY;//Y���W
    public int _count;//�s���񐔃X�g�b�N
    private void Start() {
        transform.position = new Vector2((_pointX - 10) * 1.28f, (_pointY - 5) * 1.448f);
        _allData = _deckData.text.Split('\n');
        for (int i = 0; i < _deckNumber; i++) {
            _deck = _allData[i];
        }
        _cardData = new int[4, 10];//�ς������Ȃ�
        for (int i = 0; _deck.Length > 1; i++) {
            for (int j = 0; _deck.Substring(0, 1) != "/"; j++) {
                _cardData[i, j] = int.Parse(_deck.Substring(0, _deck.IndexOf(",")));
                _deck = _deck.Substring(_deck.IndexOf(",") + 1);
            }
            _deck = _deck.Substring(1);
        }
        switch (_characterType) {
            case 2://�G
                GetComponentInParent<HexMaster>()._hex[_pointX, _pointY] = 2;
                break;
            case 3://����
                GetComponentInParent<HexMaster>()._hex[_pointX, _pointY] = 3;
                break;
            case 5://�v���C���[
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
                gameObject.SetActive(false);//HP��0�ȉ��Ȃ疳����
            } else if (_hp > _hpMax) {
                _hp = _hpMax;//HP���ő�l�������Ă���Ȃ�ő�HP�ɕ␳
            }
            switch (_characterType) {
                case 2://�G
                    //�s��
                    //�^�[���G���h����
                    break;
                case 3://����
                    //�s��
                    //�^�[���G���h����
                    break;
                case 5://�v���C���[
                    //�^�[���G���h����
                    break;
                default:
                    break;
            }
            _count -= 1;
        }
    }
}