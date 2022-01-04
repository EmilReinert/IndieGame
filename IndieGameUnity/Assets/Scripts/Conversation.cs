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

    // Start is called before the first frame update
    void Start()
    {
        paragraphIDX = 0;
        paragraphs = new List<string>();
        ReadTextFile(textpath);//+"/test.txt");
        text.text = "";
        currentlyReading = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ReadLine();
            paragraphIDX++;
        }
    }

    void ReadLine()
    {
        if (paragraphIDX >= paragraphs.Count) { text.text = ""; return; }
        if (currentlyReading)
            Abort();
        StartCoroutine(
            ReadSlowly(paragraphs[paragraphIDX], 0.1f));
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

    }

    void ReadTextFile(string file_path)
    {
        StreamReader inp_stm = new StreamReader(file_path);

        while (!inp_stm.EndOfStream)
        {
            string inp_ln = inp_stm.ReadLine();
            paragraphs.Add(inp_ln);
        }

        inp_stm.Close();
    }

}
