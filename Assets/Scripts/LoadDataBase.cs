using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public struct Pos
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }
}

public struct Scale
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }
}

public struct Rot
{
    public float X{ get; set; }
    public float Y { get; set; }
    public float Z { get; set; }
}

public struct ColorHieu
{
    public float R { get; set; }
    public float G { get; set; }
    public float B { get; set; }
    public float A { get; set; }
}

public struct HandTut
{
    public Pos pos;
    public Scale scale;
    public Rot rot;
    public int step;
}

public struct Bomb
{
    public Pos pos;
    public Scale scale;
    public Rot rot;
}

public struct Board
{
    public Pos pos;
    public Scale scale;
    public Rot rot;
    public int layer;
    public ColorHieu color;
    public Bomb bomb;
}


public struct Slot
{
    public Pos pos;
    public Scale scale;
    public Rot rot;
    public bool hasNail;
    public bool hasLock;
}

public struct Nail
{
    public Pos pos;
    public Scale scale;
    public Rot rot;
}

public struct Bg
{
    public Pos pos;
    public Scale scale;
    public Rot rot;
}

public struct Txt
{
    public Pos pos;
    public Scale scale;
    public Rot rot;
}

public struct Hint
{
    public Pos pos; 
    public Scale scale;
    public Rot rot;
    public int hintId;
}

public struct Lock
{
    public Pos pos;
    public Scale scale;
    public Rot rot;
    public int layer;
    public ColorHieu color;
}

public struct Key
{
    public Pos pos;
    public Scale scale;
    public Rot rot;
    public int layer;
    public ColorHieu color;
}

public struct Ad
{
    public Pos pos;
    public Scale scale;
    public Rot rot;
}


public class RootLevel
{
    public List<Slot_Item> litsslot;
    public List<Nail_Item> litsnail;
    public List<Lock_Item> litslock;
    public List<Ad_Item> listad;
    public List<Board_Item> listboard;
    public List<Key_Item> listkey;
    public List<Hint_Item> listHint;
    public Bg_Item bgItem;
    public RootLevel()
    {
        litsslot = new List<Slot_Item>();
        litsnail = new List<Nail_Item>();
        litslock = new List<Lock_Item>();
        listad   = new List<Ad_Item>();
        listboard = new List<Board_Item>();
        listkey = new List<Key_Item>();
        listHint = new List<Hint_Item>();
    }

    public void ClearRoot()
    {
        for (int i = 0; i < litsnail.Count; i++)
        {
            litsnail[i].ResetPool();
        }
        for (int i = 0; i < listboard.Count; i++)
        {
            listboard[i].ResetPool();
        }
        for (int i = 0; i < litsslot.Count; i++)
        {
            litsslot[i].ResetPool();
        }

        for(int i = 0; i < listkey.Count; i++)
        {
            listkey[i].ResetPool();
        }

        for (int i = 0; i < litslock.Count; i++)
        {
            litslock[i].ResetPool();
        }
        
        for(int i = 0; i< listad.Count; i++)
        {
            listad[i].ResetPool();
        }

        for(int i = 0; i < listHint.Count; i++)
        {
            listHint[i].ResetPool();
        }

        bgItem.ResetPool();
        bgItem = null;

        litsnail.Clear();
        listboard.Clear();
        litsslot.Clear();
        litslock.Clear();
        listad.Clear();
        listkey.Clear();
        litslock.Clear();
        listHint.Clear();
    }

    public bool Findslotfornail(Nail_Item nail)
    {
        List<Slot_Item> fakelistlots = litsslot;
        for (int i = 0; i < fakelistlots.Count; i++)
        {
            if (fakelistlots[i].hasNail == true && fakelistlots[i].transform.position == nail.transform.position)
            {
                fakelistlots[i].nail_item = nail;
                nail.slot_item = fakelistlots[i];
                nail.transform.parent = fakelistlots[i].transform;
                return true;
            }
        }
        return false;
    }

