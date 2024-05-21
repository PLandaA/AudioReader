using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControllerTargetIndicator : MonoBehaviour {
    public Canvas canvas;

    public List<TargetIndicator> targetIndicators = new List<TargetIndicator>(); 


    public Camera MainCamera;

    public GameObject targetIndicatorPrefab;


    void Start () {


    }

    void Update () {
        if (targetIndicators.Count > 0) {
            for (int i = 0; i < targetIndicators.Count; i++) {
                targetIndicators [i].UpdateTargetIndicator();

            }

        }
    }

    public void AddTargetIndicator (GameObject target) {
        TargetIndicator indicator = GameObject.Instantiate(targetIndicatorPrefab, canvas.transform).GetComponent<TargetIndicator>();
        indicator.InitialiseTargetIndicator(target, MainCamera, canvas);
        targetIndicators.Add(indicator);
    }
    
   

    

    
}
