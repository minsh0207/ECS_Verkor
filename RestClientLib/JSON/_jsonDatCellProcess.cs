using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestClientLib
{
    /// <summary>
    /// JSON Format Body : tb_dat_cell_process Request
    /// </summary>
    public class _jsonDatCellProcessRequest : __baseRequest
    {
        public _jsonDatCellProcessRequest() { }
        ~_jsonDatCellProcessRequest() { }
    }


    /// <summary>
    /// JSON Format Body : tb_dat_cell_process Response
    /// </summary>
    public class _jsonDatCellProcessResponse : _baseResponse
    {
        public _jsonDatCellProcessResponse() { }
        ~_jsonDatCellProcessResponse() { }

        public List<_dat_cell_process> DATA;
    }

}
