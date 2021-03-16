using Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Models.Database;
using Models.JSON;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;

namespace GIthub_Test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            try
            {
                logger.Debug("init main");
                var host = CreateHostBuilder(args).Build();

                // 初始化数据库
                CreateDbIfNotExists(host);
                logger.Info("init databse complete");

                host.Run();
            }
            catch (Exception exception)
            {
                //NLog: catch setup errors
                logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }

            //CreateHostBuilder(args).Build().Run();
        }

        private static void CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var dbContext = services.GetRequiredService<GithubTestDbContext>();
                    // 清除数据库
                    //dbContext.Database.EnsureDeleted();
                    //dbContext.Database.EnsureCreated();
                    dbContext.Lefts.RemoveRange(dbContext.Lefts);
                    dbContext.Rights.RemoveRange(dbContext.Rights);
                    int count = dbContext.SaveChanges();

                    var configuration = services.GetRequiredService<IConfiguration>();

                    // 获取github数据
                    // TODO url地址是否改到配置文件中方便随时修改
                    var request = new HttpRequestMessage(HttpMethod.Get, configuration["GithubApiUrl"]);

                    var httpClientFactory = services.GetRequiredService<IHttpClientFactory>();
                    var client = httpClientFactory.CreateClient("github");
                    var response = client.SendAsync(request).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string json = response.Content.ReadAsStringAsync().Result;
                        // using var responseStream = response.Content.ReadAsStream();

                        // 因为想分开数据库和业务逻辑，所里这里不直接用数据库对象反序列化json数据
                        // responseStream.Write
                        IEnumerable<OrganizationRepository> repositories = JsonSerializer.Deserialize<IEnumerable<OrganizationRepository>>(json);

                        List<Left> lefts = new List<Left>();
                        foreach (OrganizationRepository repository in repositories)
                        {
                            lefts.Add(new Left
                            {
                                Id = repository.Id,
                                Name = repository.Name, // TODO 反射属性获取长度限制
                                Url = repository.Url, // TODO 反射属性获取长度限制
                                HtmlUrl = repository.HtmlUrl, // TODO 反射属性获取长度限制

                                // TODO 这部分会有BaseDbContext默认修改
                                InsertDate = DateTimeOffset.Now,
                                InsertUser = Guid.Empty,
                                UpdateDate = DateTimeOffset.Now,
                                UpdateUser = Guid.Empty,
                                DeleteFlag = false,
                            });
                        }

                        dbContext.Lefts.AddRange(lefts);
                        count += dbContext.SaveChanges();
                    }
                    else
                    {
                        // GetPullRequestsError = true;
                        // PullRequests = Array.Empty<GitHubPullRequest>();
                    }

                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Trace);
                })
                .UseNLog();  // NLog: Setup NLog for Dependency injection;;
    }
}
