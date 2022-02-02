using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flute : Puzzle
{
    string[] notes; // types of note
    string[] song; // types of note
    int songline;
    int currentNote;

    public GameObject body;
    public GameObject notePrefab;
    public GameObject[] tubes;
    private List<Note> noteObjects;

    public ParticleSystem success;
    public ParticleSystem fail;


    int necessaryhits;
    public int currenthits;
    public int notecount;

    float moveSpeed = 0.33f;

    public override void StartPuzzle()
    {
        noteObjects = new List<Note>();
        notes = new string[] { "A", "B", "C", "D", "E" };
        song = new string[]
        { //mirrored!
            "01000", 
            "10000",
            "00100",
            "00010",
            "00100",
            "00100",
            "00010",
            "00001",
            "00010",
            "00100",
            "01000",
            "00100",
            "00010",
            "00100",
            "01000",
            "10000",
            "01000",
            "00100",
            "01000",
            "01000",
            "10000",
            "01000",
            "01000",
            "00100"

        };

        currentNote = 2; //C;
        songline = 0;

        UpdateNote(currentNote);
        foreach (Note n in
GameObject.FindObjectsOfType<Note>())
            Destroy(n.gameObject);

        CancelInvoke("CreateNote");
        InvokeRepeating("CreateNote", 1, 0.66f);// last one is playing speed
        currenthits = 0;
        notecount = 0;

        GameObject.FindObjectOfType<Walk>().Freeze(true);

        success.gameObject.SetActive(true); success.Stop();
        fail.gameObject.SetActive(true); fail.Stop();

        GameObject.Find("Player").GetComponentInChildren<Animator>().SetBool("Cfront", true);

    }

    public override void UpdatePuzzle()
    {

        //update note positions;
        for (int i = 0; i < noteObjects.Count; i++)
        {
            Note n = noteObjects[i];
            if (n == null) return;
            n.transform.position += n.transform.up * Time.deltaTime * moveSpeed;
            //success
            if (n.hit)
            {
                noteObjects.Remove(n);
                Destroy(n.gameObject);
                currenthits++;
                StartCoroutine(Hit());
            }
            //fail
            if (n.done)
            {
                noteObjects.Remove(n);
                Destroy(n.gameObject);
                StartCoroutine(Miss());
                faily = true;
            }
        }

        if(notecount>1&&notecount == song.Length)
        {
            // evaluate level todo
            done = true;
        }


    }

    public override void EndPuzzle()
    {
        GameObject.Find("Player").GetComponentInChildren<Animator>().SetBool("Cfront", false);
        GameObject.FindObjectOfType<Walk>().Freeze(false);
        gameObject.SetActive(false);
    }

    public override void Move(int i)
    {
        if (i == -1)
            UpdateNote(currentNote + 1);
        if (i == 1)
            UpdateNote(currentNote - 1);
    }

    void CreateNote()
    {
        if (songline > song.Length - 1) return;
        for(int i = 0; i < song[songline].Length; i++)
        {
            if (song[songline][i] == '1')
            {
                //making note at given 1 position
                GameObject t = tubes[i];
                Transform start = t.transform.Find("start");
                GameObject note = Instantiate(notePrefab);
                note.transform.parent = body.transform;
                note.transform.position = start.position;
                note.transform.rotation = start.rotation;
                noteObjects.Add(note.GetComponent<Note>());
            }
        }
        songline++;
    }

    void UpdateNote(int n)
    {
        currentNote = n;
        if (n < 0) { currentNote = 0; return; }
        if (n > notes.Length - 1) { n = notes.Length - 1; return; }

        foreach(GameObject t in tubes)
        {
            t.transform.Find("goal").gameObject.SetActive(false);
        }
        tubes[currentNote].transform.Find("goal").gameObject.SetActive(true);

    }
    
    IEnumerator Hit()
    {
        success.gameObject.SetActive(true);
        success.Play();
        yield return new WaitForSeconds(0.5f);
        success.Pause();
        success.gameObject.SetActive(false);
        notecount++;
    }
    IEnumerator Miss()
    {
        fail.gameObject.SetActive(true);
        fail.Play();
        yield return new WaitForSeconds(0.5f);
        fail.Pause();
        fail.gameObject.SetActive(false);
        notecount++;
    }
}
