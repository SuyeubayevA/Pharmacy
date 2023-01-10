using Pharmacy.Infrastructure.Business.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.API.Helpers
{
    public static class Helper
    {
        public static IResult GetIResult<T>(CQRSResponse<T> response)
        {
            if(response.Message == string.Empty && response.IsSuccess== false)
            {
                return Results.BadRequest();
            }
            if (response.IsSuccess)
            {
                return Results.Ok(response.Model);
            }
            else
            {
                return Results.NotFound();
            }
        }
    }
}
