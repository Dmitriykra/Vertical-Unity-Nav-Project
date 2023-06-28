using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetections : MonoBehaviour
{
    //создаю событие
    public static event OnSwipeInput SwipeEvent;
    //создаем  делегат для метода который вызовет событие. 
    public delegate void OnSwipeInput(Vector2 direction);
    //начальное касание
    Vector2 startTap;
    //дельта расстояния
    Vector2 delataTap;
    float deadZone = 80f; 
    bool isSwiping;
    bool isMobile;
    // Start is called before the first frame update
    void Start()
    {
        //определяем это смартфон или нет
        isMobile = Application.isMobilePlatform;
    }

    // Update is called once per frame
    void Update()
    {
        //if use PC
        if(!isMobile)
        {
            GetTouchOnPc();
        } 
        //if use mobile device
        else
        {
            GetTouchOnMobile();
        }

        ChekSwipe();
    }

    void GetTouchOnPc()
    {
        if(Input.GetMouseButtonDown(0))
        {
            isSwiping = true;
            startTap = Input.mousePosition;
        }

        //при отжатии мыши сбрасываем параметры свайпа.
        else if(Input.GetMouseButtonUp(0))
        {
            ResetSwipe();
        }
    }

    private void GetTouchOnMobile()
    {
         if(Input.touchCount > 0)
        {
            //если фаза касания - ее начало то устанавливаем в переменную startTap
            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                isSwiping = true;

                startTap = Input.GetTouch(0).position;
            }
            //если конец или отмена то сбрасываем
            else if(Input.GetTouch(0).phase == TouchPhase.Canceled || 
                Input.GetTouch(0).phase == TouchPhase.Ended) 
            {
                ResetSwipe();
            }   
        }
    }

    private void ChekSwipe()
    {
        //сначала обнуляем дельту
        delataTap = Vector2.zero;

        //если свайп произошел
        if(isSwiping)
        {   
            //если пк и свайп мышью
            if(!isMobile && Input.GetMouseButton(0))
            {
                //устанавливаем в дельту значение последнего касания мыши - стратовую позицию.
                delataTap = (Vector2)Input.mousePosition - startTap;
            }

            //если смартфон то проверяем наличия касания по экрану
            else if (Input.touchCount > 0)
            {
                //берем конечную позицию - начальну/
                delataTap = Input.GetTouch(0).position - startTap;
            }
        }

        //вектор2.magnitude возвращает длинну вектора (только для чтения)
        if(delataTap.magnitude > deadZone)
        {
            //если событие не нулевое
            if(SwipeEvent != null)
            {
                //проверяем 
                if(Mathf.Abs(delataTap.x) > Mathf.Abs(delataTap.y))
                {
                    SwipeEvent(delataTap.x > 0 ? Vector2.right : Vector2.left);
                }
                else
                {
                    SwipeEvent(delataTap.y > 0 ? Vector2.up : Vector2.down);
                }

                ResetSwipe();
            }
        }
    }

    //онуляем переменные
    private void ResetSwipe()
    {
        isSwiping = false;
        startTap = Vector2.zero;
        delataTap = Vector2.zero;
    }
}
