using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spellchanger : MonoBehaviour
{
    private string[] _allData;
    [SerializeField] private TextAsset _deckData;
    [SerializeField] private int _deckNumber;//ÉfÉbÉLî‘çÜ
    private string _deck;
    private int[,] _cardData;
    private int _squareInputArray;
    private int _serectType;

    [SerializeField] private IconContainer _container;
    private int _carsor = 1;
    private bool _interval = true;
    private int _inputType = 0;
    private float _time;
    void Start()
    {
        _allData = _deckData.text.Split('\n');
        for (int i = 0; i < _deckNumber; i++) {
            _deck = _allData[i];
        }
        _cardData = new int[4, 10];//ïœÇ¶ÇΩÇ¢Ç»Çü
        for (int i = 0; _deck.Length > 1; i++) {
            for (int j = 0; _deck.Substring(0, 1) != "/"; j++) {
                _cardData[i, j] = int.Parse(_deck.Substring(0, _deck.IndexOf(",")));
                _deck = _deck.Substring(_deck.IndexOf(",") + 1);
            }
            _deck = _deck.Substring(1);
        }
    }
    void Update()
    {
        _time += Time.deltaTime;
        print(_carsor);
        if (Input.GetAxisRaw("Vertical") < 0 && _inputType == 0) {
            if (_interval && _carsor <= 3) {
                _carsor++;
                _time = 0;
                _interval = false;
            }
        } else if (Input.GetAxisRaw("Vertical") > 0 && _inputType == 0) {
            if (_interval && _carsor >= 2) {
                _carsor--;
                _time = 0;
                _interval = false;
            }
        } else {
            _interval = true;
        }
        if (_time >= 0.5) {
            _interval = true;
        }
        if (Input.GetButtonDown("Submit") && _inputType == 0) {
            _inputType = _carsor;
            _carsor = 0;
        }
        if (_inputType != 0) {
        
        }
    }
}
