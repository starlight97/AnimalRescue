using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    public FloatingJoystick floatingJoystick;
    private Rigidbody rBody;
    private Coroutine moveRoutine;
    private GameObject modelGo;

    public Vector3 dir;
    public UnityAction onMove;
    public UnityAction onMoveComplete;

    public void Init(float moveSpeed)
    {
        this.rBody = GetComponent<Rigidbody>();
        this.modelGo = transform.Find("model").gameObject;
        Move(moveSpeed);
    }

    public void Move(float moveSpeed)
    {
        if (this.moveRoutine != null)
        {
            this.StopCoroutine(this.moveRoutine);
        }

        this.moveRoutine = StartCoroutine(this.MoveRoutine(moveSpeed));
    }

    private IEnumerator MoveRoutine(float moveSpeed)
    {
        while (true)
        {
            this.LimitPosition();
            this.dir = Vector3.forward * floatingJoystick.Vertical + Vector3.right * floatingJoystick.Horizontal;
            if (this.dir != Vector3.zero)
            {
                this.modelGo.transform.rotation = Quaternion.LookRotation(this.dir);
                this.transform.Translate(this.dir.normalized * moveSpeed * Time.deltaTime);
                //this.onMove();
            }

            yield return null;
        }
    }

    private void LimitPosition()
    {
        var posX = this.transform.position.x;
        var posZ = this.transform.position.z;

        #region 뚕
        if (posX >= 73)
        {
            posX = 73;
            this.transform.position = new Vector3(posX, 0, this.transform.position.z);
        }
        else if (posX <= -73)
        {
            posX = -73;
            this.transform.position = new Vector3(posX, 0, this.transform.position.z);
        }

        if (posZ >= 73)
        {
            posZ = 73;
            this.transform.position = new Vector3(this.transform.position.x, 0, posZ);
        }
        else if (posZ <= -73)
        {
            posZ = -73;
            this.transform.position = new Vector3(this.transform.position.x, 0, posZ);
        }

        #endregion
    }
}
