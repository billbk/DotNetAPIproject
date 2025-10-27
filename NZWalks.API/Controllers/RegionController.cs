using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;

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

            //Get data from database - Domain Models
            var regionsDomain = dbContext.Regions.ToList();
            //Map Domain Models to DTOs
            var regionDtos = new List<RegionDto>();
            foreach (var regionDomain in regionsDomain)
            {
                regionDtos.Add(new RegionDto()
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl
                });
            }

            //Return DTOs
            return Ok(regionDtos);

        }

        // GET SINGLE REGION (Get Region By ID)
        //GET: https://localhost:portnumber/api/region/{id}
        [HttpGet]
        [Route("(id:Guid)")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            //var region = dbContext.Regions.Find(id);
            // Get Region Domain Model from database
            var regionDomain = dbContext.Regions.FirstOrDefault(x => x.Id == id);
            if (regionDomain == null)
            {
                return NotFound();
            }
            // Map Region Domain Model to Region DTO
            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };
            // Return DTO back to client
            return Ok(regionDto);


        }

        // POST: To Create New Region
        // POST: https://localhost:portnumber/api/region
        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Map/convert DTO to Domain Model
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };

            dbContext.Regions.Add(regionDomainModel);
            dbContext.SaveChanges();

            // Map Domain model back to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        // Update Region
        // PUT: https://localhost:portnumber/api/region/{id}
        [HttpPut]
        [Route("(id:Guid)")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdareRegionRequestDto updareRegionRequestDto )
        {
            //Check if region exits
            var regionDomainModel = dbContext.Regions.FirstOrDefault(x => x.Id == id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }
            // map  DTo to Domain Model
            regionDomainModel.Code = updareRegionRequestDto.Code;
            regionDomainModel.Name = updareRegionRequestDto.Name;
            regionDomainModel.RegionImageUrl = updareRegionRequestDto.RegionImageUrl;
            dbContext.SaveChanges();
            //Convert Domain Model to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
           };
            return Ok();
        }

    }
}
