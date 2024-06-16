using System;
using System.Media;

namespace phantom_field.sounds
{
    public class Audio
    {
        static public SoundPlayer soundPlayer = new SoundPlayer();

        static public void playClick()
        {
            soundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "\\Audio\\click.wav";
            soundPlayer.Play();
        }

        static public void playStart()
        {
            soundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "\\Audio\\start.wav";
            soundPlayer.Play();
        }

        static public void playWin()
        {
            soundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "\\Audio\\win.wav";
            soundPlayer.Play();
        }

        static public void playLose()
        {
            soundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "\\Audio\\lose.wav";
            soundPlayer.Play();
        }
    }
}
