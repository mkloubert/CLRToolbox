// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox;

namespace MarcelJoachimKloubert.DragNBatch.PlugIns
{
    /// <summary>
    /// Describes a plugin.
    /// </summary>
    public interface IPlugIn : ITMDisposable, IIdentifiable, IHasName, IInitializable<IPlugInContext>
    {

    }
}