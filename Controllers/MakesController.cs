using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vega.Models;
using Vega.Persistence;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Vega.Controllers.Resources;

namespace Vega.Controllers
{
    public class MakesController : Controller
    {
        private readonly VegaDbContext context;
        private readonly IMapper mapper;
        public MakesController(VegaDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet("api/makes")]
         public async Task<IEnumerable<MakeResource>> GetMakes()
        {
            try
            { 
            var makes = await context.Makes.Include(m => m.Models)
                .ToListAsync();

            //foreach (var item in makes)
            //{
            //    if(item.Id == 4)
            //    {
            //        item.ativo = true;
            //    }
            //}

            return mapper.Map<List<Make>, List<MakeResource>>(makes);
            }
            catch(Exception ex)
            {
                throw ex.InnerException;
            }
        }
    }
}
