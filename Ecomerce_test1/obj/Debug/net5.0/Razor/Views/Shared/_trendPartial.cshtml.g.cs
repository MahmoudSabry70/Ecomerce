#pragma checksum "E:\ITI project\c#\MVC\Ecomerce\Ecomerce_test1\Views\Shared\_trendPartial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "96dd2224612bb652f6c905a5ecc13ac117ddd8ef"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__trendPartial), @"mvc.1.0.view", @"/Views/Shared/_trendPartial.cshtml")]
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
#line 1 "E:\ITI project\c#\MVC\Ecomerce\Ecomerce_test1\Views\_ViewImports.cshtml"
using Ecomerce_test1;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\ITI project\c#\MVC\Ecomerce\Ecomerce_test1\Views\_ViewImports.cshtml"
using Ecomerce_test1.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "E:\ITI project\c#\MVC\Ecomerce\Ecomerce_test1\Views\_ViewImports.cshtml"
using Ecomerce_test1.Models.Entities;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "E:\ITI project\c#\MVC\Ecomerce\Ecomerce_test1\Views\_ViewImports.cshtml"
using Ecomerce_test1.Models.viewModel;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "E:\ITI project\c#\MVC\Ecomerce\Ecomerce_test1\Views\_ViewImports.cshtml"
using Ecomerce_test1.Controllers;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "E:\ITI project\c#\MVC\Ecomerce\Ecomerce_test1\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"96dd2224612bb652f6c905a5ecc13ac117ddd8ef", @"/Views/Shared/_trendPartial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fe424175c8ea55a53135c5ac1e7cd9c8d8e02bb8", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__trendPartial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Product>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral("\r\n");
#nullable restore
#line 4 "E:\ITI project\c#\MVC\Ecomerce\Ecomerce_test1\Views\Shared\_trendPartial.cshtml"
 foreach (var item in Model.Take(4))
{


#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"trend__item\" style=\"display:grid;grid-template-columns: repeat(2, 1fr);\r\n    width: 9rem;gap: 0.75rem; \">\r\n                        <div");
            BeginWriteAttribute("class", " class=\"", 223, "\"", 231, 0);
            EndWriteAttribute();
            WriteLiteral(" >\r\n                            <img");
            BeginWriteAttribute("src", " src=\"", 268, "\"", 325, 2);
            WriteAttributeValue("", 274, "/Uploads/", 274, 9, true);
#nullable restore
#line 10 "E:\ITI project\c#\MVC\Ecomerce\Ecomerce_test1\Views\Shared\_trendPartial.cshtml"
WriteAttributeValue("", 283, item.ProductImages.FirstOrDefault().image, 283, 42, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" style=\"max-width: 100%;    max-height: 100%;\"");
            BeginWriteAttribute("alt", " alt=\"", 372, "\"", 378, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                        </div>\r\n                        <div class=\"trend__item__text\" >\r\n                            <h6>");
#nullable restore
#line 13 "E:\ITI project\c#\MVC\Ecomerce\Ecomerce_test1\Views\Shared\_trendPartial.cshtml"
                           Write(item.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h6>
                            <div class=""rating"">
                                <i class=""fa fa-star""></i>
                                <i class=""fa fa-star""></i>
                                <i class=""fa fa-star""></i>
                                <i class=""fa fa-star""></i>
                                <i class=""fa fa-star""></i>
                            </div>
                            <div class=""product__price"">$ ");
#nullable restore
#line 21 "E:\ITI project\c#\MVC\Ecomerce\Ecomerce_test1\Views\Shared\_trendPartial.cshtml"
                                                     Write(item.Price);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n                        </div>\r\n                    </div>\r\n");
#nullable restore
#line 24 "E:\ITI project\c#\MVC\Ecomerce\Ecomerce_test1\Views\Shared\_trendPartial.cshtml"
}

#line default
#line hidden
#nullable disable
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Product>> Html { get; private set; }
    }
}
#pragma warning restore 1591