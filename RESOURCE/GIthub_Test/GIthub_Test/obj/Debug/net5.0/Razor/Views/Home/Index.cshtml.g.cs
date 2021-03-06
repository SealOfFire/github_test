#pragma checksum "D:\MyProgram\github_test\github_test\RESOURCE\GIthub_Test\GIthub_Test\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "daa8da6d68c7d921c150ac8a1e25ca7e935af330"
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"daa8da6d68c7d921c150ac8a1e25ca7e935af330", @"/Views/Home/Index.cshtml")]
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
        <div id=""divLeft"" class=""text-left col"" style=""border: 1px solid #F00"">
            <p>All Repos From<a href=""http://github.com/idcf-boat-house"">http://github.com/idcf-boat-house</a></p>
");
            WriteLiteral(@"            <dl>
                <dd v-for=""item in list"">
                    <input v-bind:id=""item.id"" type=""checkbox"" v-bind:value=""item.id"" v-model=""selected"" />
                    <label v-bind:for=""item.id"" v-on:dblclick=""dbclick(item.id)""> {{item[""name""]}}</label>
                </dd>
            </dl>
");
            WriteLiteral(@"        </div>
        <div id=""divButtons"" class=""text-center col-2"">
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
        <div id=""divRight"" class=""text-left col"" style=""border: 1px solid #F00"">
            <p>My Favor Repos</p>
            <dl>
                <dd v-for=""item in list"">
                    <input v-bind:id=""item.id"" type=""checkbox"" v-bind:value=""item.id"" v-model=""selected"" />
                    <label v-bind:for=""item.id"" v-on:dblclick=""dbclick(item.id)""> {{item[""name""]}}</label>
                </dd>
            </dl>

");
            WriteLiteral(@"        </div>
    </div>
    <div id=""divMail"" class=""text-left row"" style=""border: 1px solid #F00"">
        <div style=""display:none;"">
            Hello Alan,
            <br />
            <br />
            This is your favor repos
            <ul>
                <li v-for=""item in list"" v-bind:key=""item.id"" v-bind:value=""item.name"">
                    - {{ item.name }} {{ item.desc }}
                    <br />
                    <a v-bind:href=""item.htmlUrl""> {{ item.htmlUrl }}</a>
                </li>
            </ul>
            <br />
        </div>
        <textarea id=""mailText"" style=""width:100%;"" rows=""5""></textarea>
    </div>
</div>

");
            DefineSection("ScriptFoot", async() => {
                WriteLiteral("\r\n    <script>\r\n        var leftValues =");
#nullable restore
#line 97 "D:\MyProgram\github_test\github_test\RESOURCE\GIthub_Test\GIthub_Test\Views\Home\Index.cshtml"
                   Write(Html.Raw(Json.Serialize(this.Model.Lefts)));

#line default
#line hidden
#nullable disable
                WriteLiteral(";\r\n        var rightValues = ");
#nullable restore
#line 98 "D:\MyProgram\github_test\github_test\RESOURCE\GIthub_Test\GIthub_Test\Views\Home\Index.cshtml"
                     Write(Html.Raw(Json.Serialize(this.Model.Rights)));

#line default
#line hidden
#nullable disable
                WriteLiteral(@";
        var mailValues = [];

        var selectLeftValus = []; // ?????????????????????
        var selectRightValues = []; // ?????????????????????

        var buttonContent = new Vue({
            el: '#divButtons',
            methods: {
                moveToRight: function () {
                    console.log(""moveToRight"");

                    var url = """);
#nullable restore
#line 110 "D:\MyProgram\github_test\github_test\RESOURCE\GIthub_Test\GIthub_Test\Views\Home\Index.cshtml"
                          Write(Url.Action("MoveToRight"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@""";
                    $.post(url, { ""ids"": divLeft.selected }, function (data, status) {
                        if (data.result) {
                            // ???????????????????????????????????????

                            // ?????????????????????
                            // moveToRight(divLeft.selected);
                            moveToRight(data.ids);

                            // ??????????????????
                            divLeft.selected = [];
                        }
                    });
                },

                moveToLeft: function () {
                    console.log(""moveToLeft"");

                    var url = """);
#nullable restore
#line 128 "D:\MyProgram\github_test\github_test\RESOURCE\GIthub_Test\GIthub_Test\Views\Home\Index.cshtml"
                          Write(Url.Action("MoveToLeft"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@""";
                    $.post(url, { ""ids"": divRight.selected }, function (data, status) {
                        if (data.result) {
                            // ???????????????????????????????????????

                            // ?????????????????????
                            // moveToLeft(divRight.selected);
                            moveToLeft(data.ids);

                            // ??????????????????
                            divRight.selected = [];
                        }
                    });
                },

                generateEmail: function () {
                    console.log(""generateEmail"");

                    // ??????email
                    mailValues.splice(0, mailValues.length);
                    var ids = [];
                    // ??????????????????????????????
                    for (i = 0; i < rightValues.length; i++) {
                        var item = findRight(rightValues[i].id);
                        //mailValues.push(item);
                        ids.push(item.id);
                    }

     ");
                WriteLiteral("               jQuery.ajaxSettings.traditional = true\r\n                    $.get(\"");
#nullable restore
#line 157 "D:\MyProgram\github_test\github_test\RESOURCE\GIthub_Test\GIthub_Test\Views\Home\Index.cshtml"
                      Write(Url.Action("GenerateEmail"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@""", { ""ids"": ids }, function (data, status) {
                        if (data.result) {
                            var text = '';
                            text = ""Hello Alan,\r\n"";
                            text += ""\r\n"";
                            text += ""This is your favor repos\r\n"";
                            for (i = 0; i < data.list.length; i++) {
                                mailValues.push(data.list[i]);
                                text += ""-"" + data.list[i].name + "" "" + data.list[i].desc + ""\r\n"";
                                text += data.list[i].htmlUrl +""\r\n""
                                $(""#mailText"").html(text);
                            }
                        }
                    });
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
                dbcl");
                WriteLiteral("ick: function (id) {\r\n                    // alert(id)\r\n                    $.post(\"");
#nullable restore
#line 184 "D:\MyProgram\github_test\github_test\RESOURCE\GIthub_Test\GIthub_Test\Views\Home\Index.cshtml"
                       Write(Url.Action("MoveToRight"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@""", { ""ids"": [id] }, function (data, status) {
                        if (data.result) {
                            moveToRight([id]);
                        }
                    });
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
                    $.post(""");
#nullable restore
#line 202 "D:\MyProgram\github_test\github_test\RESOURCE\GIthub_Test\GIthub_Test\Views\Home\Index.cshtml"
                       Write(Url.Action("MoveToLeft"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@""", { ""ids"": [id] }, function (data, status) {
                        if (data.result) {
                            moveToLeft([id]);
                        }
                    });
                },
            }
        });

        var divMail = new Vue({
            el: '#divMail',
            data: {
                list: mailValues,
            },
        });

        /**
         * ?????????????????????????????????????????????
         */
        function findLeft(id) {
            var newArr = leftValues.filter(function (p) {
                return p.id == id;
            });
            return newArr[0];
        }

        /**
         * ?????????????????????????????????????????????
         */
        function findRight(id) {
            var newArr = rightValues.filter(function (p) {
                return p.id == id;
            });
            return newArr[0];
        }

        /**
         * ?????????????????????
         */
        function moveToRight(ids) {
            // ?????????????????????
            // ????????????????????????????????????
           ");
                WriteLiteral(@" for (i = 0; i < ids.length; i++) {
                var item = findLeft(ids[i]);
                rightValues.push(item);
            }

            // ????????????????????????????????????
            for (i = 0; i < ids.length; i++) {
                leftValues.forEach(function (item, index, arr) {
                    if (item.id == ids[i]) {
                        arr.splice(index, 1);
                    }
                });
            }
        }

        /**
         * ?????????????????????
         */
        function moveToLeft(ids) {
            // ????????????????????????????????????
            for (i = 0; i < ids.length; i++) {
                var item = findRight(ids[i]);
                leftValues.push(item);
            }

            // ????????????????????????????????????
            for (i = 0; i < ids.length; i++) {
                rightValues.forEach(function (item, index, arr) {
                    if (item.id == ids[i]) {
                        arr.splice(index, 1);
                    }
                });
            }
        }

   ");
                WriteLiteral(" </script>\r\n");
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
