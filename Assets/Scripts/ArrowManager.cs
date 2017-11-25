using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableArrowsbyDistance : MonoBehaviour
{

    public GameObject route1;
    public GameObject route2;
    public GameObject route3;

    OnHit[] route1Children;
    OnHit[] route2Children;
    OnHit[] route3Children;

    public int _selectedRoute = 0;



    // Use this for initialization
    void Start()
    {
        route1Children = route1.GetComponentsInChildren<OnHit>();
        route2Children = route2.GetComponentsInChildren<OnHit>();
        route3Children = route3.GetComponentsInChildren<OnHit>();



    }

    // Update is called once per frame

    void Update()
    {

    }

    public void SetActiveRoute(int selectedRoute)

    {
        _selectedRoute = selectedRoute;
        if (selectedRoute == 0)
        {
            for (int i = 0; i < route1Children.Length; i++)
            {
                route1Children[i].transform.parent.gameObject.SetActive(false);
            }

            for (int i = 0; i < route2Children.Length; i++)
            {
                route2Children[i].transform.parent.gameObject.SetActive(false);
            }

            for (int i = 0; i < route3Children.Length; i++)
            {
                route3Children[i].transform.parent.gameObject.SetActive(false);
            }
        }


        if (selectedRoute == 1)
        {
            for (int i = 0; i < route1Children.Length; i++)
            {
                route1Children[i].transform.parent.gameObject.SetActive(true);
            }

            for (int i = 0; i < route2Children.Length; i++)
            {
                route2Children[i].transform.parent.gameObject.SetActive(false);
            }

            for (int i = 0; i < route3Children.Length; i++)
            {
                route3Children[i].transform.parent.gameObject.SetActive(false);
            }
        }

        if (selectedRoute == 2)
        {
            for (int i = 0; i < route2Children.Length; i++)
            {
                route2Children[i].transform.parent.gameObject.SetActive(true);

            }
            for (int i = 0; i < route1Children.Length; i++)
            {
                route1Children[i].transform.parent.gameObject.SetActive(false);
            }

            for (int i = 0; i < route3Children.Length; i++)
            {
                route3Children[i].transform.parent.gameObject.SetActive(false);
            }
        }

        if (selectedRoute == 3)
        {
            for (int i = 0; i < route3Children.Length; i++)
            {
                route3Children[i].transform.parent.gameObject.SetActive(true);


            }

            for (int i = 0; i < route1Children.Length; i++)
            {
                route1Children[i].transform.parent.gameObject.SetActive(false);
            }

            for (int i = 0; i < route2Children.Length; i++)
            {
                route2Children[i].transform.parent.gameObject.SetActive(false);
            }
        }
    }
}
