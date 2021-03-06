using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DNTFrameworkCore.Application;
using DNTFrameworkCore.Authorization;
using DNTFrameworkCore.Functional;
using DNTFrameworkCore.Mapping;
using DNTFrameworkCore.Querying;
using DNTFrameworkCore.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;

namespace DNTFrameworkCore.Web.API
{
    [Authorize]
    public abstract class
        CrudController<TCrudService, TKey, TModel> : CrudControllerBase<TKey, TModel, TModel,
            FilteredPagedRequest>
        where TCrudService : class, ICrudService<TKey, TModel>
        where TModel : MasterModel<TKey>
        where TKey : IEquatable<TKey>
    {
        protected readonly TCrudService CrudService;

        protected CrudController(TCrudService service)
        {
            CrudService = service ?? throw new ArgumentNullException(nameof(service));
        }

        private protected override Task<IPagedResult<TModel>> ReadPagedListAsync(FilteredPagedRequest request,
            CancellationToken cancellationToken)
        {
            return CrudService.ReadPagedListAsync(request, cancellationToken);
        }

        private protected override Task<Maybe<TModel>> FindAsync(TKey id, CancellationToken cancellationToken)
        {
            return CrudService.FindAsync(id, cancellationToken);
        }

        private protected override Task<Result> EditAsync(TModel model, CancellationToken cancellationToken)
        {
            return CrudService.EditAsync(model, cancellationToken);
        }

        private protected override Task<Result> CreateAsync(TModel model, CancellationToken cancellationToken)
        {
            return CrudService.CreateAsync(model, cancellationToken);
        }

        private protected override Task<Result> DeleteAsync(TModel model, CancellationToken cancellationToken)
        {
            return CrudService.DeleteAsync(model, cancellationToken);
        }

        private protected override async Task<Result> DeleteAsync(IEnumerable<TKey> ids,
            CancellationToken cancellationToken)
        {
            var models = await CrudService.FindListAsync(ids, cancellationToken);
            return await CrudService.DeleteAsync(models, cancellationToken);
        }
    }

    [Authorize]
    public abstract class
        CrudController<TCrudService, TKey, TReadModel, TModel> : CrudControllerBase<TKey, TReadModel, TModel,
            FilteredPagedRequest>
        where TCrudService : class, ICrudService<TKey, TReadModel, TModel>
        where TReadModel : ReadModel<TKey>
        where TModel : MasterModel<TKey>
        where TKey : IEquatable<TKey>
    {
        protected readonly TCrudService CrudService;

        protected CrudController(TCrudService service)
        {
            CrudService = service ?? throw new ArgumentNullException(nameof(service));
        }

        private protected override Task<IPagedResult<TReadModel>> ReadPagedListAsync(FilteredPagedRequest request,
            CancellationToken cancellationToken)
        {
            return CrudService.ReadPagedListAsync(request, cancellationToken);
        }

        private protected override Task<Maybe<TModel>> FindAsync(TKey id, CancellationToken cancellationToken)
        {
            return CrudService.FindAsync(id, cancellationToken);
        }

        private protected override Task<Result> EditAsync(TModel model, CancellationToken cancellationToken)
        {
            return CrudService.EditAsync(model, cancellationToken);
        }

        private protected override Task<Result> CreateAsync(TModel model, CancellationToken cancellationToken)
        {
            return CrudService.CreateAsync(model, cancellationToken);
        }

        private protected override Task<Result> DeleteAsync(TModel model, CancellationToken cancellationToken)
        {
            return CrudService.DeleteAsync(model, cancellationToken);
        }

        private protected override async Task<Result> DeleteAsync(IEnumerable<TKey> ids,
            CancellationToken cancellationToken)
        {
            var models = await CrudService.FindListAsync(ids, cancellationToken);
            return await CrudService.DeleteAsync(models, cancellationToken);
        }
    }

