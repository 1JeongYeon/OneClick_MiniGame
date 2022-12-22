using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterChoose : MonoBehaviour
{
    public GameObject[] _characters;
    public GameObject _image;
    public int _imageIndex = 0;
    public string characterName = "";

    private PlayerActionData actionData;

    public void OnClickLeft()
    {
        _imageIndex = ((0 == _imageIndex) ? _characters.Length : _imageIndex) - 1;
        characterName = _characters[_imageIndex].gameObject.GetComponent<PlayerActionData>().Name;
        OnClickAnnoying();
    }

    public void OnClickRight()
    {
        _imageIndex = ((_characters.Length - 1) == _imageIndex) ?  0 : (_imageIndex + 1);
        OnClickAnnoying();
    }

    public void OnClickAnnoying()
    {
        _image.GetComponent<Image>().sprite = _characters[_imageIndex].GetComponent<PlayerActionData>().Annoying;
    }

    public void OnClickAttack0()
    {
        _image.GetComponent<Image>().sprite = _characters[_imageIndex].GetComponent<PlayerActionData>().Attack0;
    }

    public void OnClickAttack1()
    {
        _image.GetComponent<Image>().sprite = _characters[_imageIndex].GetComponent<PlayerActionData>().Attack1;
    }

    public void OnClickAttack2()
    {
        _image.GetComponent<Image>().sprite = _characters[_imageIndex].GetComponent<PlayerActionData>().Attack2;
    }

    public void OnClickStand()
    {
        _image.GetComponent<Image>().sprite = _characters[_imageIndex].GetComponent<PlayerActionData>().Stand;
    }

    public void OnClickHit()
    {
        _image.GetComponent<Image>().sprite = _characters[_imageIndex].GetComponent<PlayerActionData>().Hit;
    }

    public void OnClickGroggy()
    {
        _image.GetComponent<Image>().sprite = _characters[_imageIndex].GetComponent<PlayerActionData>().Groggy;
    }

    public void OnClickWin()
    {
        _image.GetComponent<Image>().sprite = _characters[_imageIndex].GetComponent<PlayerActionData>().Win;
    }

    public void OnClickLose()
    {
        _image.GetComponent<Image>().sprite = _characters[_imageIndex].GetComponent<PlayerActionData>().Lose;
    }

    public void OnClickPortrait()
    {
        _image.GetComponent<Image>().sprite = _characters[_imageIndex].GetComponent<PlayerActionData>().Portrait;
    }

    public void OnClickPortraitLose()
    {
        _image.GetComponent<Image>().sprite = _characters[_imageIndex].GetComponent<PlayerActionData>().PortraitLose;
    }
    

    void Start()
    {   
        OnClickAnnoying();
    }

    void Update()
    {   
    }
}
