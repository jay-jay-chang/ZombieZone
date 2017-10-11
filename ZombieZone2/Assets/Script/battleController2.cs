using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Spine.Unity.Examples;

public class battleController2 : MonoBehaviour, IPointerDownHandler
{

    public CameraAnchor cameraAnchor;
    public CanvasScaler refScaler;
    public Transform EnemyAnchor;

    public zmmModel player;

    public Transform sectionAnchor;
    public List<GameObject> sections = new List<GameObject>();
    public int currentSceneID;
    public List<string> sectionData;
    public bool SectionLoop = true;

    public SpriteRenderer Block;

    public GameObject ZombiePref;

    int travelInterval;
    float travelTime;

    float blockAnimTime = 1.0f;

    public void OnPointerDown(PointerEventData ped)
    {
        Vector2 pos = UIUtility.mouseToScreenPos(refScaler.referenceResolution.x, refScaler.referenceResolution.y, ped.position);
        if (player != null)
        {
            player.MoveForward(pos.x + (cameraAnchor.gameObject.transform.localPosition - player.transform.localPosition).x);
        }
    }

    public void OnZombieSelect(ZombieAI ai)
    {
        player.MoveForward(0);
        player.TryShoot(ai);
    }

    void generateEnemy(int sectionId)
    {
        if (sectionId < 2)
        {
            return;
        }

        if (((float)travelInterval - travelTime) < 15)
        {
            Debug.Log("generateEnemy skip");
            return;
        }
        //generate random zombie
        int num = Random.Range(0, 6);
        for (int i = 0; i < num; ++i)
        {
            GameObject enemy = (GameObject)GameObject.Instantiate(ZombiePref);
            enemy.transform.parent = EnemyAnchor.transform;
            enemy.transform.localPosition = new Vector3(refScaler.referenceResolution.x * Random.Range(-0.5f + sectionId, 0.5f + sectionId), 0, 0);
            enemy.transform.localScale = Vector3.one;
            enemy.GetComponentInChildren<ZombieView>().OnZombieSelect += OnZombieSelect;
            ZombieAI ai = enemy.GetComponent<ZombieAI>();
            ai.target = player.gameObject;
            //ai.axisBase = EnemyAnchor.gameObject;
        }
    }

    void OnSectionLoad(int sectionId)
    {
        int id = (sectionId + sectionData.Count) % sectionData.Count;
        if (sections.Count <= sectionId)
        {
            GameObject newSection = (GameObject)Instantiate(Resources.Load(sectionData[id]));
            newSection.transform.parent = sectionAnchor;
            newSection.transform.localPosition = new Vector3(refScaler.referenceResolution.x * sectionId, 0, 0);
            newSection.transform.localScale = Vector3.one;
            sections.Add(newSection);
            Debug.Log("section " + sectionId + " loaded(+)");
            generateEnemy(sectionId);
        }
        else if (sections[sectionId] == null)
        {
            GameObject newSection = (GameObject)Instantiate(Resources.Load(sectionData[id]));
            newSection.transform.parent = sectionAnchor;
            newSection.transform.localPosition = new Vector3(refScaler.referenceResolution.x * sectionId, 0, 0);
            newSection.transform.localScale = Vector3.one;
            sections[id] = newSection;
            Debug.Log("section " + sectionId + " loaded(_)");
        }
    }

    void prepareSections()
    {
        int loc = Mathf.FloorToInt((player.transform.localPosition.x + 0.5f * refScaler.referenceResolution.x) / refScaler.referenceResolution.x);
        if (loc - 1 >= 0)
        {
            OnSectionLoad(loc - 1);
        }
        OnSectionLoad(loc);
        if (loc + 1 < sectionData.Count || SectionLoop)
        {
            OnSectionLoad(loc + 1);
        }
    }

    void Reset(float mapLength)
    {
        foreach (GameObject go in sections)
        {
            GameObject.Destroy(go);
        }
        sections.Clear();
        player.Reset();
        prepareSections();
        cameraAnchor.x_axis_limit = new Vector2(mapLength, 0);
    }

