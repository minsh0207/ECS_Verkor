using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestClientLib
{
    /// <summary>
    /// JSON Format Body : tb_dat_temp_hpc Request
    /// </summary>
    public class _jsonDatTempHpcRequest : __baseRequest
    {
        public _jsonDatTempHpcRequest() { }
        ~_jsonDatTempHpcRequest() { }
    }


    /// <summary>
    /// JSON Format Body : tb_dat_temp_hpc Response
    /// </summary>
    public class _jsonDatTempHpcResponse : _baseResponse
    {
        public _jsonDatTempHpcResponse() { }
        ~_jsonDatTempHpcResponse() { }
        
        public List<_dat_temp_hpc> DATA;
    }

}
