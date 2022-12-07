# Quarantine at Home
<div>
    <h2> 게임 정보 </h2>
    <img src = "https://img.itch.zone/aW1nLzEwMTQ1MzM4LnBuZw==/347x500/fmXF5A.png"><br>
    <img src="https://img.shields.io/badge/Unity-yellow?style=flat-square&logo=Unity&logoColor=FFFFFF"/>
    <img src="https://img.shields.io/badge/Clicker-gray"/>
    <h4> 개발 일자 : 2022.09 <br><br>
    플레이 : https://goodstarter.itch.io/quarantine-at-home
    
  </div>
  <div>
    <h2> 게임 설명 </h2>
    <h3> 스토리 </h3>
     코로나 19 확진 판정을 받은 주인공은 자가 격리를 하라는 통보를 받는다.<br><br>
     일주일 간의 자가 격리 기간 동안 다양한 취미 생활을 즐기며<br><br>
     빨리 자가 격리가 끝나기를 기다린다.
    <h3> 게임 플레이 </h3>
     이 게임은 마우스 클릭으로 진행하는 클리커 게임으로 <br><br>
     클릭을 통해 시간을 보내면서 경험치를 얻고<br><br>
     경험치로 레벨을 올려 다양한 취미 활동을 해금하고 새로운 능력을 얻어<br><br>
     자가 격리 기간인 일주일을 보내는 것이 목표이다.
  </div> 
  <div>
    <h2> 게임 스크린샷 </h2>
      <table>
        <td><img src = "https://img.itch.zone/aW1hZ2UvMTcyMTk0Mi8xMDE3ODIzMy5wbmc=/347x500/HZXSXh.png"></td>
        <td><img src = "https://img.itch.zone/aW1hZ2UvMTcyMTk0Mi8xMDE3ODIzNC5wbmc=/347x500/JO0Ivk.png"></td>
        <td><img src = "https://img.itch.zone/aW1hZ2UvMTcyMTk0Mi8xMDE3ODIzNS5wbmc=/347x500/L79u2g.png"></td>
      </table>
  </div>
    <div>
    <h2> 게임 플레이 영상 </h2>
    https://youtu.be/59yCcC-kM4E
  </div>
  <div>
    <h2> 배운 점 </h2>
      처음으로 원래 기획만큼 게임을 제작했다.<br><br>
      물론 기간이 조금 길어지긴 했지만 아이디어를 완전히 구현해보는 경험이 되었다.<br><br>
      json을 활용한 데이터 저장 불러오기 기능을 구현하며 System IO에 대해 알게 되었다.<br><br>
      또한 파일을 로드할 때 생기는 부하를 고려한 코루틴을 활용한 애니메이션을 만들고 이를 모듈화 해보았다.<br><br>
  </div>
  <div>
    <h2> 수정할 점 </h2>
      왼쪽 편에 활동에 따른 애니메이션을 추가한다.
   <h2> Design Picture </h2>
   <table>
        <td><img src = "https://postfiles.pstatic.net/MjAyMjEwMDJfMjk2/MDAxNjY0Njc2MzY5OTc5.e0zxnwIelrGkbocZWSokgdMbbhFBPVnP0MyToqpH3eYg.3J6IUib14K1U9JXzR-GFlaajXtqsRauJZSh_RKb8XUkg.JPEG.tdj04131/20221002%EF%BC%BF110334.jpg?type=w773" height = 500></td>
      </table>
  </div>

   <div>
       <h2> 주요 코드 </h2>
       <h4> BGM Load 하는 코드 </h4>
    </div>
    
```csharp
public IEnumerator MainBGMLoad()
{
    //Main
    mainBGMList.AddRange(Resources.LoadAll<AudioClip>("Music/Main/"));

    //Mix
    mainBGMList = mainBGMList.OrderBy(a => Guid.NewGuid()).ToList();

    yield return new WaitForEndOfFrame();
}

public IEnumerator SideBGMLoad()
{
    for (int i = 0; i < 6; i++)
    {
        sideBGMList[i] = new List<AudioClip>();
        sideBGMList[i].AddRange(Resources.LoadAll<AudioClip>("Music/Side/" + i + "/"));
    }
    yield return new WaitForEndOfFrame();
}
``` 

<div>
<h4> 데이터 저장/불러오기 </h4>
</div>

```csharp
static public void SaveIntoJson(QuarantineData quarantineData)
{
    QuarantineData saveData = quarantineData;
    string quarantine = JsonUtility.ToJson(quarantineData);
    File.WriteAllText("./Assets/Save/" + "SaveData.json", quarantine);
}

static public QuarantineData LoadFromJson()
{
    try
    {
        string path = "./Assets/Save/SaveData.json";
        Debug.Log(path);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Debug.Log(json);
            QuarantineData qd = JsonUtility.FromJson<QuarantineData>(json);
            return qd;
        }
    }
    catch (FileNotFoundException e)
    {
        Debug.Log("The file was not found:" + e.Message);
    }
    catch (DirectoryNotFoundException e)
    {
        Debug.Log("The directory was not found: " + e.Message);
    }
    catch (IOException e)
    {
        Debug.Log("The file could not be opened:" + e.Message);
    }
    return default;
}
```
