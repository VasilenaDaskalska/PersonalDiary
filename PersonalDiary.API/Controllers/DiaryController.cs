using Microsoft.AspNetCore.Mvc;
using PersonalDiary.Business.Models.Diaries;
using PersonalDiary.Services.Contracts;

namespace PersonalDiary.API.Controllers
{
    [ApiController]
    [Route("api/diary")]
    public class DiaryController : ControllerBase
    {
        private readonly IDiaryService diaryService;

        public DiaryController(IDiaryService diaryService)
        {
            this.diaryService = diaryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var measures = await this.diaryService.GetAllAsync();

                if (measures == null)
                {
                    return this.NotFound();
                }

                return this.Ok(measures);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }

        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                var measure = await this.diaryService.GetByIdAsync(id);

                if (measure == null)
                {
                    return this.NotFound();
                }

                return this.Ok(measure);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }

        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] DiaryBiz model)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.BadRequest(this.ModelState);
                }

                var updated = await this.diaryService.UpdateAsync(model);

                if (updated == null)
                {
                    return this.NotFound();
                }

                return this.Ok(updated);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }

        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var result = await this.diaryService.DeleteAsync(id);

                if (!result)
                {
                    return this.NotFound();
                }

                return this.NoContent();
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DiaryBiz model)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.BadRequest(this.ModelState);
                }

                var created = await this.diaryService.CreateAsync(model);

                if (created == null)
                {
                    return this.BadRequest("Failed to create diary");
                }

                return this.CreatedAtAction(nameof(Get), new { id = created.ID }, created);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
    }
}
