using Prism.Interactivity.InteractionRequest;

namespace phirSOFT.IoInteraction
{
    public class SaveFileInteractionNotification : Confirmation
    {
        public string FileName { get; set; }
        public string Filter { get; set; }
        public int FilterIndex { get; set; }
        public string InitialDirectory { get; set; }
    }
}