using System;
using System.Media;

namespace phantom_field.sounds
{
    public class Audio
    {
        static public SoundPlayer soundPlayer = new SoundPlayer();

        static public void playClick()
        {
            soundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "\\sounds\\click.wav";
            soundPlayer.Play();
        }

        static public void playStart()
        {
            soundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "\\sounds\\start.wav";
            soundPlayer.Play();
        }

        static public void playWin()
        {
            soundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "\\sounds\\win.wav";
            soundPlayer.Play();
        }

        static public void playLose()
        {
            soundPlayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "\\sounds\\lose.wav";
            soundPlayer.Play();
        }
    }
}
