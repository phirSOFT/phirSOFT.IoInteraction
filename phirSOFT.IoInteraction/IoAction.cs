using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;
using Microsoft.Win32;
using Prism.Interactivity.InteractionRequest;

namespace phirSOFT.IoInteraction
{
    public class IoAction : TriggerAction<FrameworkElement>
    {
        public static readonly DependencyProperty AddExtensionProperty = DependencyProperty.Register(
            "AddExtension", typeof(bool), typeof(IoAction), new PropertyMetadata(true));

        public static readonly DependencyProperty CheckFileExistsProperty = DependencyProperty.Register(
            "CheckFileExists", typeof(bool), typeof(IoAction), new PropertyMetadata(true));

        public static readonly DependencyProperty CheckPathExistsProperty = DependencyProperty.Register(
            "CheckPathExists", typeof(bool), typeof(IoAction), new PropertyMetadata(true));

        public static readonly DependencyProperty DefaultExtensionProperty = DependencyProperty.Register(
            "DefaultExtension", typeof(string), typeof(IoAction), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty DereferenceLinksProperty = DependencyProperty.Register(
            "DereferenceLinks", typeof(bool), typeof(IoAction), new PropertyMetadata(true));

        public static readonly DependencyProperty ValidateNamesProperty = DependencyProperty.Register(
            "ValidateNames", typeof(bool), typeof(IoAction), new PropertyMetadata(default(bool)));

        public static readonly DependencyProperty CreatePromptProperty = DependencyProperty.Register(
            "CreatePrompt", typeof(bool), typeof(IoAction), new PropertyMetadata(default(bool)));

        public static readonly DependencyProperty OverwritePromptProperty = DependencyProperty.Register(
            "OverwritePrompt", typeof(bool), typeof(IoAction), new PropertyMetadata(default(bool)));

        public string DefaultExtension
        {
            get => (string) GetValue(DefaultExtensionProperty);
            set => SetValue(DefaultExtensionProperty, value);
        }

        public Collection<FileDialogCustomPlace> CustomPlaces { get; } = new Collection<FileDialogCustomPlace>();

        public bool OverwritePrompt
        {
            get => (bool) GetValue(OverwritePromptProperty);
            set => SetValue(OverwritePromptProperty, value);
        }

        public bool CreatePrompt
        {
            get => (bool) GetValue(CreatePromptProperty);
            set => SetValue(CreatePromptProperty, value);
        }

        public bool ValidateNames
        {
            get => (bool) GetValue(ValidateNamesProperty);
            set => SetValue(ValidateNamesProperty, value);
        }

        public bool DereferenceLinks
        {
            get => (bool) GetValue(DereferenceLinksProperty);
            set => SetValue(DereferenceLinksProperty, value);
        }

        public bool CheckPathExists
        {
            get => (bool) GetValue(CheckPathExistsProperty);
            set => SetValue(CheckPathExistsProperty, value);
        }

        public bool CheckFileExists
        {
            get => (bool) GetValue(CheckFileExistsProperty);
            set => SetValue(CheckFileExistsProperty, value);
        }

        public bool AddExtension
        {
            get => (bool) GetValue(AddExtensionProperty);
            set => SetValue(AddExtensionProperty, value);
        }


        protected override void Invoke(object parameter)
        {
            if (!(parameter is InteractionRequestedEventArgs args))
                return;

            FileDialog dialog;
            switch (args.Context)
            {
                case OpenFileInteractionNotification ofn:
                    dialog = new OpenFileDialog
                    {
                        AddExtension = AddExtension,
                        CheckFileExists = CheckFileExists,
                        CheckPathExists = CheckPathExists,
                        CustomPlaces = CustomPlaces,
                        DefaultExt = DefaultExtension,
                        DereferenceLinks = DereferenceLinks,
                        FileName = ofn.InitialName,
                        Filter = ofn.Filter,
                        FilterIndex = ofn.FilterIndex,
                        InitialDirectory = ofn.InitialDirectory,
                        Multiselect = ofn.Multiselect,
                        Title = ofn.Title,
                        ValidateNames = ValidateNames
                    };

                    Task.Run(() =>
                    {
                        ofn.Confirmed = (bool) dialog.ShowDialog(Window.GetWindow(AssociatedObject));
                        if (ofn.Confirmed)
                            ofn.FileNames = dialog.FileNames;
                        args.Callback?.Invoke();
                    });

                    break;
                case SaveFileInteractionNotification sfn:

                    dialog = new SaveFileDialog
                    {
                        AddExtension = AddExtension,
                        CheckFileExists = CheckFileExists,
                        CheckPathExists = CheckPathExists,
                        CreatePrompt = CreatePrompt,
                        CustomPlaces = CustomPlaces,
                        DefaultExt = DefaultExtension,
                        DereferenceLinks = DereferenceLinks,
                        FileName = sfn.FileName,
                        Filter = sfn.Filter,
                        FilterIndex = sfn.FilterIndex,
                        InitialDirectory = sfn.InitialDirectory,
                        OverwritePrompt = OverwritePrompt,
                        Title = sfn.Title,
                        ValidateNames = ValidateNames
                    };

                    Task.Run(() =>
                    {
                        sfn.Confirmed = (bool) dialog.ShowDialog(Window.GetWindow(AssociatedObject));
                        if (sfn.Confirmed)
                            sfn.FileName = dialog.FileName;
                        args.Callback?.Invoke();
                    });

                    break;
                default:
                    return;
            }
        }
    }
}