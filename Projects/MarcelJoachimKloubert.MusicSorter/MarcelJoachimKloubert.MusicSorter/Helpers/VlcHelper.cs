using System.Reflection;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using Vlc.DotNet.Core;
using Vlc.DotNet.Forms;

namespace MarcelJoachimKloubert.MusicSorter.Helpers
{
    internal static class VlcHelper
    {
        #region Methods (1)

        // Internal Methods (1) 

        internal static void FixupVlcControl(VlcControl player)
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

        #endregion Methods
    }
}
