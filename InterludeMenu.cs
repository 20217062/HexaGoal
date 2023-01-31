using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InterludeMenu : MonoBehaviour
{
    private int _squareInputArray;
    [SerializeField] private GameObject _next;
    [SerializeField] private GameObject _grouth;
    [SerializeField] private GameObject _quit;
    [SerializeField] private GameObject _status;
    [SerializeField] private GameObject _arrow;
    [SerializeField] private Text _lv;
    [SerializeField] private Text _at;
    [SerializeField] private Text _df;
    [SerializeField] private Text _hp;
    [SerializeField] private Slider _hpBer;
    private bool _statusFlag;
    [SerializeField] private GameObject _statusUI;
    [SerializeField] private Text _text;
    private void Start() {
        _lv.text = PlayerStatus._revel.ToString();
        _at.text = PlayerStatus._attack.ToString();
        _df.text = PlayerStatus._defense.ToString();
        _hp.text = PlayerStatus._hp.ToString();
    }
    void Update() {
        _hpBer.value = PlayerStatus._hp / PlayerStatus._hpMax;
        if (_statusFlag) {
            _statusUI.transform.position = Vector2.MoveTowards(_statusUI.transform.position, new Vector2(300, 540), 10);
        } else {
            _statusUI.transform.position = Vector2.MoveTowards(_statusUI.transform.position, new Vector2(-300, 540), 10);
        }
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) {
            float instantInput = Mathf.Atan2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * -180 / Mathf.PI;
            if (Mathf.Abs(instantInput) < 45) {
                _squareInputArray = 1;
                _text.text = "Next Battle";
            } else if (instantInput >= -135 && instantInput < -45) {
                _squareInputArray = 2;
                _text.text = "Unimplemented";
            } else if (Mathf.Abs(instantInput) >= 135) {
                _squareInputArray = 3;
                _text.text = "Quit Game";
            } else if (instantInput > 45 && instantInput <= 135) {
                _squareInputArray = 4;
                _text.text = "Open Status";
            }
        } else {
            _squareInputArray = 0;
            _text.text = "Command?";
        }
        if (Input.GetButtonDown("Submit")) {
            switch (_squareInputArray) {
                case 1:
                    SceneManager.LoadSceneAsync("Battle");
                    break;
                case 2:

                    break;
                case 3:
                    SceneManager.LoadSceneAsync("GameOver");
                    break;
                case 4:
                    if (_statusFlag) {
                        _statusFlag = false;
                    } else {
                        _statusFlag = true;
                    }
                    break;
                default:
                    break;
            }
        }
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) {
            _arrow.SetActive(true);
            float instantInput = Mathf.Atan2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * -180 / Mathf.PI;
            _arrow.transform.rotation = Quaternion.Euler(0f, 0f, instantInput);
        } else {
            _arrow.SetActive(false);
        }
    }
}