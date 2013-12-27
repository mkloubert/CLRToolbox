// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox.Objects;
using MarcelJoachimKloubert.CLRToolbox.Timing;

namespace MarcelJoachimKloubert.ApplicationServer
{
    /// <summary>
    /// Describes the context of an <see cref="IAppServer" /> object.
    /// </summary>
    public interface IAppServerContext : IObjectContext<IAppServer>,
                                         ITimeProvider
    {

    }
}
