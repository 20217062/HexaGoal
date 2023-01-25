using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hp_ber : MonoBehaviour {
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
    [SerializeField] private Text _actionText;
    private int _squareInputArray;
    private float _ratio;
    private void Start() {
        _spellOne.sprite = _iconOne.GetComponent<SpriteRenderer>().sprite;
        _spellTwo.sprite = _iconTwo.GetComponent<SpriteRenderer>().sprite;
        _spellThree.sprite = _iconThree.GetComponent<SpriteRenderer>().sprite;
        _spellFour.sprite = _iconFour.GetComponent<SpriteRenderer>().sprite;
    }
    void Update() {
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
        } else {
            _squareInputArray = 0;
        }
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
            _spellThree.color = new Color32(255, 255, 255, 255);
        }
        if (_master._card[3, 3] == 1) {
            _spellFour.color = new Color32(100, 100, 100, 100);
        } else {
            _spellFour.color = new Color32(255, 255, 255, 255);
        }
        switch (_master._inputType) {
            case 0:
                switch (_squareInputArray) {
                    case 1:
                        _actionText.text = "Attack";
                        break;
                    case 2:
                        _actionText.text = "Spell";
                        break;
                    case 3:
                        _actionText.text = "Rest";
                        break;
                    case 4:
                        _actionText.text = "Move";
                        break;
                    default:
                        _actionText.text = "Action?";
                        break;
                }
                break;
            case 1:
                _actionText.text = "Attack";
                break;
            case 2:
                _actionText.text = "Spell " + _squareInputArray.ToString();
                break;
            case 3:
                _actionText.text = "Rest";
                break;
            case 4:
                _actionText.text = "Move";
                break;
            case 5:
                _actionText.text = "Spell " + (_master._cast + 1).ToString();
                break;
            default:
                _actionText.text = "Action?";
                break;
        }
    }
}
