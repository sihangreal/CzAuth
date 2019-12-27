using CzAuth.Modes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CzAuth.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController:ControllerBase
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("login")]
        public  IActionResult Login(string userName,string password)
        {
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {
                //Json(new ResponseModel());
                return Ok(
                   new ResponseModel()
                );
            }
            else
            {
                return BadRequest();
            }

        }
    }
}
