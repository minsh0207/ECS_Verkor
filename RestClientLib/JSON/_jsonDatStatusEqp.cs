using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestClientLib
{
    /// <summary>
    /// JSON Format Body : tb_dat_status_eqp Request
    /// </summary>
    public class _jsonDatStatusEqpRequest : __baseRequest
    {
        public _jsonDatStatusEqpRequest() { }
        ~_jsonDatStatusEqpRequest() { }
    }


    /// <summary>
    /// JSON Format Body : tb_dat_status_eqp Response
    /// </summary>
    public class _jsonDatStatusEqpResponse : _baseResponse
    {
        public _jsonDatStatusEqpResponse() { }
        ~_jsonDatStatusEqpResponse() { }

        public List<_dat_status_eqp> DATA;
    }

}
