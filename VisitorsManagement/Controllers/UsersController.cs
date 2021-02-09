using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VisitorsManagement;
using VisitorsManagement.Models;

namespace VisitorsManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : Controller
    {
        private readonly VisitorContext ctx;

        public SecurityController(VisitorContext context)
        {
            ctx = context;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public ActionResult<User> Login([Bind("Name", "Password")] User u)
        {
            User user = ctx.Users.FirstOrDefault(e => e.Name == u.Name);
            if (user == null)
            {
                return Ok(new
                {
                    success = false,
                    message = "用户名错误"
                });
            }
            if (user.Password == u.Password)
            {
                return Ok(new
                {
                    success = true,
                    message = "登录成功"
                });
            }
            else
            {
                return Ok(new
                {
                    success = false,
                    message = "密码错误"
                });
            }
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("user")]
        public ActionResult<User> Regist(User user)
        {
            ctx.Users.Add(user);
            ctx.SaveChanges();
            return Ok(user);
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("users")]
        public ActionResult GetUserList(int page = 1, int pageSize = 10)
        {
            List<User> users = ctx.Users.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return Ok(new
            {
                page,
                pageSize,
                data = users
            });
        }

        /// <summary>
        /// 根据用户ID获取用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("user/{id}")]
        public ActionResult GetUser(Guid id)
        {
            User user = ctx.Users.Single(e => true);
            return Ok(new
            {
                data = user
            });
        }

        [HttpPut("user/{id}")]
        public ActionResult ModifyUser(Guid id, User user)
        {
            ctx.Users.Update(user);
            return Ok();
        }
    }
}
