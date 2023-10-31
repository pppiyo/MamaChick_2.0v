using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private LineRenderer _beam;
    [SerializeField] private Transform _muzzlePoint;
    [SerializeField] private float _maxLength;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        _beam.enabled = false;
    }

    private void Activate()
    {
        _beam.enabled = true;
    }

    private void Deactivate()
    {
        _beam.enabled = false;
        _beam.SetPosition(0, _muzzlePoint.position);
        _beam.SetPosition(1, _muzzlePoint.position);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) Activate();
        else if (Input.GetKeyUp(KeyCode.F)) Deactivate();
    }

    private void FixedUpdate()
    {
        //if (!_beam.enbaled) return;

        Ray ray = new Ray(_muzzlePoint.position, _muzzlePoint.forward);
        bool cast = Physics.Raycast(ray, out RaycastHit hit, _maxLength);
        Vector3 hitPosition = cast ? hit.point : _muzzlePoint.position + _muzzlePoint.forward * _maxLength;

        _beam.SetPosition(0, _muzzlePoint.position);
        _beam.SetPosition(1, hitPosition);
    }

}
