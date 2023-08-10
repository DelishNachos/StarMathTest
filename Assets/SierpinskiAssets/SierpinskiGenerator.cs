using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SierpinskiGenerator : MonoBehaviour
{

    [SerializeField] private float width = 10;
    [SerializeField] private float height = 10;
    [SerializeField] private int sides = 6;
    private int futureSlides;
    [SerializeField] private float distance = .66f;
    [SerializeField] private GameObject point;
    [SerializeField] private GameObject pointsParent;

    private GameObject[] startingPoints;
    private GameObject previousPoint;

    [Header("UI References")]
    [SerializeField] private Slider distanceSlider;
    [SerializeField] private TextMeshProUGUI distanceText;
    [Space(5)]
    [SerializeField] private Slider sidesSlider;
    [SerializeField] private TextMeshProUGUI sidesText;
    [Space(5)]
    [SerializeField] private Toggle scaleDistanceToggle;

    // Start is called before the first frame update
    void Start()
    {
        initializePoints(width, height, sides);
        initializeSlider(distanceSlider, distance);
        initializeSlider(sidesSlider, sides);
        changeDistanceSlider();
        changeSidesSlider();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            addNewPoint();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            addMultiplePoints(100);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            addMultiplePoints(5000);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            resetPoints();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            SceneManager.LoadScene(0);
        }

        //Get UI Values
        if (!scaleDistanceToggle.isOn)
        {
            distance = getSliderValue(distanceSlider);
        }           
        else
        {
            distance = calculateDistance(futureSlides);
            setSliderValue(distanceSlider, distance);
            changeDistanceSlider();
        }

        futureSlides = (int)getSliderValue(sidesSlider);
    }

    private void initializePoints(float width, float height, int sides)
    {
        startingPoints = new GameObject[sides];
        pointsParent = new GameObject("Points");
        /*startingPoints[0] = Instantiate(point, new Vector3(-width / 2, 0          , 0), Quaternion.identity, pointsParent.transform);
        startingPoints[1] = Instantiate(point, new Vector3(-width / 4, height / 2 , 0), Quaternion.identity, pointsParent.transform);
        startingPoints[2] = Instantiate(point, new Vector3(width / 4 , height / 2 , 0), Quaternion.identity, pointsParent.transform);
        startingPoints[3] = Instantiate(point, new Vector3(width / 2 , 0          , 0), Quaternion.identity, pointsParent.transform);
        startingPoints[4] = Instantiate(point, new Vector3(width / 4 , -height / 2, 0), Quaternion.identity, pointsParent.transform);
        startingPoints[5] = Instantiate(point, new Vector3(-width / 4, -height / 2, 0), Quaternion.identity, pointsParent.transform);*/
        for (int i = 0; i < sides; i++)
        {
            float angle = 2 * Mathf.PI / sides;
            float radius = width / 2;
            Vector3 position = new Vector3(radius * Mathf.Sin(i * angle), radius * Mathf.Cos(i * angle), 0);
            startingPoints[i] = Instantiate(point, position, Quaternion.identity, pointsParent.transform);
        }
        previousPoint = startingPoints[0];
    }

    private void addNewPoint()
    {
        Vector3 newPointPos;
        int nextNum = Random.Range(0, sides);
        newPointPos = Vector3.Lerp(previousPoint.transform.position, startingPoints[nextNum].transform.position, distance);
        previousPoint = Instantiate(point,  newPointPos, Quaternion.identity, pointsParent.transform);
    }

    private void addMultiplePoints(int amt)
    {
        for (int i = 0; i < amt; i++)
        {
            addNewPoint();
        }
    }

    private void resetPoints()
    {
        Destroy(pointsParent);
        sides = (int)getSliderValue(sidesSlider);
        initializePoints(width, height, sides);
    }

    private void initializeSlider(Slider slider, float var)
    {
        slider.value = var;
    }

    private void intializeInputField(TMP_InputField input, int var)
    {
        input.text = var.ToString();
    }
    
    private float getSliderValue(Slider slider)
    {
        return slider.value;
    }
    
    private void setSliderValue(Slider slider, float value)
    {
        slider.value = value;
    }

    private int getInputFieldValue(TMP_InputField input)
    {
        return int.Parse(input.text);
    }

    public void changeDistanceSlider()
    {
        distanceText.text = "Distance: " + getSliderValue(distanceSlider).ToString("F2");
        /*if (distanceSlider.value > .6f && distanceSlider.value < .7f)
        {
            distanceSlider.value = .66f;
        } else if (distanceSlider.value > .3f && distanceSlider.value < .4f)
        {
            distanceSlider.value = .33f;
        }*/
    }

    public void changeSidesSlider()
    {
        sidesSlider.value = Mathf.Round(sidesSlider.value);
        sidesText.text = "Sides: " + getSliderValue(sidesSlider);
    }

    private float calculateDistance(int sides)
    {
        float answer = 0;
        for (int i = 1; i <= (sides / 4); ++i)
        {
            answer += Mathf.Cos((2 * i * Mathf.PI) / sides);               
        }
        answer += 1;
        answer *= 2;

        answer = 1 - (1 / answer);
        return answer;
    }
}
