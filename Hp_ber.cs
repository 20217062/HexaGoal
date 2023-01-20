using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hp_ber : MonoBehaviour
{
    [SerializeField] private CharacterStat _player;
    [SerializeField] private HexMaster _master;
    [SerializeField] private Image _spellOne;
    [SerializeField] private Image _spellTwo;
    [SerializeField] private Image _spellThree;
    [SerializeField] private Image _spellFour;
    [SerializeField] private GameObject _iconOne;
    [SerializeField] private GameObject _iconTwo;
    [SerializeField] private GameObject _iconThree;
    [SerializeField] private GameObject _iconFour;
    private float _ratio;
    private void Start() {
        _spellOne.sprite = _iconOne.GetComponent<SpriteRenderer>().sprite;
        _spellTwo.sprite = _iconTwo.GetComponent<SpriteRenderer>().sprite;
        _spellThree.sprite = _iconThree.GetComponent<SpriteRenderer>().sprite;
        _spellFour.sprite = _iconFour.GetComponent<SpriteRenderer>().sprite;
    }
    void Update()
    {
        _ratio = _player._hp / _player._hpMax;
        this.GetComponent<Image>().fillAmount = _ratio;
        if (_ratio <= 0.2) {
            this.GetComponent<Image>().color = new Color(255, 0, 0, 255);
        } else if (_ratio <= 0.5) {
            this.GetComponent<Image>().color = new Color(255, 255, 0, 255);
        } else {
            this.GetComponent<Image>().color = new Color(0, 255, 0, 255);
        }
        if (_master._card[0, 3] == 1) {
            _spellOne.color = new Color32(100, 100, 100, 100);
        } else {
            _spellOne.color = new Color32(255, 255, 255, 255);
        }
        if (_master._card[1, 3] == 1) {
            _spellTwo.color = new Color32(100, 100, 100, 100);
        } else {
            _spellTwo.color = new Color32(255, 255, 255, 255);
        }
        if (_master._card[2, 3] == 1) {
            _spellThree.color = new Color32(100, 100, 100, 100);
        } else {
            _spellThree.color = new Color32(255,255,255,255);
        }
        if (_master._card[3, 3] == 1) {
            _spellFour.color = new Color32(100, 100, 100, 100);
        } else {
            _spellFour.color = new Color32(255, 255, 255, 255);
        }
    }
}
