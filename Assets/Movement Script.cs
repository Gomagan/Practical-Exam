using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MovementScript : MonoBehaviour
{
    public GameObject cube;
    public Camera cam;
    
    public GameObject jumpPos1, jumpPos2;
    
    private Vector3 _target;
    private NavMeshAgent _agent;
    private Rigidbody _rb;
    private bool _isWalking =  false;
    private Animator _animator;
    
    public GameObject camPos1, camPos2;

    public GameObject weapon1;
    
    void Start()
    {
        cam.transform.position = camPos1.transform.position;
        _rb =  GetComponent<Rigidbody>();
        _agent =  GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _target = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //StartCoroutine(Jump());
            Jum();  
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                _target = hit.point;
            }
        }
        
        _agent.destination = transform.position;
        
        if (Vector3.Distance(_agent.destination, _target) < 0.3f)
        {
            _agent.destination = transform.position;
            _animator.SetBool("Walk", false);
        }
        else
        {
            _animator.SetBool("Walk", true);
            _agent.destination = _target;
        }
    }

    private void Jum()
    {
        cam.transform.position = camPos2.transform.position;
        _target =  jumpPos2.transform.position;
        transform.position = Vector3.Lerp(transform.position, jumpPos2.transform.position, 1f);
    }
    
    private IEnumerator Jump()
    {
        transform.position = Vector3.Lerp(transform.position, jumpPos1.transform.position, 1f);
        yield return new WaitForSeconds(1f);
        transform.position = Vector3.Lerp(transform.position, jumpPos2.transform.position, 1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            Destroy(other.gameObject);
            weapon1.SetActive(true);
        }
    }
}
