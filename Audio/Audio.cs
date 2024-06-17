using System;
using System.Media;

namespace phantom_field.sounds
{
    public class Audio
    {
        static public SoundPlayer soundPlayer = new SoundPlayer();

        static public void playBGM()
        {
            soundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "/Audio/bgm.wav";
            soundPlayer.PlayLooping();
        }

        static public void playClick()
        {
            soundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "/Audio/gushing_wind.wav";
            soundPlayer.Play();
        }

        static public void playStart()
        {
            soundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "/Audio/door_open.wav";
            soundPlayer.Play();
        }

        static public void playWin()
        {
            soundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "/Audio/phantom_exorcized.wav";
            soundPlayer.Play();
        }

        static public void playLose()
        {
            soundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "/Audio/phantom_laugh.wav";
            soundPlayer.Play();
        }
    }
}
