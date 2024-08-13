using DG.Tweening;
using DG.Tweening.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerManager : MonoBehaviour
{

    public DOTweenAnimation positionAnimation;
    public DOTweenAnimation rotationAnimation;
    public DOTweenAnimation moneybox;
    public float rotateDuration = 1.0f;
    public float moveDuration = 1.0f;
    //cardpaycamera
    
   
    public bool toog;



    // Method to move the GameObject to a specified position
    public void MoveToPositionAndRotation()
    {
        if (toog)
        {
            positionAnimation.transform.DOMove(new Vector3(20.478f, 2.9f, 1.428f), moveDuration);
            rotationAnimation.transform.DORotate(new Vector3(42.39f, 180f, 0f), 1.0f);
            moneybox.transform.DOMove(new Vector3 (20.376f, 1.484f, -0.034f), moveDuration);
            toog = false;
        }
        else {
            positionAnimation.transform.DOMove(new Vector3(18.324f, 2.813f, 2.165f), moveDuration);
            rotationAnimation.transform.DORotate(new Vector3(12.922f, 120.791f, 0.158f), 1.0f);
            moneybox.transform.DOMove(new Vector3(20.376f, 1.484f, -0.794f), moveDuration);
            toog = true;



        }

    }
    public void cardpay()
    {
        if (toog)
        {

            positionAnimation.transform.DOMove(new Vector3(21.435f, 2.42f, 0.448f), moveDuration);
            rotationAnimation.transform.DORotate(new Vector3(11.485f, 84.845f, -1.565f), 1.0f);
            toog = false;
        }
        else
        {
            positionAnimation.transform.DOMove(new Vector3(18.324f, 2.813f, 2.165f), moveDuration);
            rotationAnimation.transform.DORotate(new Vector3(12.922f, 120.791f, 0.158f), 1.0f);
            toog = true;
        }


        }
    
}
