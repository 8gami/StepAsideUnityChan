using UnityEngine;
using System.Collections;

//【追加】Listを使用するためのusingディレクティブ
using System.Collections.Generic;

public class ItemGenerator : MonoBehaviour
{
    //carPrefabを入れる
    public GameObject carPrefab;
    //coinPrefabを入れる
    public GameObject coinPrefab;
    //cornPrefabを入れる
    public GameObject conePrefab;
    //スタート地点
    private int startPos = -160;
    //ゴール地点
    private int goalPos = 120;
    //アイテムを出すx方向の範囲
    private float posRange = 3.4f;

    //【追加】ユニティちゃんのオブジェクト
    private GameObject unitychan;
    //【追加】ユニティちゃんのz座標をいれる
    public float unitychanZ;

    //【追加】car用のListの変数を宣言。
    public List<GameObject> carList = new List<GameObject>();
    //【追加】coin用のListの変数を宣言。
    public List<GameObject> coinList = new List<GameObject>();
    //【追加】cone用のListの変数を宣言。
    public List<GameObject> coneList = new List<GameObject>();


    // Use this for initialization
    void Start()
    {
        //一定の距離ごとにアイテムを生成
        for (int i = startPos; i < goalPos; i += 15)
        {
            //どのアイテムを出すのかをランダムに設定
            int num = Random.Range(1, 11);
            if (num <= 2)
            {
                //コーンをx軸方向に一直線に生成
                for (float j = -1; j <= 1; j += 0.4f)
                {
                    //【補足】Instantiateで返されるのはObject型の引数。
                    GameObject cone = Instantiate(conePrefab) as GameObject;
                    cone.transform.position = new Vector3(4 * j, cone.transform.position.y, i);
                    //【追加】生成されたPrefabをListに格納
                    coneList.Add(cone);
                }
            }
            else
            {

                //レーンごとにアイテムを生成
                for (int j = -1; j <= 1; j++)
                {
                    //アイテムの種類を決める
                    int item = Random.Range(1, 11);
                    //アイテムを置くZ座標のオフセットをランダムに設定
                    int offsetZ = Random.Range(-5, 6);
                    //60%コイン配置:30%車配置:10%何もなし
                    if (1 <= item && item <= 6)
                    {
                        //コインを生成
                        GameObject coin = Instantiate(coinPrefab) as GameObject;
                        coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, i + offsetZ);
                        //【追加】生成されたPrefabをListに格納
                        coinList.Add(coin);
                    }
                    else if (7 <= item && item <= 9)
                    {
                        //車を生成
                        GameObject car = Instantiate(carPrefab) as GameObject;
                        car.transform.position = new Vector3(posRange * j, car.transform.position.y, i + offsetZ);
                        //【追加】生成されたPrefabをListに格納
                        carList.Add(car);
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //【追加】ユニティちゃんのZ座標を取得
        this.unitychan = GameObject.Find("unitychan");
        unitychanZ = unitychan.transform.position.z;
        OnBecameInvisible();
    }

    //【追加】カメラに写ってない時に呼ばれる関数
    void OnBecameInvisible()
    {
        //車の破壊処理
        for(int i = 0; i < carList.Count; i++)
        {
            //【追加】既に削除したゲームオブジェクトを参照しているかどうかのnullチェックを行う。
            if (carList[i] != null)
            {
                //【追加】もしユニティちゃんよりオブジェクトのZ軸が小さい場合
                if (unitychanZ >carList[i].transform.position.z)
                {
                   GameObject.Destroy(carList[i]);
                }

            }
        }
        //コインの破壊処理
        for (int j = 0; j < coinList.Count; j++)
        {
            //【追加】既に削除したゲームオブジェクトを参照しているかどうかのnullチェックを行う。
            if (coinList[j] != null)
            {
                //【追加】もしユニティちゃんよりオブジェクトのZ軸が小さい場合
                if (unitychanZ > coinList[j].transform.position.z)
                {
                    GameObject.Destroy(coinList[j]);
                }

            }
        }
        //コーンの破壊処理
        for (int k = 0; k < coneList.Count; k++)
        {
            //【追加】既に削除したゲームオブジェクトを参照しているかどうかのnullチェックを行う。
            if (coneList[k] != null)
            {
                //【追加】もしユニティちゃんよりオブジェクトのZ軸が小さい場合
                if (unitychanZ > coneList[k].transform.position.z)
                {
                    GameObject.Destroy(coneList[k]);
                }

            }
        }

    }
}