namespace JGM.Game
{
    public interface IAudioService
    {
        void Play(string audioFileName, bool loop = false);
        void Stop(string audioFileName);
        bool IsPlaying(string audioFileName);
        void SetVolume(string audioFileName, float volume);
    }
}