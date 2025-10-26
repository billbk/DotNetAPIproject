using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Controllers
{
    //https://localhost:1234/api/region
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;    
        public RegionController(NZWalksDbContext dbContext)
        {
                this.dbContext = dbContext;
        }

        //GET ALL REGIONS
        // GET: https://localhost:portnumber/api/region

        [HttpGet]
        public IActionResult GetAll()
        {
            //Hardcoded
            /* var regions = new List<Region>
            {
                new Region
                {
                    Id = Guid.NewGuid(),
                    Name = "Auckland Region",
                    Code ="AKL",
                    RegionImageUrl = "https://www.google.com/imgres?q=auckland%20region%20new%20zealand&imgurl=https%3A%2F%2Fuceap.universityofcalifornia.edu%2Fsites%2Fdefault%2Ffiles%2Fmarketing-images%2Flife-in-city-images%2Fauckland-new-zealand-gallery-1.jpg&imgrefurl=https%3A%2F%2Fuceap.universityofcalifornia.edu%2Flife-in-auckland-new-zealand&docid=NIqaH5nxXT1rWM&tbnid=C3F_vDM3WdofIM&vet=12ahUKEwjIjfGp0sKQAxX9FFkFHdEmJmoQM3oECD0QAA..i&w=740&h=420&hcb=2&ved=2ahUKEwjIjfGp0sKQAxX9FFkFHdEmJmoQM3oECD0QAA"
                },
                 new Region
                {
                    Id = Guid.NewGuid(),
                    Name = "Wellington Region",
                    Code ="WLG",
                    RegionImageUrl = "https://www.google.com/imgres?q=wellington%20region%20new%20zealand&imgurl=https%3A%2F%2Fcdn.britannica.com%2F25%2F180825-050-B4693350%2FWellington-Harbour-New-Zealand.jpg&imgrefurl=https%3A%2F%2Fwww.britannica.com%2Fplace%2FWellington-New-Zealand&docid=cb4OKjnxCK5PyM&tbnid=2YITAMavtjC7HM&vet=12ahUKEwi9xI7z0cKQAxX8FVkFHXwjI14QM3oECBsQAA..i&w=1024&h=576&hcb=2&ved=2ahUKEwi9xI7z0cKQAxX8FVkFHXwjI14QM3oECBsQAA"
                }
            }; */


            var regions = dbContext.Regions.ToList();
            return Ok(regions);

        }
    
    
    }
}
