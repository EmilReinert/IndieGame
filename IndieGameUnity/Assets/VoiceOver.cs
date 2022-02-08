using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceOver : MonoBehaviour
{
    private AudioSource audi;
    public AudioClip A;
    public AudioClip B;
    public AudioClip C;
    public AudioClip D;
    public AudioClip E;
    public AudioClip F;
    public AudioClip G;
    public AudioClip H;
    public AudioClip I;
    public AudioClip J;
    public AudioClip K;
    public AudioClip L;
    public AudioClip M;
    public AudioClip N;
    public AudioClip O;
    public AudioClip P;
    public AudioClip Q;
    public AudioClip R;
    public AudioClip S;
    public AudioClip T;
    public AudioClip U;
    public AudioClip V;
    public AudioClip W;
    public AudioClip X;
    public AudioClip Y;
    public AudioClip Z;

    private void Start()
    {
        audi = GetComponent<AudioSource>();
    }

    public void PlayLetter(char _)
    {
        switch (_)
        {
            case 'A': PlayAudio(A); break;
            case 'B': PlayAudio(B); break;
            case 'C': PlayAudio(C); break;
            case 'D': PlayAudio(D); break;
            case 'E': PlayAudio(E); break;
            case 'F': PlayAudio(F); break;
            case 'G': PlayAudio(G); break;
            case 'H': PlayAudio(H); break;
            case 'I': PlayAudio(I); break;
            case 'J': PlayAudio(J); break;
            case 'K': PlayAudio(K); break;
            case 'L': PlayAudio(L); break;
            case 'M': PlayAudio(M); break;
            case 'N': PlayAudio(N); break;
            case 'O': PlayAudio(O); break;
            case 'P': PlayAudio(P); break;
            case 'Q': PlayAudio(Q); break;
            case 'R': PlayAudio(R); break;
            case 'S': PlayAudio(S); break;
            case 'T': PlayAudio(T); break;
            case 'U': PlayAudio(U); break;
            case 'V': PlayAudio(V); break;
            case 'W': PlayAudio(W); break;
            case 'X': PlayAudio(X); break;
            case 'Y': PlayAudio(Y); break;
            case 'Z': PlayAudio(Z); break;
            default:
                break;
        }
    }

    void PlayAudio(AudioClip a)
    {
        audi.PlayOneShot(a);
    }
}
