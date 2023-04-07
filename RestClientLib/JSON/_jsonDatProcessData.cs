using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestClientLib
{
    /// <summary>
    /// JSON Format Body : tb_dat_process_data Request
    /// </summary>
    public class _jsonDatProcessDataRequest : __baseRequest
    {
        public _jsonDatProcessDataRequest() { }
        ~_jsonDatProcessDataRequest() { }
    }


    /// <summary>
    /// JSON Format Body : tb_dat_process_data Response
    /// </summary>
    public class _jsonDatProcessDataResponse : _baseResponse
    {
        public _jsonDatProcessDataResponse() { }
        ~_jsonDatProcessDataResponse() { }

        public List<_dat_process_data> DATA;
    }

}
