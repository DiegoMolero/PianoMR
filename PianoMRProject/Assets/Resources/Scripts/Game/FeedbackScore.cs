using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackScore : MonoBehaviour {
    public int Score;
    public int Combo;
    public int ConsecutiveHits;
    public TextMesh TxtCombo;
    public TextMesh TxtTotalScore;
    public TextMesh TxtScore;
    private AudioSource audioSource;
    [Header("Audio Clips")]
    public List<AudioClip> comboAudio;

    private GameObject aux;

    // Use this for initialization
    void Start () {
        Score = 0;
        ConsecutiveHits = 0;
        Combo = 1;
        updateLabels();
        audioSource = GetComponent<AudioSource>();
        aux = null;
    }
	// Update is called once per frame
	void Update () {		
	}

    public int IncreaseScore()
    {
        ConsecutiveHits++;
        updateCombo();
        int points = ConsecutiveHits * Combo;
        Score += points;
        updateScoreLabel(points);
        updateLabels();
        return Score;
    }

    public int DecreaseScore()
    {
        ConsecutiveHits = 0;
        Combo = 1;
        updateLabels();
        updateScoreLabel(ConsecutiveHits);
        return Score;
    }

    private void updateCombo()
    {
        int oldCombo = Combo;
        switch (ConsecutiveHits)
        {
            case 5:
                Combo = 2;
                break;
            case 10:
                Combo = 3;
                break;
            case 15:
                Combo = 4;
                break;
            case 20:
                Combo = 5;
                break;
            case 25:
                Combo = 6;
                break;
        }
        if (oldCombo != Combo) playAudioFeedback();
    }

    private void updateLabels()
    {
        TxtTotalScore.text = "Score "+Score;
        TxtCombo.text = "x" + Combo;
    }

    private void updateScoreLabel(int points)
    {
        TxtScore.text = "+ " + points;
    }
    private void playAudioFeedback()
    {
        switch (Combo)
        {
            case 2:
                audioSource.PlayOneShot(comboAudio[0],0.15f);
                break;
            case 3:
                audioSource.PlayOneShot(comboAudio[1], 0.15f);
                break;
            case 4:
                audioSource.PlayOneShot(comboAudio[2], 0.15f);
                break;
            case 5:
                audioSource.PlayOneShot(comboAudio[3], 0.15f);
                break;
            case 6:
                audioSource.PlayOneShot(comboAudio[4], 0.15f);
                break;
        }
    }

}
