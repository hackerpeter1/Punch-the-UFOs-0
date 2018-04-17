/**
 * 这个文件是用来生产飞碟的工厂
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskFactory : MonoBehaviour
{

    /**
     * diskPrefab是用来生产飞碟的模板，保存着一个飞碟的实例
     */

    public GameObject diskPrefab;

    /**
     * used是用来保存正在使用的飞碟
     * free是用来保存未激活的飞碟
     */

    private List<DiskData> used = new List<DiskData>();
    private List<DiskData> free = new List<DiskData>();

    private void Awake()
    {
        diskPrefab = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/disk"), Vector3.zero, Quaternion.identity);
        diskPrefab.SetActive(false);
    }

    /**
     * GetDisk这个函数是用来飞碟的，
     * 每次首次判断free那里还有没有未使用的飞碟，
     * 有就从free那里获取，没有就生成一个飞碟
     */

    public GameObject GetDisk(int round)
    {
        GameObject newDisk = null;
        if (free.Count > 0)
        {
            newDisk = free[0].gameObject;
            free.Remove(free[0]);
        }
        else
        {
            newDisk = GameObject.Instantiate<GameObject>(diskPrefab, Vector3.zero, Quaternion.identity);
            newDisk.AddComponent<DiskData>();
        }

        int start = 0;
        if (round == 1) start = 100;
        if (round == 2) start = 250;
        if (round == 3) start = 300;
        int selectedColor = Random.Range(start, round * 499);
        if(selectedColor > 800)
        {
            round = 3;
        }
        else if (selectedColor > 500)
        {
            round = 2;
        }
        else if (selectedColor > 300)
        {
            round = 1;
        }
        else
        {
            round = 0;
        }

        /**
         * 根据回合数来生成相应的飞碟
         */

        switch (round)
        {

            case 0:
                {
                    newDisk.GetComponent<DiskData>().color = Color.yellow;
                    newDisk.GetComponent<DiskData>().speed = 4.0f;
                    float RanX = UnityEngine.Random.Range(-2f, 2f) < 0 ? -1 : 1;
                    newDisk.GetComponent<DiskData>().direction = new Vector3(RanX, 1, 0);
                    newDisk.GetComponent<Renderer>().material.color = Color.yellow;
                    break;
                }
            case 1:
                {
                    newDisk.GetComponent<DiskData>().color = Color.red;
                    newDisk.GetComponent<DiskData>().speed = 5.5f;
                    float RanX = UnityEngine.Random.Range(-2f, 2f) < 0 ? -1 : 1;
                    newDisk.GetComponent<DiskData>().direction = new Vector3(RanX, 1, 1);
                    newDisk.GetComponent<Renderer>().material.color = Color.red;
                    break;
                }
            case 2:
                {
                    newDisk.GetComponent<DiskData>().color = Color.black;
                    newDisk.GetComponent<DiskData>().speed = 7.0f;
                    float RanX = UnityEngine.Random.Range(-2f, 2f) < 0 ? -1 : 1;
                    newDisk.GetComponent<DiskData>().direction = new Vector3(RanX, 1, 1);
                    newDisk.GetComponent<Renderer>().material.color = Color.black;
                    break;
                }
            case 3:
                {
                    newDisk.GetComponent<DiskData>().color = Color.white;
                    newDisk.GetComponent<DiskData>().speed = 10.0f;
                    float RanX = UnityEngine.Random.Range(-1f, 1f) < 0 ? -1 : 1;
                    newDisk.GetComponent<DiskData>().direction = new Vector3(RanX, 1, 1);
                    newDisk.GetComponent<Renderer>().material.color = Color.blue;
                    break;
                }
        }

        used.Add(newDisk.GetComponent<DiskData>());
        //newDisk.SetActive(true);
        newDisk.name = newDisk.GetInstanceID().ToString();
        return newDisk;
    }

    public void FreeDisk(GameObject disk)
    {
        DiskData tmp = null;
        foreach (DiskData i in used)
        {
            if (disk.GetInstanceID() == i.gameObject.GetInstanceID())
            {
                tmp = i;
            }
        }
        if (tmp != null)
        {
            tmp.gameObject.SetActive(false);
            free.Add(tmp);
            used.Remove(tmp);
        }
    }
}