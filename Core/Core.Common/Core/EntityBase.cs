using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Core.Common.Core
{
    // Use this class inheritance on Entities if using WCF services
    // It's used for clean Serialization and prevents breakages. - IExtensibleDataObject


    [DataContract]
    public abstract class EntityBase : IExtensibleDataObject
    {
        #region IExtensibleDataObject Members

        public ExtensionDataObject ExtensionData { get; set; }

        #endregion
    }

}
