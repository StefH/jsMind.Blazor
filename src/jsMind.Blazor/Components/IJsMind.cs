using System.Threading.Tasks;

namespace JsMind.Blazor.Components
{
    /// <summary>
    /// See https://github.com/hizzgdev/jsmind
    /// </summary>
    public interface IJsMind
    {
        #region  Methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ValueTask Show(JsMindOptions options);
        #endregion
    }
}