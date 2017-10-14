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
	public power Power;
	public scrap Scrap;

    public sceneController sc;

    public int PowerHealthMax;
    private int powerHealth = 100;
    public int PowerHealth
    {
        set { powerHealth = value; powerHealth = Mathf.Max(powerHealth, 0); powerHealth = Mathf.Min(PowerHealthMax, powerHealth); OnPowerHPChange(PowerHealth); }
        get { return powerHealth; }
    }

    public int RepairCost = 5;
    public int RepairHealth = 10;

    void Awake()
    {
        _gameLogic = this;
    }

    // Use this for initialization
    void Start()
    {
        PowerHealthMax = PowerHealth;
		Scrap.Amount = 100;
		PowerProducedDel += _OnPowerProduced;
		PowerConsumeDel += _OnPowerConsume;
    }

	void _OnPowerProduced(int amount)
	{
		PowerHealth -= Mathf.CeilToInt(Power.PowerDecayRate * PowerHealthMax);
	}

	void _OnPowerConsume(bool enough)
	{
		if (enough) 
		{
			sc.IsLight = true;
			Food.Produce();
			Water.Produce();
		}
		else 
		{
			sc.IsLight = false;
		}
	}

    // Update is called once per frame
    void Update()
    {
//        powerProduceTime += Time.deltaTime;
//        if (powerProduceTime >= PowerProduceInterval)
//        {
//            powerProduceTime -= PowerProduceInterval;
//            Power += Mathf.CeilToInt(((float)PowerHealth) / PowerHealthMax * PowerProduce);
//            PowerHealth -= Mathf.CeilToInt(PowerDecayRate * PowerHealthMax);
//        }
//
//        powerComsumeTime += Time.deltaTime;
//        if (powerComsumeTime >= PowerComsumeInterval)
//        {
//            powerComsumeTime -= PowerComsumeInterval;
//            if (Power >= PowerComsume)
//            {
//                Power -= PowerComsume;
//                sc.IsLight = true;
//                Food.Produce();
//                Water.Produce();
//            }
//            else
//            {
//                sc.IsLight = false;
//            }
//        }
    }

    public void OnEnterHome()
    {
    }

	public void GatherSupply(int amount)
	{
		Scrap.Amount += amount;
	}

    public void OnPowerHPChange(int amount)
    {
        if (PowerHPChangeDel != null)
        {
            PowerHPChangeDel(amount);
        }
    }
    public System.Action<int> PowerHPChangeDel;

	public void OnPowerConsume(bool enough)
	{
		if (PowerConsumeDel != null)
		{
			PowerConsumeDel(enough);
		}
	}
	public System.Action<bool> PowerConsumeDel;

	public void OnPowerProduced(int amount)
	{
		if (PowerProducedDel != null)
		{
			PowerProducedDel(amount);
		}
	}
	public System.Action<int> PowerProducedDel;

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
		if (Scrap.Amount >= RepairCost && PowerHealth < PowerHealthMax)
        {
			Scrap.Amount -= RepairCost;
            PowerHealth += RepairHealth;


            if (RepairDel != null)
            {
                RepairDel();
            }
        }
    }
    public System.Action RepairDel;
}
