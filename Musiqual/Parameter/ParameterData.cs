using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musiqual.Parameter
{

    /// <summary>
    /// The IParameterData interface.
    /// </summary>
    public interface IParameterData
    {

        bool IsNatural { get; }

    }

    /// <summary>
    /// The positive/negative parameter data.
    /// </summary>
    public abstract class NormalParameterData : IParameterData
    {

        public bool IsNatural => false;

    }

    /// <summary>
    /// The positive parameter data.
    /// </summary>
    public abstract class NaturalParameterData : IParameterData
    {

        public bool IsNatural => true;

    }

    /// <summary>
    /// The placeholder parameter data.
    /// NO NOT USE THIS CLASS AS PARAMETER UNLESS YOU KNOW WHAT YOU ARE DOING.
    /// </summary>
    public sealed class PlaceHolderParameterData : NormalParameterData
    {

    }

}