    public bool Findslotforlock(Lock_Item lockitem)
    {
        List<Slot_Item> fakelistlots = litsslot;
        for (int i = 0; i < fakelistlots.Count; i++)
        {
            if (fakelistlots[i].hasLock == true && fakelistlots[i].transform.position == lockitem.transform.position)
            {
                lockitem.transform.parent = fakelistlots[i].transform;          
                return true;
            }
        }
        return false;
    }

    public bool Findsadforlock(Ad_Item aditem)
    {
        List<Slot_Item> fakelistlots = litsslot;
        Bounds bounds1 = aditem.GetComponent<Collider2D>().bounds;
        for (int i = 0; i < fakelistlots.Count; i++)
        {
            Bounds bounds2 = fakelistlots[i].GetComponent<Collider2D>().bounds;

            if (bounds1.Intersects(bounds2))
            {
                aditem.transform.parent = fakelistlots[i].transform;            
                return true;
            }          
        }
        return false;
    }


    public void Findsnailforslot(Slot_Item slot)
    {
        List<Nail_Item> fakelistnail = litsnail;
        for (int i = 0; i < fakelistnail.Count; i++)
        {
            if (slot.transform.position == fakelistnail[i].transform.position)
            {
                fakelistnail[i].transform.parent = slot.transform;
                slot.nail_item = fakelistnail[i];
                fakelistnail[i].slot_item = slot;
                //litsnail.Remove(fakelistnail[i]);
            }
        }
    }

    public void Findslockforslot(Slot_Item slot)
    {
        List<Lock_Item> fakelistlock = litslock;
        for (int i = 0; i < fakelistlock.Count; i++)
        {
            if (slot.transform.position == fakelistlock[i].transform.position)
            {
                fakelistlock[i].transform.parent = slot.transform;
                litslock.Remove(fakelistlock[i]);
            }
        }
    }

    public void Findsadforslot(Slot_Item slot)
    {
        List<Ad_Item> fakelistad = listad;
        Bounds bounds1 = slot.GetComponent<Collider2D>().bounds;
        for (int i = 0; i < fakelistad.Count; i++)
        {
            Bounds bounds2 = fakelistad[i].GetComponent<Collider2D>().bounds;

            if (bounds1.Intersects(bounds2))
            {
                fakelistad[i].transform.parent = slot.transform;
                listad.Remove(fakelistad[i]);
            }
        }
    }
}



public class LoadDataBase : MonoBehaviour
{

    public int itemCount;
    public int boardCount;


    public GameObject gameobjecttest;

    public LevelController levelController;
    public Collider2D[] results = new Collider2D[15];
    public void LoadLevelGame(string str)
    {
        LoadFileJsonLevel(str);
    }
    private void LoadFileJsonLevel(string level)
    {
        
        AsyncOperationHandle<TextAsset> asyncOperationHandle = Addressables.LoadAssetAsync<TextAsset>(level);
        asyncOperationHandle.Completed += (handle) =>
        {
            string jsonLevel = handle.Result.text;
            string[] strings = jsonLevel.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
            itemCount = strings.Length;

            foreach (string str in strings)
            {
                Debug.Log(str);
                if (str.StartsWith("tut:"))
                {
                    HandTutEditString(str);
                }
                else if (str.StartsWith("board:"))
                {
                    HandBoaEditString(str);
                }
                else if (str.StartsWith("slot:"))
                {
                    HandSlotEditString(str);
                }
                else if (str.StartsWith("savable:"))
                {
                    HandSavableEditString(str);
                }
                else if (str.StartsWith("hint:"))
                {
                    HandHintEditString(str);
                }
                else if (str.StartsWith("time"))
                {
                    HandTimeEditString(str);
                }
                else if (str.StartsWith("level"))
                {
                    HandLevelEditString(str);
                }
                else if (str.StartsWith("spr:"))
                {
                    HandSprEditString(str);
                }
            }
        };
    }

    public void CheckTimeSetUpMap()
    {
        itemCount--;
        if (itemCount == 0)
        {
            StartCoroutine(CreatePhysic2dforboard());
        }
    }

    public IEnumerator CheckBoardSetupMap()
    {     
        while (boardCount != 0)
        {
            yield return null;
        }     
        ActivePhysic2dforboard();    
    }

