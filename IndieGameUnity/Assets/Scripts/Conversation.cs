using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class Conversation : MonoBehaviour
{
    public Text text;
    public string textpath;
    private List<string> paragraphs;
    private int paragraphIDX;
    private bool currentlyReading;
    bool continuous;
    private float talkspeed; //  == waiting betweeen words
    public GameObject canvas;
    private VoiceOver voice;
    public bool textOver; // true when last line is read

    private void Start()
    {
        voice = GetComponent<VoiceOver>();
        canvas = transform.Find("Canvas").gameObject;
        canvas.SetActive(false);
        //Reset();
    }
    // Start is called before the first frame update
    public void Reset()
    {
        canvas.SetActive(false);
        StopAllCoroutines();
        talkspeed = 0.03f;
        textOver = false;
        paragraphIDX = 0;
        paragraphs = new List<string>();
        ReadTextFile(textpath);//+"/test.txt");
        text.text = "";
        currentlyReading = false;
    }
    public void StartNew(string file_path){
        textpath= file_path;
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReadNextLine(bool c)
    {
        continuous = c;
        canvas.SetActive(true);
        if (textOver) return;
        ReadLine();
        paragraphIDX++;
    }

    void ReadLine()
    {
        if (paragraphIDX >= paragraphs.Count) {
            text.text = ""; textOver = true;
            canvas.SetActive(false);

            return; }
        if (currentlyReading)
            Abort();
        {
            StopAllCoroutines();
            StartCoroutine(
                ReadSlowly(paragraphs[paragraphIDX], talkspeed));
        }
    }

    void Abort()
    {
        StopAllCoroutines();
        text.text = paragraphs[paragraphIDX-1];
    }

    IEnumerator ReadSlowly(string t, float time)
    {
        currentlyReading = true;
        string subtext = "";
        for (int i =0; i < t.Length; i++)
        {
            subtext += t[i];
            text.text = subtext;
            voice.PlayLetter(char.ToUpper(t[i]));
            yield return new WaitForSeconds(time);
        }
        currentlyReading = false;
        if (continuous)
        {
            yield return new WaitForSeconds(2);
            ReadNextLine(true);
        }
    }

    void ReadTextFile(string file_path)
    {
        if (file_path == null || file_path == "") return;
        StreamReader inp_stm = new StreamReader(file_path);

        while (!inp_stm.EndOfStream)
        {
            string inp_ln = inp_stm.ReadLine();
            paragraphs.Add(inp_ln);
        }

        inp_stm.Close();
    }

}