    // Use this for initialization
    void Start()
    {
        sectionData = new List<string>();
        sectionData.Add("back2");

        cameraAnchor.x_axis_limit = new Vector2((sectionData.Count - 1) * refScaler.referenceResolution.x, 0);
        prepareSections();
        foreach (ZombieView view in EnemyAnchor.gameObject.GetComponentsInChildren<ZombieView>())
        {
            view.OnZombieSelect += OnZombieSelect;
        }
        gameLogic.Instance.MapTravelStartDel += OnMapTravelStart;
        gameLogic.Instance.MapTravelEndDel += OnMapTravelEnd;
        gameLogic.Instance.EnterLocationStartDel += OnEnterLocationStart;
        gameLogic.Instance.EnterLocationEndDel += OnEnterLocationEnd;
    }

    void OnMapTravelStart(int interval)
    {
        StartCoroutine(_OnMapTravelStart(interval));
    }

    IEnumerator _OnMapTravelStart(int interval)
    {
        BlockAnim(true);
        yield return new WaitForSeconds(blockAnimTime);

        sectionData = new List<string>();
        sectionData.Add("back2");

        float mapLength = player.moveStep * 30 * interval * 100;
        Debug.Log("map length " + mapLength);
        Reset(mapLength);
        player.auto = true;
        player.endPoint = mapLength;
        travelInterval = interval;
        travelTime = 0;

        BlockAnim(false);
    }

    void OnMapTravelEnd()
    {
        player.auto = false;
        player.endPoint = 0;
        player.TryMove(0);
    }

    void OnEnterLocationStart()
    {
        StartCoroutine(_OnEnterLocationStart());
    }

    IEnumerator _OnEnterLocationStart()
    {
        BlockAnim(true);
        yield return new WaitForSeconds(blockAnimTime);

        sectionData = new List<string>();
        sectionData.Add("back2");
        sectionData.Add("room2");
        sectionData.Add("room2");
        sectionData.Add("room2");

        int interval = 20;
        float mapLength = 270 + 640 * 2;
        Debug.Log("map length " + mapLength);
        Reset(mapLength);
        player.auto = true;
        player.endPoint = mapLength;
        travelInterval = interval;
        travelTime = 0;
        cameraAnchor.x_axis_limit = new Vector2(2 * refScaler.referenceResolution.x, 0);

        player.OnArrivalDel += OnEnterLocationEnd;

        BlockAnim(false);
    }

    void OnEnterLocationEnd()
    {
        player.OnArrivalDel -= OnEnterLocationEnd;
        StartCoroutine(_OnEnterLocationEnd());
    }

    IEnumerator _OnEnterLocationEnd()
    {
        BlockAnim(true);
        yield return new WaitForSeconds(blockAnimTime);

        sectionData = new List<string>();
        sectionData.Add("back2");
        Reset(0);
        player.endPoint = 0;

        BlockAnim(false);
    }

    // Update is called once per frame
    void Update()
    {
        travelTime += Time.deltaTime;
        prepareSections();
    }

    void BlockAnim(bool show)
    {
        Block.enabled = true;
        if (show)
        {
            Hashtable args = new Hashtable();
            args.Add("from", Block.color);
            args.Add("to", new Color(0, 0, 0, 1));
            args.Add("time", blockAnimTime);
            args.Add("easeType", iTween.EaseType.easeInCubic);
            args.Add("oncompletetarget", this.gameObject);
            args.Add("oncomplete", "OnBlockAnimComplete");
            args.Add("oncompleteparams", true);
            args.Add("onupdate", "OnBlockColorUpdate");
            iTween.ValueTo(this.gameObject, args);
        }
        else
        {
            Hashtable args = new Hashtable();
            args.Add("from", Block.color);
            args.Add("to", new Color(0, 0, 0, 0));
            args.Add("time", blockAnimTime);
            args.Add("easeType", iTween.EaseType.linear);
            args.Add("oncompletetarget", this.gameObject);
            args.Add("oncomplete", "OnBlockAnimComplete");
            args.Add("oncompleteparams", false);
            args.Add("onupdate", "OnBlockColorUpdate");
            iTween.ValueTo(this.gameObject, args);
        }
    }

    void OnBlockColorUpdate(Color color)
    {
        Block.color = color;
    }

    void OnBlockAnimComplete(bool show)
    {
        if (!show)
        {
            Block.enabled = false;
        }
    }
}