    public IEnumerator CreatePhysic2dforboard()
    {
        yield return new WaitForSeconds(1);
        int m = 0;
        boardCount = 0;
        for (int i=0; i < Controller.Instance.rootlevel.litsnail.Count; i++)
        {
            m++;       
            Bounds boundnail = Controller.Instance.rootlevel.litsnail[i].ColiderNail.bounds;
            Vector2 size = boundnail.size;
            List<int> layerboard = new List<int>();       
            Collider2D[] colliders = Physics2D.OverlapBoxAll(Controller.Instance.rootlevel.litsnail[i].transform.position, size, 0);           
            
            foreach (Collider2D collider in colliders)
            {

               
                Bounds bounds1 = collider.bounds;
                if (bounds1.Intersects(boundnail) && collider.CompareTag("Board"))
                {
                    Bounds overlapBounds = boundnail;
                    overlapBounds.Encapsulate(bounds1.min);
                    overlapBounds.Encapsulate(bounds1.max);
                    float overlapArea = overlapBounds.size.x * overlapBounds.size.y;
                    float overlapPercentage = (overlapArea / (bounds1.size.x * bounds1.size.y)) * 100f;
                    if (overlapPercentage >= 90 && overlapPercentage <= 100.5f)
                    {
                        boardCount++;
                        Board_Item board = collider.GetComponent<Board_Item>();
                        LoadSlotBoardAddressAble(board, Controller.Instance.rootlevel.litsnail[i]);
                        layerboard.Add(collider.gameObject.layer - 6);
                    }
                }
            }
            //Debug.Log("==============================================================");
            Controller.Instance.rootlevel.litsnail[i].gameObject.layer = Controller.Instance.nailLayerController.InputNumber(layerboard);
        }
        StartCoroutine(CheckBoardSetupMap());
    }

    public void ActivePhysic2dforboard()
    {
        for(int i=0; i< Controller.Instance.rootlevel.listboard.Count; i++)
        {
            Controller.Instance.rootlevel.listboard[i].SetupRb();
        }
    }


    public void LoadSlotBoardAddressAble(Board_Item board, Nail_Item nail_item)
    {
        Slot_board_Item slotboarditem = slotboard_Spawn.Instance._pool.Get();
        slotboarditem.transform.position = nail_item.transform.position;
        slotboarditem.transform.rotation = Quaternion.Euler(new Vector3(nail_item.nail.rot.X, nail_item.nail.rot.Y, nail_item.nail.rot.Z));
        slotboarditem.transform.SetParent(board.transform);
        slotboarditem.transform.localScale = new Vector3(nail_item.nail.scale.X, nail_item.nail.scale.Y, nail_item.nail.scale.Z);
        board.AddSlotforBoard(slotboarditem);
        HingeJoint2D hingeJoint = board.gameObject.AddComponent<HingeJoint2D>();
        hingeJoint.anchor = board.transform.InverseTransformPoint(nail_item.transform.position);
        hingeJoint.enableCollision = true;
        nail_item.listHingeJoin.Add(hingeJoint);
       
        boardCount--;

        slotboarditem.hingeJointInSlot = hingeJoint;
    }





    private void HandBoaEditString(string str)
    {
        string[] strings = DoubleStringEditNameandValue(str);
        Board board = JsonConvert.DeserializeObject<Board>(strings[1]);  
        LoadBoardAddressAble(strings[0],board);
    }

    private void HandTutEditString(string str)
    {
        string[] strings = DoubleStringEditNameandValue(str);
        HandTut hieu = JsonConvert.DeserializeObject<HandTut>(strings[1]);
        CheckTimeSetUpMap();
    }

    private void HandSlotEditString(string str)
    {
        string[] strings = DoubleStringEditNameandValue(str);
        Slot slot = JsonConvert.DeserializeObject<Slot>(strings[1]);
        LoadSlotAddressAble(strings[0], slot);
    }

