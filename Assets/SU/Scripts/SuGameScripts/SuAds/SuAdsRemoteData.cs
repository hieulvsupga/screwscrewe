using System.Collections.Generic;
[System.Serializable]
public struct Inter_Ad_Config
{
    public int id, level_start_show, capping_time, level_start_replay_show;
}
[System.Serializable]
public struct Loading_Config
{
    public int id, loading_time;
}
[System.Serializable]
public struct Open_Ad_Config
{
    public int id, capping_time, time_in_background_show;
    public bool on, cold_start;
}
[System.Serializable]
public struct Rate_Config
{
    public int id, level_show, repeat;
    public bool on;
}
[System.Serializable]
public struct Offline_Play_On_Off
{
    public int id;
    public bool on;
}
[System.Serializable]
public struct Ads_Network_Config
{
    public int id;
    public List<string> ads_networks;
}
[System.Serializable]
public struct Rank_Play_Config
{
    public int id;
    public int rank1;
    //public int rankn;
    public bool on;
    public int level_start;
    public string exclude_levels;
}

[System.Serializable]
public struct Remove_options_popup_complete
{
    public int id;
    public int on;
    public int level_start_showing;
}
[System.Serializable]
public struct Daily_gift
{
    public int id;
    public bool on;
}

[System.Serializable]
public struct Banner_id
{
    public int id;
    public string banner_small;
    public string banner_big;
    public bool use_banner_big;
}
[System.Serializable]
public struct Rewarded_id
{
    public string id;
}
[System.Serializable]
public struct Interstitial_id
{
    public string id;
}
[System.Serializable]
public struct Ads_open_id
{
    public string id;
}

[System.Serializable]
public struct reward_ad_config
{
    public int id;
    public int level_start_show;
}
[System.Serializable]
public class Moves_config
{
    public int id;
    public string moves;
}
[System.Serializable]
public class MoveItem
{
    public int level;
    public int move;
}

[System.Serializable]
public class Buttons_Config
{
    public int id;
    public int level_to_show;
}

[System.Serializable]
public class Replay_config
{
    public int id;
    public int moved;
    public int play_time;
}

[System.Serializable]
public class Out_of_moves_config
{
    public int id;
    public int original;
    public int add_more;
}

//{4:15,5:25,6:30,7:32,8:30,9:38,10:45,11:48,12:34,13:58,14:60,15:33,16:90,17:45,18:115,19:87,20:110,21:115,22:89,23:79,24:175,25:150,26:191,27:110,28:213,29:150,30:160,31:160,32:200,33:91,34:135,35:105,36:164,37:124,38:225,39:152,40:175,41:258,42:89,43:170,44:77,45:110,46:200,47:68,48:210,49:240,50:160,51:201,52:165,53:126,54:250,55:82,56:105,57:157,58:260,59:124,60:108,61:180,62:158,63:192,64:200,65:210,66:152,67:192,68:260,69:250,70:167,71:155,72:264,73:200,74:200,75:240,76:225,77:310,78:180,79:160,80:82,81:115,82:128,83:141,84:143,85:150,86:82,87:155,88:157,89:159,90:164,91:102,92:171,93:175,94:180,95:184,96:96,97:191,98:193,99:195,100:208}
// rank_play_config:{"id": 1, "rank1": 50, "level_start": 20, "on": true, "exclude_levels": "21,22,23,27,33,35,37,42,44,45,47,55,56,59,60,80,81,86,91,96"}