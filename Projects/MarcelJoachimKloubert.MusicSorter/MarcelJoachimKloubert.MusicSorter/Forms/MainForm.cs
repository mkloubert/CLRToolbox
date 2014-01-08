using System;
using System.Reflection;
using System.Windows.Forms;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using Vlc.DotNet.Core;
using Vlc.DotNet.Forms;

namespace MarcelJoachimKloubert.MusicSorter.Forms
{
    public partial class MainForm : Form
    {
        #region Constructors (1)

        public MainForm()
        {
            this.InitializeComponent();

            FixupVlcControl(this.VlcControl_Main);
        }

        #endregion Constructors

        #region Methods (2)

        // Private Methods (2) 

        private static void FixupVlcControl(VlcControl player)
        {
            // private setter of VlcControl.Medias
            var mediasPropertySetter = player.GetType()
                                             .GetProperty(player.GetMemberName(p => p.Medias),
                                                          BindingFlags.Instance | BindingFlags.Public)
                                             .GetSetMethod(true);

            // internal constructor VlcMediaListPlayer(IVlcControl)
            var mediaListPlayerConstructor = CollectionHelper.Single(typeof(VlcMediaListPlayer).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance),
                                                                     c =>
                                                                     {
                                                                         var @params = c.GetParameters();

                                                                         return @params.Length == 1 &&
                                                                                typeof(IVlcControl).Equals(@params[0].ParameterType);
                                                                     });

            // create instance
            var mediaListPlayer = (VlcMediaListPlayer)mediaListPlayerConstructor.Invoke(new object[] { player });

            // set property with created instance
            mediasPropertySetter.Invoke(player,
                                        new object[] { mediaListPlayer });
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        #endregion Methods
    }
}
