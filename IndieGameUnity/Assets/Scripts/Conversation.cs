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
    public bool continuous;
    private float talkspeed; //  == waiting betweeen words
    public GameObject canvas;

    public bool textOver; // true when last line is read

    private void Start()
    {
        canvas = transform.Find("Canvas").gameObject;
        Reset();
        continuous = false;
    }
    // Start is called before the first frame update
    public void Reset()
    {
        canvas.SetActive(false);
        StopAllCoroutines();
        talkspeed = 0.01f;
        textOver = false;
        paragraphIDX = 0;
        paragraphs = new List<string>();
        ReadTextFile(textpath);//+"/test.txt");
        text.text = "";
        currentlyReading = false;
    }
    public void StartNew(string file_path){
        file_path = textpath;
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space)) ReadNextLine();
        
    }

    public void ReadNextLine()
    {
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
        StartCoroutine(
            ReadSlowly(paragraphs[paragraphIDX], talkspeed)); 
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
            yield return new WaitForSeconds(time);
        }
        currentlyReading = false;
        if (continuous)
        {
            yield return new WaitForSeconds(0.5f);
            ReadNextLine();
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
