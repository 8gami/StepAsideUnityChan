using UnityEngine;
using System.Collections;

//【追加】Listを使用するためのusingディレクティブ
using System.Collections.Generic;

public class ItemGenerator_Advanced : MonoBehaviour
{
    //carPrefabを入れる
    public GameObject carPrefab;
    //coinPrefabを入れる
    public GameObject coinPrefab;
    //cornPrefabを入れる
    public GameObject conePrefab;
    //スタート地点
    //private int startPos = -160;
    //ゴール地点
    private int goalPos = 120;
    //アイテムを出すx方向の範囲
    private float posRange = 3.4f;

    //【発展・追加】ユニティちゃんのオブジェクト
    private GameObject unitychan;
    //【発展・追加】ユニティちゃんのz座標をいれる
    public float unitychanZ;
    //【発展・追加】進行管理用のz座標
    public float Z;
    //【発展・追加】スタート地点でのユニティちゃんのz座標をいれる
    public float startunitychanZ;

    //【追加】car用のListの変数を宣言。
    public List<GameObject> carList = new List<GameObject>();
    //【追加】coin用のListの変数を宣言。
    public List<GameObject> coinList = new List<GameObject>();
    //【追加】cone用のListの変数を宣言。
    public List<GameObject> coneList = new List<GameObject>();

    //【追加】カメラのオブジェクト
    private GameObject maincamera;
    //【追加】カメラのz座標をいれる。
    public float maincameraZ;


    // Use this for initialization
    void Start()
    {
        //【発展・追加】開始時点でのユニティちゃんのZ座標を取得
        this.unitychan = GameObject.Find("unitychan");
        startunitychanZ = unitychan.transform.position.z;

        //【発展・調整】UnityちゃんのZ軸+45の範囲でアイテムを作成する
        for (float i = startunitychanZ + 15; i < startunitychanZ + 45; i += 15)
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
        Z = startunitychanZ + 45;
    }

    // Update is called once per frame
    void Update()
    {
        //【発展・追加】ユニティちゃんのZ座標を取得
        this.unitychan = GameObject.Find("unitychan");
        unitychanZ = unitychan.transform.position.z;

        //【発展・質問】40~50の振れ幅を厳密にするかどうかで色々変わりそう。
        float num = Random.Range(15, 25);

        //【発展・追加】ゴール前付近にはアイテムが生成できないようにしておく。
        if(unitychanZ >= startunitychanZ + num && goalPos-25 > startunitychanZ + num)
        {
            appear(num);
            Z += num;
        }

        //【追加】カメラのZ座標を取得
        this.maincamera = GameObject.Find("Main Camera");
        maincameraZ = maincamera.transform.position.z;

        //【追加】オブジェクトが画面外かどうかを判定する関数
        OffScreen();
    }

    //【発展・追加】オブジェクトの生成
    void appear(float a)
    {
        for (float i = Z + 15; i < Z + 45; i += a)
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

    //【追加】画面外かどうかを判定する関数の中身
    void OffScreen()
    {
        //車の破壊処理
        for (int i = 0; i < carList.Count; i++)
        {
            //【追加】既に削除したゲームオブジェクトを参照しているかどうかのnullチェックを行う
            if (carList[i] != null)
            {
                //【追加】もしユニティちゃんよりオブジェクトのZ軸が小さい場合
                if (maincameraZ > carList[i].transform.position.z)
                {
                    GameObject.Destroy(carList[i]);
                }
            }
            //【追加】nullになったオブジェクトの中身をRemoveする処理（3月5日のメンタリング用として追加しました。）
            else if (carList[i] == null)
            {
                carList.RemoveAt(i);
            }
        }
        //コインの破壊処理
        for (int j = 0; j < coinList.Count; j++)
        {
            //【追加】既に削除したゲームオブジェクトを参照しているかどうかのnullチェックを行う
            if (coinList[j] != null)
            {
                //【追加】もしユニティちゃんよりオブジェクトのZ軸が小さい場合
                if (maincameraZ > coinList[j].transform.position.z)
                {
                    GameObject.Destroy(coinList[j]);
                }
                //【追加】nullになったオブジェクトの中身をRemoveする処理（3月5日のメンタリング用として追加しました。）
                else if (coinList[j] == null)
                {
                    coinList.RemoveAt(j);
                }
            }
        }
        //コーンの破壊処理
        for (int k = 0; k < coneList.Count; k++)
        {
            //【追加】既に削除したゲームオブジェクトを参照しているかどうかのnullチェックを行う
            if (coneList[k] != null)
            {
                //【追加】もしユニティちゃんよりオブジェクトのZ軸が小さい場合
                if (maincameraZ > coneList[k].transform.position.z)
                {
                    GameObject.Destroy(coneList[k]);
                }

            }
            //【追加】nullになったオブジェクトの中身をRemoveする処理（3月5日のメンタリング用として追加しました。）
            else if (coneList[k] == null)
            {
                coneList.RemoveAt(k);
            }
        }

    }
}