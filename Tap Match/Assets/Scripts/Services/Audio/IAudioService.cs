namespace JGM.Game
{
    public interface IAudioService
    {
        void Play(string audioFileName, bool loop = false);
    }
}