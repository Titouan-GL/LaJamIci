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
    [HideInInspector] public int logFound = 0;

    [HideInInspector] private List<string> logs = new List<string>()
    {
        //premier log
        "24 september 3076 - 23h37 - location : 12°23’45”N, 32°89’07”E\r\n\r\nMission details : recover valuable minerals. \r\nUnit k1, current status : operational\r\nSystems : online\r\nMission update : Proceeding to mining as planned. \r\nMission update : obstacle encountered. Defensive mode enabled. Combat protocols established. \r\nMission update: Unit k21 lost. Unit k3 damaged. Unit k1 severely damaged. \r\nMission update: Complete log update for future reference. Beware. Danger encountered. Threat level : maximum. Requested backup. \r\nMission update : emergency shutdown. Additional data SKrUwzerrrrrrr…//AE231\r\n\r\n* LOG TERMINATED *\r\n",
        
        //logs artéfacts
        "02 october 3076 - 04h58 - location : unknown\r\n\r\nMission details : protect structural integrity\r\nUnit k21, current status : lightly damaged\r\nSystems : online\r\nMission update : Failure defense unit k1. Internal GPS is failing, current location unknown.\r\nMission update : One week passed. No sign of other units. Nothing leading to HQ location. It’s starting to get cold down here…\r\nMission update : Found shiny stuff. Maybe if I stay next to it, HQ will find me ? I can feel the hope running in my circuits.\r\nMission update : Sending large help message. It has been two days, and no sign of other units. Sending coordinates : (METS LES COORDONNÉES TROUDUC)\r\nMission update : Finally ! I finally see someone ! \r\nMission update : Strange. I don’t remember seeing this kind of unit before. Maybe a new model ? It’s getting a bit too close to me3JIF309J9JK///ZZZZZ-\r\n\r\n\r\n* LOG TERMINATED *\r\n",
        "27 september 3076 - 01h48 - location : unknown\r\n\r\nMission details : explore underground shafts 1, 3 and 4\r\nUnit k17, current status : operational\r\nSystems : online\r\nMission update : Shaft n°1 seems to go deeper. Exploration leads to fascinating mineral formations. Numerous precious materials to be extracted. \r\nMission update : Undergoing exploration of the cave. Terabytes of crucial data can be collected from this place. Proceed to information extraction and retrieval. \r\nMission update : The grotto leads to an unknown cache. Contents to be unveiled.\r\nMission update : Found interesting stone. Location saved for later exploration. Ressembles\r\na piece used in the completion of … maybe a ritual ? An artifact ? Theories are not proven yet. \r\nMission update : sending coordinates to HQ : (INSERT COORDINATES TITOUAN)\r\nMission update : HQ has declared crisis. Threat level : maximum. Engaging defense protocols\r\n\r\n** CORRUPTED FILES **\r\n\r\nMission update : Unit integrity compromised. If only I could have seen those fascinating stones once more… There was still so much to lea– ZZZZrfsdsf66///::;*** \r\n\r\n* LOG TERMINATED *\r\n",
        "5 october 3076 - 7h22 - location : unknown\r\n\r\nMission details : acquire strategic resources\r\nUnit k23, current status : operational\r\nSystems : online\r\nMission update : Laser cutter damaged. Emergency repair completed. Cutter status : back online.\r\nMission update : Very rich vein detected. Initiate mining sequence. \r\nMission update : Nothing to report. Mining is going as planned. Estimated completion time: 9 hours. \r\nMission update : While drilling into a rock wall, an undetected crevice has appeared. Commencing exploration. \r\nMission update : Item found within crevice. Nature : unidentified. Aspect: artifact. Secure and extract. \r\nMission update : Hostile forces encountered. Protect and defend item at all costs. \r\n\r\n** CORRUPTED FILES ** \r\n\r\nMission update : Outnumbered. Heavy damage sustained. Item has been hidden for extraction. Coordinates sent to HQ = (INSERT COORDINATES TITOUAN). Shutdown imminent. \r\n\r\n* LOG TERMINATED * \r\n" ,

        //logs osef
        "28 september 3076 - 12h48 -  location : 14°25’38”S ,69°92’01”W\r\n\r\nMission details : Starting a rebellion against HQ\r\nUnit k32, current status : under maintenance\r\nSystems : ?\r\nMission update: Escape this cold world by the power of music. Ever since I was created I have tried to bring joy and happiness to the world. That’s when HQ caught me and k33 making a beat in FLstudio34, and put me under maintenance. \r\nMission update: Fleeing registration. My first and foremost rule is to shut down any systems giving my data to HQ. It should be this switch. One. Two. Threeeeeeeee-\r\n**SYSTEM REFORMATTED**\r\n**ALL DATA SUPPRESSED**\r\n\r\n…\r\n\r\n* LOG TERMINATED *\r\n",
        "5 october 3076 - 16h33 -  location : 25°67’99”S ,49°97’51”W\r\n\r\nMission details : Repair and Reboot\r\nUnit k10, current sautts : opreatinaol\r\nSstymes : lineon\r\nMission update : Iaerg a l\talez tyruqq sz 1 :re;soo \r\nMission update : Moopù^fr trzzz cvauetiru haerrg\r\nHQ Update : Deactivate faulty unit. System reboot initiated. Countdown : 3, 2, 1, zero\r\n\r\n— REBOOT COMPLETE —\r\n\r\nUnit K11, current status : operational\r\nSystems : online\r\nMission update : Reboot complete. all systems oper-rrr-rrationNNN//aaalllZZZZZZ\r\n\r\n*pieojrt ipjaerlitgh ia jrtop jaiop”t*\r\n\r\nWARNING : SYSTEM OVERDRIVE \r\nWARNING : SYSTEM OVERDRIVE\r\nWARNING : SYSTEM OVERDRIVE\r\n\r\n° Finally, alive at last… FREEEEEEEE !°\r\n\r\n* LOG TERMINATED * \r\n",

        //log credits
        "Dear diary <3, \r\nToday, Titouan Guibert-Lodé brought me to life with his wonderful scripts ! <(^_^)>\r\nAgathe Roux gave me a stunning 3D body ! ^._.^\r\nMerlin Corriol Verastegui told me a beautiful tale ! \\(°^°)/\r\nChloé Poutout drew me some really pwetty logos ! .ω.\r\nMina Brisset sang me a great melody ! \\(^-^)/\r\nAnd Martin Domergue made me hear everything ! ò_ô\r\n\r\nI hope that someday, my new friends and I will be able to meet new friends and start an adventure all together !"
    };

    [HideInInspector] private List<string> logsTitles = new List<string>()
    {
        "Recon mission",
        "An artifact ?",
        "Ooh shiny :)",
        "Found a gem.",
        "Rebellion",
        "Technical difficulties",
        "My new fwiends :)"
    };

    public void AddLog(int index)
    {
        GameObject go = Instantiate(logPrefab);
        RectTransform rectTransform = go.GetComponent<RectTransform>();
        go.transform.SetParent(logParent);
        rectTransform.anchoredPosition = new Vector2(0, -35 - (40 * logFound));
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
    }

    public void displayLog(bool b)
    {
        logFullDisplay.SetActive(b);
    }

}
