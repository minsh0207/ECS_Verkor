using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestClientLib
{
    /// <summary>
    /// JSON Format Body : tb_dat_status_unit Request
    /// </summary>
    public class _jsonDatStatusUnitRequest : __baseRequest
    {
        public _jsonDatStatusUnitRequest() { }
        ~_jsonDatStatusUnitRequest() { }
    }


    /// <summary>
    /// JSON Format Body : tb_dat_status_unit Response
    /// </summary>
    public class _jsonDatStatusUnitResponse : _baseResponse
    {
        public _jsonDatStatusUnitResponse() { }
        ~_jsonDatStatusUnitResponse() { }

        public List<_dat_status_unit> DATA;
    }

}
