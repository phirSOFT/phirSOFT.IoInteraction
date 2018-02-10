using System.Collections.Generic;
using Prism.Interactivity.InteractionRequest;

namespace phirSOFT.IoInteraction
{
    public class OpenFileInteractionNotification : Confirmation
    {
        public string InitialName { get; set; }
        public string Filter { get; set; }
        public IEnumerable<string> FileNames { get; set; }
        public int FilterIndex { get; set; }
        public string InitialDirectory { get; set; }
        public bool Multiselect { get; set; }
    }
}