    private void HandHintEditString(string str)
    {
        string[] strings = DoubleStringEditNameandValue(str);
        Hint hint = JsonConvert.DeserializeObject<Hint>(strings[1]);
        LoadHintAddressAble(strings[0], hint);
    }
    public void LoadHintAddressAble(string str, Hint hint)
    {
        Hint_Item hintItem = Hint_Spawner.Instance._pool.Get();
        hintItem.transform.position = new Vector3(hint.pos.X, hint.pos.Y, hint.pos.Z);
        hintItem.transform.rotation = Quaternion.Euler(new Vector3(hint.rot.X, hint.rot.Y, hint.rot.Z));
        hintItem.transform.SetParent(levelController.MainLevelSetupCreateMap);
        hintItem.transform.localScale = new Vector3(hint.scale.X, hint.scale.Y, hint.scale.Z);
        hintItem.SetUpTextIdHint(hint.hintId.ToString());
        Controller.Instance.rootlevel.listHint.Add(hintItem);
        CheckTimeSetUpMap();
    }

    public void HandTimeEditString(string str)
    {
        string[] parts = str.Split(new string[] { "~~" }, StringSplitOptions.None);
        CheckTimeSetUpMap();
    }
    public void HandLevelEditString(string str)
    {
        string[] parts = str.Split(new string[] { "~~" }, StringSplitOptions.None);
        CheckTimeSetUpMap();
    }
    private void HandSprEditString(string str)
    {
        string[] strings = DoubleStringEditNameandValue(str);
        switch (strings[0])
        {
            case "lock":
                HandSprEditString_Lock(strings[1]);
                break;
            case "key":
                HandSprEditString_Key(strings[1]);
                break;
        }
    }

    private void HandSprEditString_Key(string str)
    {
        Key key = JsonConvert.DeserializeObject<Key>(str);
        LoadKeyAddressAble(key);
    }

    public void HandSprEditString_Lock(string str)
    {
        Lock lock1 = JsonConvert.DeserializeObject<Lock>(str);
        LoadLockAddressAble("lock", lock1);
    }
    public void LoadKeyAddressAble(Key key)
    {
        Key_Item keyItem = KeySpawner.Instance._pool.Get();
        keyItem.transform.position = new Vector3(key.pos.X, key.pos.Y, key.pos.Z);
        keyItem.transform.rotation = Quaternion.Euler(new Vector3(key.rot.X, key.rot.Y, key.rot.Z));
        keyItem.transform.SetParent(levelController.MainLevelSetupCreateMap);
        keyItem.transform.localScale = new Vector3(key.scale.X, key.scale.Y, key.scale.Z);
        SpriteRenderer spriteRenderer = keyItem.GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = key.layer;
        spriteRenderer.color = new Color(key.color.R, key.color.G, key.color.B, key.color.A);
        Controller.Instance.rootlevel.listkey.Add(keyItem);
        CheckTimeSetUpMap();
    }

    public void LoadLockAddressAble(string str, Lock lock1)
    {
        Lock_Item lockItem = LockSpawner.Instance._pool.Get();
        lockItem.transform.position = new Vector3(lock1.pos.X, lock1.pos.Y, lock1.pos.Z);
        lockItem.transform.rotation = Quaternion.Euler(new Vector3(lock1.rot.X, lock1.rot.Y, lock1.rot.Z));
        lockItem.transform.localScale = new Vector3(lock1.scale.X, lock1.scale.Y, lock1.scale.Z);
        SpriteRenderer spriteRenderer = lockItem.GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = lock1.layer;
        spriteRenderer.color = new Color(lock1.color.R, lock1.color.G, lock1.color.B, lock1.color.A);

        if (Controller.Instance.rootlevel.Findslotforlock(lockItem) == false)
        {
            Controller.Instance.rootlevel.litslock.Add(lockItem);
        }

        Controller.Instance.rootlevel.litslock.Add(lockItem);
        CheckTimeSetUpMap();
    }



