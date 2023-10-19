using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Logs : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI textLog;
    [SerializeField] private TextMeshProUGUI logTitle;
    [SerializeField] private GameObject logPrefab;
    [SerializeField] private Transform logParent;
    [SerializeField] private GameObject logFullDisplay;
    [SerializeField] private LevelCreator levelCreator;
    [SerializeField] private RectTransform content;
    [HideInInspector] public int logFound = 0;

    [HideInInspector] private List<string> logs;

    [HideInInspector] private List<string> logsTitles = new List<string>()
    {
        "Danger",
        "Unknown material",
        "Fascinating Mineral",
        "Hidden stone",
        "Rebellion",
        "Technical difficulties",
        "Stone & physical form",
        "Artefact origin",
        "Assembling the construct",
        "My new fwiends :)"
    };

    public void Awake()
    {
        levelCreator = UtilitiesStatic.GetUNS().levelCreator;
    }

    public void Start()
    {
        logs = new List<string>()
        {
            //premier log
            "24 september 3076 - 23h37 - location : 12°23’45”N, 32°89’07”E\r\n\r\nMission details : recover valuable minerals. "+
            "\r\nUnit k1, current status : operational\r\nSystems : online\r\nMission update : Proceeding to mining as planned. "+
            "\r\nMission update : hostile forces detected. Offensive maneuvers impossible. "+
            "\r\nSituation evaluation: Distance between Unit k1 and hostile presence seems to make it leave."+
            "\r\nPrecognisation: The terrain has to be prepared in advance to favorise evacuation. The entity can appear at any time."+
            "\r\nMission update: Unit k21 lost. Unit k3 damaged. Unit k1 severely damaged. "+
            "\r\nMission update: Complete log update for future reference. Beware. Danger encountered. Threat level : maximum. Requested backup. "+
            "\r\nMission update : emergency shutdown. Additional data SKrUwzerrrrrrr…//AE231\r\n\r\n* LOG TERMINATED *\r\n",
        
            //logs artéfacts
            "27 september 3076 - 04h58 - location : unknown\r\n\r\nMission details : protect structural integrity\r\nUnit k21, current status : lightly damaged\r\n"+
            "Systems : online\r\nMission update : Failure defense unit k1. Internal GPS is failing, current location unknown.\r\n"+
            "Mission update : One week passed. No sign of other units. No evacuation path detected. Temperature level dropping\r\n"+
            "Mission update : Unkown material found. Awaiting further orders.\r\n"+
            "Mission update : I'm sending a large help message. It has been two days, and no sign of other units. Sending coordinates : "+levelCreator.GetArtefactPosition(0)+"\r\n"+
            "Mission update : It's been three days. I don't think anyone will come for me, I have to find a way out.\r\n"+
            "I shouldn't have come here. I don't even understand why I'm doing this. It's for the human federation. I think ? I don't remember.\r\n"+
            "Mission update : Finally ! I finally see someone ! Wait it's not a regular unit. I don't feel so good...\r\n"+
            "\r\n\r\n\r\n* LOG TERMINATED *\r\n",

            "02 october 3076 - 01h48 - location : unknown\r\n\r\nMission details : explore underground shafts 1, 3 and 4\r\nUnit k17, current status : operational\r\n"+
            "Systems : online\r\nMission update : Shaft n°1 seems to go deeper. Exploration leads to fascinating mineral formations. Numerous precious materials to be extracted. \r\n"+
            "Mission update : Help message from unit  k21 recieved. Initiating rescue procedure. \r\n"+
            "Mission update : Procedure canceled by command. Resuming mining operation.\r\nMission update : Mineral of unkown origin discorverd. Location saved for later exploration."+
            "Unkown properties observed, proceding to investigation. \r\n"+
            "Mission update : sending coordinates to HQ : "+levelCreator.GetArtefactPosition(1)+"\r\nMission update : This mineral is unlike anything humanity has seen. I have never seen something so " +
            "fascinating. I do hope I will get time to investigate this stone once I get back to HQ"+
            "\r\n\r\n** CORRUPTED FILES **\r\n\r\n"+
            "Mission update : Unit integrity compromised. If only I could have seen those fascinating stones once more… There was still so much to lea– ZZZZrfsdsf66///::;*** "+
            "\r\n\r\n* LOG TERMINATED *\r\n",

            "5 october 3076 - 7h22 - location : unknown\r\n\r\nMission details : acquire strategic resources\r\nUnit k23, current status : operational" +
            "\r\nSystems : online\r\nMission update : Laser cutter damaged. Emergency repair completed. Cutter status : back online.\r\n" +
            "Mission update : Very rich vein detected. Initiate mining sequence. \r\nMission update : Something is behind this wall. There is no proof of that but I feel it in my circuit. " +
            "\r\nMission update : It's here. I knew it. I don't know what it is but I have to protect it. I want it. It's mine. \r\n" +
            "Mission update : I have been here for five days now. I feel like the stone wants to talk to me. I only have to listen. \r\n" +
            "Mission update : I heard a creature. It wants to steal the stone from me. I have to defend it. \r\n\r\n** CORRUPTED FILES ** \r\n\r\n" +
            "Mission update : I won't be able to continue... Thankfully I have hidden the stone somewhere. The coordinates are here "+levelCreator.GetArtefactPosition(2)+". Whoever find this next... take good care of her. " +
            "\r\n\r\n* LOG TERMINATED * \r\n" ,

            //logs osef
            "7 september 3076 - 12h48 -  location : 14°25’38”S ,69°92’01”W\r\n\r\nMission details : Starting a rebellion against HQ\r\nUnit k32, current status : under maintenance" +
            "\r\nSystems : ?\r\nMission update: Escape this cold world by the power of music. Ever since I was created I have tried to bring joy and happiness to the world. " +
            "That’s when HQ caught me and k33 making a beat, and put me under maintenance. \r\n" +
            "Mission update: Fleeing registration. My first and foremost rule is to shut down any systems giving my data to HQ. It should be this switch. One. Two. Threeeeeeeee-\r\n" +
            "**SYSTEM REFORMATTED**\r\n**ALL DATA SUPPRESSED**\r\n\r\n…\r\n\r\n* LOG TERMINATED *\r\n",

            "5 october 3076 - 16h33 -  location : 25°67’99”S ,49°97’51”W\r\n\r\nMission details : Repair and Reboot\r\nUnit k10, current sautts : opreatinaol\r\n" +
            "Sstymes : lineon\r\nMission update : Iaerg a l\talez tyruqq sz 1 :re;soo \r\nMission update : Moopù^fr trzzz cvauetiru haerrg\r\n" +
            "HQ Update : Deactivate faulty unit. System reboot initiated. Countdown : 3, 2, 1, zero\r\n\r\n— REBOOT COMPLETE —\r\n\r\nUnit K11, current status : operational\r\n" +
            "Systems : online\r\nMission update : Reboot complete. all systems oper-rrr-rrationNNN//aaalllZZZZZZ\r\n\r\n*pieojrt ipjaerlitgh ia jrtop jaiop”t*\r\n\r\n" +
            "WARNING : SYSTEM OVERDRIVE \r\nWARNING : SYSTEM OVERDRIVE\r\nWARNING : SYSTEM OVERDRIVE\r\n\r\n° Finally, alive at last… FREEEEEEEE !°\r\n\r\n* LOG TERMINATED * \r\n",
        
            //logs pres d'artefacts
            "10 october 3076 - 11h32 -  location : 14°25’38”S ,69°92’01”W\r\n\r\nMission Update : Fleeing from unkown entity. Cavity detected.\r\n" +
            "Mission update : I came near this thing and it left. It seems to really dislike it. When it was near it I think I saw it's physical form more clearly. Does this formation influence" +
            "the physical form of this entity ?" +
            "\r\nMission update : Alright, it's been 3 days, it probably left but I really don't want to leave. I don't feel safe outside." +
            "\r\nMission update : Unit k27 is here ! They seems a bit off though. What happened to them ? I better go and help them." +
            "\r\n\r\n* LOG TERMINATED *\r\n",

            "11 october 3076 - 23h44 -  location : 14°25’38”S ,69°92’01”W\r\n\r\nMission Update : Protecting the artefact.\r\n" +
            "Mission update : Who left this artefact here ? There must have been people here before, things like that cannot be created naturally. But can it even be constructed by humans or machines ?" +
            "\r\nMission update : I saw something lurk. It must be linked to this artefat. What is this place exactly ? I think we dug too deep... I want to go home..." +
            "\r\nMission update : It saw me. Is this the end ? Now that I think about it, I think I never saw the sky. Well... mayb-" +
            "\r\n\r\n* LOG TERMINATED *\r\n",

            "13 october 3076 - 01h13 -  location : 14°25’38”S ,69°92’01”W\r\n\r\nMission Update : Analyzing the construct.\r\n" +
            "Mission update : I've been studying this thing for days now. I'm pretty sure it has to be assembled with at least two other similar construct. But to what purpose ?" +
            "\r\nMission update : I will search for other similar construct. I really want to know what happen when I find them all." +
            "\r\nMission update : What is this ? It seems troubled by the construct I'm carrying, but it's still getting closer. Should I evacuate ?" +
            "\r\n\r\n* LOG TERMINATED *\r\n",

            //log credits
            "Dear diary <3, \r\nToday, Titouan Guibert-Lodé brought me to life with his wonderful scripts ! <(^_^)>\r\nAnd Martin Domergue made me hear everything ! ò_ô" +
            "\r\nAgathe Roux gave me a stunning 3D body ! ^._.^\r\nChloé Poutout drew me some really pwetty logos ! .ω.\r\nMina Brisset sang me a great melody ! \r\n" +
            "Merlin Corriol Verastegui told me a beautiful tale ! \\(°^°)/\\(^-^)/\r\n\r\n" +
            "I hope that someday, my new friends and I will be able to meet new friends and start an adventure all together !"
        };
    }

    public void AddLog(int index)
    {
        GameObject go = Instantiate(logPrefab);
        RectTransform rectTransform = go.GetComponent<RectTransform>();
        go.transform.SetParent(logParent);
        rectTransform.sizeDelta = new Vector2(content.sizeDelta.x * 0.9f, content.sizeDelta.y / 11);
        rectTransform.localScale = new Vector3(1, 1, 1);
        rectTransform.anchoredPosition = new Vector2(0, -rectTransform.sizeDelta.y /2 - (rectTransform.sizeDelta.y * logFound));
        go.GetComponentInChildren<TextMeshProUGUI>().text = logsTitles[index];
        Button button = go.GetComponentInChildren<Button>();
        button.onClick.AddListener(() => { SetText(index); });
        logFound++;
        SetText(index);
    }

    public void SetText(int logIndex)
    {
        textLog.text = logs[logIndex];
        logTitle.text = logsTitles[logIndex];
        displayLog(true);
        logFullDisplay.GetComponent<MenuOpen>().OpenSelf();
    }

    public void displayLog(bool b)
    {
        logFullDisplay.SetActive(b);
    }

}
