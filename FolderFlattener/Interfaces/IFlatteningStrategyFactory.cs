using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderFlattener.Interfaces
{
    /// <summary>
    /// Creates instances of <see cref="IFlatteningStrategy"/><br/>
    /// Lifespan: singleton
    /// </summary>
    public interface IFlatteningStrategyFactory
    {
        public IFlatteningStrategy Create(FlatteningStrategyEnum flatteningStrategy);
    }
}