    private void HandSavableEditString(string str)
    {
        string[] strings = DoubleStringEditNameandValue(str);
        switch (strings[0])
        {
            case "nail":
                HandSavableEditString_Nail(strings[1]);
                break;
            case "bg":
                HandSavableEditString_Bg(strings[1]);
                break;
            case "txt":
                HandSavableEditString_Txt(strings[1]);
                break;
            case "ad":
                HandSavableEditString_Ad(strings[1]);
                break;
        }
    }
    private void HandSavableEditString_Ad(string str)
    {
        Ad ad = JsonConvert.DeserializeObject<Ad>(str);   
        LoadAdAddressAble("ad", ad);
    }
    private void HandSavableEditString_Nail(string str)
    {
        Nail nail = JsonConvert.DeserializeObject<Nail>(str);    
        LoadNailAddressAble("nail",nail);
    }

    private void HandSavableEditString_Bg(string str)
    {
        Bg bg = JsonConvert.DeserializeObject<Bg>(str);      
        LoadBgAddressAble(bg);
    }

    private void HandSavableEditString_Txt(string str)
    {
        Txt hieu = JsonConvert.DeserializeObject<Txt>(str);
        CheckTimeSetUpMap();
    }


    private string[] DoubleStringEditNameandValue(string str)
    {
        
        int colonIndex = str.IndexOf(':');
        int tildeIndex = str.IndexOf("~~");

        // Trích xuất chuỗi
        string firstString = str.Substring(colonIndex + 1, tildeIndex - colonIndex - 1);
        string secondString = str.Substring(tildeIndex + 2);
        return new string[] {firstString,secondString};
    }

    public void LoadBoardAddressAble(string str, Board board)
    {


        Board_Item boardItem = null;
        switch (str)
        {
            case "board_550x100":
                boardItem = board_550x100.Instance._pool.Get();
                break;
            case "board_400x175":
                boardItem = board400x175spawner.Instance._pool.Get();
                break;
            case "board_700x100":
                boardItem = Board_700x100Spawner.Instance._pool.Get();
                break;
            case "board_W":
                boardItem = board_W_Spawner.Instance._pool.Get();
                break;
            case "board_L":
                boardItem = board_L_Spawner.Instance._pool.Get();
                break;
            case "board_400x400":
                boardItem = board_400x400_Spawner.Instance._pool.Get();
                break;
            case "board_400x100":
                boardItem = board_400x100_Spawner.Instance._pool.Get();
                break;
            case "board_150x150":
                boardItem = board_150x150_Spawner.Instance._pool.Get();
                break;
            case "board_275x100":
                boardItem = board_275x100_Spawner.Instance._pool.Get();
                break;
        }
        if (boardItem == null) return;
        boardItem.transform.position = new Vector3(board.pos.X, board.pos.Y, board.pos.Z);
        boardItem.transform.rotation = Quaternion.Euler(new Vector3(board.rot.X, board.rot.Y, board.rot.Z));
        boardItem.transform.parent = levelController.MainLevelSetupCreateMap;
        boardItem.transform.localScale = new Vector3(board.scale.X, board.scale.Y, board.scale.Z);
        SpriteRenderer spriteRenderer = boardItem.GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = board.layer + 5;
        boardItem.gameObject.layer = 6 + board.layer;
        spriteRenderer.color = new Color(board.color.R, board.color.G, board.color.B, board.color.A);




        //AsyncOperationHandle<GameObject> asyncOperationHandle2 = Addressables.LoadAssetAsync<GameObject>(AddressAbleStringEdit.URLAddress("bomb"));
        //asyncOperationHandle2.Completed += (handle2) =>
        //{
        //    GameObject bomb = Instantiate(handle2.Result, new Vector3(board.bomb.pos.X, board.bomb.pos.Y, board.bomb.pos.Z), Quaternion.Euler(new Vector3(board.bomb.rot.X, board.bomb.rot.Y, board.bomb.rot.Z)));
        //    bomb.transform.localScale = new Vector3(board.bomb.scale.X, board.bomb.scale.Y, board.bomb.scale.Z);
        //    bomb.transform.parent = a.transform;
        //};
        //Board_Item board_Item = a.GetComponent<Board_Item>();



        Controller.Instance.rootlevel.listboard.Add(boardItem);
        CheckTimeSetUpMap();
        //try
        //{
        //    AsyncOperationHandle<GameObject> asyncOperationHandle = Addressables.LoadAssetAsync<GameObject>(AddressAbleStringEdit.URLAddress(str));
        //    asyncOperationHandle.Completed += (handle) =>
        //    {
        //        if (handle.Status == AsyncOperationStatus.Succeeded)
        //        {
        //            try
        //            {
        //                GameObject a = Instantiate(handle.Result, new Vector3(board.pos.X, board.pos.Y, board.pos.Z), Quaternion.Euler(new Vector3(board.rot.X, board.rot.Y, board.rot.Z)), levelController.MainLevelSetupCreateMap);
        //                a.transform.localScale = new Vector3(board.scale.X, board.scale.Y, board.scale.Z);
        //                SpriteRenderer spriteRenderer = a.GetComponent<SpriteRenderer>();
        //                spriteRenderer.sortingOrder = board.layer;

        //                a.layer = 6 + board.layer;
        //                spriteRenderer.color = new Color(board.color.R, board.color.G, board.color.B, board.color.A);
        //                AsyncOperationHandle<GameObject> asyncOperationHandle2 = Addressables.LoadAssetAsync<GameObject>(AddressAbleStringEdit.URLAddress("bomb"));
        //                asyncOperationHandle2.Completed += (handle2) =>
        //                {
        //                    GameObject bomb = Instantiate(handle2.Result, new Vector3(board.bomb.pos.X, board.bomb.pos.Y, board.bomb.pos.Z), Quaternion.Euler(new Vector3(board.bomb.rot.X, board.bomb.rot.Y, board.bomb.rot.Z)));
        //                    bomb.transform.localScale = new Vector3(board.bomb.scale.X, board.bomb.scale.Y, board.bomb.scale.Z);
        //                    bomb.transform.parent = a.transform;
        //                };
        //                Board_Item board_Item = a.GetComponent<Board_Item>();
        //                levelController.rootlevel.listboard.Add(board_Item);

        //            }
        //            catch
        //            {

        //            }
        //        }
        //        else
        //        {
        //            Debug.Log("loi roi hhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhh");
        //        }


        //        CheckTimeSetUpMap();
        //    };
        //}
        //catch
        //{

        //}
    }


