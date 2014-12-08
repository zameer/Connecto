using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connecto.DataObjects.EntityFramework
{
    /// <summary>
    /// DataObjectFactory caches the connectionstring so that the context can be created quickly.
    /// </summary>
    public class DataObjectFactory
    {
        /// <summary>
        /// Creates the Context using the current connectionstring.
        /// </summary>
        /// <remarks>
        /// Gof pattern: Factory method. 
        /// </remarks>
        /// <returns>BizLiteManager Entities context.</returns>
        public static ConnectoManagerEntities CreateContext()
        {
            return new ConnectoManagerEntities();
        }
    }
}
