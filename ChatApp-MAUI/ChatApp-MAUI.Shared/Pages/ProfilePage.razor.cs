using ChatApp_MAUI.Shared.Common;
using ChatApp_MAUI.Shared.Services.CustomAuthenticationServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp_MAUI.Shared.Pages
{
    public partial class ProfilePageBase : ComponentBase
    {
        [Inject] protected ILoginService _loginService { get; set; } = default!;
        [Inject] protected IJSRuntime _jsRuntime { get; set; } = default!;
        protected IBrowserFile? selectedFile;
        protected override async Task OnInitializedAsync()
        {

        }
        protected async Task UploadFiles(IBrowserFile file)
        {
            selectedFile = file;
            if (selectedFile != null)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    await selectedFile.OpenReadStream(209715200).CopyToAsync(memoryStream);
                }
            }
            //TODO upload the files to the server
        }
        protected async Task GetVerificationEmailLink()
        {
            var response = await _loginService.GetVerificationLink(GlobalClass.Token, GlobalClass.User.Email);
            await _jsRuntime.InvokeVoidAsync("open", response, "_blank");
        }
    }
}
