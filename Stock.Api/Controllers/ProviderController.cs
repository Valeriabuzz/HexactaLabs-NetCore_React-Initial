﻿using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stock.Api.DTOs;
using Stock.AppService.Services;
using Stock.Model.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Stock.Api.Controllers
{
    /// <summary>
    /// Product type endpoint.
    /// </summary>
    [Produces("application/json")]
    [Route("api/provider")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private readonly ProviderService service;
        private readonly IMapper mapper;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductTypeController"/> class.
        /// </summary>
        /// <param name="service">Product type service.</param>
        /// <param name="mapper">Mapper configurator.</param>
        public ProviderController(ProviderService service, IMapper mapper)
        {
            this.service = service ?? throw new ArgumentException(nameof(service));
            this.mapper = mapper ?? throw new ArgumentException(nameof(mapper));
        }

        /// <summary>
        /// Get all products.
        /// </summary>
        /// <returns>List of <see cref="ProviderDTO"/></returns>
        [HttpGet]
        public ActionResult<IEnumerable<ProviderDTO>> Get()
        {
            return Ok(mapper.Map<IEnumerable<ProviderDTO>>(service.GetAll()).ToList());
        }

        /// <summary>
        /// Gets a product by id.
        /// </summary>
        /// <param name="id">Product id to return.</param>
        /// <returns>A <see cref="ProviderDTO"/></returns>
        [HttpGet("{id}")]
        public ActionResult<ProviderDTO> Get(string id)
        {
            return Ok(mapper.Map<ProviderDTO>(service.Get(id)));
        }

        /// <summary>
        /// Add a product.
        /// </summary>
        /// <param name="value">Product information.</param>
        [HttpPost]
        public Provider Post([FromBody] ProviderDTO value)
        {
            TryValidateModel(value);
            var productType = service.Create(mapper.Map<Provider>(value));
            return mapper.Map<Provider>(productType);
        }

        /// <summary>
        /// Updates a product.
        /// </summary>
        /// <param name="id">Provider id to edit.</param>
        /// <param name="value">Provider information.</param>
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] ProviderDTO value)
        {
            var provider = service.Get(id);
            TryValidateModel(value);
            mapper.Map<ProviderDTO, Provider>(value, provider);
            service.Update(provider);
        }

        /// <summary>
        /// Deletes a provider.
        /// </summary>
        /// <param name="id">provider id to delete.</param>
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var provider = service.Get(id);
            
            if (provider is null) 
                return NotFound();
            
            service.Delete(provider);
            return Ok();
        }
    }
}
