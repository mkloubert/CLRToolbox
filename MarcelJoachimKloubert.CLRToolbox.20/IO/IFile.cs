// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Data;

namespace MarcelJoachimKloubert.CLRToolbox.IO
{
    /// <summary>
    /// Describes a file.
    /// </summary>
    public interface IFile : IHasName, IContentProvider
    {
    }
}