    public void LoadSlotAddressAble(string str, Slot slot)
    {
        Slot_Item slot_Item = slot_Spawner.Instance._pool.Get();
        slot_Item.transform.position = new Vector3(slot.pos.X, slot.pos.Y, slot.pos.Z);
        slot_Item.transform.rotation = Quaternion.Euler(new Vector3(slot.rot.X, slot.rot.Y, slot.rot.Z));
        slot_Item.transform.parent = levelController.MainLevelSetupCreateMap;
        slot_Item.transform.localScale = new Vector3(slot.scale.X, slot.scale.Y, slot.scale.Z);
        slot_Item.hasNail = slot.hasNail;
        slot_Item.hasLock = slot.hasLock;
        Controller.Instance.rootlevel.litsslot.Add(slot_Item);
        if (slot_Item.hasNail == true || slot_Item.hasLock == true)
        {
            if (slot_Item.hasNail == true)
            {
                Controller.Instance.rootlevel.Findsnailforslot(slot_Item);
            }
            if (slot_Item.hasLock == true)
            {
                Controller.Instance.rootlevel.Findslockforslot(slot_Item);
            }

        }
        Controller.Instance.rootlevel.Findsadforslot(slot_Item);
        CheckTimeSetUpMap();

        //AsyncOperationHandle<GameObject> asyncOperationHandle = Addressables.LoadAssetAsync<GameObject>(AddressAbleStringEdit.URLAddress(str));
        //asyncOperationHandle.Completed += (handle) =>
        //{
        //    if (handle.Status == AsyncOperationStatus.Succeeded)
        //    {
        //        GameObject a = Instantiate(handle.Result, new Vector3(slot.pos.X, slot.pos.Y, slot.pos.Z), Quaternion.Euler(new Vector3(slot.rot.X, slot.rot.Y, slot.rot.Z)), levelController.MainLevelSetupCreateMap);
        //        a.transform.localScale = new Vector3(slot.scale.X, slot.scale.Y, slot.scale.Z);
        //        Slot_Item slot_Item = a.GetComponent<Slot_Item>();
        //        slot_Item.hasNail = slot.hasNail;
        //        slot_Item.hasLock = slot.hasLock;
        //        levelController.rootlevel.litsslot.Add(slot_Item);
        //        if (slot_Item.hasNail == true || slot_Item.hasLock == true)
        //        {
        //            if (slot_Item.hasNail == true)
        //            {
        //                levelController.rootlevel.Findsnailforslot(slot_Item);
        //            }
        //            if (slot_Item.hasLock == true)
        //            {
        //                levelController.rootlevel.Findslockforslot(slot_Item);
        //            }

        //        }

        //        levelController.rootlevel.Findsadforslot(slot_Item);
        //    }
        //    else
        //    {
        //        // Handle the failure case
        //    }

        //    CheckTimeSetUpMap();
        //};
    }

