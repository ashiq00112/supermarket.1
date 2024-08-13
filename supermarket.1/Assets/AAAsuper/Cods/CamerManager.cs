using DG.Tweening;
using DG.Tweening.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerManager : MonoBehaviour
{

    public DOTweenAnimation positionAnimation;
    public DOTweenAnimation rotationAnimation;
 
    public float rotateDuration = 1.0f;
    public Vector3 Registerposition;
    public Vector3 Registerrotation;
    public float moveDuration = 1.0f;
    public Vector3 cammainposiiton;
    public Vector3 cammainrotation;


    public bool toog;



    // Method to move the GameObject to a specified position
    public void MoveToPositionAndRotation()
    {
        if (toog)
        {
            positionAnimation.transform.DOMove(Registerposition, moveDuration);
            rotationAnimation.transform.DORotate(Registerrotation, 1.0f);
            toog = false;
        }
        else {
            positionAnimation.transform.DOMove(cammainposiiton, moveDuration);
            rotationAnimation.transform.DORotate(cammainrotation, 1.0f);
            toog = true;

        }

    }
   

}
