using Database;
using GIthub_Test.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models.Database;
using Models.JSON;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace GIthub_Test.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IHttpClientFactory httpClientFactory;

        public HomeController(ILogger<HomeController> logger,
            GithubTestDbContext dbContext,
             IHttpClientFactory clientFactory) : base(logger, dbContext)
        {
            this.httpClientFactory = clientFactory;
        }

        [ActionName("index")]
        public async Task<IActionResult> IndexAsync()
        {
            logger.LogInformation("Hello, this is the index!");

            #region 数据库操作时会验证用户登录信息，所以这里做个假的登录用户

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "staff1@qq.com"),
                new Claim(ClaimTypes.Email, "staff1@qq.com"),
                new Claim("FullName", "staff1"),
                new Claim(ClaimTypes.Role, "Administrator"),
                new Claim("Id",Guid.Empty.ToString()),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                //AllowRefresh = <bool>,
                // Refreshing the authentication session should be allowed.

                //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                // The time at which the authentication ticket expires. A 
                // value set here overrides the ExpireTimeSpan option of 
                // CookieAuthenticationOptions set with AddCookie.

                //IsPersistent = true,
                // Whether the authentication session is persisted across 
                // multiple requests. When used with cookies, controls
                // whether the cookie's lifetime is absolute (matching the
                // lifetime of the authentication ticket) or session-based.

                //IssuedUtc = <DateTimeOffset>,
                // The time at which the authentication ticket was issued.

                //RedirectUri = <string>
                // The full path or absolute URI to be used as an http 
                // redirect response value.
            };

            //this.HttpContext.SignInAsync();

            //this.HttpContext.SignOutAsync();

            //this.HttpContext.User;

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            #endregion

            //var request = new HttpRequestMessage(HttpMethod.Get, "orgs/idcf-boat-house/repos");

            //var client = this.httpClientFactory.CreateClient("github");

            //var response = await client.SendAsync(request);

            //if (response.IsSuccessStatusCode)
            //{
            //    using var responseStream = await response.Content.ReadAsStreamAsync();

            //    // 因为想分开数据库和业务逻辑，所里这里不直接用数据库对象反序列化json数据
            //    IEnumerable<OrganizationRepository> repositories = await JsonSerializer.DeserializeAsync<IEnumerable<OrganizationRepository>>(responseStream);

            //    List<Left> lefts = new List<Left>();
            //    foreach (OrganizationRepository repository in repositories)
            //    {
            //        lefts.Add(new Left
            //        {
            //            Id = repository.Id,
            //            Title = repository.Name, // TODO 反射属性获取长度限制
            //            url = repository.Url, // TODO 反射属性获取长度限制
            //            html_url = repository.HtmlUrl, // TODO 反射属性获取长度限制
            //            LastUpdate = DateTime.Now, // TODO 将来会删除

            //            // TODO 这部分会有BaseDbContext默认修改
            //            InsertDate = DateTimeOffset.Now,
            //            InsertUser = Guid.Empty,
            //            UpdateDate = DateTimeOffset.Now,
            //            UpdateUser = Guid.Empty,
            //            DeleteFlag = false,
            //        });
            //    }

            //    await this.dbContext.Lefts.AddRangeAsync(lefts);
            //    int count = await this.dbContext.SaveChangesAsync();
            //}
            //else
            //{
            //    // GetPullRequestsError = true;
            //    // PullRequests = Array.Empty<GitHubPullRequest>();
            //}

            // 获取数据库中的项目列表
            HomeModel model = new HomeModel();
            model.Lefts = await this.dbContext.Lefts.AsNoTracking().ToListAsync();
            model.Rights = await this.dbContext.Rights.AsNoTracking().ToListAsync();

            return View(model);
        }

        /// <summary>
        /// 移动到左边的项目
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [ActionName("MoveToLeft")]
        [HttpPost]
        public async Task<IActionResult> MoveToLeftAsync(int[] ids)
        {
            IEnumerable<Right> selected = from rights in this.dbContext.Rights where ids.Contains(rights.Id) select rights;

            List<Left> lefts = new List<Left>();
            foreach (Right right in selected)
            {
                lefts.Add(new Left
                {
                    Id = right.Id,
                    Name = right.Name,
                    Url = right.Url,
                    HtmlUrl = right.HtmlUrl,
                });
            }

            // 删除右边
            this.dbContext.Rights.RemoveRange(selected);
            // 插入左边
            await this.dbContext.Lefts.AddRangeAsync(lefts);

            return View();
        }

        /// <summary>
        /// 移动到右边的项目
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [ActionName("MoveToRight")]
        [HttpPost]
        public async Task<IActionResult> MoveToRightAsync(int[] ids)
        {
            IEnumerable<Left> selected = from lefts in this.dbContext.Lefts where ids.Contains(lefts.Id) select lefts;

            List<Right> rights = new List<Right>();
            foreach (Left left in selected)
            {
                rights.Add(new Right
                {
                    Id = left.Id,
                    Name = left.Name,
                    Url = left.Url,
                    HtmlUrl = left.HtmlUrl,
                });
            }

            //Left left = await this.dbContext.Lefts.FindAsync(id);

            //Right right = new Right
            //{
            //    Id = left.Id,
            //    Title = left.Title,
            //    Url = left.Url,
            //    HtmlUrl = left.HtmlUrl,
            //    LastUpdate = DateTime.Now,
            //};

            // 删除左边
            this.dbContext.Lefts.RemoveRange(selected);
            // 插入右边
            await this.dbContext.Rights.AddRangeAsync(rights);

            return View();
        }

        /// <summary>
        /// 生成邮件
        /// </summary>
        /// <param name="ids">需要生成邮件的id列表</param>
        /// <returns></returns>
        [ActionName("GenerateEmail")]
        [HttpGet]
        public async Task<IActionResult> GenerateEmailAsync(int[] ids)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "orgs/idcf-boat-house/repos");

            var client = this.httpClientFactory.CreateClient("github");

            // 循环id获取信息
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
