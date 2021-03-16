#pragma checksum "D:\MyProgram\github_test\github_test\RESOURCE\GIthub_Test\GIthub_Test\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "fde0756b4253ca2a790433c6e1190f9d9ff2a5de"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\MyProgram\github_test\github_test\RESOURCE\GIthub_Test\GIthub_Test\Views\_ViewImports.cshtml"
using GIthub_Test;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\MyProgram\github_test\github_test\RESOURCE\GIthub_Test\GIthub_Test\Views\_ViewImports.cshtml"
using GIthub_Test.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "D:\MyProgram\github_test\github_test\RESOURCE\GIthub_Test\GIthub_Test\Views\Home\Index.cshtml"
using Models.Database;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fde0756b4253ca2a790433c6e1190f9d9ff2a5de", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6b0e55d7b57b2a98cef2082e89d249f53974207d", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<HomeModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 4 "D:\MyProgram\github_test\github_test\RESOURCE\GIthub_Test\GIthub_Test\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Home Page";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            DefineSection("ScriptsHead", async() => {
                WriteLiteral("\r\n    <script>\r\n\r\n    </script>\r\n");
            }
            );
            WriteLiteral(@"
<div class=""text-center"">
    <h1 class=""display-4"">Welcome</h1>
    <p>Learn about <a href=""https://docs.microsoft.com/aspnet/core"">building Web apps with ASP.NET Core</a>.</p>

    <div class=""row"">
        <div id=""divLeft"" class=""col"" style=""border: 1px solid #F00"">

");
            WriteLiteral(@"            <dl>
                <dd v-for=""item in list"">
                    <input v-bind:id=""item.id"" type=""checkbox"" v-bind:value=""item.id"" v-model=""selected"" />
                    <label v-bind:for=""item.id"" v-on:dblclick=""dbclick(item.id)""> {{item[""name""]}}</label>
                </dd>
            </dl>
");
            WriteLiteral(@"        </div>
        <div id=""divButtons"" class=""col"">
            <button id=""btnMoveToRight"" v-on:click=""moveToRight"">>>>>></button>
            <br />
            <br />
            <br />
            <br />
            <button id=""btnMoveToLeft"" v-on:click=""moveToLeft""><<<<<</button>
            <br />
            <br />
            <button id=""btnGenerateEmail"" v-on:click=""generateEmail"">Generate Email</button>
        </div>
        <div id=""divRight"" class=""col"" style=""border: 1px solid #F00"">
            <dl>
                <dd v-for=""item in list"">
                    <input v-bind:id=""item.id"" type=""checkbox"" v-bind:value=""item.id"" v-model=""selected"" />
                    <label v-bind:for=""item.id"" v-on:dblclick=""dbclick(item.id)""> {{item[""name""]}}</label>
                </dd>
            </dl>

");
            WriteLiteral(@"        </div>
    </div>
    <div id=""divMail"" class=""row"" style=""border: 1px solid #F00"">
        Hello Alan,
        <ul>
            <li v-for=""item in list"" v-bind:key=""item.id"" v-bind:value=""item.name"">
                {{ item.name }}
            </li>
        </ul>
    </div>
</div>

");
            DefineSection("ScriptFoot", async() => {
                WriteLiteral("\r\n    <script>\r\n        var leftValues =");
#nullable restore
#line 87 "D:\MyProgram\github_test\github_test\RESOURCE\GIthub_Test\GIthub_Test\Views\Home\Index.cshtml"
                   Write(Html.Raw(Json.Serialize(this.Model.Lefts)));

#line default
#line hidden
#nullable disable
                WriteLiteral(";\r\n        var rightValues = ");
#nullable restore
#line 88 "D:\MyProgram\github_test\github_test\RESOURCE\GIthub_Test\GIthub_Test\Views\Home\Index.cshtml"
                     Write(Html.Raw(Json.Serialize(this.Model.Rights)));

#line default
#line hidden
#nullable disable
                WriteLiteral(@";
        var mailValues = [];

        var selectLeftValus = []; // 选中的左边数据
        var selectRightValues = []; // 选中的右边数据

        var btnMoveToRight = new Vue({
            el: '#divButtons',
            methods: {
                moveToRight: function () {
                    console.log(""moveToRight"");
                    // 数据移动到右边
                    moveToRight(divLeft.selected);

                    // 清除选择列表
                    divLeft.selected = [];

                },
                moveToLeft: function () {
                    console.log(""moveToLeft"");
                    // 数据移动到左边
                    moveToLeft(divRight.selected);

                    // 清除选择列表
                    divRight.selected = [];
                },
                generateEmail: function () {
                    console.log(""generateEmail"");
                    // 创建email
                    mailValues.splice(0, mailValues.length);
                    // 从右边获取列表，遍历
                    fo");
                WriteLiteral(@"r (i = 0; i < rightValues.length; i++) {
                        var item = findRight(rightValues[i].id);
                        mailValues.push(item);
                        // $(""#divMail"");
                    }
                },
            },
        });

        var divLeft = new Vue({
            el: '#divLeft',
            data: {
                list: leftValues,
                selected: selectLeftValus,
            },
            methods: {
                dbclick: function (id) {
                    // alert(id)
                    moveToRight([id]);
                },
            }
        });

        var divRight = new Vue({
            el: '#divRight',
            data: {
                list: rightValues,
                selected: selectRightValues,
            },
            methods: {
                dbclick: function (id) {
                    //alert(id)
                    moveToLeft([id]);
                },
            }
        });

        var ");
                WriteLiteral(@"divMail = new Vue({
            el: '#divMail',
            data: {
                list: mailValues,
            },
        });

        /**
         * 从左侧列表数据中查找指定的项目
         */
        function findLeft(id) {
            var newArr = leftValues.filter(function (p) {
                return p.id == id;
            });
            return newArr[0];
        }

        /**
         * 从右侧列表数据中查找指定的项目
         */
        function findRight(id) {
            var newArr = rightValues.filter(function (p) {
                return p.id == id;
            });
            return newArr[0];
        }

        /**
         * 数据移动到右边
         */
        function moveToRight(ids) {
            // 数据移动到右边

            // $(post) 在成功的返回值函数中做一下操作
            // 选中项目添加到右边列表中
            for (i = 0; i < ids.length; i++) {
                var item = findLeft(ids[i]);
                rightValues.push(item);
            }

            // 从左边列表中移除选定项目
            for (i = 0; i < ids.length; ");
                WriteLiteral(@"i++) {
                leftValues.forEach(function (item, index, arr) {
                    if (item.id == ids[i]) {
                        arr.splice(index, 1);
                    }
                });
            }
        }

        /**
         * 数据移动到左边
         */
        function moveToLeft(ids) {
            // $(post) 在成功的返回值函数中做一下操作
            // 选中项目添加到右边列表中
            for (i = 0; i < ids.length; i++) {
                var item = findRight(ids[i]);
                leftValues.push(item);
            }

            // 从左边列表中移除选定项目
            for (i = 0; i < ids.length; i++) {
                rightValues.forEach(function (item, index, arr) {
                    if (item.id == ids[i]) {
                        arr.splice(index, 1);
                    }
                });
            }
        }

    </script>
");
            }
            );
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<HomeModel> Html { get; private set; }
    }
}
#pragma warning restore 1591