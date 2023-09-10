using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Client.Interfaces
{
    public interface IHttpClientFactory
    {
        IHttpClient Create();
    }
}
