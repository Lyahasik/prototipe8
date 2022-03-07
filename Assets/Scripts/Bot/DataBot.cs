using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBot : MonoBehaviour
{
    public GameObject[] PrefabsCharacter;

    private Player _componentPlayer;
    private Move _componentMove;
    private AttackBot _componentAttack;

    private GameObject _prefabCharacter;
    private ICharacter _componentICharacter;
    private Animator _animatorCharacter;

    void Start()
    {

        _componentPlayer = gameObject.GetComponent<Player>();
        _componentAttack = gameObject.GetComponent<AttackBot>();
        _componentMove = gameObject.GetComponent<Move>();

        _componentPlayer.SetComponentMove(_componentMove);
        _componentAttack.SetComponentPlayer(_componentPlayer);
        _componentAttack.SetComponentMove(_componentMove);

        SetObjectCharacter();
        UpdateAnimators();
        UpdateComponentICharacter();
    }

    private void SetObjectCharacter()
    {
        _prefabCharacter = PrefabsCharacter[0];
    }

    public GameObject GetObjectCharacter()
    {
        return _prefabCharacter;
    }

    private void UpdateAnimators()
    {
        _animatorCharacter = _prefabCharacter.GetComponent<Animator>();
        _componentMove.SetAnimatorCharacter(_animatorCharacter);
        _componentPlayer.SetAnimatorCharacter(_animatorCharacter);
        _componentAttack.SetAnimatorCharacter(_animatorCharacter);
    }

    private void UpdateComponentICharacter()
    {
        _componentICharacter = _prefabCharacter.GetComponent<ICharacter>();
        _componentAttack.SetICharacter(_componentICharacter);
    }

    public Animator GetAnimatorCharacter()
    {
        return _animatorCharacter;
    }
}
