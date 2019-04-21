using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ax.secure.dataManagement.Models;
using ax.storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ax.secure.dataManagement.Controllers
{
    /// <summary>
    /// Zip file storage controller.
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ZipFileStorageController : ControllerBase
    {
        public IStorageManager StorageManager { get; set; }
        public IMapper Mapper { get; set; }

        public ZipFileStorageController(IStorageManager storageManager, IMapper mapper)
        {
            StorageManager = storageManager;
            Mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<string>))]
        [ProducesResponseType(400)]
        public IActionResult Get(int rowCount = 10)
        {
            var result = StorageManager.List(rowCount);

            if (result.IsFailure)
                return BadRequest(result.Error);

            var zipArchiveModels = result.Value.Select(x => Mapper.Map<ZipArchiveModel>(x)).ToList();

            return Ok(zipArchiveModels);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Post([FromBody]string rawContent)
        {
            var result = StorageManager.Save(rawContent);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok();
        }
    }
}
