using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerMotor))]
public class PlayerControlling : MonoBehaviour
{

    public Interactable focus;
    public LayerMask movementMask;
    Camera cam;
    PlayerMotor motor;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray,out hit,100,movementMask)){

                //Debug.Log("We hit " + hit.collider.name + " "+hit.point);
                //移動player到滑鼠左鍵點擊處
                motor.MoveToPoint(hit.point);
                //取消移動行動
                RemoveFocus();
            }
        }
        if(Input.GetMouseButtonDown(1)){
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray,out hit,100,movementMask)){

                //check if we hit an interactable
                //Debug.Log("We hit " + hit.collider.name + " " + hit.point);
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                //if we did set it as our focus
                Debug.Log("interactable is null: "+ (interactable==null));
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }
    }
    void SetFocus(Interactable newFoucs)
    {
        if(newFoucs != focus)
        {
            if(focus != null)
                focus.DeFocused();
            focus = newFoucs;
            motor.FollowTarget(newFoucs);
        }
        newFoucs.OnFocused(transform);
    }
    void RemoveFocus()
    {
        if(focus != null)
            focus.DeFocused();
        focus = null;
        motor.StopFollowingTarget();
    }
}
