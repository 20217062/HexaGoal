using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconControler : MonoBehaviour
{
    [SerializeField] private HexMaster _master;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _attack;
    [SerializeField] private GameObject _spell;
    [SerializeField] private GameObject _rest;
    [SerializeField] private GameObject _move;
    [SerializeField] private GameObject _point;
    void Update()
    {
        transform.position = _player.transform.position;
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) {
            if (_master._inputType == 0) {
                _point.SetActive(false);
                float instantInput = Mathf.Atan2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * -180 / Mathf.PI;
                if (Mathf.Abs(instantInput) < 45) {
                    _attack.SetActive(true);
                    _spell.SetActive(false);
                    _rest.SetActive(false);
                    _move.SetActive(false);
                } else if (instantInput >= -135 && instantInput < -45) {
                    _attack.SetActive(false);
                    _spell.SetActive(true);
                    _rest.SetActive(false);
                    _move.SetActive(false);
                } else if (Mathf.Abs(instantInput) >= 135) {
                    _attack.SetActive(false);
                    _spell.SetActive(false);
                    _rest.SetActive(true);
                    _move.SetActive(false);
                } else if (instantInput > 45 && instantInput <= 135) {
                    _attack.SetActive(false);
                    _spell.SetActive(false);
                    _rest.SetActive(false);
                    _move.SetActive(true);
                } else {
                    _attack.SetActive(false);
                    _spell.SetActive(false);
                    _rest.SetActive(false);
                    _move.SetActive(false);
                }
            } else if (_master._inputType == 1 || _master._inputType == 4 || _master._inputType == 5) {
                if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) {
                    _point.SetActive(true);
                    float instantInput = Mathf.Atan2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * -180 / Mathf.PI;
                    if (instantInput >= -60 && instantInput < 0) {
                        _point.transform.localPosition = new Vector2(1.22f,1.448f + 0.4f);
                    } else if (instantInput >= -120 && instantInput < -60) {
                        _point.transform.localPosition = new Vector2(2.44f,0.4f);
                    } else if (instantInput >= -180 && instantInput < -120) {
                        _point.transform.localPosition = new Vector2(1.22f, -1.448f + 0.4f);
                    } else if (instantInput >= 120 && instantInput < 180) {
                        _point.transform.localPosition = new Vector2(-1.22f, -1.448f + 0.4f);
                    } else if (instantInput >= 60 && instantInput < 120) {
                        _point.transform.localPosition = new Vector2(-2.44f,0.4f);
                    } else if (instantInput >= 0 && instantInput < 60) {
                        _point.transform.localPosition = new Vector2(-1.22f, 1.448f + 0.4f);
                    }
                } else {
                    _point.SetActive(false);
                }
            }
        }
    }
}
