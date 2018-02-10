using Prism.Interactivity.InteractionRequest;

namespace phirSOFT.IoInteraction
{
    public class OpenFileInteractionNotification : Confirmation
    {
        public string InitialName { get; internal set; }
        public string Filter { get; internal set; }
        public string[] FileNames { get; internal set; }
        public int FilterIndex { get; internal set; }
        public string InitialDirectory { get; internal set; }
        public bool Multiselect { get; internal set; }
    }
}