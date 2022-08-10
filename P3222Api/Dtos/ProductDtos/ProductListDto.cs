using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3222Api.Dtos.ProductDtos
{
    public class ProductListDto
    {
        public int TotalCount { get; set; }
        public List<ProductReturnDto> Items { get; set; }

    }
}