    public void LoadNailAddressAble(string str,  Nail nail)
    {
        // AsyncOperationHandle<GameObject> asyncOperationHandle = Addressables.LoadAssetAsync<GameObject>(AddressAbleStringEdit.URLAddress(str));
        // asyncOperationHandle.Completed += (handle) =>
        // {
        //     if (handle.Status == AsyncOperationStatus.Succeeded)
        //     {
        //         GameObject a = Instantiate(handle.Result, new Vector3(nail.pos.X, nail.pos.Y, nail.pos.Z), Quaternion.Euler(new Vector3(nail.rot.X, nail.rot.Y, nail.rot.Z)));
        //         a.transform.localScale = new Vector3(nail.scale.X, nail.scale.Y, nail.scale.Z);
        //         Nail_Item nail_Item = a.GetComponent<Nail_Item>();
        //         nail_Item.nail = nail;
        //         levelController.rootlevel.Findslotfornail(nail_Item);
        //         levelController.rootlevel.litsnail.Add(nail_Item);
        //     }
        //     else
        //     {
        //         // Handle the failure case
        //     }
        //     CheckTimeSetUpMap();
        // };


    
        Nail_Item nail_Item = Controller.Instance.nailSpawner._pool.Get();
        if(nail_Item != null){
            nail_Item.nail = nail;
            nail_Item.transform.localScale = new Vector3(nail.scale.X, nail.scale.Y, nail.scale.Z);
            nail_Item.transform.position = new Vector3(nail.pos.X, nail.pos.Y, nail.pos.Z);
            nail_Item.transform.rotation = Quaternion.Euler(new Vector3(nail.rot.X, nail.rot.Y, nail.rot.Z));
            Controller.Instance.rootlevel.Findslotfornail(nail_Item);
            Controller.Instance.rootlevel.litsnail.Add(nail_Item);
            CheckTimeSetUpMap();
        }
    }
    public void LoadAdAddressAble(string str, Ad ad)
    {
        Ad_Item aditem = Ad_Spawner.Instance._pool.Get();
        aditem.transform.position = new Vector3(ad.pos.X, ad.pos.Y, ad.pos.Z);
        aditem.transform.rotation = Quaternion.Euler(new Vector3(ad.rot.X, ad.rot.Y, ad.rot.Z));
        aditem.transform.localScale = new Vector3(ad.scale.X, ad.scale.Y, ad.scale.Z);
        Controller.Instance.rootlevel.Findsadforlock(aditem);
        Controller.Instance.rootlevel.listad.Add(aditem);
        CheckTimeSetUpMap();    
    }

    public void LoadBgAddressAble(Bg bg)
    {
        Bg_Item bg_Item = bg_Spawner.Instance._pool.Get();
        bg_Item.transform.position = new Vector3(bg.pos.X, bg.pos.Y, bg.pos.Z);
        bg_Item.transform.rotation = Quaternion.Euler(new Vector3(bg.rot.X, bg.rot.Y, bg.rot.Z));
        bg_Item.transform.SetParent(levelController.MainLevelSetupCreateMap);
        bg_Item.transform.localScale = new Vector3(bg.scale.X, bg.scale.Y, bg.scale.Z);
        Controller.Instance.rootlevel.bgItem = bg_Item;
        CheckTimeSetUpMap();
    }
}


