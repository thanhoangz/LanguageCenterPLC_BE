﻿using LanguageCenterPLC.Application.Interfaces;
using LanguageCenterPLC.Application.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionService _permissionService;


        public PermissionsController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        // GET: api/Permissions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PermissionViewModel>>> GetPermissions()
        {
            return await Task.FromResult(_permissionService.GetAll());
        }


        // GET: api/Permissions
        [HttpPost]
        [Route("get-permission-by-user")]
        public async Task<ActionResult<IEnumerable<PermissionViewModel>>> GetPermissionsByUser(string Id)
        {
            return await Task.FromResult(_permissionService.GetAllByUser(new Guid(Id)));
        }



        // PUT: api/Permissions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPermission(int id, PermissionViewModel permission)
        {
            if (permission.Id != id)
            {
                throw new Exception(string.Format("Lỗi không trùng Id!"));
            }

            try
            {
                await Task.Run(() =>
                {

                    _permissionService.Update(permission);
                    _permissionService.SaveChanges();
                    return Ok();
                });

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PermissionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        private bool PermissionExists(int id)
        {
            return _permissionService.IsExists(id);
        }
    }
}
