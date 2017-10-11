using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameLogic : MonoBehaviour
{

    private static gameLogic _gameLogic;
    public static gameLogic Instance
    {
        get
        {
            return _gameLogic;
        }
    }

    public food Food;
    public water Water;
    //public Text foodText;
    //public Text waterText;
    public Text powerText;
    public Text scrapText;

    public sceneController sc;

    //public float FoodProduceStep = 0.1f;
    //public int FoodProduce = 1;
    //public int FoodProduced = 0;
    //private float foodProduceProcess;

    public float WaterProduceStep = 0.1f;
    public int WaterProduce = 1;
    public int WaterProduced = 0;
    private float waterProduceProcess;

    public float PowerProduceInterval = 1.0f;
    public int PowerProduce = 15;
    public float PowerDecayRate = 0.01f;
    private float powerProduceTime = 0f;
    public int PowerHealthMax;
    private int powerHealth = 100;
    public int PowerHealth
    {
        set { powerHealth = value; powerHealth = Mathf.Max(powerHealth, 0); powerHealth = Mathf.Min(PowerHealthMax, powerHealth); OnPowerHPChange(PowerHealth); }
        get { return powerHealth; }
    }

    public float PowerComsumeInterval = 1.0f;
    public int PowerComsume = 10;
    private float powerComsumeTime = 0f;

    public int RepairCost = 5;
    public int RepairHealth = 10;

    private int food;
    private int water;
    private int power;
    private int scrap;


    //public int Food
    //{
    //    set { food = value >= 0 ? value : 0; foodText.text = food.ToString(); }
    //    get { return food; }
    //}

    

    public int Power
    {
        set { power = value >= 0 ? value : 0; powerText.text = power.ToString(); }
        get { return power; }
    }

    public int Scrap
    {
        set { scrap = value >= 0 ? value : 0; scrapText.text = scrap.ToString(); }
        get { return scrap; }
    }

    void Awake()
    {
        _gameLogic = this;
    }

    // Use this for initialization
    void Start()
    {
        PowerHealthMax = PowerHealth;
        Scrap = 100;
    }

    // Update is called once per frame
    void Update()
    {
        powerProduceTime += Time.deltaTime;
        if (powerProduceTime >= PowerProduceInterval)
        {
            powerProduceTime -= PowerProduceInterval;
            Power += Mathf.CeilToInt(((float)PowerHealth) / PowerHealthMax * PowerProduce);
            PowerHealth -= Mathf.CeilToInt(PowerDecayRate * PowerHealthMax);
        }

        powerComsumeTime += Time.deltaTime;
        if (powerComsumeTime >= PowerComsumeInterval)
        {
            powerComsumeTime -= PowerComsumeInterval;
            if (Power >= PowerComsume)
            {
                Power -= PowerComsume;
                sc.IsLight = true;
                Food.Produce();
                Water.Produce();
            }
            else
            {
                sc.IsLight = false;
            }
        }
    }

    public void OnEnterHome()
    {
    }

    public void OnPowerHPChange(int amount)
    {
        if (PowerHPChangeDel != null)
        {
            PowerHPChangeDel(amount);
        }
    }
    public System.Action<int> PowerHPChangeDel;

    public void OnFoodProduced(int amount)
    {
        if (FoodProducedDel != null)
        {
            FoodProducedDel(amount);
        }
    }
    public System.Action<int> FoodProducedDel;

    public void OnWaterProduced(int amount)
    {
        if (WaterProducedDel != null)
        {
            WaterProducedDel(amount);
        }
    }
    public System.Action<int> WaterProducedDel;

    public void GatherFood()
    {
        Food.Gather();
    }

    public void GatherWater() 
    {
        Water.Gather();
    }

    public void OnGatherFood(int amount)
    {
        if(OnGatherFoodDel != null)
        {
            OnGatherFoodDel(amount);
        }
    }
    public System.Action<int> OnGatherFoodDel;

    public void OnGatherWater(int amount)
    {
        if (OnGatherWaterDel != null)
        {
            OnGatherWaterDel(amount);
        }
    }
    public System.Action<int> OnGatherWaterDel;

    public void ShowMap()
    {
        if (ShowMapDel != null)
        {
            ShowMapDel();
        }
    }
    public System.Action ShowMapDel;

    public void MapTravelStart(int interval)
    {
        if (MapTravelStartDel != null)
        {
            MapTravelStartDel(interval);
        }
    }

    public System.Action<int> MapTravelStartDel;

    public void MapTravelEnd()
    {
        if (MapTravelEndDel != null)
        {
            MapTravelEndDel();
        }
    }

    public System.Action MapTravelEndDel;

    public void EnterLocationStart()
    {
        if (EnterLocationStartDel != null)
        {
            EnterLocationStartDel();
        }
    }

    public System.Action EnterLocationStartDel;

    public void EnterLocationEnd()
    {
        if (EnterLocationEndDel != null)
        {
            EnterLocationEndDel();
        }
    }

    public System.Action EnterLocationEndDel;

    public void Repair()
    {
        if (Scrap >= RepairCost && PowerHealth < PowerHealthMax)
        {
            Scrap -= RepairCost;
            PowerHealth += RepairHealth;


            if (RepairDel != null)
            {
                RepairDel();
            }
        }
    }
    public System.Action RepairDel;
}
