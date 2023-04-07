using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestClientLib
{
    /// <summary>
    /// JSON Format Body : tb_mst_unit Request
    /// </summary>
    public class _jsonMstUnitRequest : __baseRequest
    {
        public _jsonMstUnitRequest() { }
        ~_jsonMstUnitRequest() { }
    }


    /// <summary>
    /// JSON Format Body : tb_mst_unit Response
    /// </summary>
    public class _jsonMstUnitResponse : _baseResponse
    {
        public _jsonMstUnitResponse() { }
        ~_jsonMstUnitResponse() { }

        public List<_mst_unit> DATA;
    }

}
