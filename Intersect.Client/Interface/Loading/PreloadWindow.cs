using Gtk;
using Pango;
using System;
using System.Threading.Tasks;

namespace Intersect.Client.Interface.Loading
{
    public class PreloadWindow
    {
        private Window _window;
        private ProgressBar _progressBar;
        private FontDescription _fontDescription = new FontDescription
        {
            Family = "Consolas, Arial, Sans-Serif",
            Size = (int)(14 * Pango.Scale.PangoScale),
            Weight = Weight.Ultrabold
        };

        public PreloadWindow()
        {
            Application.Init();
            _window = new Window("Middle Ages: Online");
            _window.SetDefaultSize(480, 360);
            _window.DeleteEvent += delegate { Application.Quit(); };
            Gdk.Color bgColor = new Gdk.Color(0, 38, 45);
            _window.ModifyBg(StateType.Normal, bgColor);
            _window.Icon = new Gdk.Pixbuf("resources/misc/Icon.ico");

            var vbox = new VBox();
            var logo = new Image("resources/misc/MAO_icon_text.png");
            var label = new Label("Loading, please wait...");
            _progressBar = new ProgressBar();

            // Set text color
            label.ModifyFg(StateType.Normal, new Gdk.Color(200, 145, 62));

            var attrList = new AttrList();
            attrList.Insert(new AttrFontDesc(_fontDescription));
            label.Attributes = attrList;

            vbox.PackStart(logo, true, true, 0);
            vbox.PackStart(label, true, true, 0);
            vbox.PackStart(_progressBar, true, true, 0);

            _window.Add(vbox);

            // Center the window
            _window.SetPosition(WindowPosition.Center);

            // Remove close, minimize, and maximize buttons
            _window.Decorated = false;

            _window.ShowAll();

            _window.DeleteEvent += _window_DeleteEvent;
        }

        private void _window_DeleteEvent(object o, DeleteEventArgs args)
        {
            Environment.Exit(0);
        }

        public void Show()
        {
            Task.Run(() => Application.Run());
            Task.Run(() => AnimateProgressBar());
        }

        private void AnimateProgressBar()
        {
            while (true)
            {
                Application.Invoke(delegate
                {
                    _progressBar.Pulse();
                });
                System.Threading.Thread.Sleep(100); // Adjust the speed of the animation if needed
            }
        }

        public void Close()
        {
            Application.Invoke(delegate
            {
                _window.DeleteEvent -= _window_DeleteEvent;
                _window.Destroy();
                Application.Quit();
            });
        }
    }
}