    [Authorize]
    public abstract class
        CrudController<TCrudService, TKey, TReadModel, TModel, TFilteredPagedRequest> :
            CrudControllerBase<TKey, TReadModel, TModel, TFilteredPagedRequest>
        where TCrudService : class, ICrudService<TKey, TReadModel, TModel, TFilteredPagedRequest>
        where TReadModel : ReadModel<TKey>
        where TModel : MasterModel<TKey>, new()
        where TFilteredPagedRequest : class, IFilteredPagedRequest, new()
        where TKey : IEquatable<TKey>
    {
        protected readonly TCrudService CrudService;

        protected CrudController(TCrudService service)
        {
            CrudService = service ?? throw new ArgumentNullException(nameof(service));
        }

        private protected override Task<IPagedResult<TReadModel>> ReadPagedListAsync(TFilteredPagedRequest request,
            CancellationToken cancellationToken)
        {
            return CrudService.ReadPagedListAsync(request, cancellationToken);
        }

        private protected override Task<Maybe<TModel>> FindAsync(TKey id, CancellationToken cancellationToken)
        {
            return CrudService.FindAsync(id, cancellationToken);
        }

        private protected override Task<Result> EditAsync(TModel model, CancellationToken cancellationToken)
        {
            return CrudService.EditAsync(model, cancellationToken);
        }

        private protected override Task<Result> CreateAsync(TModel model, CancellationToken cancellationToken)
        {
            return CrudService.CreateAsync(model, cancellationToken);
        }

        private protected override Task<Result> DeleteAsync(TModel model, CancellationToken cancellationToken)
        {
            return CrudService.DeleteAsync(model, cancellationToken);
        }

        private protected override async Task<Result> DeleteAsync(IEnumerable<TKey> ids,
            CancellationToken cancellationToken)
        {
            var models = await CrudService.FindListAsync(ids, cancellationToken);
            return await CrudService.DeleteAsync(models, cancellationToken);
        }
    }

    [ApiController]
    [Produces("application/json")]
    public abstract class
        CrudControllerBase<TKey, TReadModel, TModel, TFilteredPagedRequest> : ControllerBase
        where TReadModel : ReadModel<TKey>
        where TModel : MasterModel<TKey>
        where TFilteredPagedRequest : class, IFilteredPagedRequest, new()
        where TKey : IEquatable<TKey>
    {
        protected abstract string CreatePermissionName { get; }
        protected abstract string EditPermissionName { get; }
        protected abstract string ViewPermissionName { get; }
        protected abstract string DeletePermissionName { get; }

        private protected abstract Task<IPagedResult<TReadModel>> ReadPagedListAsync(TFilteredPagedRequest request,
            CancellationToken cancellationToken);

        private protected abstract Task<Maybe<TModel>> FindAsync(TKey id, CancellationToken cancellationToken);
        private protected abstract Task<Result> EditAsync(TModel model, CancellationToken cancellationToken);
        private protected abstract Task<Result> CreateAsync(TModel model, CancellationToken cancellationToken);
        private protected abstract Task<Result> DeleteAsync(TModel model, CancellationToken cancellationToken);
        private protected abstract Task<Result> DeleteAsync(IEnumerable<TKey> ids, CancellationToken cancellationToken);

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Get([FromQuery] TFilteredPagedRequest request,
            CancellationToken cancellationToken)
        {
            if (!await HasPermission(ViewPermissionName)) return Forbid();

            var result = await ReadPagedListAsync(request ?? Factory<TFilteredPagedRequest>.New(),
                cancellationToken);

            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([BindRequired] TKey id, CancellationToken cancellationToken)
        {
            if (!await HasPermission(EditPermissionName)) return Forbid();

            var model = await FindAsync(id, cancellationToken);

            if (!model.HasValue) return NotFound();

            return Ok(model.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Post(TModel model, CancellationToken cancellationToken)
        {
            if (!await HasPermission(CreatePermissionName)) return Forbid();

            var result = await CreateAsync(model, cancellationToken);
            return !result.Failed ? Created("", model) : BadRequest(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Put([BindRequired] TKey id, TModel model, CancellationToken cancellationToken)
        {
            if (!model.Id.Equals(id)) return BadRequest();

            if (!await HasPermission(EditPermissionName)) return Forbid();

            model.Id = id;

            var result = await EditAsync(model, cancellationToken);
            return !result.Failed ? Ok(model) : BadRequest(result);
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([BindRequired] TKey id, CancellationToken cancellationToken)
        {
            if (!await HasPermission(DeletePermissionName)) return Forbid();

            var model = await FindAsync(id, cancellationToken);
            if (!model.HasValue) return NotFound();

            var result = await DeleteAsync(model.Value, cancellationToken);
            return !result.Failed ? NoContent() : BadRequest(result);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> BulkDelete(IEnumerable<TKey> ids, CancellationToken cancellationToken)
        {
            if (!await HasPermission(DeletePermissionName))
            {
                return Forbid();
            }

            var result = await DeleteAsync(ids, cancellationToken);

            return !result.Failed ? NoContent() : BadRequest(result);
        }

        protected async Task<bool> HasPermission(string permissionName)
        {
            var policyName = PermissionConstant.PolicyPrefix + permissionName;
            var authorization = HttpContext.RequestServices.GetRequiredService<IAuthorizationService>();
            return (await authorization.AuthorizeAsync(User, policyName)).Succeeded;
        }
        
        protected IActionResult BadRequest(Result result)
        {
            ModelState.AddResult(result);
            return BadRequest(ModelState);
        }
    }
}