using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestClientLib
{
    /// <summary>
    /// JSON Format Body : tb_dat_cell Request
    /// </summary>
    public class _jsonDatCellRequest : __baseRequest
    {
        public _jsonDatCellRequest() { }
        ~_jsonDatCellRequest() { }
    }


    /// <summary>
    /// JSON Format Body : tb_dat_cell Response
    /// </summary>
    public class _jsonDatCellResponse : _baseResponse
    {
        public _jsonDatCellResponse() { }
        ~_jsonDatCellResponse() { }

        public List<_dat_cell> DATA;
    }

}
