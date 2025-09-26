using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FolderFlattener.Interfaces;

namespace FolderFlattener.Implementations
{
    public class FlatteningStrategyFactory : IFlatteningStrategyFactory
    {
        IFlatteningStrategy IFlatteningStrategyFactory.Create(FlatteningStrategyEnum flatteningStrategy)
        {
            switch (flatteningStrategy)
            {
                case FlatteningStrategyEnum.Alphabetical:
                    return new AlphabeticalFlatteningStrategy();

                default:
                    throw new NotImplementedException($"Strategy {flatteningStrategy} is not implemented");
            }
        }
    }
}
