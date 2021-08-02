using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlay : MonoBehaviour
{
    //����� �÷���//

    [SerializeField] AudioSource BGMSource, SFXSource;
    [SerializeField] AudioClip[] BGMClip, SFXClip;

    //�׽�Ʈ��...
    int b_play = 0, s_play = 0;

    public void BGMPlay(int audioID)
    {
        BGMSource.Stop();
        BGMSource.clip = BGMClip[audioID + b_play];
        BGMSource.loop = true;
        BGMSource.Play();

        b_play = b_play < 2 ? b_play + 1 : 0; //�׽�Ʈ��!! ��������
    }

    public void SFXPlay(int audioID)
    {
        SFXSource.PlayOneShot(SFXClip[audioID + s_play]);

        s_play = s_play < 2 ? s_play + 1 : 0; //�׽�Ʈ��!! ��������
    }
}
