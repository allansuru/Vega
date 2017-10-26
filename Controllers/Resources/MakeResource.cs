using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;



namespace Vega.Controllers.Resources
{
    public class MakeResource : KeyValuePairResource
    {


        public bool? ativo { get; set; }

        public ICollection<KeyValuePairResource> Models { get; set; }

        public MakeResource()
        {
            Models = new Collection<KeyValuePairResource>();
        }
    }
}
