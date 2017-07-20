namespace RdcManDemoPlugin
{
    // Assembly name needs to be of format "Plugin.*" as RDCMan looks in the RDCMan.exe directory for dll's of the format "Plugin.*.dll" to attempt to load
    using System;
    using System.ComponentModel.Composition;
    using System.Windows.Forms;
    using RdcMan;
    using RdcManDemoPlugin.Interfaces.Models;
    using RdcManDemoPlugin.Models;
    using RdcManPluginFix;
    using RdcManPluginFix.Events;

    /// <summary>
    /// Exported interface of the plugin which remote desktop connection manager hooks in to
    /// </summary>
    [Export(typeof(IPlugin))]
    public class Plugin : PluginFix
    {
        private IPluginSettings settings;

        public Plugin()
        {
            OnContextMenuEvent += AddNodeDetailsToServerConxtextMenu;

            OnDockServerEvent += WellDoneOnDockingServer;

            OnUndockServerEvent += ChangeUndockedColour;

            PostLoadEvent += AddNewNodeAfterLoad;

            PreLoadEvent += WriteWhenLastSaveHappened;

            SaveSettingsEvent += SaveTime;
            SaveSettingsEvent += SaveTime;

            ShutdownEvent += SayGoodbye;
        }

        private void AddNewNodeAfterLoad(object sender, EventArgs e)
        {
            IPluginContext context = sender as IPluginContext;
            Group testGroup = Group.Create("Test", context.Tree.RootNode);
            Server.Create("127.0.0.1", "localhost", testGroup);
        }

        private void AddNodeDetailsToServerConxtextMenu(object sender, EventArgs e)
        {
            if (sender is Server server && e is ContextMenuStripEventArgs args)
            {
                args.ContextMenuStrip.Items.Add("Server Details").Click += (s, a) =>
                {
                    MessageBox.Show($"Display Name: {server.DisplayName}{Environment.NewLine}Server Name: {server.ServerName}");
                };
            }
        }

        private void ChangeUndockedColour(object sender, EventArgs e)
        {
            if (e is MenuStripEventArgs args)
            {
                args.MenuStrip.BackColor = System.Drawing.Color.Pink;
            }
        }

        private void SaveTime(object sender, EventArgs e)
        {
            if (e is XmlNodeEventArgs args)
            {
                settings.LastSave = DateTime.Now.ToString("o");
                args.XmlNode = settings.ToNode();
            }
        }

        private void SayGoodbye(object sender, EventArgs e)
        {
            MessageBox.Show("Goodbye!");
        }

        private void WellDoneOnDockingServer(object sender, EventArgs e)
        {
            if (sender is Server server)
            {
                MessageBox.Show($"Well done you docked {server.DisplayName}!");
            }
        }

        private void WriteWhenLastSaveHappened(object sender, EventArgs e)
        {
            Form mainWindowForm = (sender as IPluginContext)?.MainForm as Form;
            XmlNodeEventArgs args = e as XmlNodeEventArgs;
            bool updatingMainWindowText = false;

            if (mainWindowForm == null || args == null)
            {
                return;
            }

            settings = args.XmlNode != null ? PluginSettings.CreateSettings(args.XmlNode) : new PluginSettings();

            mainWindowForm.TextChanged += (s, a) =>
            {
                if (!updatingMainWindowText && !string.IsNullOrWhiteSpace(settings.LastSave))
                {
                    try
                    {
                        updatingMainWindowText = true;
                        mainWindowForm.Text += $" (Settings last saved {settings.LastSave})";
                    }
                    finally
                    {
                        updatingMainWindowText = false;
                    }
                }
            };
            mainWindowForm.Text = string.Empty;
        }
    }
}