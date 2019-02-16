using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreFrame.SearchSerever.Model
{
    public interface IEsClientProvider
    {
        ElasticClient GetClient();
    }
}
